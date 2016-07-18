



$(document).ready(function() {
    var urlPathName = window.location.pathname;
    if (urlPathName == "/" || urlPathName == "/Home/Index") {
        $("#search-form").show();
    }
    else {
        $("#search-form").hide();
    }
});