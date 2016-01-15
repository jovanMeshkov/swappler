

$(document).ready(function() {

    var $swapItemChoosePhoto = $("#swap-item-choose-photo").eq(0);
    var $swapItemPhotoInput = $("#swap-item-photo-input").eq(0);
    var $swapItemChoosenPhoto = $("#swap-item-choosen-photo").eq(0);
    var $btnSwapItemPublish = $("#btn-swap-item-publish").eq(0);

    $swapItemChoosePhoto.on("click", function (event) {
        $swapItemPhotoInput.trigger("click");
    });
    
    $swapItemPhotoInput.change(function (event) {
        if ($swapItemPhotoInput.get(0).files[0] != null) {

            var file = $swapItemPhotoInput.get(0).files[0];
            var reader = new FileReader();

            reader.onload = function (readerEvent) {
                $swapItemChoosenPhoto.attr("src", reader.result);
            };

            reader.readAsDataURL(file);
        }

    });

    $btnSwapItemPublish.on("click", function (event) {
        formSwapItemPublish = $("#form-swap-item-publish")[0];

        var formSwapItemPublishData = new FormData(formSwapItemPublish);

        $btnSwapItemPublish.toggleClass("spin");

        $.ajax({
            method: "POST",
            url: "/Home/PublishSwapItem",
            data: formSwapItemPublishData,
            processData: false,
            contentType: false,
            success: function (result, status, xhr) {
                $btnSwapItemPublish.toggleClass("spin");

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
            error: function (xhr, status, error) {
                $btnSwapItemPublish.toggleClass("spin");

                if (result.Error) {
                    loginStatus(MessageTypes.Error, error);
                }
            }
        });
    });
});