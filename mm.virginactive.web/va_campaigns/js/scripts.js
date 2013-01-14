$(function () {
    function are_cookies_enabled() {
        var cookieEnabled = (navigator.cookieEnabled) ? true : false;

        if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
            document.cookie = "testcookie";
            cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
        }
        return (cookieEnabled);
    }

    if (!are_cookies_enabled()) {
        $('#cookie-ribbon,#hdnCookieShowing').hide();
        $('body').removeClass('cookie-show');
    }

    var $html = $("html"),
		$article = $("article"),
		$innerPanels = $(".inner"),
		$innerLaunchOverlay = $(".inner:first"),
		$story = $(".inner-story").children(".story"),
		$overlayBg = $("<div id='overlay'></div>"),
		isValid = false,
        hasSub = false;


    var elems = [$(".text-name"), $(".text-email"), $(".text-contact")],
		elemsData = [["name", "^[0-9]+$"], ["email", "^((?!@).)*$"], ["contact number", "^[a-zA-Z]+$"]],
		count = 0, clubName = "Select a club", validClubName = false, clubGUID = '';

    $html.removeClass("nojs");


    //Setup overlay
    $innerLaunchOverlay.find("img, .launchOverlay a").click(function (e) {
        e.preventDefault();
        setupOverlay();
        $innerLaunchOverlay.css({ display: "none" }).next().css({ display: "block" });
    });

    function setupOverlay() {
        $("body").append($overlayBg.click(function () { closeOverlay(); }));
        $overlayBg.css({ opacity: 0.8 }).fadeIn(150);
        $(document).keyup(handleEscape);
    }

    function closeOverlay() {
        $(document).unbind("keyup", handleEscape);
        var remove = function () {
            $(this).remove();
        }
        $overlayBg.fadeOut(remove);
        $innerPanels.css({ display: "none" });
        $innerLaunchOverlay.css({ display: "block" });
    }
    function handleEscape(e) {
        if (e.which == 27) {
            closeOverlay();
        }
    }

    //Handle back/cancel/close buttons
    $innerPanels.find(".btn-prev, #close").click(function (e) {
        e.preventDefault();
        $this = $(this);

        if ($this.hasClass("closeOverlay")) {
            closeOverlay();
        } else {
            $innerPanels.css({ display: "none" });
            $this.closest(".inner").prev().css({ display: "block" });
        }
    });

    //Reset page after form postback completed
    if ($("article").hasClass("ImageOverlay")) {
        setupOverlay();
        //storyValidation();
    }


    //Handle next/submit buttons
    $innerPanels.find(".btn-next, .btn-submit").click(function (e) {
        e.preventDefault();
        $this = $(this);
        $this.children("a").blur();

        if ($this.closest(".inner").hasClass("inner-story")) {
            storyValidation();
        } else if ($this.closest(".inner").hasClass("inner-form")) {
            detailsValidation();
        }

        //Move to next panel if not submitting form
        if ($this.hasClass("btn-next")) {
            if (isValid === true) {
                $innerPanels.css({ display: "none" });
                $(this).closest(".inner").next().css({ display: "block" });
            }
        } else {
            if (isValid) {
                //Stop multiple clicks on submit buttons
                if (!hasSub) {
                    hasSub = true;
                    $this.addClass("btn-disabled");
                    __doPostBack('content_0$btnSubmit', '');

                    //Add OpenTag tracking for Mens Fitness campaign
                    var formName = $this.attr("id").substr(4);

                    if ("weblead" === formName) {
                        if (typeof (_gaq) !== undefined) {
                            //this is a web lead -load open tag for web lead confirmation e.g. mensfitness
                            var ot = document.createElement('script');
                            ot.async = true;
                            ot.src = ('//d3c3cq33003psk.cloudfront.net/opentag-32825-68231.js');
                            var s = document.getElementsByTagName('script')[0];
                            s.parentNode.insertBefore(ot, s);

                            //record trackevent
                            var gaqCategory = "Form",
							    gaqAction = "Submit",
							    gaqLabel = $this.data("gaqlabel");

                            _gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);
                        }
                    }
                    else {
                        //record trackevent
                        if (typeof (_gaq) !== undefined) {
                            var gaqCategory = "Form";
                            var gaqAction = "Submit";
                            var gaqLabel = $this.data("gaqlabel");

                            _gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);
                        }

                    }
                }
            }
        }

        //If details section is visible, do inline validation checks
        if ($(".inner-form").is(":visible")) {
            inlineValidation();
        }
    });


    //Test for placeholder support
    $.support.placeholder = false;
    var testInput = document.createElement("input");
    if ("placeholder" in testInput) {
        $.support.placeholder = true;
    }

    //Add form porn
    $(".chzn-clublist").chosen();
    $('.chzn-selectbox').chosen();

    //Membership campaign: resize price if has a decimal point
    $price = $(".membs-price");
    if ($price.length > 0) {
        if ($price.width() > 416) {
            $price.addClass("membs-price-small");
        }
    }

    //Add gaq tag info
    $(".gaqTag").click(function () {
        if (typeof (_gaq) !== undefined) {
            var $link = $(this),
			    gaqCategory = $link.data("gaqcategory"),
			    gaqAction = $link.data("gaqaction"),
			    gaqLabel = $link.data("gaqlabel");

            _gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);
        }
    });


    /* Common validation 
    **********************************/

    //Grab chosen clubname
    if ($(".chzn-container").length > 0) {
        if ($(".chzn-container span").text() != "Select a club") {
            validClubName = true;
            clubName = $(".chzn-clublist option:selected").text();
            $("input.clubGUID").val(clubName.val());
            $(".chzn-clublist").addClass("valValid").next().removeClass("chzn-error");
        }
        $(".chzn-clublist").change(function () {
            if ("" != this.value) {
                validClubName = true;
                clubName = $(".chzn-clublist option:selected").text();
                $("input.clubGUID").val(this.value);
                $(".chzn-clublist").addClass("valValid").next().removeClass("chzn-error");
            } else {
                validClubName = false;
                $(".chzn-clublist").removeClass("valValid").next().addClass("chzn-error");
            }
        });
    }

    function detailsValidation() {
        isValid = false;
        $(".btn-next:visible a").blur();

        $.each(elems, function (i, el) {
            validChecks(i, el)
        });
    }

    function inlineValidation() {
        isValid = false;

        $.each(elems, function (i, el) {
            el.focusout(function () {
                validChecks(i, el)
            });
        });
    }

    function validChecks(i, el) {
        if ("Select a club" == clubName) {
            $(".chzn-container").addClass("chzn-error");
        }
        var validationMessage = "Please enter your ";

        if ($innerPanels.hasClass('use-their')) {
            validationMessage = "Please enter their ";
        }

        if ((el.val() == "") || (el.val() == validationMessage + elemsData[i][0])) {
            if ($.support.placeholder) {
                el.addClass("error").removeClass("valValid").val("").attr("placeholder", validationMessage + elemsData[i][0]);
            } else {
                el.addClass("error").removeClass("valValid").val(validationMessage + elemsData[i][0]).focus(function () {
                    el.val("");
                });
            }
        } else if (el.val().match(elemsData[i][1])) {
            el.addClass("error").removeClass("valValid").val("").attr("placeholder", "Please enter a valid " + elemsData[i][0]);
        } else {
            el.removeClass("error").addClass("valValid");
        }

        //Check number of required fields and test they're all valid
        var countValid = 0,
			$required = $(".valRequired");

        $required.each(function () {
            if ($(this).hasClass("valValid")) {
                countValid++;
            }
        });

        if (countValid === $required.length) {
            isValid = true;
            $(".box-club").text(clubName);
            $(".box-name").text($(".text-name").val());
            $(".box-email").text($(".text-email").val());
            $(".box-contact").text($(".text-contact").val());
        }
    }



    /* Membership campaign
    **********************************/
    if ($article.hasClass("membs")) {
        //Making results list equal height
        $(".club-results-list").livequery(function () {
            $(".club-summary-wrap").each(function () {
                var $this = $(this),
					$panelLeft = $this.children(".club-summary"),
					$panelRight = $this.children(".info-panel");

                if ($panelLeft.outerHeight() > $panelRight.outerHeight()) {
                    $panelRight.height($panelLeft.outerHeight() - ($panelRight.outerHeight() - $panelRight.height()));
                } else {
                    $panelLeft.height($panelRight.outerHeight() - ($panelLeft.outerHeight() - $panelLeft.height()));
                }
            });
        });
    }


    /* Feedback campaign
    **********************************/
    if ($article.hasClass("feedback")) {
        var $carousel = $("#carousel"),
			$slides = $carousel.children("li"),
			activeSlide = 0,
			slideWidth = $slides.children("img").width(),
			slideSpeed = 7500,
			slideText = 500;

        //Add carousel functionality
        $slides.css({ left: "-" + slideWidth + "px" }).filter(":first").css({ left: 0 }).addClass("active-slide");

        //Add caption and quote
        $slides.each(function () {
            var $this = $(this);
            $('<p class="caption">' + $this.children("img").data("caption") + '</p><p class="quote">' + $this.children("img").data("quote") + '</p>').appendTo($this);
            $slides.eq(activeSlide).children("p").fadeIn(slideText)
        });

        if ($slides.length > 0) {
            setInterval(function (e) { shiftSlide(); }, slideSpeed);
        }

        function shiftSlide() {
            var prevActiveSlide = activeSlide;
            activeSlide++
            if (activeSlide == $slides.length) {
                activeSlide = 0;
            }
            $slides.eq(activeSlide).addClass("active-slide").css({ "left": slideWidth + "px" }).stop(true, true).animate({ "left": 0 }, slideText, function () {
                $slides.eq(activeSlide).children("p").fadeIn(slideSpeed / 5);
            });
            $slides.eq(prevActiveSlide).removeClass("active-slide").children("p").fadeOut(300).end().stop(true, true).animate({ "left": -slideWidth + "px" }, slideText);
        }


        //If file upload input exists
        if ($(".file-upload").length > 0) {
            $fileUpload = $(".file-upload");

            //Style file upload input
            if (!$html.is(".ie6, .ie7, .ie8")) {
                $fileUpload.wrap('<div class="file-upload-wrap" />');
                $(".file-upload-wrap").append('<div class="styled-upload"><input placeholder="gif/jpg, max 5Mb" data-placeholder="gif/jpg, max 5Mb" data-extplaceholder="gif/jpg format only" data-sizeplaceholder="File size limit 5Mb" /><a href="#" class="styled-btn">Browse</a></div>');
                $styledUpload = $(".styled-upload input");

                $styledUpload.keypress(function () {
                    return false;
                });
            }

            if (!$.support.placeholder) {
                if ($html.is(".ie6, .ie7, .ie8")) {
                    $fileUpload.val($fileUpload.data("placeholder"));
                } else {
                    $styledUpload.val($styledUpload.data("placeholder"));
                }
            }

            $fileUpload.change(function () {
                var imgName = encodeURI($(this).val());
                imgName = imgName.replace("C:%5Cfakepath%5C", "");

                //Add validation for file extension
                var ext = imgName.substring(imgName.indexOf(".") + 1)
                if (ext == "jpg" || ext == "jpeg" || ext == "gif") {
                    if ($.support.placeholder) {
                        $styledUpload.attr("placeholder", "").val(imgName).parent().removeClass("upload-error");
                    } else {
                        if ($html.is(".ie6, .ie7, .ie8")) {
                            $fileUpload.val(imgName).parent().removeClass("upload-error");
                        } else {
                            $styledUpload.val(imgName).parent().removeClass("upload-error");
                        }
                    }
                } else {
                    if ($.support.placeholder) {
                        $styledUpload.val("").attr("placeholder", "gif/jpg format only").parent().addClass("upload-error");
                    } else {
                        if ($html.is(".ie6, .ie7, .ie8")) {
                            $fileUpload.val($fileUpload.data("extplaceholder")).parent().addClass("upload-error");
                        } else {
                            $styledUpload.val($styledUpload.data("extplaceholder")).parent().addClass("upload-error");
                        }
                    }
                }

                //Add validation for file size if browser supports File API
                if (window.File) {
                    var file = document.forms["form1"]["content_0_filename"].files[0];
                    if (file.size >= 500000) {
                        var errorMsg = $(".file-upload-error").text();
                        $(".styled-upload").addClass("upload-error").children("input").attr("placeholder", "File size limit 5Mb").val("");
                    }
                }
            });
        }

        //Add file upload placeholder error message
        if ($(".file-upload-error").text() != "") {
            var errorMsg = $(".file-upload-error").text();
            if ($.support.placeholder) {
                $(".styled-upload").addClass("upload-error").children("input").attr("placeholder", errorMsg).val("");
            } else {
                $(".file-upload").addClass("upload-error-noplaceholder").val("placeholder");
            }
        }

        //Remove error styling once text is entered into textarea
        $story.keypress(function () {
            $story.removeClass("error");
        });

        //Validation for textarea
        function storyValidation() {
            if ($.trim($(".story").val()) == "") {
                $story.attr("placeholder", "Please enter your story").addClass("error");
            } else {
                isValid = true;
                var tmp = [],
					myStory = "";

                tmp = $story.val().split(/\n\n/);
                for (var i = 0; i < tmp.length; i++) {
                    myStory += "<p>" + tmp[i] + "</p>";
                }

                $(".box-story").html(myStory) //.jScrollPane();
            }
        }
    }
});