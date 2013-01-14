$(function () {
	function are_cookies_enabled()
	{
		var cookieEnabled = (navigator.cookieEnabled) ? true : false;
	
		if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled)
		{ 
			document.cookie="testcookie";
			cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
		}
		return (cookieEnabled);
	}
	
	if(!are_cookies_enabled()){
		$('#cookie-ribbon,#hdnCookieShowing').hide();
		$('body').removeClass('cookie-show');
	}
	
    var $html = $("html"),
		$form = $html.find(".panel-wrap"),
		$row = $form.find(".row"),
		$enter = $html.find("#enter"),
		isiPad, isTouchNotiPad,
		isMember = false,
		isTeam = false,
		hasSubmitted = false,
		count = 0,
		elems, elemsName,
		errorFields = [];

    $html.removeClass("nojs");

    if (navigator.userAgent.match(/iPad/i)) {
        isiPad = true;
        $html.addClass("iPad");
    } else if (($html.hasClass("touch")) && (!navigator.userAgent.match(/iPad/i))) {
        isTouchNotiPad = true;
    }

    //Update class on body tag to move bg image on thanks page
    if ($html.find("article").parent().hasClass("wrapper-thanks")) {
        $("div.outer-wrap").addClass("wrap-thanks");
    }

    /* Membership Info Tooltip                              ------------------------------------------- */
    $(".more-info").hover(function (e) {
        e.preventDefault();
        $(".tooltip").fadeToggle();
    }).click(function (e) {
        e.preventDefault();
    });

    //Add movement to arrow on ballow box
    $enter.wrapInner("<a href='#' />");
    $("#envelope").hover(function () {
        $(this).addClass("hover").children("#arrow").stop(true, true).animate({ top: "10px" });
    }, function () {
        $(this).removeClass("hover").children("#arrow").stop(true, true).animate({ top: "0px" });
    });

    $("#envelope").click(function () {
        $("html,body").animate({ scrollTop: "538px" }, 2000, "easeOutQuint");
    });

    //Switch "Refer a friend" with image replacement if <ie9
    if ($html.is(".ie6, .ie7, .ie8")) {
        $("#enter a").addClass("img-rep").prepend("<span class='rep' />")
    }


    //Add placeholder support
    $.support.placeholder = false;
    var testInput = document.createElement("input");
    if ("placeholder" in testInput) {
        $.support.placeholder = true;
    }
    if (!$.support.placeholder) {
        $(".text-dob").val("dd/mm/yyyy");
    }
    $(".text-dob").focus(function () {
        if (!$.support.placeholder) {
            if ($(this).val() == "dd/mm/yyyy")
                $(this).val("");
        }
    });
    $(".text-dob").blur(function () {
        if (!$.support.placeholder) {
            if ($(this).val() == "")
                $(this).val("dd/mm/yyyy");
        }
    });

    //Style dropdowns 
    if (!$html.is(".ie6") && isTouchNotiPad) {
        $("select").styleSelects().closest(".row").addClass("row-styleSelects");
    } else if (!$html.is(".ie6")) {
        $("select:not(.chzn-clublist)").styleSelects().closest(".row").addClass("row-styleSelects");
        $(".chzn-clublist").chosen().parent().addClass("row-chzn");
    } else {
        $("select").parent().addClass("row-select");
    }

    //Add sizing info for selectStyle dropdowns
    $(".text-topsize").css({ width: "130px" });
    $(".text-event").css({ width: "268px" });

    if ($form.find("select.text-half").next().hasClass("styleSelectWrap")) {
        $form.find("select.text-half").next(".styleSelectWrap").addClass("styleSelectWrapHalf")
    }

    //Add optgroup to event dropdown
    var optgroups = [], optgroups;
    $form.find(".text-event").children("option").each(function () {
        if ($(this).attr("data-optgroup")) {
            optgroups.push($(this).data("optgroup"));
        }
    });
    len = optgroups.length;
    for (var i = 0; i < len; i++) {
        $form.find(".text-event").children("option[data-optgroup='" + optgroups[i] + "']").wrapAll("<optgroup label='" + $form.find(".text-event").data(optgroups[i]) + "' />");
    }


    //Add/clear error messages
    function clearErrorMsgs() {
        $row.each(function () {
            $(this).removeClass("row-error").children(".error").remove();
        });
    }

    function addErrorMsg() {
        $row.each(function () {
            var $this = $(this),
				msg = ($this.hasClass("row-invalid")) ? $this.data("validmsg") : msg = $this.data("errormsg");

            //On name .row, update error message
            if ($this.hasClass("row-extra")) {
                msg = msg.replace(/name$/, $this.data("errMsg"));
            }

            //Determine error message and add to page
            if ($this.children("p").hasClass("error")) {
                $this.find("p span").text(msg);
            } else if ($this.hasClass("row-error") && $this.hasClass("row-checkbox")) {
                $this.append('<p class="error error-checkbox"><span>' + msg + '</span></p>');
            } else if ($this.hasClass("row-error")) {
                $this.append('<p class="error"><span>' + msg + '</span></p>');
            }
        });
    }

    //Update validation criteria depending on whether user is a member/in a team
    function updateValidationData() {
        elems = [[$form.find(".text-member"), "^[0-9 ]{4,20}$"],
                    [$form.find(".text-firstname"), "^[a-zA-Z-' ]+$"],
					[$form.find(".text-surname"), "^[a-zA-Z-' ]+$"],
					[$form.find(".text-email"), "^[a-zA-Z0-9_-]+(?:\.[a-zA-Z0-9_-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$"],
					[$form.find(".text-number"), "^[0-9 ()]+$"],
                    [$form.find(".text-firstname2"), "^[a-zA-Z-' ]+$"],
					[$form.find(".text-surname2"), "^[a-zA-Z-' ]+$"],
					[$form.find(".text-email2"), "^[a-zA-Z0-9_-]+(?:\.[a-zA-Z0-9_-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$"],
					[$form.find(".text-number2"), "^[0-9 ()]+$"]];

//        if (isMember) {
//            elems.unshift([$form.find(".text-member"), "^[0-9 ]{4,20}$"]);
//        }
//        if (isTeam) {
//            elems.push([$form.find(".text-teamname"), "^[a-zA-Z0-9 ]+$"]);
//        }

        //Loop thru validation on blur
        $.each(elems, function (i, el) {
            el[0].focusout(function () {
                validChecks(i, el);
            });
        });
    }

    //Show/hide form rows
//    function showAdditionalField(val, $el) {
//        (val == "isMember") ? isMember = true : isTeam = true;
//        $el.slideDown().addClass("row-join-top").prev().addClass("row-join-bottom");
//    }
//    function hideAdditionalField(val, $el) {
//        (val == "isMember") ? isMember = false : isTeam = false;
//        $el.removeClass("row-error row-join-top").prev().removeClass("row-join-bottom").end().children(".error").remove().end().slideUp();
//    }

    function showHideRow($el, eventHandler, validationCheck) {
        var $formEl = (eventHandler == "change") ? ($el.prev().find("select")) : ($el.prev().children("input"));

        //Hide appropriate form rows on page refresh
        if (eventHandler == "click") {
            $formEl.removeAttr("checked").end().next().hide();
        } else {
            $el.hide();
        }

        $formEl.bind(eventHandler, function () {
            var isErrorTest = (eventHandler == "change") ? ($formEl.find("option:selected").data("optgroup") == "team") : ($formEl.prop("checked"))

            if (isErrorTest) {
                showAdditionalField(validationCheck, $el);
            } else {
                hideAdditionalField(validationCheck, $el);
            }
            updateValidationData();
        });
    }

//    showHideRow($form.find("#membership"), "click", "isMember");
//    showHideRow($form.find("#teamname"), "change", "isTeam");


    //Inline validation for text fields
    updateValidationData();


    function validChecks(i, el, validateBoth) {
        //Determine whether field is empty, invalid or valid
        if (el[0].val() == "") {
            el[0].closest(".row").addClass("row-error").removeClass("row-invalid");
        } else if (!el[0].val().match(el[1])) {
            el[0].closest(".row").addClass("row-error row-invalid");
        } else {
            el[0].closest(".row").removeClass("row-error").children(".error").remove();
        }

        //For name fields
        if (el[0].hasClass("text-multi")) {
            //Add/remove name to array
            if ((el[0].val() == "") || (!el[0].val().match(el[1]))) {
                errorFields.push(el[0].attr("placeholder").toLowerCase());
                errorFields = $.unique(errorFields.sort());
                el[0].removeClass("has-content");
            } else {
                for (var i = 0; i < errorFields.length; i++) {
                    if (errorFields[i] == el[0].attr("placeholder").toLowerCase())
                        errorFields.splice(i, 1);
                }
                el[0].addClass("has-content");
            }

            //Concatenate error fields and attach data to .row
            var errorFieldsStr = (errorFields.length > 1) ? errorFields.join(" and ") : errorFields.join("");
            if (errorFieldsStr != "") {
                el[0].parent(".row").addClass("row-error row-extra").data("errMsg", errorFieldsStr + " name");
            }
        }

        addErrorMsg();
    }


    //Inline validation for select/checkboxes/radios
    validateElems($form.find(".row-chzn select, .row-styleSelects select, .row-select select"), "change");
    validateElems($form.find(".row-toValidate input"), "click");

    function validateElems($el, eventHandler) {
        $el.bind(eventHandler, function () {
            var $this = $(this),
				isErrorTest = (eventHandler == "change") ? $this.val() == "default" : !$this.is(":checked"),
				clubGUID = $form.find(".clublist option:selected").val();
                clubGUID2 = $form.find(".clublist2 option:selected").val();

            $form.find(".clubGUID").val(clubGUID);
            $form.find(".clubGUID2").val(clubGUID2);

            if (isErrorTest) {
                $this.closest(".row").addClass("row-error");
            } else {
                $this.closest(".row").removeClass("row-error").children(".error").remove();
            }
            addErrorMsg();
        });
    }

    //Ensure that error messages are cleared and form fields are blurred before clicking to submit
    $form.find(".btn-submit").mouseover(function () {
        $form.find(".row-error input").blur();
    });

    $form.find(".btn-submit").click(function (e) {
        e.preventDefault();
        this.blur();
        count = 0;
        clearErrorMsgs();

        //Validate radio buttons/checkboxes 
        $form.find(".row-toValidate").each(function () {
            if (!$(this).children("input").is(":checked")) {
                $(this).addClass("row-error");
            }
        });

        //Validate text fields
        $.each(elems, function (i, el) {
            validChecks(i, el);
        });

        //Validate styled selects and form porn dropdowns
        $form.find(".row-select, .row-styleSelects, .row-chzn").each(function () {
            var $this = $(this),
				testCondition;

            if ($this.hasClass("row-select")) {
                testCondition = $this.find("option:selected").val() == "default";
            } else {
                testCondition = ($this.hasClass("row-styleSelects")) ? ($this.find(".styleSelect").attr("value") == "default") : ($this.find(".chzn-single span").text() == "Select a club");
            }
            (testCondition) ? $this.addClass("row-error") : $this.removeClass("row-error");
        });

        addErrorMsg();

        $form.find(".row-error").each(function () {
            count++;
        });

        if (count > 0) {
            $(".error-text").hide();
            $("<p class='error-text'>Oops, you've missed something, please check the required fields and submit your form again.</p>").prependTo(".row-last");
        }
        else {
            $(".error-text").hide();
            //Submit form only once
            if (!hasSubmitted) {
                hasSubmitted = true;
                $form.find(".btn-submit").addClass("btn-disabled");

                if (typeof (_gaq) !== undefined) {
                    //this is a web lead -load open tag for web lead confirmation 
                    var ot = document.createElement('script');
                    ot.async = true;
                    ot.src = ('//d3c3cq33003psk.cloudfront.net/opentag-32825-68231.js');
                    var s = document.getElementsByTagName('script')[0];
                    s.parentNode.insertBefore(ot, s);
                
                    _gaq.push(["_trackEvent", "Form", "Submit", "Campaign-Refer-A-Friend"]);
                }
                __doPostBack('content_0$btnSubmit', '');
            }
        }
    });

});