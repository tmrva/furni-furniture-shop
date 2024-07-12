$(document).ready(function () {
    $(document).on("click", ".category-button", function (e) {
        e.preventDefault(); // Prevent default anchor behavior
        const categoryName = $(this).data("category"); // Get category name from data attribute
        console.log("Category clicked:", categoryName); // Log the category name

        $.ajax({
            url: "/Shop/Search/",
            type: "get",
            data: {
                key: categoryName // Pass the category name as the key
            },
            success: function (res) {
                console.log("AJAX request succeeded:", res); // Log the response
                $("#products").css("display", "none");
                $("#loadMoreRow").css("display", "none");
                $("#products2").empty();
                $("#products2").append(res); // Append the returned products
            },
            error: function (xhr, status, error) {
                console.error("AJAX request failed: ", status, error); // Log the error
            }
        });
    });
});
