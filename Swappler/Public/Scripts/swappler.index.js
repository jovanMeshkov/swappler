
$(document).ready(function () {
    
    //if ($notificationsBody != null) {
    //    $notificationsBody.animateCss("fadeOutUp", "fadeInDown");
    //}

    $btnLoadMoreItems = $("#btn-load-more").eq(0);
    $listSwapItems = $("#list-swap-items").eq(0);

    $btnLoadMoreItems.on("click", function(event) {
        $btnLoadMoreItems.toggleClass("spin");

        $.ajax({
            method: "POST",
            url: "/Home/LoadMoreSwapItems",
            processData: false,
            contentType: false,
            success: function (result, status, xhr) {
                $btnLoadMoreItems.toggleClass("spin");

                $listSwapItems.append(result);
            },
            error: function (xhr, status, error) {
                $btnLoadMoreItems.toggleClass("spin");
            }
        });
    });

    $(".swap-item-icon").on("click", function() {
        window.open($(this).attr("src"), "_blank");
    });

    $(".feed-swap-item .btn-swap-request").on("click", function (event) {
        var guid = $(this).parents(".feed-swap-item").eq(0).attr("data-id");
        window.location.replace("/SwapRequest/Create?requestedSwapItemGuid=" + guid);
    });

    function showNotification(notificationData) {
        var $notificationsBody = $("#notifications-body").eq(0);
        if (notificationData.Type == NotificationTypes.SwapRequest) {
            
        }
    }

});