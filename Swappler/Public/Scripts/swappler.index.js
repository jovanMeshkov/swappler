
$(document).ready(function() {

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

    $(".create-swap-request").on("click", function (event) {
        var guid = $(this).parent().data("id") + "";
        window.location.replace("/SwapRequest/Create?requestedSwapItemGuid=" + guid);
    });

    // Handling toggling
    $("#btn-notifications-toggle").on("click", function() {
        var toggle = $(this).data("toggle");
        if (toggle == 0) {
            $(this).removeClass("fa-chevron-down");
            $(this).addClass("fa-chevron-up");
            $(this).data("toggle", 1);
        }
        else {
            $(this).removeClass("fa-chevron-up");
            $(this).addClass("fa-chevron-down");
            $(this).data("toggle", 0);
        }
    });

});