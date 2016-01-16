
$(document).ready(function() {

    var $photoInput = $("#edit-user-photo-input").eq(0);
    var $photo = $("#edit-user-photo").eq(0);
    var $btnChangePhoto = $("#btn-edit-user-change-photo").eq(0);
    var $btnSaveChanges = $("#btn-edit-user-save-changes").eq(0);
    var $saveChangesStatus = $(".save-changes-status").eq(0);

    $btnChangePhoto.on("click", function(event) {
        $photoInput.trigger("click");
    });

    $photoInput.on("change", function(event) {
        var files = $photoInput.get(0).files;
        if (files != null && files.length === 1) {
            var file = files[0];

            var reader = new FileReader();

            reader.onload = function(readerEvent) {
                $photo.attr("src", reader.result);
            };

            reader.readAsDataURL(file);
        }
    });

    $.validator.addMethod("requiredOnChange", function (value, element, param) {
        if ($("#edit-user-new-password").val() != "" &&
            $("#edit-user-current-password").val() == "") {
            return false;
        }
        return true;
    }, "");



    $("#form-edit-profile").validate({
        ignore: [],
        rules: {
            "FirstName": {
                minlength: 2,
                maxlength: 10
            },
            "LastName": {
                minlength: 2,
                maxlength: 10
            },
            "Username": {
                minlength: 2,
                maxlength: 25,
                pattern: RegexPattern.Username
            },
            "Email": {
                minlength: 3,
                maxlength: 253,
                pattern: RegexPattern.Email
            },
            "CurrentPassword": {
                minlength: 3,
                maxlength: 100,
                pattern: RegexPattern.Password,
                requiredOnChange: true
            },
            "NewPassword": {
                minlength: 3,
                maxlength: 100,
                pattern: RegexPattern.Password
            },
            "PasswordConfirmation": {
                equalTo: "#edit-user-new-password"
            }
        },
        messages: {
            "FirstName": {
                minlength: "First name must be between 2 - 10 characters long.",
                maxlength: "First name must be between 2 - 10 characters long."
            },
            "LastName": {
                minlength: "Last name must be between 2 - 10 characters long.",
                maxlength: "Last name must be between 2 - 10 characters long."
            },
            "Username": {
                minlength: "Username must be between 2 - 25 characters long.",
                maxlength: "Username must be between 2 - 25 characters long.",
                pattern: "Username can start only with letter, can include numbers and one sign."
            },
            "Email": {
                minlength: "Email must be between 3 - 253 characters long.",
                maxlength: "Email must be between 3 - 253 characters long.",
                pattern: "Please enter a valid email."
            },
            "CurrentPassword": {
                requiredOnChange: "Enter your current password to create new password!",
                minlength: "Password must be between 3 - 100 characters long.",
                maxlength: "Password must be between 3 - 100 characters long.",
                pattern: "Please enter valid password!"
            },
            "NewPassword": {
                minlength: "Password must be between 3 - 100 characters long.",
                maxlength: "Password must be between 3 - 100 characters long.",
                pattern: "Please enter valid password!"
            },
            "PasswordConfirmation": {
                required: "Password confirmation is required!",
                equalTo: "Passwords don't match!"
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


    function saveChangesStatus(messageType, statusMessage) {
        
        $saveChangesStatus.removeClass("alert-warning");
        $saveChangesStatus.removeClass("alert-danger");
        $saveChangesStatus.removeClass("alert-info");
        $saveChangesStatus.removeClass("alert-success");

        if (messageType === MessageTypes.Success) {
            $saveChangesStatus.addClass("alert-success");
        }
        else
            if (messageType === MessageTypes.Info) {
                $saveChangesStatus.addClass("alert-info");
            }
            else
                if (messageType === MessageTypes.Warning) {
                    $saveChangesStatus.addClass("alert-warning");
                }
                else
                    if (messageType === MessageTypes.Error) {
                        $saveChangesStatus.addClass("alert-danger");
                    }

        $saveChangesStatus.text(statusMessage);
    }

    $btnSaveChanges.on("click", function (event) {
        var formEditProfile = $("#form-edit-profile")[0];

        if ($(formEditProfile).valid() == false) {
            return;
        }

        var formEditProfileData = new FormData(formEditProfile);

        $btnSaveChanges.toggleClass("spin");

        $.ajax({
            method: "POST",
            url: "/User/UpdateProfile",
            data: formEditProfileData,
            processData: false,
            contentType: false,
            success: function(result, status, xhr) {
                $btnSaveChanges.toggleClass("spin");

                if (result.Redirect) {
                    window.location.replace(result.RedirectTo);
                }

                if (result.Error) {
                    saveChangesStatus(MessageTypes.Error, result.ErrorMessage);
                }

                if (result.Warning) {
                    saveChangesStatus(MessageTypes.Warning, result.WarningMessage);
                }

                if (result.Info) {
                    saveChangesStatus(MessageTypes.Info, result.InfoMessage);
                }

                if (result.Success) {
                    applyChanges();
                    saveChangesStatus(MessageTypes.Success, result.SuccessMessage);
                }
            },
            error: function(xhr, status, error) {
                $btnSaveChanges.toggleClass("spin");

                if (result.Error) {
                    saveChangesStatus(MessageTypes.Error, error);
                }
            }
        });
    });

    function applyChanges() {
        var formEditProfileElements = [
            {
                element: $("#edit-user-first-name"),
                update: true
            },
            {
                element: $("#edit-user-last-name"),
                update: true
            },
            {
                element: $("#edit-user-username"),
                update: true
            },
            {
                element: $("#edit-user-email"),
                update: true
            },
            {
                element: $("#edit-user-current-password"),
                update: false
            },
            {
                element: $("#edit-user-new-password"),
                update: false
            },
            {
                element: $("#edit-user-password-confirmation"),
                update: false
            }
        ];

        $firstName = formEditProfileElements[0].element;

        if ($firstName.val() != null) {
            if ($firstName.val() != "") {
                $("#master-user-profile").text($firstName.val());
            }
        }

        for (var i = 0; i < formEditProfileElements.length;i++) {
            var entry = formEditProfileElements[i];

            if (entry.element.val() != null) {
                if (entry.element.val() != "") {
                    if (entry.update) {
                        entry.element.attr("placeholder", entry.element.val());
                    }
                    entry.element.val("");
                }
            }
        }
    }

});