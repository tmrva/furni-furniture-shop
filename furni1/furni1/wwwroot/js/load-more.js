let skipCount = 8;
let productsCount = $("#loadMore").next().val()
$(document).on("click", "#loadMore", function () {
    $.ajax({
        url: "/Shop/LoadMore/",
        type: "get",
        data: {
            "skip": skipCount
        },
        success: function (res) {
            $("#products").append(res);
            if (res = "ok") {

            }
            skipCount += 8;
            if (productsCount <= skipCount) {
                $("#loadMore").remove()
            }
        }
    });
});