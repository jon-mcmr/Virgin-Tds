(function ($) {

    /* Global settings ------------------------------------------- */
    var App = {};
    App.panels = {
        formHasSubmitted: false,
        currentPanel: $('.current-panel', $('#content')),
        panel2Data: "",
        validation: new Validation($('.current-panel', $('#content')), '.form-wrap', 'input,select'),
        setPanel2Text: function (formContainer) {
            $('#membership-text').html(formContainer.find('input.membership').attr('value'));
            var dob = formContainer.find('.dob-day').attr('value') + '/' + formContainer.find('.dob-month').attr('value') + '/' + formContainer.find('.dob-year').attr('value');
            $('#dob-text').html(dob);
        },
        showPanel2: function () {
            $('.full-panel').hide();
            App.panels.setPanel2Text($('.current-panel', $('#content')));
            App.panels.currentPanel.hide();
            App.panels.currentPanel = $('#panel-two');
            App.panels.validation = new Validation(App.panels.currentPanel, '.form-wrap', 'input,select');
            App.panels.setPanel2DataFields();
            App.panels.currentPanel.addClass('current-panel').fadeIn('slow');
        },
        setPanel2DataFields: function () {
            $('.personal-details-inner').find('.recordId').attr('value', App.panels.panel2Data[0].recordId);
            App.panels.currentPanel.find('.form-text-field-name').attr('value', App.panels.panel2Data[0].firstName);
            App.panels.currentPanel.find('.form-text-field-surname').attr('value', App.panels.panel2Data[0].surname);
            App.panels.currentPanel.find('.form-text-field-email').attr('value', App.panels.panel2Data[0].email);
            App.panels.currentPanel.find('.form-text-field-homephone').attr('value', App.panels.panel2Data[0].homePhone);
            App.panels.currentPanel.find('.form-text-field-workphone').attr('value', App.panels.panel2Data[0].workPhone);
            App.panels.currentPanel.find('.form-text-field-mobile').attr('value', App.panels.panel2Data[0].mobilePhone);
            App.panels.currentPanel.find('.form-text-field-address1').attr('value', App.panels.panel2Data[0].address1);
            App.panels.currentPanel.find('.form-text-field-address2').attr('value', App.panels.panel2Data[0].address2);
            App.panels.currentPanel.find('.form-text-field-address3').attr('value', App.panels.panel2Data[0].address3);
            App.panels.currentPanel.find('.form-text-field-address4').attr('value', App.panels.panel2Data[0].address4);
            App.panels.currentPanel.find('.form-text-field-address5').attr('value', App.panels.panel2Data[0].address5);
            App.panels.currentPanel.find('.form-text-field-postcode').attr('value', App.panels.panel2Data[0].postcode);


            if (App.panels.panel2Data[0].marketingByMail === 'True') {
                $('.checkbox-email input', $('#contact')).attr('checked', true);
                App.panels.validation.setRequiredGroupValidationEvent('.validation-contact-group');
            }
        },
        setPanel3Text: function () {
            var $panel2 = $('#panel-two');
            App.panels.currentPanel.find('.save-swipeno').html($('input.membership', '.panel-one').attr('value'));
            App.panels.currentPanel.find('.save-fistname').html($panel2.find('.form-text-field-name').attr('value'));
            App.panels.currentPanel.find('.save-surname').html($panel2.find('.form-text-field-surname').attr('value'));
            App.panels.currentPanel.find('.save-email').html($panel2.find('.form-text-field-email').attr('value'));
            App.panels.currentPanel.find('.save-homephone').html($panel2.find('.form-text-field-homephone').attr('value'));
            App.panels.currentPanel.find('.save-workphone').html($panel2.find('.form-text-field-workphone').attr('value'));
            App.panels.currentPanel.find('.save-mobile').html($panel2.find('.form-text-field-mobile').attr('value'));
            App.panels.currentPanel.find('.save-dob').html($('#dob-text').html());
            App.panels.currentPanel.find('.save-address1').html($panel2.find('.form-text-field-address1').attr('value'));
            App.panels.currentPanel.find('.save-address2').html($panel2.find('.form-text-field-address2').attr('value'));
            App.panels.currentPanel.find('.save-address3').html($panel2.find('.form-text-field-address3').attr('value'));
            App.panels.currentPanel.find('.save-address4').html($panel2.find('.form-text-field-address4').attr('value'));
            App.panels.currentPanel.find('.save-address5').html($panel2.find('.form-text-field-address5').attr('value'));
            App.panels.currentPanel.find('.save-postcode').html($panel2.find('.form-text-field-postcode').attr('value'));

            var isEmailChecked = $('.checkbox-email input', $('#contact')).attr('checked');
            if (isEmailChecked === "checked") {
                App.panels.currentPanel.find('.save-chk-email').removeClass('hide');
            } else {
                App.panels.currentPanel.find('.save-chk-email-none').removeClass('hide');
            }
        }
    }

    //$("section.panel-one").show();

    /* Submit buttons click event ------------------------------------------- */
    $('.btn-submit').bind('click', function (e) {
        e.preventDefault();
        var $formContainer = $(this).closest('section');
        // call validation
        if (App.panels.validation.validate()) {
            // show next section
            if ($(this).hasClass('show-section')) {
                var showSection = $(this).attr('href');
                if (showSection === '#panel-two') {
                    $(this).addClass('loading');
                    var swipeNr = $formContainer.find('input.membership').attr('value');
                    var dob = $formContainer.find('.dob-day').attr('value') + '-' + $formContainer.find('.dob-month').attr('value') + '-' + $formContainer.find('.dob-year').attr('value');
                    var postcode = $formContainer.find('input.input-postcode', $('.panel-one')).attr('value');
                    var data1 = GetMemberDetails(swipeNr, dob, postcode);
                    return;
                }
                if (showSection === '#panel-three') {
                    App.panels.currentPanel = $('.panel-three', $('#content'));
                    App.panels.setPanel3Text();
                    $formContainer.hide();
                    App.panels.currentPanel.fadeIn('slow');
                    return;
                }
                $formContainer.hide();
                $(showSection).addClass('current-panel').fadeIn('slow');
            }
        }
    });

    /* Toggle email validation event      ------------------------------------------- */
    $('.checkbox-email input', ('#contact')).bind('click', function (e) {
        var checked = $(this).attr('checked');
        if (checked == 'checked') {
            App.panels.validation.setRequiredGroupValidation('.validation-contact-group');
        } else {
            App.panels.validation.removeRequiredGroupValidation('.validation-contact-group');
        }
    });

    /* Edit/go back button click event      ------------------------------------------- */
    $('.btn-edit', $('.panel-three', $('#content'))).bind('click', function (e) {
        e.preventDefault();
        $('.panel-three', $('#content')).hide().find('.contact div.form-value').addClass('hide');
        App.panels.currentPanel = $('#panel-two');
        $('#panel-two').fadeIn('slow');

        // reset required group validation
        App.panels.validation.removeRequiredGroupValidation('.validation-contact-group');
        var checked = $('.checkbox-email input', ('#contact')).attr('checked');
        if (checked == 'checked') {
            App.panels.validation.setRequiredGroupValidation('.validation-contact-group');
        } else {
            App.panels.validation.removeRequiredGroupValidation('.validation-contact-group');
        }
    });

    /* scroll to form on landing page       ------------------------------------------- */
    $("#btn-mship").click(function (e) {
        e.preventDefault();
        $("html,body").animate({ scrollTop: "720px" }, 2000, "easeOutQuint");
    });

    /* Add loading indicator to save button & disable mulpiple form posts     --------- */
    $('.btn-save', $('#content')).bind('click', function (e) {
        if (!App.panels.formHasSubmitted) {
            $(this).addClass('loading').addClass('btn-disabled-big');
            App.panels.formHasSubmitted = true;
        } else {
            e.preventDefault();
        }
    });

    /* Tooltip                              ------------------------------------------- */
    $(".more-info").hover(function (e) {
        e.preventDefault();
        $(".tooltip").fadeToggle();
    }).click(function (e) {
        e.preventDefault();
    });

    /* Ajax function                        ------------------------------------------- */
    function GetMemberDetails(swipeNo, dateOfBirth, postcode) {
        //Perform an AJAX call to populate member details
        var params = {
            ajax: 1,
            cmd: 'GetMemberDetails',
            swipeNo: swipeNo,
            dateOfBirth: dateOfBirth,
            postcode: postcode
        };

        $.get(thisUrl, params, function (data) {
            if (data.length !== 4) {
                var obj = $.parseJSON(data.toString());
                App.panels.panel2Data = obj;
                $('.returnedData').attr('value', data);
                App.panels.showPanel2();
            } else {
                $('.error-msg-wrap', $('.panel-one')).fadeIn();
                $('.btn-submit', $('.panel-one')).removeClass('loading');
            }
        });
    }

} (jQuery));


