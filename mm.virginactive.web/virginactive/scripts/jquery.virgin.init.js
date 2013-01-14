/*global _gaq:false, Sys:false, google:false, directionsDisplay:false, _filters: false, FilterTriggered:false,showAllClubs: false, InfoBox: false, _SelectedClubLat: false, _SelectedClubLng:false, _SelectedClub:false, vaOptions: false */

/***
Virgin Active
2011 McCormackMorrison
***/

(function ($) {
    /* Scope - This initialises the namespace if it has not been initialised elsewhere */
    $.virginActive = $.virginActive || {};

    /* Global fields - Any global objects that you need access to are defined here */
    var g_ready = false,
		g_self = this;

    /* API methods - If the class has any API methods then put them here (methods that can be called directly on objects e.g. $("body").customMethod(); */
    $.fn.preferences = function () {
        return this.each(function () {
            //alert("API method called on " + this);
        });
    };

    /* Class */
    $.virginActive.init = function (options) {
        var c_self = this,
			c_ready = false,
			$html = $("html"),
			isiPad = false,
			isTouchNotiPad = false;
        c_self.options = $.extend({}, $.virginActive.init.defaults, options);
        
        /* Instance methods */
        c_self.functions = {

            //General page set up
            vaOnReady: function () {

                var params = {
                    ajax: 1,
                    cmd: 'ClubNamesList'
                }, $el = null;


                $("html").removeClass("nojs");

                //Enable club filter list overlays -- eg member login button, kids club page
                $(".overlay-clubfilterlist").vaOverlay({ replaceDivContent: false, updateClubFilterList: true });

                if (navigator.userAgent.match(/iPad/i)) {
                    isiPad = true;
                    $html.addClass("iPad");
                } else if (($html.hasClass("touch")) && (!navigator.userAgent.match(/iPad/i))) {
                    isTouchNotiPad = true;
                }

                //Style selects
                if ($html.is(".ie6") || isTouchNotiPad) {
                    $("select").styleSelects();
                } else {
                    //Style longer lists with chzn plugin
                    $("select:not(.chzn-clublist)").chosen();
                    
                    $(".chzn-clublist").chosen({"hasSearch":true});
                    
                    if ($html.hasClass("ie")) {
                        $(".chzn-clublist").addClass("chzn-ie");
                    }
                }

                //Activate promo panel
                $("#reveal").hide();
                $("p.show").click(c_self.functions.showPromoPanel);

                //Add gaq tag info
                $(".gaqTag").click(function () {
                    if (typeof (_gaq) !== "undefined") {
                        var $link = $(this),
							gaqCategory = $link.data("gaqcategory"),
							gaqAction = $link.data("gaqaction"),
							gaqLabel = $link.data("gaqlabel");

                            _gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);
                    }
                });

                //Tweaks to sitemap - add a child link if heading doesn't have one
                $(".sitemap h3").each(function () {
                    $el = $(this);
                    if (!$el.next().is("h4") && !$el.next().is("ul")) {
                        $el.after("<ul><li>" + $el.html() + "</li></ul>");
                    }
                });


                //Add functionality for skip links
                $("#skiplinks a").click(function (e) {
                    e.preventDefault();
                    var $this = $(this);

                    if ($this.attr("id") === "skipto-search") {
                        $("#search").focus();
                    } else if ($this.attr("id") === "skipto-mainnav") {
                        $("#nav-faci").addClass("dropdown-hover nav-faci-hover").children("a").attr("tabIndex", "0").focus();
                        $this.blur();
                    } else {
                        $("#content").find("a:eq(0)").attr("tabIndex", "0").focus();
                        $("html,body").animate({ scrollTop: ($("#content").offset().top - 28) }, 2000, "easeOutQuint");
                    }
                });

                //Make columns same height (used in main nav/facilities & classes)
                $("#nav-your .dropdown, #nav-memb .dropdown").sameHeightCols();
                //$("#nav-faci .dropdown").sameHeightColsFirstDropdown();
                //$(".layout-inner-panels").sameHeightCols({groupByContainerClass:true });

                //Add hover intent for main drop down
                function megaHoverOver() {
                    var $this = $(this);
                    $this.addClass($this.attr("id") + "-hover dropdown-hover");
                }
                function megaHoverOut() {
                    var $this = $(this);
                    $this.removeClass($this.attr("id") + "-hover dropdown-hover");
                }
                $("#main-nav li.level1").hoverIntent({ sensitivity: 1, timeout: 250, interval: 100, over: megaHoverOver, out: megaHoverOut });


                //Add current nav arrow for 3 main nav elements
                $("#main-nav .active .dropdown").after('<div class="nav-indicator"></div>');


                //Add footer country dropdown functionality, works on hover or onclick (for keyboard access)
                $("#change-country a").mouseenter(function () {
                    showCountryLinks();
                });
                $("#footer-sub").mouseleave(function () {
                    hideCountryLinks();
                });
                $("#change-country > a").toggle(function (e) {
                    e.preventDefault();
                    showCountryLinks();
                }, function () {
                    hideCountryLinks();
                });
                function showCountryLinks($countryLink) {
                    $("#change-country #country-wrap").css({ display: "block" }).children("ul").stop(true, true).slideDown();
                }
                function hideCountryLinks() {
                    $("#change-country #country-wrap ul").stop(true, true).slideUp(function () {
                        $("#change-country #country-wrap").css({ display: "none" });
                    });
                }

                /* Dynamic back to top */
                if ($html.hasClass("no-touch")) {
                    $().UItoTop({ easingType: 'easeOutQuart' });

                    if (document.body.offsetWidth > 1140) {
                        $('#toTop').removeClass('hidden');
                    }

                    //jQuery used incase a resize event already exists
                    $(window).resize(function () {
                        if (document.body.offsetWidth > 1140) {
                            $('#toTop').removeClass('hidden');
                        }
                        else if ($('#toTop').hasClass('hidden') === false) {
                            $('#toTop').addClass('hidden');
                        }
                    });
                }

                /* adds placeholder text for non-supporting browsers */
                $('input[placeholder], textarea[placeholder]').placeholder();

                //Restyle main search results list, to remove dodgy anchor tags from older browsers (eg ff3.6)
                $(".results-list").livequery(function () {
                    var $resultsList = $(this);

                    if ($resultsList.height() > 100) {
                        var h2Val = $resultsList.children("h2").text();
                        var h3SpanVal = $resultsList.children("h3 span").text();
                        var h3RemainderVal = $resultsList.children("h3").text();
                        var h3PVal = $resultsList.children("p").text();

                        $resultsList.empty().append("<h2>" + h2Val + "<h2><h3>" + h3RemainderVal + "</h3><p>" + h3PVal + "</p>");
                    }

                    if ($resultsList.parent().hasClass("searchTerm")) {
                        $resultsList.parent().parent("a").addClass("searchTermWrap");

                        var seeallclubs = $resultsList.parent().data("seeallclubs");
                        $resultsList.find("p").wrapInner("<span class='loads' />").append("<span class='seeall'>" + seeallclubs + "</span>");
                    }
                });

                //Styling the googleadservices img to remove whitespace at foot of page
                $('img[src*="googleadservices"]').livequery(function () {
                    $(this).addClass("googleadservices").css({ float: "left" });
                });
            },

            showPromoPanel: function () {
                var $this = $(this);
                $this.parent("div").toggleClass("showmore-active");
                $("#reveal").toggleClass("reveal").slideToggle(1000, "easeOutQuint");
                $this.children("a").text($this.children("a").text() == "Show more" ? "Hide details" : "Show more");
            },


            sitemap: function () {

            },


            workouts: function () {
                $("div.workout-article").not(":first").hide();
                $("#worksouts-sub a").click(function (e) {
                    e.preventDefault();
                    var $this = $(this);
                    var clicked = $("#worksouts-sub a").index(this);
                    $("div.workout-article").hide();
                    $("div.workout-article:eq(" + clicked + ")").show();
                    $("#worksouts-sub li").removeClass("active");
                    $this.parent().addClass("active");
                });
            },

            addDatepicker: function () {
                var today = new Date();
                $(".datepicker").datepicker({
                    showOn: "button",
                    buttonImage: "/virginactive/images/icons/calendar.png",
                    buttonImageOnly: true,
                    minDate: new Date(today.getFullYear(), today.getMonth(), today.getDate())
                });
            },

            addTreatmentToDropDown: function () {
                $(".treatment a").click(function (e) {
                    //e.preventDefault();
                    var $this = $(this);
                    var treatmentName = $this.data("treatment");
                    $(".treatmentSelect > option").each(function () {
                        if (treatmentName == this.value) {
                            this.selected = true;
                        }
                    });
                    var $selectedItem = $(".treatmentSelect").siblings("span");
                    if ($selectedItem !== null) {
                        $selectedItem.children(".select-text").text(treatmentName);
                    }
                });
            },


            membershipReciprocal: function () {
                $(".select-panel, .club-access-info").hide();

                var $reciprocalAccessPanel = $(".club-access-info");
                $(".select-panel .btn-cta").click(function (e) {
                    e.preventDefault();

                    //Tracking done after form submitted successfully
                    if (typeof (_gaq) !== "undefined") {
                        _gaq.push(["_trackEvent", "Form", "Submit", "Multi-Club Access"]);
                    }
                    $reciprocalAccessPanel.slideDown();

                    $(".clubs-locations").sameHeightCols();
                });

                $(".close-btn a").click(function (e) {
                    e.preventDefault();
                    $reciprocalAccessPanel.slideUp();
                });
            },


            cmfDials: function () {
                var $cmf = $(".cmf"), degrees = [], html;

                if (!$html.hasClass("no-csstransforms") && !$html.hasClass("ie9")) {
                    $cmf.each(function (i) {
                        var $thisCmf = $(this);

                        $thisCmf.children("td:not(.details), li").each(function (ii) {
                            var $thisInner = $(this);
                            degrees.push(360 * ($(this).find(".pc").text() / 100));

                            if (degrees[ii] > 180) {
                                html = '<span class="cmf-dial"><span class="cmf-inner cmf-move"><span class="cmf-rotator cmf-move" style="-ms-transform:rotate(' + degrees[ii] + 'deg); -moz-transform:rotate(' + (degrees[ii] - 180) + 'deg); -webkit-transform:rotate(' + (degrees[ii] - 180) + 'deg); -o-transform:rotate(' + (degrees[ii] - 180) + 'deg); "></span></span></span>';
                            } else {
                                html = '<span class="cmf-dial"><span class="cmf-inner"><span class="cmf-rotator" style="-ms-transform:rotate(' + degrees[ii] + 'deg); -moz-transform:rotate(' + degrees[ii] + 'deg); -webkit-transform:rotate(' + degrees[ii] + 'deg); -o-transform:rotate(' + degrees[ii] + 'deg); "></span></span></span>';
                            }
                            $thisInner.children(".cmf-wrap").prepend(html);

                            $thisCmf.find(".cmf-dial:eq(1)").css({ "msTransform": "rotate(" + degrees[0] + "deg)", "-moz-transform": "rotate(" + degrees[0] + "deg)", "-webkit-transform": "rotate(" + degrees[0] + "deg)", "-o-transform": "rotate(" + degrees[0] + "deg)" });
                            $thisCmf.find(".cmf-dial:eq(2)").css({ "msTransform": "rotate(" + (degrees[0] + degrees[1]) + "deg)", "-moz-transform": "rotate(" + (degrees[0] + degrees[1]) + "deg)", "-webkit-transform": "rotate(" + (degrees[0] + degrees[1]) + "deg)", "-o-transform": "rotate(" + (degrees[0] + degrees[1]) + "deg)" });
                        });
                        degrees.length = 0;
                    });
                }
            },


            setupForms: function () {
                
                if ($html.hasClass("ie")) {
                    //Forcing ie to update error status
                    $('#content_0_centre_0_ctl00_drpHowDidYouFindUs').change(function(){
                        var $this = $(this);

                         
                            if ($this.find("option:selected").text() == "Select") {
                               $(this).next().children(".chzn-single").addClass("error").closest(".wrap").addClass("wrapErrorFullWidth").children("span.span1").css({ display: "inline" }).parents('.wrap').removeClass('valid');
                            } else {
                                $(this).removeClass("error").next().children(".chzn-single").removeClass("error").closest(".wrap").removeClass("wrapErrorFullWidth").children("span").css({ display: "none" }).parents('.wrap').addClass('valid');
                            }
                        
                    })

                }

               

                function CancelPostbackForSubsequentSubmitClicks(sender, args) {
                    //var formName = $btnSubmit.parent().attr("id").substring(4),
                    var formName = GlobalFormName;

                    if (args.get_postBackElement().id == $(".btn-submit").attr("id")) {
                        $(".btn-submit").addClass("btn-disabled-" + GlobalBtnSize);
                        //Tracking done after form submitted successfully
                        if (typeof (_gaq) != "undefined") {
                            _gaq.push(["_trackEvent", "Form", "Submit", formName]);
                        }

                        //Adding OpenTag
                        if ("ClubEnquiry" == formName || formName.indexOf("Offer-") != -1) {
                            var ot = document.createElement('script');
                            ot.async = true;
                            ot.src = ('//d3c3cq33003psk.cloudfront.net/opentag-32825-68231.js');
                            var s = document.getElementsByTagName('script')[0];
                            s.parentNode.insertBefore(ot, s);
                        }

                        if (requestManager.get_isInAsyncPostBack()) {
                            args.set_cancel(true);
                        }
                    }
                }
                
                function updateErrorMessaging($el) {
                
                    //If only select has value of "Select a club", add error message
                    if (($(".select-row").length == 1) && ($(".select-row .chzn-single span").text() == "Select a club")) {
                        $(".select-row").addClass("select-row-error").find(".chzn-single span").addClass("placeholder").end().siblings(".span1").css({ display: "block" });
                    }

                    //Ensure .NET validation doesn't cause problems
                    $(".select-row-new select").attr("id", aspValidationID);

                    //Determine if current value is valid and if there is only one select, show the "hide" icon and remove error messaging
                    isClubSelected = (multiClubNames.length > 0) ? true : false;

                    //If last dropdown doesn't have a value or if only dropdown doesn't have a value, remove "add" icon
                    if ((isClubSelected) && ($(".select-row-new").find(".chzn-single span").text() == "Select a club") || (!isClubSelected) || ($html.hasClass("ie6"))) {
                        $(".select-row-new").find(".icon-add").animate({ opacity: 0 }, function () {
                            $(this).addClass("icon-add-hide");
                        });
                    } else if ((isClubSelected) && (!$el.closest(".select-row").next().hasClass("select-row"))) {
                        $el.closest(".select-row")
							.find(".chzn-single span").removeClass("placeholder").end()
							.find(".icon-add").removeClass("icon-add-hide").animate({ opacity: 1 }).end().removeClass("select-row-error").siblings(".span1").css({ display: "none" });
                    }
                }


                function updateHiddenInputs() {
                    //Reset arrays
                    multiClubNames.length = 0, multiClubGUIDs.length = 0, multiClubEmails.length = 0;
                    $(".select-row option:selected").each(function () {
                        var $thisOption = $(this);
                        if ($thisOption.text() != "Select a club") {
                            multiClubNames.push($thisOption.text());
                            multiClubGUIDs.push($thisOption.val());
                            multiClubEmails.push($thisOption.data("email"));
                        }
                    });

                    var $selectParentWrap = $(".selectParentWrap");
                    $selectParentWrap.find(".clubName").val(multiClubNames.join("|"));
                    $selectParentWrap.find(".clubGUID").val(multiClubGUIDs.join("|"));
                    $selectParentWrap.find(".clubEmail").val(multiClubEmails.join("|"));
                }
                
                //Remove focus from button once clicked
                $(".btn").click(function (e) {
                    $(this).blur();
                });

                //Fix datepicker in ie
                $(".ui-datepicker .ui-datepicker-calendar a").live("click", function () {
                    $("input.datepicker").blur();
                });

                //Stop multiple clicks on submit buttons
                var $btnSubmit = $(".btn-submit");

                if ($btnSubmit.length > 0) {

                    var GlobalFormName = '', GlobalBtnSize = '', GlobalBtnClasses = '';

                    if (typeof ($btnSubmit.parent().attr("id")) != "undefined") {
                        GlobalFormName = $btnSubmit.parent().attr("id").substr(4);
                    }

                    if (typeof ($(".btn-submit").attr("class")) != "undefined") {
                        GlobalBtnClasses = $(".btn-submit").attr("class").split(" ");
                    }

                    for (var i = 0; i < GlobalBtnClasses.length; i++) {
                        if (GlobalBtnClasses[i].substring(0, 8) == "btn-cta-") {
                            GlobalBtnSize = GlobalBtnClasses[i].substr(GlobalBtnClasses[i].lastIndexOf("-") + 1);
                        }
                    }

                    var requestManager = Sys.WebForms.PageRequestManager.getInstance();
                    requestManager.add_initializeRequest(CancelPostbackForSubsequentSubmitClicks);
                }

                

                //Submit form - for club enquiry form
                if ($(".chzn-clublist").length > 0) {
                    var $chznClublist = $(".chzn-clublist"),
						$chznClublistWrap = $(".chzn-clublist").closest(".selectParentWrap"),
						$currentSelectedClub = $chznClublist.next().find("span"),
						$clubEmailRedirect = $chznClublistWrap.find("input.clubEmail"),
						$clubGUIDRedirect = $chznClublistWrap.find("input.clubGUID"),
						$clubNameRedirect = $chznClublistWrap.find("input.clubName"),
						len = $chznClublist.children("option").length,
						clubGUID, clubEmail, clubGUIDs, clubEmails, clubNames,
						multiClubGUIDs = [], multiClubEmails = [], multiClubNames = [];

                    if ($chznClublist.hasClass("update-inputs")) {
                        if ($clubNameRedirect.val() !== '') {
                            $currentSelectedClub.text($clubNameRedirect.val());
                        }
                        $chznClublist.children("option[value='" + $clubGUIDRedirect.val() + "']").attr('selected', 'selected');

                        $chznClublist.change(function () {
                            for (var i = 0; i < len; i++) {
                                if ($currentSelectedClub.text() === $chznClublist.children("option:eq(" + i + ")").text()) {
                                    clubGUID = $chznClublist.children("option:eq(" + i + ")").val();
                                    clubEmail = $chznClublist.children("option:eq(" + i + ")").data("email");
                                }
                            }

                            $clubEmailRedirect.val(clubEmail);
                            $clubGUIDRedirect.val(clubGUID);
                            $clubNameRedirect.val($currentSelectedClub.text());

                            //Forcing ie to update error status
                            if ($html.hasClass("ie")) {
                                if ($currentSelectedClub.text() == "Select a club") {
                                    $(this).next().children(".chzn-single").addClass("error").closest(".wrap").addClass("wrapErrorFullWidth").children("span.span1").css({ display: "inline" });
                                } else {
                                    $(this).removeClass("error").next().children(".chzn-single").removeClass("error").closest(".wrap").removeClass("wrapErrorFullWidth").children("span").css({ display: "none" });
                                }
                            }
                        });

                    } else if ($(".enquiry-corporate select").hasClass("chzn-corporate-enquiry")) {
                        var $select = $(".chzn-corporate-enquiry"),
							$htmlAdd = $(".select-row").children(".add-club"),
							isClubSelected = false,
							aspValidationID = $select.attr("id");

                        //Add new select/icons if add icon is clicked
                        $(".add-club .icon-add").live("click", function (e) {
                            e.preventDefault();

                             $select = $(".chzn-corporate-enquiry").last();
                             

                            //Clone select list and append
                            $(".select-row").removeClass("select-row-new").children(".add-club").removeAttr("style");
                            $select.clone().insertAfter($(this).closest(".select-row")).wrap("<div class='select-row select-row-new' />");
                            $(".select-row-new").append($htmlAdd.clone()).children("select").css({ display: "block" }).removeClass("chzn-done").removeAttr("id");

                            var count = $(".select-row").length;
                            $(".select-row-new select").attr('name','content_0$centre_0$ctl00$findclub'+count).find('option[value="'+$select.val()+'"]').remove();

                            //Apply correct styling for dropdown list
                            if (isTouchNotiPad) {
                                $(".select-row-new select").styleSelects();
                            } else {
                                $(".select-row-new select").chosen();
                            }

                            //If more than one select, remove "hide" icon and show "add" icon
                            if ($(".select-row").length > 1) {
                                $(".select-row")
									.find(".icon-add").addClass("icon-add-hide").end()
									.find(".icon-del").removeClass("icon-del-hide").animate({ opacity: 1 }).end()
									.filter(".select-row-new").find(".icon-del").addClass("icon-del-hide");
                            }
                        });

                        //Remove row if delete icon is pressed, and update hidden inputs
                        $(".add-club .icon-del").live("click", function (e) {
                            e.preventDefault();
                            $(this).closest(".select-row").remove();

                            updateHiddenInputs();
                            updateErrorMessaging($(".select-row-new"));
                        });

                        //Update hidden input fields if dropdown changes
                        $(".select-row select").live("change", function (e) {
                            updateHiddenInputs();
                            updateErrorMessaging($(this));
                        });

                        

                        //Else for all other forms (eg Club finder)
                    } else {
                        $chznClublist.change(function () {

                            //Check if GUID
                            var numericExpression = "\^[0-9A-Z]{32}$";
                            if (this.value.match(numericExpression)) {
                                if (this.value !== "") {
                                    //debug("Selected club " + this.value);
                                    var params = {
                                        ajax: 1,
                                        clubid: this.value,
                                        cmd: "GetClubUrlForId"
                                    };

                                    var $sectionUrl = "";

                                    //see if we are redirecting to a particular club section
                                    if ($chznClublist.hasClass("club-section-redirect")) {
                                        //get section to redirect to from parent data-url value
                                        if ($chznClublist.closest(".cta-wrap").data("url") !== null) {
                                            $sectionUrl = $chznClublist.closest(".cta-wrap").data("url");
                                        }
                                    }

                                    $.get(document.location, params, function (data) {
                                        window.location = data + $sectionUrl;
                                    });
                                }
                                return false;
                            }
                            else {
                                //if not GUID then go to the address passed in
                                window.location = this.value;
                                return false;
                            }
                        });

                    }
                }
				
				function updateChznPlaceholder() {
                    if ($clubContainer.text() == "Select a club") {
                        $clubContainer.addClass("placeholder");
                    } else {
                        $clubContainer.removeClass("placeholder");
                    }
                }
                
                //Update placeholder text colour on chzn selects
                var $clubContainer = $(".chzn-container span");
                updateChznPlaceholder();
                $("select.chzn-clublist").change(function () {
                    updateChznPlaceholder();
                });

                

                //Update placeholder text colour on normal selects
                $(".select-wrap").livequery(function () {
                    function updateSelect() {
                        if ($styledSelect.text() == "Select") {
                            $styledSelect.addClass("select-text");
                        } else {
                            $styledSelect.removeClass("select-text");
                        }
                    }
                    
                    var $this = $(this),
						$styledSelect = $this.find(".styleSelect");

                    updateSelect();
                    $(".select-wrap select").change(function () {
                        updateSelect();
                    });

                });


                //Contact us form porn
                if ($(".contact-panel").hasClass("contact-panel-club")) {
                    $(".contact-panel").parent().addClass("contact-wrap");
                }

                var $contactWrap = $(".contact-wrap"),
                    contactClubName, contactClubGuid, contactClubEmail;

                $contactWrap.find(".chzn-clublist").change(function () {
                    var $this = $(this);

                    contactClubName = "", contactClubGuid = "", contactClubEmail = "";

                    contactClubName = $this.next().find(".chzn-single span").text();
                    contactClubGuid = $this.find("option:selected").val();
                    contactClubEmail = $this.find("option:selected").data("email");



                    if ($this.hasClass("club-select-top")) {

                        $($('.club-select-bottom')).val($(this).val());           // updating second club dropdown box to have the same value as the first club dropdown box
                        $($('.club-select-bottom')).trigger("liszt:updated");     // updating second club dropdownbox visually

                        // hide second dropdown box on iphone if first dropbox is selected
                        if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/blackberry*/)) || (navigator.userAgent.match(/android/i))) {
                            $('.row-club-select-bottom').hide();
                        }

                        if ($(".form-to-complete").hasClass("hidden")) {
                            //if form has been submitted -clear confirmation screen and clear data entry fields
                            $(".form-to-complete").removeClass('hidden');
                            $(".form-completed").addClass('hidden');

                            $(".form-text-field").val('');
                            $(".msg-main-text").val('');
                            $(".text").val('');

                            $(".text").chosen();
                            $('.club-select-bottom').chosen({"hasSearch":true});
                            
                            
                            $($('.club-select-bottom')).val($(this).val());           // updating second club dropdown box to have the same value as the first club dropdown box
                            $($('.club-select-bottom')).trigger("liszt:updated");     // updating second club dropdownbox visually
							
                        }




                    } else if ($this.hasClass("club-select-bottom")) {
                        $(".club-select-top").next().find(".chzn-single span").text(contactClubName);
                    }

                    //Set error message
                    if (contactClubName == "Select a club") {
                        $(".club-select-bottom").addClass("error").siblings("span:first").css({ display: "inline" }).closest(".form-row").addClass("wrapErrorFullWidth").find(".chzn-single").addClass("error");

                    }
                    else {
                        $(".club-select-bottom").removeClass("error").siblings("span:first").css({ display: "none" }).closest(".form-row").removeClass("wrapErrorFullWidth").find(".chzn-single").removeClass("error");
                    }

                    $("input.clubName").val(contactClubName);
                    $("input.clubGUID").val(contactClubGuid);
                    $("input.clubEmail").val(contactClubEmail);
                });


            },


            homepageSpecific: function () {


                //Add tooltips on hover, if not on a touch screen device
                if ($html.hasClass("no-touch")) {
                    $(".carousel-btns li, .has-tooltip").each(function (i) {
                        var $this = $(this);
                        var $btn = $this.children("a");
                        $this.addClass("tooltip-" + i);

                        $btn.hover(function () {
                            var w = (($btn.outerWidth() - 24) > 130) ? ($btn.outerWidth() - 24) : 130;
                            $btn.next(".tooltip:not(#clubfinder-panel)").css({ width: w + "px" });
                            $btn.next(".tooltip").stop(true, true).fadeIn().children(".tooltip-arrow").fadeIn();
                            $btn.next(".tooltip:not(#clubfinder-panel)").css({ top: "-" + ($this.children(".tooltip").outerHeight() + 25) + "px" });
                        }, function () {
                            $btn.next(".tooltip").stop(true, true).fadeOut().children(".tooltip-arrow").fadeOut();
                        });
                    });
                }

                //Streetview glow
                $("#streetview-wrap a").hover(function () {
                    $(this).closest("#streetview-wrap").removeClass("glow-active");
                }, function () {
                    $(this).closest("#streetview-wrap").addClass("glow-active");
                });

                //Close last visited popup
                $("#clubfinder-panel .close-btn a").click(function (e) {
                    e.preventDefault();
                    $(this).closest("#clubfinder-panel").fadeOut();
                });

                //Last visited popup - show confirmation if "Make it your home club" is clicked
                $("#last-homeclub a").click(function (e) {
                    e.preventDefault();
                    var $this = $(this),
						thisHeight = $this.height();
                    $this.addClass("confirmation").css({ height: thisHeight + "px" });

                    SetHomeClub_Header();

                    setTimeout(function () {
                        $("#clubfinder-panel").remove();
                    }, 2000);
                });

                //Add styling to carousel buttons
                $(".carousel-btns li:eq(0)").addClass("first");

                if($(window).width() > 1050){
	                //Add More button and functionality
	                $('<p id="carousel-more" class="rep"><a href="#bottom"><span></span>More &rsaquo;</a></p>').appendTo("#carousel-wrap");
	                $.autoscroll.init('#carousel-more a');
                }
            },


            setupImageRotator: function () {
       
                var $carousel = $("#carousel"),
					$slides = $carousel.children("li"),
					activeSlide = 0,
					numSlides = $slides.length,
					slideWidth = $carousel.width(),
					slideSpeed = 5000,
					slideText = 1000,
					clickedDot, prevActiveSlide, setSlideRotate = null;

                $slides.css({ left: "-" + slideWidth + "px" }).children("img").css({ display: "block" }).end().filter(":first").css({ left: 0 }).addClass("active-slide");

                //Insert dots
                var dotsHTML = '<ul id="carousel-dots">';
                for (var i = 0; i < numSlides; i++) {
                    dotsHTML += '<li><a href="#" class="rep"><span></span>Slide ' + (i + 1) + '</a></li>';
                }
                dotsHTML += "</ul>";
                $(dotsHTML).appendTo(".title-wrap");

                //Create active dot and make them clickable
                var $dots = $("#carousel-dots a");
                $dots.eq(0).addClass("active");
                $dots.click(function (e) {
                    e.preventDefault();
                    clickedDot = $("#carousel-dots a").index($(this));
                    shiftSlide(clickedDot);
                });

                //Set up auto carousel
                if ($slides.length > 0) {
                    setSlideRotate = setInterval(function (e) { shiftSlide(); }, slideSpeed);
                }

                function shiftSlide(clickedDot) {
                    prevActiveSlide = activeSlide;
                    if (clickedDot) {
                        activeSlide = clickedDot;
                    } else {
                        activeSlide++;
                    }
                    if (activeSlide == $slides.length) { activeSlide = 0; }

                    $slides.eq(activeSlide).addClass("active-slide").children(".carousel-header, .carousel-intro").css({ display: "none" }).end().css({ "left": slideWidth + "px" }).stop(true, true).animate({ "left": 0 }, slideText, function () {
                        $slides.eq(activeSlide).children(".carousel-header, .carousel-intro").fadeIn(slideSpeed / 5);
                    });
                    $dots.eq(prevActiveSlide).removeClass("active");
                    $dots.eq(activeSlide).addClass("active");
                    $slides.eq(prevActiveSlide).removeClass("active-slide").children(".carousel-header, .carousel-intro").hide().end().stop(true, true).animate({ "left": -slideWidth + "px" }, slideText);

                    if (clickedDot) { setDot(); }
                }

                function setDot() {
                    $dots.removeClass("active").eq(activeSlide).addClass("active");
                    clearInterval(setSlideRotate);
                    setSlideRotate = setInterval(function (e) { shiftSlide(); }, slideSpeed);
                }
            },


            setupTools: function () {

			
                if ($html.hasClass("no-touch")) {
                    $(".input-number").each(function () {
                        $(this).attr("type", "text");
                    });
                }

                var toolType = $(".tool-main").attr("id"),
					unitsSet = "imperial",
					$toolType = $("#" + toolType),
					$unitsSwitch = $toolType.find(".units-switch"),
					$elOne = $toolType.find(".form-inputs div:eq(0)"),
					$elTwo = $toolType.find(".form-inputs div:eq(1)"),
					$resultsMsg = $toolType.find(".result-message");

                $unitsSwitch.on("click", "a", function (e) {
                    var $this = $(this), unitchanged = true;
                    $unitsSwitch.children("a").removeClass("active").filter($this).addClass("active");

                    if (unitsSet === $this.data("unit")) {
                        unitchanged = false;
                    } else {
                        unitsSet = $this.data("unit");
                    }

                    $elOne.find(".unit").text($unitsSwitch.children("a:eq(0)").data(unitsSet));
                    $elTwo.find(".unit").text($unitsSwitch.children("a:eq(1)").data(unitsSet));

                    if (unitchanged === true && $('#label-weight').val() != 'Enter') {

                        //Update with correct value for the weight
                        if ($('#label-weight').val() != $this.attr('data-weight-val')) {
                            $this.attr('data-weight-val', $('#label-weight').val());

                            if (unitsSet === 'metric' && $('#label-weight').val()) {
                                $('#label-weight').val($.conversions.LbstoKg($('#label-weight').val()).kg);
                            } else if (unitsSet === 'imperial' && $('#label-weight').val()) {
                                $('#label-weight').val($.conversions.KgtoLbs($('#label-weight').val()).pounds);
                            }

                            $this.siblings('a').attr('data-weight-val', $('#label-weight').val());

                        }
                        else {
                            $('#label-weight').val($(this).siblings('a').attr('data-weight-val'));
                        }

                        //Update with correct value for the height
                        if ($('#label-height').val() != $this.attr('data-height-val')) {
                            $this.attr('data-height-val', $('#label-height').val());

                            if (unitsSet === 'metric' && $('#label-height').val()) {
                                $('#label-height').val($.conversions.InchestoCms($('#label-height').val()));
                            } else if (unitsSet === 'imperial' && $('#label-height').val()) {
                                $('#label-height').val($.conversions.CmstoInches($('#label-height').val()));
                            }

                            $this.siblings('a').attr('data-height-val', $('#label-height').val());

                        }
                        else {
                            $('#label-height').val($(this).siblings('a').attr('data-height-val'));
                        }

                    }

                    return false;
                });

                $toolType.find(".btn").click(function (e) {
                    e.preventDefault();
                    $(this).blur();

                    var $inputOne = $elOne.children("input"),
						$inputTwo = $elTwo.children("input"),
						inputTypeOne = $inputOne.data("type"),
						inputTypeTwo = $inputTwo.data("type"),
						errorMsg,errorValues, toolScore, toolScoreGrade, toolScoreComment,inputTypeThree,errorTypeThree,$inputThree,$elThree;

                    var errorTypeOne = !$inputOne.val() || isNaN($inputOne.val()),
						errorTypeTwo = !$inputTwo.val() || isNaN($inputTwo.val());

                    if ($toolType.find(".form-inputs div:eq(2)").length > 0) {
                        $elThree = $toolType.find(".form-inputs div:eq(2)");
						$inputThree = $elThree.children("input");
						inputTypeThree = $inputThree.data("type");
						errorTypeThree = !$inputThree.val() || isNaN($inputThree.val());
                    }
                    if ($toolType.find(".form-inputs div:eq(3)").length > 0) {
                        var $elFour = $toolType.find(".form-inputs div:eq(3)"),
							$inputFour = $elFour.find("select"),
							inputTypeFour = $elFour.find("select").data("type");
                    }

                    if (errorTypeOne || errorTypeTwo || errorTypeThree) {
                        errorMsg = $resultsMsg.data("errormsg");
						errorValues = [];

                        $(".show-results").hide().prev(".results-intro").show();

                        //Add input type to error array
                        if (errorTypeOne) { errorValues.push(inputTypeOne); }
                        if (errorTypeTwo) { errorValues.push(inputTypeTwo); }
                        if (errorTypeThree) { errorValues.push(inputTypeThree); }

                        //Focus on correct input field
                        if (errorTypeThree) { $inputThree.select().focus(); }
                        if (errorTypeTwo) { $inputTwo.select().focus(); }
                        if (errorTypeOne) { $inputOne.select().focus(); }

                        if (errorValues.length > 1) {
                            errorValues = errorValues.slice(0, errorValues.length - 1).join(", ").concat(" and " + errorValues[errorValues.length - 1]);
                        }
                        $resultsMsg.addClass("tools-error").html("<p>" + errorMsg + errorValues + "</p>");

                    } else {
                        $(".results-intro").hide().next(".show-results").show();

						var calculateCalories = function() {
							var genderVal = $(".gender-select input:radio:checked").val(),
								heightVal = parseInt($inputOne.val()),
								weightVal = parseInt($inputTwo.val()),
								ageVal = parseInt($inputThree.val()),
								activityVal = $inputFour.attr("value");



							if (unitsSet === "imperial") {
								heightVal = heightVal * 2.54;
								weightVal = weightVal / 2.2;
							}
							if (genderVal === "female") {
								heightVal = heightVal * 1.7;
								weightVal = (weightVal * 9.6) + 655;
								ageVal = ageVal * 4.7;
							} else {
								heightVal = heightVal * 5;
								weightVal = (weightVal * 13.7) + 66;
								ageVal = ageVal * 6.8;
							}
						
	
							toolScore = Math.ceil((weightVal + heightVal - ageVal) * activityVal);
							$(".update-result").text(toolScore);
							$resultsMsg.html("");
						};
	
						var calculateBMI = function() {
							var heightVal = $inputOne.val();
							var weightVal = $inputTwo.val();
							
							if (unitsSet === "imperial") {
								heightVal = heightVal * 2.54;
								weightVal = weightVal * 0.45359237;
							}
							toolScore = Math.round(weightVal / (heightVal / 100 * heightVal / 100));
							$(".update-result").text(toolScore);
							
							if (toolScore >= 30) {
								toolScoreGrade = $resultsMsg.data("obese");
								toolScoreComment = $resultsMsg.data("comment-obese");
							} else if ((toolScore >= 25) && (toolScore < 30)) {
								toolScoreGrade = $resultsMsg.data("over");
								toolScoreComment = $resultsMsg.data("comment-over");
							} else if ((toolScore >= 20) && (toolScore < 25)) {
								toolScoreGrade = $resultsMsg.data("optimum");
								toolScoreComment = $resultsMsg.data("comment-optimum");
							} else {
								toolScoreGrade = $resultsMsg.data("under");
								toolScoreComment = $resultsMsg.data("comment-under");
							}
							$resultsMsg.removeClass("tools-error").html("<p>" + $resultsMsg.data("comment1") + " <strong>" + toolScoreGrade + "</strong>. " + toolScoreComment + "</p>");
						};
	
						var calculateWHR = function() {
							var ratio;
							if (unitsSet === "imperial") {
								ratio = $inputOne.val() / $inputTwo.val();
							} else {
								ratio = ($inputOne.val() * 2.54) / ($inputTwo.val() * 2.54);
							}
							toolScore = Math.round((ratio) * 100) / 100;
							$(".update-result").text(toolScore);
							
							if (toolScore >= 0.9) {
									toolScoreGrade = $resultsMsg.data("extreme");
							toolScoreComment = $resultsMsg.data("comment-extreme");
							} else if ((toolScore >= 0.85) && (toolScore < 0.9)) {
								toolScoreGrade = $resultsMsg.data("high");
								toolScoreComment = $resultsMsg.data("comment-high");
							} else if ((toolScore >= 0.8) && (toolScore < 0.85)) {
								toolScoreGrade = $resultsMsg.data("average");
								toolScoreComment = $resultsMsg.data("comment-average");
							} else if ((toolScore >= 0.75) && (toolScore < 0.8)) {
								toolScoreGrade = $resultsMsg.data("good");
								toolScoreComment = $resultsMsg.data("comment-good");
							} else {
								toolScoreGrade = $resultsMsg.data("excellent");
								toolScoreComment = $resultsMsg.data("comment-excellent");
							}
							$resultsMsg.removeClass("tools-error").html("<p>" + $(".result-value .main").text() + " <strong>" + toolScoreGrade + "</strong></p> <p>" + toolScoreComment + "</p>");
						};
	
						//Calculate and move tool marker
						var positionMarker = function() {
							var bmiMarker = $(".result-marker"),
							markerPosition = 0,
							markerWidth = 24,
							scaleSectionStart = 0,
							stepsInSection = 0,
							scaleStart = 0,
							scaleEnd = 0,
							scaleMax = 0;
							
							// scale separeted in 4 setions, each of different proportions, hense scale values array below.
							// first value for section start position, second for section length. each section is five scale steps in length.
							var scale = [];
							if (toolType === "tool-bmi") {
								scaleStart = 15;
								scaleEnd = 35;
								scaleMax = 430;
								scale = [[15, 20, 0, 86, "sad"], [20, 25, 87, 145, "smile"], [25, 30, 231, 115, "neutral"], [30, 35, 346, 84, "sad"]];
							} else if (toolType === "tool-whr") {
								scaleStart = 0.65;
								scaleEnd = 1;
								scaleMax = 194;
								scale = [[0.55, 0.75, 0, 59, "smile"], [0.75, 0.8, 60, 88, "smile"], [0.8, 0.85, 89, 119, "neutral"], [0.85, 0.9, 120, 149, "neutral"], [0.9, 0.95, 150, 178, "sad"], [0.95, 1, 179, 206, "sad"]];
							}
							
							// determine where on a scale marker should sit, if not on the edges of the scale - decrease position by half marker width.
							if (toolScore <= scaleStart) {
								markerPosition = 0;
								bmiMarker.children().attr("class", "smiley " + scale[0][4]);
							} else if (toolScore >= scaleEnd) {
								markerPosition = scaleMax;
								bmiMarker.children().attr("class", "smiley " + "sad");
							} else {
								$.each(scale, function (index, val) {
									if (toolScore > val[0] && toolScore <= val[1]) {
										scaleSectionStart = val[2];
										stepsInSection = toolScore - val[0];
										markerPosition = scaleSectionStart + (stepsInSection * (val[3] / 5));
										bmiMarker.children().attr("class", "smiley " + val[4]);
									}
								});
							}
							
							if (bmiMarker.is(':visible')) {
								if (toolType === "tool-bmi") {
									bmiMarker.animate({ left: markerPosition - markerWidth / 2 }, 1000, "easeOutQuint");
								} else {
									bmiMarker.animate({ bottom: markerPosition + markerWidth / 2 }, 1000, "easeOutQuint");
								}
							} else {
								if (toolType === "tool-bmi") {
									bmiMarker.css({ left: markerPosition - markerWidth / 2, display: 'block' });
								} else {
									bmiMarker.css({ bottom: markerPosition + markerWidth / 2, display: 'block' });
								}
							}
						};

						if (toolType === "tool-whr") {
							calculateWHR();
							positionMarker();
						} else if (toolType === "tool-bmi") {
							calculateBMI();
							positionMarker();
						} else if (toolType === "tool-cc") {
							calculateCalories();
						}
					}
				});

                //Trigger click event if enter button is pressed
                $(document).keypress(function (e) {
                    if (e.which == 13) {
                        $(this).blur();
                        $toolType.find(".btn").trigger("click");
                    }
                });

            },


            setupAccordions: function () {
                var accordionsList = $('.accordion-list .accordion-item');
                if (accordionsList.length > 0) {
                    accordionsList.each(function () {
                        var _self = $(this);
                        var thisTrigger = _self.find('h5');
                        var thisBody = _self.find('.accordion-body');

                        thisTrigger.hover(function () {
                            $(this).addClass("accordian-hover");
                        }, function () {
                            $(this).removeClass("accordian-hover");
                        });
                        thisTrigger.bind('click', function (e) {
                            e.preventDefault();
                            if (!thisBody.is(':visible')) {
                                thisTrigger.removeClass('closed').addClass('open');
                                thisBody.slideDown(300);
                            } else {
                                thisTrigger.removeClass('open').addClass('closed');
                                thisBody.slideUp(300);
                            }
                        });
                    });
                }
            },

            // sets up club filtering categories
            setupClubFilter: function () {
                var filterResultsPanel = $('#filter-results-panel');
                var narrowByFilter = $('.narrow-by-filter');

                if (narrowByFilter.length > 0) {
                    var slideSpeed = 300;
                    var categoryFilterLinks = narrowByFilter.find('.accordion-item ul li a');

                    //Check if page has pre-populated _filters data
                    $(".narrow-by-filter a").each(function () {
                        if ($(this).attr("rel") == _filters) {
                            var $this = $(this);
                            $this.addClass('active').closest(".accordion-body").css({ display: "block" }).prev("h5").removeClass("closed").addClass("open");
                            sectionSelectedCheck($this);
                            addToFilters($this);
                        }
                    });

                    categoryFilterLinks.bind('click', function () {
                        if ($(this).hasClass('active')) {
                            $(this).removeClass('active');
                            var thisGUID = $(this).attr('rel');
                            var thisSection = $(this).parents('.accordion-item').attr('rel');
                            removeFromFilters(thisSection, thisGUID);
                        } else {
                            $(this).addClass('active');
                            sectionSelectedCheck($(this));
                            addToFilters($(this));
                        }
                        return false;
                    });

                    $('a.close-category').live('click', function () {
                        if ($(this).parents('li').length > 0) {
                            var thisGUID = $(this).parents('li').data('guid');
                            var thisSection = $(this).parents('li').data('section');
                            removeFromFilters(thisSection, thisGUID);

                            return false;
                        }
                    });
                }


                function addToFilters(obj) {
                    var thisMasterSection = obj.parents('.accordion-list').prev('.master-section').text();
                    var thisSection = obj.parents('.accordion-item').attr('rel');
                    var thisGUID = obj.attr('rel');
                    var thisText = $.trim(obj.text());
                    // debug('this data: ' + thisMasterSection + ' ' + thisSection + ' ' + thisText + ', guid=' + thisGUID);


                    // run section check
                    //sectionSelectedCheck($(this));

                    _filters[_filters.length] = '' + thisGUID;
                    FilterTriggered();

                    var tmpFilterObj = $("<li class='new-item'><a href='#' class='close-category'>" + thisSection + ": <span class='bold'>" + thisText + "</span></a></li>")
						.css({ display: 'none' })
						.data('section', thisSection)
						.data('guid', thisGUID)
						.click(function () {
							var j = 0;
							while (j < _filters.length) {
								if (_filters[j] == $(this).data('guid').toString()) {
									_filters.splice(j, 1);
								} else { j++; }
							}
							FilterTriggered();
						});

                    var activeFilterPanels = filterResultsPanel.find('.filter-section');
                    var matchingFilterPanel = activeFilterPanels.filter(function () { return $(this).attr('rel') == thisMasterSection; });

                    if (matchingFilterPanel.length > 0) {
                        matchingFilterPanel.find('ul').append(tmpFilterObj);
                        matchingFilterPanel.find('ul li.new-item').slideDown(slideSpeed).removeClass('new-item');
                    } else {
                        var tmpPanel = $("<div class='filter-results filter-section' rel='" + thisMasterSection + "'><h3>" + thisMasterSection + "</h3></div>").append($("<ul></ul>").append(tmpFilterObj));
                        filterResultsPanel.append(tmpPanel);
                        filterResultsPanel.find('.filter-section ul li.new-item').slideDown(slideSpeed).removeClass('new-item');
                    }
                }

                function removeFromFilters(section, guid) {
                    var activeFilterPanels = filterResultsPanel.find('.filter-section');
                    //debug('remove filter');

                    var j = 0;
                    while (j < _filters.length) {
                        if (_filters[j] == guid.toString()) {
                            _filters.splice(j, 1);
                        } else { j++; }
                    }
                    FilterTriggered();
                    activeFilterPanels.each(function () {
                        var _this = $(this);
                        var activeFiltersList = _this.find('ul li');
                        var thisFilter = activeFiltersList.filter(function () { return $(this).data('guid') == guid; });
                        thisFilter.slideUp(slideSpeed, function () { thisFilter.remove(); });

                        setTimeout(function () {
                            if (_this.find('ul li').length < 1) {
                                _this.slideUp(slideSpeed, function () { _this.remove(); });
                            }
                        }, slideSpeed + 20);
                    });

                    var tmpFiltersList = narrowByFilter.find('.accordion-item').filter(function () { return $(this).attr('rel') == section; }).find('ul li a');
                    var filterToRemove = tmpFiltersList.filter(function () { return $(this).attr('rel') == guid; }).removeClass('active');
                    sectionSelectedCheck(filterToRemove);
                }

                // checks if there are any selected options inside the ddl and sets section class
                function sectionSelectedCheck(obj) {
                    var thisParentSection = obj.parents('.accordion-item');
                    if (thisParentSection.find('.accordion-body ul li a').hasClass('active')) {
                        thisParentSection.addClass('selected');
                    } else {
                        thisParentSection.removeClass('selected');
                    }
                }

            },


            clubsGoogleMapsSetup: function () {
                var mapPlaceHolder = $('#map-canvas');
                if (mapPlaceHolder.length > 0) {
                    var vaClubsInfoPanels = $('#va-map-panels .map-info-panel');

                    if (vaClubsInfoPanels.length > 0) {
                        var initGoogleMap = false;
                        var vaGoogleMap = 0; // placeholder var for google map
                        var mapCenterOverride = false;

                        // check is preferred map center location is defined - ie if user clicked on 'show on map' link of club search result
                        var mapCenter = $('#va-map-panels .map-center');
                        if (mapCenter.length > 0) {
                            var latOverride = mapCenter.find('.va-marker-lat').val();
                            var lngOverride = mapCenter.find('.va-marker-lng').val();
                            mapCenterOverride = true;
                        }

                        var bounds = new google.maps.LatLngBounds();

                        vaClubsInfoPanels.each(function (i) {
                            var _thisClubPanel = $(this);
                            var thisLat = _thisClubPanel.find('.va-marker-lat').val();
                            var thisLng = _thisClubPanel.find('.va-marker-lng').val();
                            var latlng = new google.maps.LatLng(thisLat, thisLng);
							var myOptions = {};

                            if (showAllClubs === true) { bounds.extend(latlng); }

                            // if map hasn't been initiated yet
                            if (!initGoogleMap) {
                                if (showAllClubs === false) {
                                    var altLatLng = new google.maps.LatLng(_SelectedClubLat, _SelectedClubLng);
                                    myOptions = {
                                        zoom: 14,
                                        center: altLatLng,
                                        mapTypeId: google.maps.MapTypeId.ROADMAP
                                    };
                                } else {
                                    myOptions = {
                                        zoom: 11,
                                        center: latlng,
                                        mapTypeId: google.maps.MapTypeId.ROADMAP
                                    };
                                }

                                vaGoogleMap = new google.maps.Map(document.getElementById("map-canvas"), myOptions);
                                initGoogleMap = true;
                            }

                            // for each info panel:
                            var markerImage = new google.maps.MarkerImage("/virginactive/images/panels/marker-va.png",
								new google.maps.Size(24, 34),
								new google.maps.Point(0, 0),
								new google.maps.Point(12, 34));
                            var markerShadow = new google.maps.MarkerImage("/virginactive/images/panels/marker-shadow.png",
								new google.maps.Size(44, 34),
								new google.maps.Point(0, 0),
								new google.maps.Point(12, 34));
                            var markerShape = { type: "poly", coord: [12, 0, 16, 1, 18, 2, 19, 3, 20, 4, 21, 5, 21, 6, 22, 7, 22, 8, 22, 9, 22, 10, 22, 11, 22, 12, 22, 13, 22, 14, 22, 15, 22, 16, 21, 17, 21, 18, 20, 19, 20, 20, 19, 21, 19, 22, 18, 23, 18, 24, 17, 25, 16, 26, 16, 27, 15, 28, 14, 29, 14, 30, 13, 31, 12, 32, 12, 33, 11, 33, 11, 32, 10, 31, 9, 30, 8, 29, 8, 28, 7, 27, 7, 26, 6, 25, 5, 24, 5, 23, 4, 22, 4, 21, 3, 20, 3, 19, 2, 18, 2, 17, 1, 16, 1, 15, 1, 14, 1, 13, 0, 12, 0, 11, 1, 10, 1, 9, 1, 8, 1, 7, 2, 6, 2, 5, 3, 4, 4, 3, 5, 2, 7, 1, 11, 0, 12, 0] };
                            var marker = new google.maps.Marker({
                                map: vaGoogleMap,
                                draggable: false,
                                icon: markerImage,
                                shadow: markerShadow,
                                shape: markerShape,
                                position: latlng,
                                visible: true
                            });

                            var vaBoxHTML = _thisClubPanel.get(0);

                            var infoBoxOptions = {
                                content: vaBoxHTML,
                                disableAutoPan: false,
                                maxWidth: 0,
                                alignBottom: true,
                                pixelOffset: new google.maps.Size(-184, -50),
                                zIndex: null,
                                boxStyle: {
                                    width: "370px"
                                },
                                closeBoxURL: "",
                                infoBoxClearance: new google.maps.Size(10, 10),
                                isHidden: false,
                                pane: "floatPane",
                                enableEventPropagation: false
                            };

                            var vaInfoBox = new InfoBox(infoBoxOptions);

                            //If "show on map" link clicked, show infoWindow
                            if ((showAllClubs === false) && (i == _SelectedClub)) {
                                vaInfoBox.open(vaGoogleMap, marker);
                            }

                            //Add handlers to open/close panel on marker click
                            google.maps.event.addListener(marker, "click", function (e) {
                                vaInfoBox.open(vaGoogleMap, this);
                            });
                            _thisClubPanel.find(".close").click(function () {
                                vaInfoBox.close();
                            });
                        });

                        // If "Map View" tab is clicked, show all pins within map boundary
                        if (showAllClubs === true) { vaGoogleMap.fitBounds(bounds); }
                    }
                }

            },



            clubsSetup: function () {
            	//Fix height
            	$('.clubs_height_controller').css('min-height',$('#carousel-wrap').height() - 40);
            	
        		$(window).resize(function() {
        			setTimeout(function(){$('.clubs_height_controller').css('min-height',$('#carousel-wrap').height() - 40);}, 5)
        		});    
        		
                //Jump nav links
				$.autoscroll.init('.jump-nav a');
                

                //Print functionality on timetable page
                var $printIcon = $(".icon-print");
                if ($printIcon.length > 0) {
                    $printIcon.click(function (e) {
                        e.preventDefault();
                        window.print();
                    });
                }

                //Show/hide info depending on which tab is clicked (Timetable/PT listing pages)
                var filterBy, filterType;
                $(".filter-tabs ul a").click(function (e) {
                    e.preventDefault();
                    var $this = $(this);
                    $(".filter-tabs ul a").removeClass("active").filter($this).addClass("active");
                    var temp = $this.attr("class").split(" ");
                    for (var i = 0; i < temp.length; i++) {
                        if (temp[i].substr(0, 5) == "show_") {
                            filterBy = temp[i].substring(temp[i].indexOf("_") + 1);
                        }
                    }

                    filterType = $(".filter-tabs").attr("id");
                    filterType = filterType.substring(0, filterType.indexOf("-"));
                    var $filterObj = (filterType == "timetable") ? $(".timetable") : $(".people-list li");

                    if (filterBy != "all") {
                        $filterObj.removeClass("hide-filtered").not("." + filterType + "_" + filterBy).addClass("hide-filtered").removeAttr("style");
                    } else {
                        if (filterType == "timetable") {
                            //Check whether a class filter has been applied before removing styles
                            if ($this.closest(".timetable-wrap").hasClass("timetable-filtered")) {
                                $(".timetable-wrap .timetable").each(function () {
                                    if ($(this).hasClass("no-classes")) {
                                        $(this).addClass("hide-filtered");
                                    } else {
                                        $(this).removeClass("hide-filtered");
                                    }
                                });
                            } else {
                                $filterObj.stop(true, true).removeClass("hide-filtered").fadeIn().removeAttr("style").find("tbody tr").removeAttr("style");
                            }
                        } else {
                            $filterObj.stop(true, true).removeClass("hide-filtered").fadeIn().removeAttr("style").find(".people-list li a").removeAttr("style");
                        }
                    }
                    if (filterType == "timetable") { addRowHighlight(); }

                    ///JONFIX!!!
                    //Hide day if all the rows in the table are hidden
                    $(".timetable-wrap .timetable").each(function () {
                        var $this = $(this);
                        if (($this.is(":visible")) && ($this.find("tbody tr").length === 0)) {
                            $this.addClass("no-classes");
                            if ($this.find(".no-classes").length > 0) {
                                $this.find(".no-classes").remove();
                            }
                            $this.append('<p class="no-classes">Sorry, there is no class information available for today</p>');
                        }
                    });
                    ///JONFIX!!!

                });
                addRowHighlight();

                //Update "filter by class" dropdown list and update timetable shown
                if ($(".timetable-wrap").length > 0) {
                    var $timetableWrap = $(".timetable-wrap");

                    //Grab array of exercise classes
                    var classesArray = [], classesId;
                    $timetableWrap.find("span.classname").each(function () {
                        classesArray.push($(this).text());
                    });

                    if (classesArray.length > 0) {
                        classesArray = $.unique(classesArray);
                        classesId = createIds(classesArray);
                        createClassesList(classesArray.sort(), classesId);
                    }
                    //Add name of exercise class to table row
                    $timetableWrap.find("span.classname").each(function () {
                        var $this = $(this);
                        var classType = $this.text();
                        classType = classType.replace(/ & |, | /g, "-");
                        $this.parent().parent().addClass(classType);
                    });

                    $(".filter-class").css({ width: $(".filter-class a").outerWidth() });

                    //Toggle dropdown list of classes
                    $(".filter-class a").click(function (e) {
                        e.preventDefault();
                        var $this = $(this);

                        //Check whether classes have been filtered and update .timetable
                        if ($this.hasClass("clear-filter")) {
                            $(".filter-class a").removeClass("clear-filter").text($this.text() == $this.data("clearfilter") ? $this.data("filterbyclass") : $this.data("clearfilter")).blur().closest(".timetable-wrap").removeClass("timetable-filtered");
                            $(".timetable-wrap").children(".timetable").each(function () {
                                var $thisTimetable = $(this);
                                $thisTimetable.removeClass("hide-filtered no-classes").find(".no-classes").remove();
                                $thisTimetable.find("tr").each(function () {
                                    $(this).show();
                                });
                            });
                            addRowHighlight();
                        } else {
                            $this.parent().next().slideToggle(600, "easeOutQuint");
                        }
                    });

                    //Add tooltip on timetable for class info and "token" copy
                    var classtip = $('.classtip span');
                    $(classtip).hide();

                    $('.classtip').not('.click').hover(function () {
                        $(this).find(classtip).stop(true, true).fadeToggle();
                    });
                    
                    $('.classtip.click').click(function () {
                    	
                    	$('.classtip.click span').stop(true, true).fadeOut();
                    
                    	if($(this).hasClass('active') == false){
                        	$(this).find(classtip).stop(true, true).fadeIn();
                        	$('.classtip.click').removeClass('active');
                        	$(this).addClass('active');
                        }
                        else{
                        	$('.classtip.click').removeClass('active');
                        }
                        
                        //$(this).toggleClass('active');
                    });

                    //Hide dropdown list once filter has been selected
                    $("#list-classes a").click(function (e) {
                        e.preventDefault();
                        var $clicked = $(this);
                        var $this = $(this), $filterByClass = $(".filter-class a");

                        $clicked.closest("ul").hide();
                       
                        $filterByClass.text($filterByClass.data("clearfilter"));
                        
                        
                        $filterByClass.addClass("clear-filter").closest(".timetable-wrap").addClass("timetable-filtered");

                        $(".timetable").removeClass("hide-filtered").find("tr").show();

                        //Turn off rows that do not match the filtered class name
                        $(".timetable-wrap tbody tr").each(function () {
                            var $this = $(this);
                            if (!$this.hasClass($clicked.attr("class"))) {
                                $this.hide();
                            }
                        });
                        //Hide day if all the rows in the table are hidden
                        $(".timetable-wrap .timetable").each(function () {
                            var $this = $(this);
                            if ($this.find("tbody tr:visible").length === 0) {
                                $this.addClass("hide-filtered no-classes");
                                ///JONFIX!!!
                                if ($this.find("p .no-classes").length === 0) {
                                    $this.append('<p class="no-classes">Sorry, there is no class information for ' + $clicked.text() + ' available today</p>');
                                }
                                ///JONFIX!!!

                            }
                        });

                        addRowHighlight();
                    });
                }

                function addRowHighlight() {
                    $(".timetable tr").removeClass("highlight").filter(":visible:odd").addClass("highlight");
                }

                function createIds(array) {
                    var temp, len = classesArray.length, out = [];
                    for (var i = 0; i < len; i++) {
                        temp = array[i].replace(/ & |, | /g, "-");
                        out.push(temp);
                    }
                    return out.sort();
                }

                function createClassesList(array, addClass) {
                    var len = classesArray.length, addClasses = '<ul id="list-classes">';
                    for (var i = 0; i < len; i++) {
                        addClasses += '<li><a href="#" class="' + classesId[i] + '">' + classesArray[i] + '</a></li>';
                    }
                    addClasses += "</ul>";
                    $(addClasses).insertAfter($(".filter-class"));
                    $("#list-classes").css({ width: $(".filter-class").width() });
                }
            },


            showFirstThree: function () {
                $(".show-limit").each(function () {
                    var $this = $(this);
                    if ($this.children("section").length > 3) {
                        $this.children(":nth-child(4)").addClass("show-all-link").append('<p class="show-all"><a href="#">Show all</a></p>').nextAll().hide();
                    } else if ($this.children("li").length > 3) {
                        $this.children(":nth-child(3)").addClass("show-all-link").append('<p class="show-all"><a href="#">Show all</a></p>').nextAll().hide();
                    }
                });
                $(".show-limit .show-all").click(function (e) {
                    e.preventDefault();
                    var $this = $(this);
                    var text = $this.children("a").text();
                    $this.toggleClass("show-all-less").parent().toggleClass("show-all-link").nextAll().slideToggle();
                    $this.children("a").text(text == "Show all" ? "Show less" : "Show all");
                });
            },



            clubsMapOverlay: function () {

                var map, directionDisplay, markersArray = [], icons = {},
					directionsService = new google.maps.DirectionsService(),
					selectedMode = "DRIVING",
					suggestedRoute = 0,
					$locTitle = $(".location-address .location-title"),
					$directionsForm = $(".get-directions"),
					lat = $locTitle.children("a").data("lat"),
					lng = $locTitle.children("a").data("lng"),
					dist = 20,
					latLng = new google.maps.LatLng(lat, lng),
					hasDirections = false,
					directionsReversed = false,
					activeInput = "from",
					directionsListHeight = $(".directions-list").height();

                //Adding bounding box
                var defaultBounds = new google.maps.LatLngBounds(
                //Rough bounding lat/long for the UK:
                //new google.maps.LatLng(49.83798, -5.97656),
                //new google.maps.LatLng(59.68993, 1.75781));
					dest(lat, lng, 225, dist),
					dest(lat, lng, 45, dist));

                //Autocomplete for "from" field
                var autocompleteFrom = new google.maps.places.Autocomplete($directionsForm.find(".from input")[0], {
                    bounds: defaultBounds,
                    types: ["geocode"]
                });
                google.maps.event.addListener(autocompleteFrom, "place_changed", function () {
                    var place = autocompleteFrom.getPlace();
                    autocompleteFrom.bindTo("bounds", map);
                });
                //Autocomplete for "to" field
                var autocompleteTo = new google.maps.places.Autocomplete($directionsForm.find(".to input")[0], {
                    bounds: defaultBounds,
                    types: ["geocode"]
                });
                google.maps.event.addListener(autocompleteTo, "place_changed", function () {
                    var place = autocompleteTo.getPlace();
                    autocompleteTo.bindTo("bounds", map);
                });

                //Styling the icons/polylines
                icons["va"] = new google.maps.MarkerImage("/virginactive/images/panels/marker-va.png",
					new google.maps.Size(24, 34),
					new google.maps.Point(0, 0),
					new google.maps.Point(12, 34));
                var markerShadow = new google.maps.MarkerImage("/virginactive/images/panels/marker-shadow.png",
					new google.maps.Size(44, 34),
					new google.maps.Point(0, 0),
					new google.maps.Point(12, 34));
                var markerShape = { type: "poly", coord: [12, 0, 16, 1, 18, 2, 19, 3, 20, 4, 21, 5, 21, 6, 22, 7, 22, 8, 22, 9, 22, 10, 22, 11, 22, 12, 22, 13, 22, 14, 22, 15, 22, 16, 21, 17, 21, 18, 20, 19, 20, 20, 19, 21, 19, 22, 18, 23, 18, 24, 17, 25, 16, 26, 16, 27, 15, 28, 14, 29, 14, 30, 13, 31, 12, 32, 12, 33, 11, 33, 11, 32, 10, 31, 9, 30, 8, 29, 8, 28, 7, 27, 7, 26, 6, 25, 5, 24, 5, 23, 4, 22, 4, 21, 3, 20, 3, 19, 2, 18, 2, 17, 1, 16, 1, 15, 1, 14, 1, 13, 0, 12, 0, 11, 1, 10, 1, 9, 1, 8, 1, 7, 2, 6, 2, 5, 3, 4, 4, 3, 5, 2, 7, 1, 11, 0, 12, 0] };
                var vaPolyLines = {
                    path: [],
                    strokeColor: "#c00",
                    strokeWeight: 3
                };

                vaOptions = new google.maps.Map(document.getElementById("map-large"), {
                    center: latLng,
                    zoom: 14,
                    panControl: false,
                    mapTypeControl: false,
                    zoomControl: true,
                    zoomControlOptions: {
                        style: google.maps.ZoomControlStyle.SMALL
                    },
                    scaleControl: false,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });
                map = new google.maps.Map(document.getElementById("map-large"), vaOptions);
                createMarker(latLng, "va", $locTitle.children("a").text());

                directionsDisplay = new google.maps.DirectionsRenderer({ suppressMarkers: true, polylineOptions: vaPolyLines });
                directionsDisplay.setMap(map);

                //Select content in input fields if focussed
                $directionsForm.find(".from input, .to input").focusin(function () {
                    $(this).select();
                });

                //Show and hide Address/Directions panels
                $(".tab-upper a").click(function (e) {
                    e.preventDefault();
                    var $this = $(this);
                    $this.blur();
                    if ($this.hasClass("inactive")) { return false; }

                    var $lowerPanel = $this.closest(".lower-panel");
                    var clicked = $(".tab-upper a").index($this);

                    $(".tab-upper a").removeClass("active").filter($this).addClass("active");
                    $(".tab-lower .tab-detail").hide().filter($(".tab-lower .tab-detail:eq(" + clicked + ")")).show();
                });

                //Update form if reverse icon is clicked
                $("#reverse a").click(function (e) {
                    e.preventDefault();
                    $directionsForm = $("#directions-form");
                    $directionsForm.toggleClass("directions-reversed");

                    var directionsReversed = $directionsForm.hasClass("directions-reversed") ? true : false;
                    var activeInput = $directionsForm.hasClass("directions-reversed") ? "to" : "from";
                    var nonActiveInput = $directionsForm.hasClass("directions-reversed") ? "from" : "to";
                    var placeholderText = (directionsReversed) ? "Ending location" : "Starting location";
                    var currentInputValue = $directionsForm.find("." + nonActiveInput + " input").val() || "";

                    $directionsForm.find("." + nonActiveInput + " input").attr({ "value": "", "placeholder": $locTitle.children("a").text(), "disabled": "disabled" });
                    $directionsForm.find("." + activeInput + " input").removeAttr("disabled").attr({ "value": currentInputValue, "placeholder": placeholderText });
                });

                //Calculate route when Get Directions button is clicked
                $directionsForm.find(".btn").click(function (e) {
                    e.preventDefault();
                    $(this).blur();

                    calcRoute();
                });

                //Change transport mode (driving/walking), and update map/directions
                $(".transport a").click(function (e) {
                    e.preventDefault();
                    var $this = $(this);
                    $(".transport a").children("span").removeClass("active");
                    $this.children("span").addClass("active");
                    selectedMode = $this.attr("id").toUpperCase();

                    calcRoute();
                });

                //Recentre map, clear directions and add custom marker when clicking on club name
                $locTitle.children("a").click(function (e) {
                    e.preventDefault();

                    resetSearch();
                    $directionsForm.find("." + activeInput + " input").val("").attr("placeholder", "Starting location");
                });

                //Change suggested route and update map/directions
                $(".suggested-route").live('click', function (e) {
                    e.preventDefault();
                    var $this = $(this);
                    $(".suggested-route").removeClass("selected-route");
                    $this.addClass("selected-route");
                    suggestedRoute = $this.data("route");

                    calcRoute();
                });

                function resetSearch() {
                    deleteOverlays();
                    directionsDisplay.setMap(null);
                    map.setCenter(latLng);
                    createMarker(latLng, "va", $locTitle.children("a").text());
                    $directionsForm.find(".btn").blur();
                    $(".directions-tab a").addClass("inactive");
                }

                function calcRoute() {
                    //Define start and end of route depending on whether journey is reversed or not
                    var start = (directionsReversed) ? latLng : $directionsForm.find(".from input").val().replace(/ | /g, "+"),
					end = (directionsReversed) ? $directionsForm.find(".to input").val().replace(/ | /g, "+") : latLng,
					directionsInfo = "", routesInfo = "";

                    if (!directionsReversed && start === "") { $directionsForm.find(".btn").blur(); return false; }
                    if (directionsReversed && end === "") { $directionsForm.find(".btn").blur(); return false; }

                    deleteOverlays();
                    hasDirections = true;
                    $(".directions-tab a").removeClass("inactive");

                    var request = {
                        origin: start,
                        destination: end,
                        travelMode: google.maps.DirectionsTravelMode[selectedMode],
                        provideRouteAlternatives: true
                    };
                    directionsService.route(request, function (response, status) {
                        if (status == google.maps.DirectionsStatus.OK) {
                            directionsDisplay.setDirections(response);

                            // Render selected route on the map
                            directionsDisplay.setRouteIndex(suggestedRoute);

                            //Grab routes and insert into ul.routes-list
                            var routes = response.routes;
                            if (routes.length > 1) {
                                //routesInfo += '<h4>Suggested Routes</h4>';
                                routesInfo += '<h4>' + $(".maptitle").data("suggestedroutes") + '</h4>';
                                routesInfo += "<ul>";
                                for (var r = 0; r < routes.length; r++) {
                                    var r_dist_dur = "",
                                   selectRouteClass = suggestedRoute == r ? ' selected-route' : '';
                                    routesInfo += '<li><a class="suggested-route' + selectRouteClass + '" href="#" data-route="' + r + '">' + routes[r].summary;
                                    if (routes[r].legs[0].distance && routes[r].legs[0].distance.text) { r_dist_dur += routes[r].legs[0].distance.text; }
                                    if (routes[r].legs[0].duration && routes[r].legs[0].duration.text) { r_dist_dur += ", " + routes[r].legs[0].duration.text; }
                                    if (r_dist_dur !== "") {
                                        routesInfo += ' <span class="dist-dur">' + r_dist_dur + '</span></a></li>';
                                    } else {
                                        routesInfo += "</li>";
                                    }
                                }
                                routesInfo += "</ul>";
                                $(".routes-list").html(routesInfo);

                                // Reset height of directions list
                                var newHeight = directionsListHeight - $(".routes-list").height();
                                $(".directions-list").css({ height: newHeight + 'px' });
                            }

                            //Add icons at start and end of journey
                            var legs = response.routes[suggestedRoute].legs;
                            createMarker(legs[0].start_location, "start", legs[0].start_address);
                            createMarker(legs[0].end_location, "end", $locTitle.children("a").text());

                            //Grab direction steps and insert into ul.directions-list
                            var steps = legs[0].steps;
                            for (var j = 0; j < steps.length; j++) {
                                var nextSegment = steps[j].path;
                                directionsInfo += "<li>" + steps[j].instructions;
                                var dist_dur = "";
                                if (steps[j].distance && steps[j].distance.text) { dist_dur += steps[j].distance.text; }
                                if (steps[j].duration && steps[j].duration.text) { dist_dur += ", " + steps[j].duration.text; }
                                if (dist_dur !== "") {
                                    directionsInfo += ' <span class="dist-dur">(' + dist_dur + ')</span></li>';
                                } else {
                                    directionsInfo += "</li>";
                                }
                            }
                            $(".directions-list").html(directionsInfo);
                        } else {
                            resetSearch();
                            $directionsForm.find(".from input").attr("placeholder", "0 results, please search again").val("");
                        }
                    });

                    //Switch to Directions tab
                    $(".tab-upper a").trigger("click");
                }


                //Trigger click event if enter button is pressed
                $(document).keypress(function (e) {
                    if (e.which == 13) {
                        $directionsForm.find("." + activeInput + " input").blur();
                        $(".get-directions .btn").trigger("click");
                    }
                });

                //Print functionality on timetable page
                var $printIcon = $(".icon-print");
                if ($printIcon.length > 0) {
                    $printIcon.parents("body").addClass("hasMapOverlay");
                    $printIcon.click(function (e) {
                        e.preventDefault();
                        window.print();
                    });
                }

                //Add correct icon/s to map (initial va location or start/end directions)
                function getMarkerImage(iconLabel) {
                    if ((typeof (iconLabel) == "undefined") || (iconLabel === null)) {
                        iconLabel = "va";
                    }
                    if (!icons[iconLabel]) {
                        icons[iconLabel] = new google.maps.MarkerImage("/virginactive/images/panels/marker-" + iconLabel + ".png",
							new google.maps.Size(24, 34),
							new google.maps.Point(0, 0),
							new google.maps.Point(12, 34));
                    }
                    return icons[iconLabel];
                }

                //Create marker array
                function createMarker(latlng, pos, label) {
                    var marker = new google.maps.Marker({
                        position: latlng,
                        map: map,
                        icon: getMarkerImage(pos),
                        shadow: markerShadow,
                        shape: markerShape,
                        title: label,
                        zIndex: Math.round(latlng.lat() * -100000) << 5
                    });
                    markersArray.push(marker);
                }

                //Delete markers from previous results
                function deleteOverlays() {
                    if (markersArray) {
                        for (var i = 0; i < markersArray.length; i++) {
                            markersArray[i].setMap(null);
                        }
                        markersArray.length = 0;
                    }
                }

                //Calculate bounding box based on current lat/long
                function toRad(num) {
                    return num * Math.PI / 180;
                }
                function toDeg(num) {
                    return num * 180 / Math.PI;
                }
                function dest(lat, lng, brng, dist) {
                    this._radius = 6371;
                    dist = typeof (dist) == "number" ? dist : typeof (dist) == "string" && dist.trim() !== "" ? +dist : NaN;
                    dist = dist / this._radius;
                    brng = toRad(brng);
                    var lat1 = toRad(lat),
						lon1 = toRad(lng);
                    var lat2 = Math.asin(Math.sin(lat1) * Math.cos(dist) + Math.cos(lat1) * Math.sin(dist) * Math.cos(brng));
                    var lon2 = lon1 + Math.atan2(Math.sin(brng) * Math.sin(dist) * Math.cos(lat1), Math.cos(dist) - Math.sin(lat1) * Math.sin(lat2));
                    lon2 = (lon2 + 3 * Math.PI) % (2 * Math.PI) - Math.PI;
                    return new google.maps.LatLng(toDeg(lat2), toDeg(lon2));
                }

            },


            streetview: function () {
                var currentClub = "canary-riverside";
                var panoOptions = {
                    pano: "reception",
                    visible: true,
                    //scrollwheel: false,
                    addressControl: false,
                    zoomControl: false,
                    panControl: false,
                    pov: {
                        heading: 0,
                        zoom: 1,
                        pitch: -5
                    },
                    panoProvider: function (pano) {
                        var room = rooms[pano];
                        return createCustomPanoramaData(room, pano);
                    }
                };

                var panorama = new google.maps.StreetViewPanorama(document.getElementById("streetview-canvas"), panoOptions);
                panorama.setPano("reception");

                function createCustomPanoramaData(room, pano) {
                    return {
                        location: {
                            pano: pano,
                            description: "Virgin Active " + room.club + ": " + room.name
                        },
                        links: room.links,
                        copyright: "Imagery (c) 2011 Virgin Active",
                        tiles: {
                            tileSize: new google.maps.Size(1024, 512),
                            worldSize: new google.maps.Size(1024, 512),
                            centerHeading: room.originHeading,
                            getTileUrl: function (room, zoom, x, y) {
                                return "/virginactive/images/streetview/" + currentClub + "/" + room + "_0_0_0.jpg";
                            }
                        }
                    };
                }

                $('<ul id="streetview-links"><li id="streetview-club1" class="streetview-club streetview-club-on">Canary Riverside</li><li id="streetview-club2" class="streetview-club">Northampton</li><li id="streetview-club3" class="streetview-club">West London</li></ul>').insertBefore("#streetview-canvas");
                //Centre streetview on Canary Riverside reception
                $("#streetview-club1").live("click", function () {
                    currentClub = "canary-riverside";
                    panorama.setPano("reception");
                    $(".streetview-club").removeClass("streetview-club-on").filter(this).addClass("streetview-club-on");
                });
                //Centre streetview on Northampton reception
                $("#streetview-club2").live("click", function () {
                    currentClub = "northampton";
                    panorama.setPano("n-reception");
                    $(".streetview-club").removeClass("streetview-club-on").filter(this).addClass("streetview-club-on");
                });
                //Centre streetview on West London reception
                $("#streetview-club3").live("click", function () {
                    currentClub = "west-london";
                    panorama.setPano("wl-reception");
                    $(".streetview-club").removeClass("streetview-club-on").filter(this).addClass("streetview-club-on");
                });


                //Colour text, arrow and line when hovering over the text
                $("text").live({ mouseenter: function () {
                    $(this).attr("fill", "#c00").prev().prev().attr("fill", "#c00").prev().prev().attr("fill", "#c66");
                },
                    mouseleave: function () {
                        $(this).attr("fill", "white").prev().prev().attr("fill", "white").prev().prev().attr("fill", "white");
                    }
                });
                //Colour text, arrow and line when hovering over the arrow
                $('path[cursor="pointer"]').live({ mouseenter: function () {
                    $(this).attr("fill", "#c00").next().attr("fill", "#c00").prev().prev().attr("fill", "#c00").prev().prev().attr("fill", "#c66");
                },
                    mouseleave: function () {
                        $(this).attr("fill", "white").next().attr("fill", "white").prev().prev().attr("fill", "white").prev().prev().attr("fill", "white");
                    }
                });

                //Find title panel in the html and update styles
                $(".gmnoprint").livequery(function () {
                    $(this).prev().children().css({ padding: "5px 10px", backgroundColor: "#fff", color: "#333", border: "1px solid #666", fontWeight: "bold" }).children("span").css({ display: "none" });
                });
                
                
                function onLinksChanged() {
                    $(".gmnoprint").prev().children().children("span").css({ display: "none" });
                }
                
                //Check when links are changed and remove the "address is approximate" text
                google.maps.event.addListener(panorama, "links_changed", onLinksChanged);
                

            }


           
        };
       


        c_self.functions.vaOnReady();
        c_self.functions.showFirstThree();
        c_ready = true;

    };

    // defaults
    $.virginActive.init.defaults = {
        //TODO:
        animationSpeed: "1000",
        intervalFrequency: "10000"
    };

    // run initialisation on jQuery ready
    // global object for access to functions (optional)
    $(document).ready(function () {
        $.va_init = new $.virginActive.init();

    });

})(jQuery);


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

var virginactive = function () {
    var cookies_page = function () {
        $('.cookies .saveCookie .btn-submit').click(function () {
            $(this).addClass('activated');
        });

        $('.cookies').on('click', '.cookieType input',function(){
$('.cookies .saveCookie .info').attr('class','info');
			$('.cookies .saveCookie .confirmation').attr('class','confirmation hidden');
        });
    };

	var cookie_bar = function(){
		if(!are_cookies_enabled()){
			$('#cookie-ribbon').remove();
		}
	};

    return {
        init: function () {
            if ($('.cookies').length) {
                cookies_page();
            }
            
            if($('#cookie-ribbon').length){
				cookie_bar();
            }
        }
    };
} ();

$(document).ready(function () {
	virginactive.init();
});