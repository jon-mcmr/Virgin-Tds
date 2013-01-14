(function ($) {
    /* Validation Class -------------------------------------------
     * USAGE: -----------------------------------------------------
     * Step1: initiate class: Validation(formContainer, errorWrapClass, selectionlist);
     *        example: var validation = new Validation($('.current-panel', $('#content')), '.form-wrap', 'input,select');
     * Step2: Call validation.validate() to return true or false - use alongside click or submit event
     *        example $('.btn-submit').bind('click', function (e) {
     *                   e.preventDefault();
     *                   if (validation.validate()) {
     *                       // submit the form
     *                   }
     * Step3: add css classes to fields that need validation
     *        example:   required field  :    class="validation-required"
     *                   email field     :    class="validate-email"
     *                   postcode field  :    class="validate-postcode"
     *                   membership field:    class="validate-membership"
     *                   telephone field :    class="validate-telephone"
     *  Step4: add custom error message to fields
     *        example:   required field  :   data-placeholder="Please enter a membership number"
     *                   any other field :   data-validate-msg="Please enter a valid membership number"
     *  Step5: style error message if needed:
     *        -  style error styles to .validation-error
     *        -  error message are appended to the css class element (2nd variable) passed into the Validation class,
     *           eg: <div class="error-msg"><p>Error message text</p></div>
     *
     *  Exceptions:
     *  Group form elements:        -if validation has to be performed on a group such as Date of Birth
     *                              ->attach class .validation-group to ensure that group is validated as a whole
     *  Alternate empty values:     -if alternate value should qualify as empty/'' use data-validate-exception="Please select an option"
     *
     * */

    function Validation(formContainer, errorWrapClass, selectionlist) {
        this.errorDiv ='<div class="error-msg">&nbsp;</div>';
        this.hasSubmitted = false;
        this.isValid = false;
        // regular expressions for validation
        this.emailRegex = /^([0-9a-zA-Z"](["-.+\w]*[0-9a-zA-Z"])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.?)+[a-zA-Z]{1,9})$/;
        this.postcodeRegex = /^$|[A-Z]{1,2}[0-9][0-9A-Z]? ?[0-9][A-BD-HJLNP-UW-Z]{2}$/;
        this.membershipRegex = /^[0-9a-zA-Z][0-9a-zA-Z ]{0,10}$/;
        this.telephoneRegex = /^$|\(?0( *\d\)?){9,10}$/;
        this.formContainer = formContainer;
        this.errorWrapClass = errorWrapClass;
        this.selectionlist = selectionlist;
        this.init();
    }

    Validation.prototype.init = function () {
        var that = this;
        this.formContainer.find(this.selectionlist).focus( function(){
            var currentFocusElement = $(this);
            var isSelect = currentFocusElement.is("select");

            if (isSelect) {
                currentFocusElement.bind('change', function (e) {
                    setTimeout(function () {
                        if (currentFocusElement.attr('value') !== "") {
                            that.update(that, false, currentFocusElement);
                        }
                    }, 200);
                });
            } else {
                currentFocusElement.bind('blur', function (e) {
                        if ( !currentFocusElement.hasClass('validation-required') ) {
                            currentFocusElement.closest(that.errorWrapClass).removeClass('validation-error').find('.error-msg').remove();
                        }
                        setTimeout(function () {
                        if (currentFocusElement.attr('value') !== "") {
                            that.update(that, false, currentFocusElement);
                        }
                    }, 200);
                });
            }
        });
    }

    Validation.prototype.validate = function () {
        var that = this;
        that.update(that, true);
        if (!this.isValid) {
            return false;
        } else {
            return true;
        }
    }

    Validation.prototype.clearErrorMsg = function (targetClass) {
        var that = this;
        this.formContainer.find(targetClass).each(function (i) {
            $(this).closest(that.errorWrapClass).removeClass('validation-error').find('.error-msg').remove();
        });
    }

    Validation.prototype.update = function (that, validateAll, singleElement) {
        var validateElements = this.formContainer.find(this.selectionlist);
        var updateValid = true;
        if (validateAll) {
            that.clearErrorMsg('.validation-required');
        } else {
            var isGroup = singleElement.hasClass('validation-group');
            if (isGroup) {
                singleElement.addClass('isFocus');
                singleElement.closest(that.errorWrapClass).removeClass('validation-error').find('.error-msg').remove();
                validateElements = singleElement.closest(that.errorWrapClass).find('.validation-group.isFocus');
            } else {
                validateElements = singleElement;
            }
        }
        validateElements.each(function (i) {
            var currentValue = $(this).attr('value');

            if ($(this).hasClass('validate-membership')) {
                updateValid = that.validateExpression('.validate-membership', that.membershipRegex, that);
            }

            if ($(this).hasClass('validate-postcode')) {
                updateValid = that.validateExpression('.validate-postcode', that.postcodeRegex, that);
            }

            if ($(this).hasClass('validate-telephone')) {
                if ($(this).attr('value') !== '') {
                    updateValid = that.validateExpression('.validate-telephone', that.telephoneRegex, that);
                }
            }

            if ($(this).hasClass('validate-email')) {
                updateValid = that.validateExpression('.validate-email', that.emailRegex, that);
            }

            if ($(this).hasClass('validation-required')) {
                if (($(this).attr('value') === '') || ($(this).attr('value') === $(this).attr('data-validate-exception'))) {
                    if ($(this).closest(that.errorWrapClass).find('.error-msg').length === 0) {
                        $(this).closest(that.errorWrapClass).addClass('validation-error').append(that.errorDiv).find('.error-msg').html('<p>' + $(this).attr('data-placeholder') + '</p>');
                    } else {
                        $(this).closest(that.errorWrapClass).addClass('validation-error').find('.error-msg').html('<p>' + $(this).attr('data-placeholder') + '</p>');
                    }
                    updateValid = false;
                }
            }

        });
        that.isValid = updateValid;
    }

    Validation.prototype.validateExpression = function (targetClass, targetRegex, that) {
        that.clearErrorMsg(targetClass);
        var isValid = false;
        this.formContainer.find(targetClass).each(function (i) {
            if (!that.isValidExpression($(this).attr('value'), targetRegex)) {
                $(this).closest(that.errorWrapClass).addClass('validation-error').append(that.errorDiv).find('.error-msg').html('<p>' + $(this).attr('data-validate-msg') + '</p>');
            } else {
                if (that.formContainer.find('.validation-error').length !==0) {
                    isValid = false;
                } else {
                    isValid = true;
                }
            }
        });
        return isValid;
    }

    Validation.prototype.isValidExpression = function (string, regexString) {
        if (regexString === this.postcodeRegex) {
            string = string.toUpperCase();
        }
        var re = regexString;
        return re.test(string);
    }

    Validation.prototype.setRequiredGroupValidationEvent = function (targetClassName) {
        var that = this;
        var targetElements = $(targetClassName);
        targetElements.addClass('validation-required');
        targetElements.change( that.setRequiredGroupValidationEventHandler(that, targetClassName) );
        that.setRequiredGroupValidation('.validation-contact-group');
    }

    Validation.prototype.setRequiredGroupValidationEventHandler = function (that, targetClassName) {
        if ( !that.isRequiredGroupValidationEmpty(targetClassName) ) {
            that.removeRequiredGroupValidation(targetClassName);
        } else if ( that.isRequiredGroupValidationEmpty(targetClassName) ){
            that.setRequiredGroupValidation(targetClassName);
        }
    }

    Validation.prototype.setRequiredGroupValidation = function (targetClassName) {
        var that = this;
        var targetElements = $(targetClassName);
        var checked = $('.checkbox-email input', ('#contact')).attr('checked');
        if ( (checked == 'checked') && (that.isRequiredGroupValidationEmpty(targetClassName)) ){
                targetElements.addClass('validation-required');
        }
        targetElements.change( function() {
            if ( !that.isRequiredGroupValidationEmpty(targetClassName) ) {
                that.removeRequiredGroupValidation(targetClassName);
            } else if ( that.isRequiredGroupValidationEmpty(targetClassName) ){
                that.setRequiredGroupValidation(targetClassName);
            }
        });
    }

    Validation.prototype.isRequiredGroupValidationEmpty = function (targetClassName) {
        var targetElements = $(targetClassName);
        var isEmpty = true;
        targetElements.each(function (i) {
            if ($(this).attr('value') !== '') {
                isEmpty = false;
            }
        });
        return isEmpty;
    }

    Validation.prototype.removeRequiredGroupValidation = function (targetClassName) {
        var targetElements = $(targetClassName);
        targetElements.removeClass('validation-required');
        targetElements.closest(this.errorWrapClass).removeClass('validation-error').find('.error-msg').remove();
    }

    window.Validation = Validation;
    /* Validation Class ends ------------------------------------------- */
} (jQuery));