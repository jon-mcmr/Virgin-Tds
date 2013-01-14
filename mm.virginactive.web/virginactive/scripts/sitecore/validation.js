
Array.prototype.unique =
  function () {
      var a = [];
      var l = this.length;
      for (var i = 0; i < l; i++) {
          for (var j = i + 1; j < l; j++) {
              if (this[i] === this[j])
                  j = ++i;
          }
          a.push(this[i]);
      }
      return a;
  };

//  Fix issue with club selector and ASPNET validators: redeclare MS validator code with fix
  function ValidatorOnChange(event) {      
    if (!event) {
        event = window.event;
    }
    Page_InvalidControlToBeFocused = null;
    var targetedControl;
    if ((typeof (event.srcElement) != "undefined") && (event.srcElement != null)) {
        targetedControl = event.srcElement;
    }
    else {
        targetedControl = event.target;
    }
    var vals;
    if (typeof (targetedControl.Validators) != "undefined") {
        vals = targetedControl.Validators;
    }
    else {
        if (targetedControl.tagName.toLowerCase() == "label") {
            targetedControl = document.getElementById(targetedControl.htmlFor);
            vals = targetedControl.Validators;
        }
    }
    var i;
    if (vals) {
        for (i = 0; i < vals.length; i++) {
            ValidatorValidate(vals[i], null, event);
        }
    }
    ValidatorUpdateIsValid();
}


