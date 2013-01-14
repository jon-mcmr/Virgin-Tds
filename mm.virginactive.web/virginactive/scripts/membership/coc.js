var changeCircumstance = function () {
	var triedSubmission = false;
	var submitted = false;

	var tooltip = function () {
		// Tooltip
		$(".more-info").hover(function (e) {
			e.preventDefault();
			$(".tooltip").fadeToggle();
		}).click(function (e) {
			e.preventDefault();
		});
	};

	var validation = function () {

		var validateClick = function () {
			var valid = true, itemname = $('.submit_button').attr('name');

			if (!submitted) {
				valid = validateForm(false);

				if (valid === true) {
					sendAnalytics();
					submitted = triedSubmission = true;
					$('input[type="submit"]').addClass('submitted');

					__doPostBack(itemname, '');
				}
				else {
					scrollTo(0, $('.error').position().top - 50);
					triedSubmission = true;
				}
			}

			return false;
		};

		//Send the google analytics code
		var sendAnalytics = function () {
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
					gaqLabel = $('fieldset').attr('data-name');
				_gaq.push(["_trackEvent", gaqCategory, gaqAction, gaqLabel]);

			}
		};

		var validateForm = function () {

			//Clear old errors
			$('.error-msg').remove();
			$('input, select, .chzn-container').removeClass('error');
			var $formFields = $('fieldset input, fieldset select, fieldset textarea, fieldset .checkbox-group').not(':disabled'), valid = true;

			$formFields.each(function (event) {
				var $this = $(this), validationClass = $this.attr('class') || "", furtherValidation = true, value = $this.val() || "", default_val = $this.attr('data-default') || "";
				//If its a checkbox group handle the validation seperately
				if (validationClass.indexOf("checkbox-group") !== -1) {

					//Check at least one is selected
					if (validationClass.indexOf("one-required") !== -1) {

						if ($this.find('input[type="checkbox"]:checked').length === 0) {
							setError($this, $this.attr('data-required'));
						}

					}
				}
				else {

					//Validate if it is required
					if (validationClass.indexOf("required") !== -1) {
						if ($this.attr('type') === "checkbox") {

							if (validateCheckbox($this) === false) {
								valid = false;
								setError($this, $this.attr('data-required'));

								furtherValidation = false;

							}
						}
						else {
							if (validateRequired(value, default_val) === false) {
								valid = false;
								setError($this, $this.attr('data-required'));
								furtherValidation = false;

							}
						}
					}

					//If has passsed required OR is not required but requires valid value then validate further
					if (furtherValidation) {
						if (validationClass.indexOf("email") !== -1) {
							if (validateEmail(value) === false) {
								valid = false;
								setError($this, $this.attr('data-email'));
							}
						}
						else if (validationClass.indexOf("phone") !== -1) {
							if (validatePhone(value) === false) {
								valid = false;
								setError($this, $this.attr('data-phone'));

							}
						}
						else if (validationClass.indexOf("postcode") !== -1) {
							if (validatePostCode(value) === false) {
								valid = false;
								setError($this, $this.attr('data-postcode'));

							}
						}
						else if (validationClass.indexOf("membership") !== -1) {
							if (validateMembershipNo(value) === false) {
								valid = false;
								setError($this, $this.attr('data-membership'));

							}
						}
					}
				}

			});

			$('#content_0_centre_0_Panel2').off().on('keyup', 'fieldset input[type="text"]', onChangeEvent).on('change','fieldset input[type="checkbox"], fieldset select, fieldset textarea, fieldset .checkbox-group', onChangeEvent);

			return valid;
		};

		var onChangeEvent = function(){
			validateForm();

			return true;
		};

		var setError = function ($this, error) {

			var tag = $this.prop('tagName'),
				group = $this.attr('data-group') || false;
			errorHTML = '<div class="error-msg js-error"><p>' + error + '</p></div>';
			if ($this.next().hasClass('chzn-container')) {
				$this.next().addClass('error');
			}
			else {
				$this.addClass('error');
			}


			if ($this.parents('.terms').length) {
				$this.parents('.terms').append(errorHTML);
			}
			else if ($this.parents('.form-column').length) {
				$this.parents('.form-block').append(errorHTML);

			}
			else if ($this.parents('.form-options').length) {
				$this.parents('.form-options').append(errorHTML);
			}
			else if ($this.parents('.form-block').length) {
				$this.parents('.form-block').append(errorHTML);
			}
			else {
				if ($this.parents('.form-wrap').find("div.error-msg").length === 0) {
					$this.parents('.form-wrap').append(errorHTML);
				}
			}

			if (group !== false) {
				$this.parents('.form-wrap').find('.error-msg').not(':last, .chzn-container').remove();
			}

			$(".accordion_content").each(function () {
				var $this = $(this);

				if ($this.find("div.error-msg.js-error").length) {
					//$(this).show();
					$this.prev().addClass('active');
					$this.show();
				}
			});

		};

		var validateEmail = function (value) {
			var valid = false;

			if (value.replace(" ", "") === "") {
				valid = true;
			}
			else if (value.match(/^([0-9a-zA-Z"](["-.+\w]*[0-9a-zA-Z"])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.?)+[a-zA-Z]{1,9})$/)) {
				valid = true;
			}

			return valid;
		};

		var validateCheckbox = function ($this) {
			return $this.is(':checked');
		};

		var validateRequired = function (value, default_val) {
			var valid = true;

			value = value.split(' ').join('');

			if (value === null || value === "" || value === default_val) {
				valid = false;
			}

			return valid;
		};

		var validatePhone = function (value) {
			var valid = false;

			value = value.split(' ').join('');

			if (value === "") {
				valid = true;
			}
			else if (value.replace(" ", "").match(/^[-]?\d*\.?\d*$/) && value.length > 10) {
				valid = true;
			}

			return valid;
		};

		var validatePostCode = function (value) {
			var valid = false;

			if (value.match(/^$|[A-Z]{1,2}[0-9][0-9A-Z]? ?[0-9][A-BD-HJLNP-UW-Z]{2}$/i)) {
				valid = true;
			}

			return valid;
		};

		var validateMembershipNo = function (value) {
			var valid = false;

			if (value.match(/^[0-9a-zA-Z][0-9a-zA-Z ]{0,10}$/)) {
				valid = true;
			}

			return valid;
		};


		return {
			validate: validateClick,
			validateForm: validateForm
		};
	} ();

	var toggleFields = function () {

		$(".change-membership select").each(function () {
			if ($(this).find("option").first().attr("selected")) {
				$(this).attr('disabled', true).trigger("liszt:updated");
				$(this).parents('.form-options').addClass("disabled");
			}
		});

		$('.change-membership').on('click', 'input[type="radio"]', function () {
			//Clear old errors
			$('.change-membership .error-msg').remove();
			$('.change-membership input, .change-membership select, .change-membership .chzn-container').removeClass('error');

			$(".change-membership select").attr('disabled', true).removeClass('required').trigger("liszt:updated");
			$(".change-membership select").parents('.form-options').addClass("disabled");
			$(this).parents('fieldset').next().removeClass('disabled');
			$(this).parent().next().find('select').addClass('required').attr('disabled', false).trigger("liszt:updated");

			$('.change-membership').find('.cancel-changes').remove();
			$(this).parent().append('<a href="#" class="cancel-changes">Cancel</a>');

			activateResponsePreferences();
		});

		$('.change-membership').on('click', '.cancel-changes', function () {
			$(this).parent().find("input[type='radio']").prop("checked", false);
			$('.change-membership .error-msg').remove();
			$('.change-membership input, .change-membership select, .change-membership .chzn-container').removeClass('error');

			$(".change-membership select").attr('disabled', true).removeClass('required').trigger("liszt:updated");
			$(".change-membership select").parents('.form-options').addClass("disabled");
			$(this).remove();

			if ($('#content_0_centre_0_Panel2FreezeMonthDropDownList').val() === "" && $('#content_0_centre_0_Panel2CancelMonthDropDownList').val() === "") {
				deactivateResponsePreferences();
			}

			return false;
		});



	};

	var responsePreferences = function () {

		$('#response-preferences-container').hide();

		$('.response-pref').on('click', 'input[type="checkbox"]', function () {
			$('#' + $(this).attr('data-requires')).toggleClass('required');
		});
	};

	var activateResponsePreferences = function () {
		$('#response-preferences-container').show();
		$('#response-preferences-container .checkbox-group').addClass('one-required');
		$('#content_0_centre_0_Panel2PreferencesDateTimeTextBox').addClass('required');
	};

	var deactivateResponsePreferences = function () {
		$('#response-preferences-container').hide();
		$('#response-preferences-container .checkbox-group').removeClass('one-required');
		$('#content_0_centre_0_Panel2PreferencesDateTimeTextBox').removeClass('required');
	};

	var membershipOnHold = function () {

		$('#content_0_centre_0_Panel2FreezeMonthDropDownList').change(function () {
			if ($('#content_0_centre_0_Panel2FreezeMonthDropDownList').val() !== "") {
				$('#content_0_centre_0_Panel2FreezeDurationDropDownList').addClass('required');
				activateResponsePreferences();
			}
			else {
				$('#content_0_centre_0_Panel2FreezeDurationDropDownList').removeClass('required');
			}

		});

		$('#content_0_centre_0_Panel2CancelMonthDropDownList').change(function () {
			if ($('#content_0_centre_0_Panel2CancelMonthDropDownList').val() !== "") {
				activateResponsePreferences();
			}

		});
	};

	var accordion = function () {
		$('.accordion_title a').on('click', function () {
			$(this).parent().toggleClass('active');
			$(this).parent().next().slideToggle();
			return false;
		}).parent().next().hide();
	};

	var openCorrectSections = function () {
		if ($('[name="content_0$centre_0$changeSelector"]:checked').length) {
			activateResponsePreferences();
			$('[name="content_0$centre_0$changeSelector"]:checked').parents('.accordion_content').show().prev().addClass('active');
		}

		if ($('#content_0_centre_0_Panel2FreezeMonthDropDownList').val() !== "") {
			$('#content_0_centre_0_Panel2FreezeDurationDropDownList').addClass('required');
			activateResponsePreferences();
			$('#content_0_centre_0_Panel2FreezeMonthDropDownList').parents('.accordion_content').show().prev().addClass('active');
		}

		if ($('#content_0_centre_0_Panel2CancelMonthDropDownList').val() !== "") {
			activateResponsePreferences();
			$('#content_0_centre_0_Panel2CancelMonthDropDownList').parents('.accordion_content').show().prev().addClass('active');
		}
	};

	return {
		init: function () {
			tooltip();

			toggleFields();
			responsePreferences();
			accordion();
			openCorrectSections();

			membershipOnHold();

			if (typeof Sys !== 'undefined') {

				var prm = Sys.WebForms.PageRequestManager.getInstance();

				prm.add_endRequest(function () {
					$('select').chosen();

					$('[name="content_0$centre_0$changeSelector"]:checked').click();


					if ($('[name="content_0$centre_0$changeSelector"]:checked').length && triedSubmission === true) {
						validation.validateForm();
					}
				});
			}

		},
		saveDetails: function(){
			if($('#content_0_centre_0_Panel3SubmitButton').attr('data-clicked') !== "true"){
				$('#content_0_centre_0_Panel3SubmitButton').addClass('submitted');
				$('#content_0_centre_0_Panel3SubmitButton').attr('data-clicked', 'true');
			}
		},
		validate: function(){
			return validation.validate();
		}
	};

} ();

(function ($) {
	changeCircumstance.init();
	

} (jQuery));


