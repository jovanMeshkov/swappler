var statusMessageClassName = ".status-message";

var MessageTypes = Object.freeze({
    Error: 0,
    Warning: 1,
    Info: 2,
    Success: 3
});

var RegexPattern = Object.freeze({
    Email: /.*@.*\..*/,
    Username: /^[a-zA-Z][a-zA-Z0-9]*[!@#$%^&*()_+-/.:]?[a-zA-Z0-9]+$/,
    Password: /^.*$/
});

$.fn.extend({
    
    // Status Message Method
    statusMessage: function (messageType, message) {

        $fieldParent = $(this).parent();
        $statusMessage = $(this).siblings(statusMessageClassName).eq(0);

        $fieldParent.removeClass("has-error");
        $fieldParent.removeClass("has-warning");
        $fieldParent.removeClass("has-success");

        if (messageType === MessageTypes.Error) {
            $fieldParent.addClass("has-error");
        }
        else
            if (messageType === MessageTypes.Warning) {
                $fieldParent.addClass("has-warning");
            }
            else
                if (messageType === MessageTypes.Success) {
                    $fieldParent.addClass("has-success");
                }

        $statusMessage.text(message);

    }

});

$(document).ready(function () {
  

    function signUpStatus(messageType, statusMessage) {

        var $signUpStatus = $(".sign-up-status").eq(0);
        
        $signUpStatus.removeClass("alert-warning");
        $signUpStatus.removeClass("alert-error");
        $signUpStatus.removeClass("alert-info");
        $signUpStatus.removeClass("alert-success");

        if (messageType === MessageTypes.Success) {    
            $signUpStatus.addClass("alert-success");
        }
        else
        if (messageType === MessageTypes.Info) {
            $signUpStatus.addClass("alert-info");
        }
        else
        if (messageType === MessageTypes.Warning) {
            $signUpStatus.addClass("alert-warning");
        }
        else
        if (messageType === MessageTypes.Error) {
            $signUpStatus.addClass("alert-danger");
        }

        $signUpStatus.text(statusMessage);
    }

   
    // Registration Form validation setup (rules, messages..)
    $("#form-register").validate({
        ignore: [],
        rules: {
            "FirstName": {
                required: true,
                minlength: 2,
                maxlength: 10
            },
            "LastName": {
                required: true,
                minlength: 2,
                maxlength: 10
            },
            "Username": {
                required: true,
                minlength: 2,
                maxlength: 25,
                pattern: RegexPattern.Username
            },
            "Email": {
                required: true,
                minlength: 3,
                maxlength: 253,
                pattern: RegexPattern.Email
            },
            "Password": {
                required: true,
                minlength: 3,
                maxlength: 100,
                pattern: RegexPattern.Password
            },
            "PasswordConfirmation": {
                required: true,
                equalTo: "#password"
            },
            "TermsAndConditions": {
                required: true,
                pattern: /^1$/
            }
        },
        messages: {
            "FirstName": {
                required: "First name is required!",
                minlength: "First name must be between 2 - 10 characters long.",
                maxlength: "First name must be between 2 - 10 characters long."
            },
            "LastName": {
                required:  "Last name is required!",
                minlength: "Last name must be between 2 - 10 characters long.",
                maxlength: "Last name must be between 2 - 10 characters long."
            },
            "Username": {
                required: "Username is required!",
                minlength: "Username must be between 2 - 25 characters long.",
                maxlength: "Username must be between 2 - 25 characters long.",
                pattern: "Username can start only with letter, can include numbers and one sign."
            },
            "Email": {
                required: "Email is required",
                minlength: "Email must be between 3 - 253 characters long.", 
                maxlength: "Email must be between 3 - 253 characters long.",
                pattern: "Please enter a valid email."
            },
            "Password": {
                required: "Password is required!",
                minlength: "Password must be between 3 - 100 characters long.",
                maxlength: "Password must be between 3 - 100 characters long.",
                pattern: "Please enter valid password!"
            },
            "PasswordConfirmation": {
                required: "Password confirmation is required!",
                equalTo: "Passwords don't match!"
            },
            "TermsAndConditions": {
                required: "Terms and Conditions must be agreed!",
                pattern: "Terms and Conditions must be agreed!"
            }
        },
        onkeyup: function (element) {
            $(element).statusMessage(null, "");
            $(element).valid();
        },
        onfocusout: function (element) {
            $(element).statusMessage(null, "");
            $(element).valid();
        },
        showErrors: function (errorMap, errorList) {
            $(errorList).each(function (i, validationObject) {
                $(validationObject.element).statusMessage(MessageTypes.Error, validationObject.message);
            });
        },
        submitHandler: function (form) {
            
        }
    });
    
    var $buttonSignUp = $("#signup");
    
    $buttonSignUp.on("click", function () {
        signUpStatus(null, "");
        var formRegister = $("#form-register")[0];
        var formRegisterData = new FormData(formRegister);

        if ($(formRegister).valid()) {
            $buttonSignUp.toggleClass("spin");

            $.ajax({
                method: "POST",
                url: "/Register",
                data: formRegisterData,
                processData: false,
                contentType: false,
                success: function (result, status, xhr) {
                    $buttonSignUp.toggleClass("spin");
                    
                    if (result.Error) {
                        signUpStatus(MessageTypes.Error, result.ErrorMessage);
                    }
                    
                    if (result.Warning) {
                        signUpStatus(MessageTypes.Warning, result.WarningMessage);
                    }
                    
                    if (result.Info) {
                        signUpStatus(MessageTypes.Info, result.InfoMessage);
                    }
                    
                    if (result.Success) {
                        signUpStatus(MessageTypes.Success, result.SuccessMessage);
                    }
                },
                error: function (xhr, status, error) {
                    $buttonSignUp.toggleClass("spin");

                    if (result.Error) {
                        signUpStatus(MessageTypes.Error, error);
                    }
                }
            });
        }
    });

    $("#btn-agree-t-and-c").on("click", function () {
        $("#terms-and-conditions").statusMessage(null, "");

        $("#terms-and-conditions").eq(0).prop("value", 1);
        
        $(this).removeClass("btn-primary");
        $(this).addClass("btn-success");
        $(this).disabled = true;
    });

});