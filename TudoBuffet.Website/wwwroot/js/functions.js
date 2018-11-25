(function ($, window, document, undefined) {

    $(function () {
        $("#header").load("header.html");
        $("#footer").load("footer.html");
    });

})(jQuery, window, document);

function ShowMessage(message, status) {
    if (status >= 500) {
        window.location = "404.html";
    }

    $('#message-body')[0].innerText = message;
    $('#message-modal').modal();
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}