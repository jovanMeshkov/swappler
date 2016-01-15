

$(document).ready(function() {

    function loginStatus(messageType, statusMessage) {

        var $loginStatus = $(".login-status").eq(0);

        $loginStatus.removeClass("alert-warning");
        $loginStatus.removeClass("alert-error");
        $loginStatus.removeClass("alert-info");
        $loginStatus.removeClass("alert-success");

        if (messageType === MessageTypes.Success) {
            $loginStatus.addClass("alert-success");
        }
        else
        if (messageType === MessageTypes.Info) {
            $loginStatus.addClass("alert-info");
        }
        else
        if (messageType === MessageTypes.Warning) {
            $loginStatus.addClass("alert-warning");
        }
        else
        if (messageType === MessageTypes.Error) {
            $loginStatus.addClass("alert-danger");
        }

        $loginStatus.text(statusMessage);
    }

    $buttonLogin = $("#btn-login");

    $buttonLogin.on("click", function(event) {
        loginStatus(null, "");

        var formLogin = $("#form-login")[0];
        var formLoginData = new FormData(formLogin);
        
        $buttonLogin.toggleClass("spin");

        $.ajax({
            method: "POST",
            url: "/Login",
            data: formLoginData,
            processData: false,
            contentType: false,
            success: function(result, status, xhr) {
                $buttonLogin.toggleClass("spin");

                if (result.Redirect) {
                    window.location.replace(result.RedirectTo);
                }

                if (result.Error) {
                    loginStatus(MessageTypes.Error, result.ErrorMessage);
                }

                if (result.Warning) {
                    loginStatus(MessageTypes.Warning, result.WarningMessage);
                }

                if (result.Info) {
                    loginStatus(MessageTypes.Info, result.InfoMessage);
                }

                if (result.Success) {
                    loginStatus(MessageTypes.Success, result.SuccessMessage);
                }
            },
            error: function(xhr, status, error) {
                $buttonLogin.toggleClass("spin");

                if (result.Error) {
                    loginStatus(MessageTypes.Error, error);
                }
            }
        });
    });

});