(function ($, window, document, undefined) {

    $(function () {
        $("#header").load("header.html");
        $("#footer").load("footer.html");
    });

})(jQuery, window, document);

function ShowMessage(message, status) {
    if (status >= 500) {
        window.location = "error.html";
    }

    $('#message-body')[0].innerText = message;
    $('#message-modal').modal();
}