$(function () {


    var $html = $("html"),
		$form = $html.find("#form"),
		$row = $form.find(".row"),
		isiPad, isTouchNotiPad,
		isValid = false,
		hasSubmitted = false,
		count = 0,
		elems;

    $html.removeClass("nojs");
    if (navigator.userAgent.match(/iPad/i)) {
        isiPad = true;
        $html.addClass("iPad");
    } else if (($html.hasClass("touch")) && (!navigator.userAgent.match(/iPad/i))) {
        isTouchNotiPad = true;
    }


    //Style dropdowns 
    if (!$html.is(".ie6") && isTouchNotiPad) {
        $("select").styleSelects().closest(".row").addClass("row-styleSelects");
    } else if (!$html.is(".ie6")) {
        $("select:not(.chzn-clublist)").styleSelects().closest(".row").addClass("row-styleSelects");
    } else {
        $("select").parent().addClass("row-select");
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

            if ($this.children("p").hasClass("error")) {
                $this.find("p span").text(msg);
            } else if ($this.hasClass("row-error") && $this.hasClass("row-checkbox")) {
                $this.append('<p class="error error-checkbox"><span>' + msg + '</span></p>');
            } else if ($this.hasClass("row-error")) {
                $this.append('<p class="error"><span>' + msg + '</span></p>');
            }
        });
    }

    //Update validation criteria depending on whether user is a member or not
    function updateValidationData() {
        elems = [[$form.find(".text-name"), "^[a-zA-Z-' ]+$"],
    [$form.find(".text-email"), '^([0-9a-zA-Z"](["-.+\w]*[0-9a-zA-Z"])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.?)+[a-zA-Z]{1,9})$'],
    [$form.find(".text-number"), "^[0-9 ()]+$"]];

        $.each(elems, function (i, el) {
            el[0].focusout(function () {
                validChecks(i, el);
            });
        });
    }

    //Inline validation for text fields
    updateValidationData();

    function validChecks(i, el) {
        if (el[0].val() == "") {
            el[0].closest(".row").addClass("row-error");
        } else if (!el[0].val().match(elems[i][1])) {
            el[0].closest(".row").addClass("row-error row-invalid");
        } else {
            el[0].closest(".row").removeClass("row-error").children(".error").remove();
        }
        addErrorMsg();
    }


    //Inline validation for select/checkboxes/radios
    validateElems($form.find(".row-styleSelects select, .row-select select"), "change");
    validateElems($form.find(".row-toValidate input"), "click");

    function validateElems($el, eventHandler) {
        $el.bind(eventHandler, function () {
            var $this = $(this),
                isErrorTest = (eventHandler == "change") ? $this.val() == "default" : !$this.is(":checked");

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

        //Validate text fields
        $.each(elems, function (i, el) {
            validChecks(i, el);
        });

        //Validate radio buttons/checkboxes 
        $form.find(".row-toValidate").each(function () {
            if (!$(this).children("input").is(":checked")) {
                $(this).addClass("row-error");
            }
        });

        //Validate styled selects and form porn dropdowns
        $form.find(".row-select, .row-styleSelects").each(function () {
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

        if (count == 0) {
            isValid = true;
            //submit form

            if (!hasSubmitted) {
                hasSubmitted = true;
                $form.find(".btn-submit").addClass("btn-disabled");
                var itemname = $form.find(".btn-submit").attr('name');
                __doPostBack(itemname, '');

                //Add OpenTag tracking for weblead campaigns

                //this is a web lead -load open tag for web lead confirmation 
                if (typeof (_gaq) !== undefined) {
                    var ot = document.createElement('script');
                    ot.async = true;
                    ot.src = ('//d3c3cq33003psk.cloudfront.net/opentag-32825-68231.js');
                    var s = document.getElementsByTagName('script')[0];
                    s.parentNode.insertBefore(ot, s);

                    //record trackevent
                    var gaqCategory = "Form",
						gaqAction = "Submit",
						gaqLabel = $('.gaqTag').data("gaqlabel");

                    _gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);

                }

            }
        }
    });

});