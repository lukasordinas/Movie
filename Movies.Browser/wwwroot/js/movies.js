$("#searchButton").on("click", function () {

    const titleInput = $('#titleInput').val();
    const genreInput = $('#genreInput').val();

    $.ajax({
        traditional: true,
        type: "POST",
        url: "/Movies?handler=Search",
        data: { title: titleInput },
        dataType: "json",
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        }
    })

        .done(function (data) {
            showResults(data);
        });
});

function showResults(data) {
    $("#movieResults").removeClass("d-none");
    $("#movieResults").html(data);
}