function ValidatorUpdateIsValid() {
    clearAllValidationErrors();
    //create an array of all controls to be validated
    var myControlsToValidate = new Array();
    $(Page_Validators).each(function (i) {
        var controlToValidate = document.getElementById(Page_Validators[i].controltovalidate);
        if (controlToValidate != null) {
            var controlExists = false;
            for (x in myControlsToValidate) {
                if (myControlsToValidate[x] == controlToValidate.id) {
                    controlExists = true;
                    break;
                }
            }
            if (controlExists == false) {
                myControlsToValidate.push(controlToValidate.id);
            }
        }
    });

    //Clear all validation class styling
    for (x in myControlsToValidate) {
        var controlToValidate = document.getElementById(myControlsToValidate[x]);
        if (controlToValidate != null) {
            controlToValidate.className = controlToValidate.className.toString().replace(" error", "");
            var parentWrap = controlToValidate.parentNode;

            if (parentWrap != null) {
                parentWrap.className = parentWrap.className.toString().replace(" valid", "")
            }

            //remove error class on 'form p' controls
            if (controlToValidate.className.indexOf("chzn-clublist") != -1) {
                //remove class="error" on select control (for multiple select control)
                var arr1 = document.getElementsByTagName("input");
                for (i = 0; i < arr1.length; i++) {
                    //find 'form p' input control
                    if (arr1[i].className == "default error") {
                        arr1[i].className = arr1[i].className.toString().replace(" error", "");
                    }
                }
                //remove class="error" on chosen option (for single select control)
                var arr2 = document.getElementsByTagName("a");
                for (i = 0; i < arr2.length; i++) {
                    //find 'form p' chosen option
                    if (arr2[i].className == "chzn-single error") {
                        arr2[i].className = arr2[i].className.toString().replace(" error", "");
                    }
                }
            }
            //remove error class on error panel for reciprocal access
            if (parentWrap.className.indexOf("select-panel") != -1) {
                //remove class="error" on error panel
                var errorPanel = document.getElementById("error-panel");
                errorPanel.className = errorPanel.className.toString().replace("error", "");
                //remove class="error-wrap" on error-wrap
                var errorWrap = document.getElementById("error-wrap");
                errorWrap.className = errorWrap.className.toString().replace("error-wrap", "");
                //remove inner html from custom service error-message
                var errorMessage = document.getElementById("error-message");
                errorMessage.innerHTML = "";
            }

        }
    };

    //now loop through each control to validate again
    for (x in myControlsToValidate) {
        var controlToValidate = document.getElementById(myControlsToValidate[x]);
        if (controlToValidate != null) {
            var controlValid = true;
            //now loop through each validator to see which ones linked to control
            for (i = 0; i < Page_Validators.length; i++) {
                var control = document.getElementById(Page_Validators[i].controltovalidate);
                if (control == controlToValidate) {
                    if (!Page_Validators[i].isvalid) {
                        controlValid = false;
                    }
                }
            };
            //now assign relevant class names
            if (controlValid == false) {
                //assign class="error" on control itself and on parent element
                if (controlToValidate.className.indexOf(" error") == -1) {
                    controlToValidate.className = controlToValidate.className + " error";

                    if (controlToValidate.tagName == "INPUT") {
                        if (controlToValidate.parentNode.tagName == "DIV") {
                            addClass(controlToValidate.parentNode, "wrapError");
                        }
                    }
                    //If <select> has class of error
                    if (controlToValidate.tagName == "SELECT" && hasClass(controlToValidate, "error")) {
                        if (hasClass(controlToValidate.parentNode, "select-row")) {
                            addClass(controlToValidate.parentNode, "select-row-error")
                        } else {
                            //If member enquiry form
                            if (hasClass(controlToValidate.parentNode, "wrap-enquiry-member")) {
                                //Don't add error class on ie, as ie doesn't clear this class once field is valid
                                if (!hasClass(controlToValidate, "chzn-ie")) {
                                    addClass(controlToValidate.parentNode, "wrapErrorFullWidth")
                                }
                                removeClass(controlToValidate.parentNode.parentNode, "valid");
                            //Else if main contact form
                            } else if (hasClass(controlToValidate.parentNode.parentNode, "msg-select")) {
                                //Don't add error class on ie, as ie doesn't clear this class once field is valid
                                if (!hasClass(controlToValidate, "chzn-ie")) {
                                    //addClass(controlToValidate.parentNode, "wrapErrorFullWidth");
                                }
                            } else {
                                addClass(controlToValidate.parentNode, "wrapError");
                            }
                        }
                    }
                }
                
                //set error class to 'form p' controls
                if (controlToValidate.className.indexOf("chzn-clublist") != -1) {
                    if (controlToValidate.className.indexOf(" error") != -1) {
                        //assign class="error" on select control
                        var arr1 = document.getElementsByTagName("input");
                        for (i = 0; i < arr1.length; i++) {
                            //find 'form p' input control
                            if (arr1[i].className == "default") {
                                arr1[i].className = arr1[i].className + " error";
                            }
                        }
                        //assign class="error" on chosen option
                        var arr2 = document.getElementsByTagName("a");
                        for (i = 0; i < arr2.length; i++) {
                            //find 'form p' chosen option
                            if (arr2[i].className == "chzn-single") {
                                arr2[i].className = arr2[i].className + " error";
                            }
                        }
                    }
                }
                //add error class on error panel for reciprocal access
                var parentWrap = controlToValidate.parentNode;
                if (parentWrap.className.indexOf("select-panel") != -1) {
                    //add class="error" on error panel
                    var errorPanel = document.getElementById("error-panel");
                    if (errorPanel.className.indexOf("error") == -1) {
                        errorPanel.className = errorPanel.className + "error";
                    }
                    //add class="error-wrap" on error-wrap
                    var errorWrap = document.getElementById("error-wrap");
                    if (errorWrap.className.indexOf("error-wrap") == -1) {
                        errorWrap.className = errorWrap.className + "error-wrap";
                    }
                }
            }
            else {
                //check if empty field -user is yet to fill out so do not show valid sign
                if (controlToValidate.value != "" && controlToValidate.value != "Select") {
                    //assign class="valid" on parent wrap
                    var parentWrap = controlToValidate.parentNode;
                    if (parentWrap != null) {
                        if (controlToValidate.tagName == "SELECT") {
                            //If selects are styled
                            if (parentWrap.className == "select-wrap") {
                                /*
                                //Check which form - "select-row" used on corporate enquiry form
                                if (hasClass(parentWrap.parentNode, "select-row")) {  
                                    removeClass(parentWrap.parentNode, "select-row-error")
                                } else {
                                */
                                    addClass(parentWrap.parentNode, "valid");
                                    removeClass(parentWrap.parentNode, "wrapError");
                                //}
                            //Else if select is on member enquiry form
                            } else if (hasClass(parentWrap, "select-row")) {
                                removeClass(parentWrap.parentNode, "select-row-error")
                                //removeClass(parentWrap, "wrapErrorFullWidth");
                                //removeClass(parentWrap, "wrapError");
                            //Else if selects are unstyled
                            } else {
                                addClass(parentWrap.parentNode, "valid");
                                removeClass(parentWrap, "wrapErrorFullWidth");
                                removeClass(parentWrap, "wrapError");
                            }
                        } else if (controlToValidate.tagName == "INPUT") {
                            if (parentWrap.tagName == "DIV") {
                                addClass(parentWrap, "valid");
                                removeClass(parentWrap, "wrapError");
                            }
                        }
                    }
                }
            }
        }
    }

    _errors = _errors.unique();
    Page_IsValid = AllValidatorsValid(Page_Validators);
}

function addClass(el, cls) {
    if (!this.hasClass(el, cls)) el.className += " " + cls;
}

function hasClass(el, cls) {
    return el.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'));
}

function removeClass(el, cls) {
    if (hasClass(el, cls)) {
        el.className = el.className.replace(new RegExp('(\\s|^)' + cls + '(\\s|$)'), ' ').replace(/^\s+|\s+$/g, '');
    }
}

function clearAllValidationErrors() {
    do {
        _errors.pop();
    }
    while (_errors.length > 0)

}

var _errors = new Array();

function registerError(message) {
    _errors.push(message);
}

function forceAndDisplayError(message) {
    clearAllValidationErrors();
    ValidatorUpdateIsValid();
    _errors.push(message);
}

function clubFindSelect_Top_Validate(source, args) {
    ValidatorUpdateIsValid();
}
