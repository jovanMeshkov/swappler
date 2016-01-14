
$(document).ready(function() {

	$listSwapItems = $("#list-swap-items").eq(0);

	$("#btn-search-submit").on("click", function (event) {
		var partOfNameSearched = $("#area-input-search").val();
		$.ajax({
			method: "GET",
			url: "/Home/SearchSwapItems?partOfSwapItemName=" + partOfNameSearched,
			processData: false,
			contentType: false,
			success: function (result, status, xhr) {
					$listSwapItems.html(result);
			},
			error: function (xhr, status, error) {
				$listSwapItems.html('<div><h1> Sorry :( No results were found! </h1></div>');
			}
		});
	});

	$(".swap-item-icon").on("click", function () {
		window.open($(this).attr("src"), "_blank");
	});

	$(".create-swap-request").on("click", function (event) {
		var guid = $(this).parent().data("id") + "";
		window.location.replace("/Home/CreateSwapRequest?requestedSwapItemGuid=" + guid)
	});

});