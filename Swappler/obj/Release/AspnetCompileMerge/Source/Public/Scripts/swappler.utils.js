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

        var $fieldParent = $(this).parent();
        var $statusMessage = $(this).siblings(statusMessageClassName).eq(0);

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

