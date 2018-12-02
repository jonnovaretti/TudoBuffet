function Photo(data) {
    this.name = data.name;
    this.deleteUrl = data.deleteUrl;
    this.thumbnailUrl = data.thumbnailUrl;
    this.type = data.type;
    this.url = data.url;
    this.size = data.size;
    this.id = data.id;
    this.functionDelete = "javascript:callApi('" + data.deleteUrl + "', '" + data.id +"')"
}

function PhotosViewModel() {
    var self = this;

    self.photos = ko.observableArray([]);
    self.token = ko.observable(window.sessionStorage.getItem('token'));
    self.params = getUrlVars();

    $.ajax("/api/admin/fotos?buffetId=" + self.params['buffetId'], {
        type: "get",
        contentType: "application/json",
        headers: { 'Authorization': 'Bearer ' + self.token() },
        success: function (payload) {
            var photosFound = $.map(payload, function (item) { return new Photo(item) });
            self.photos(photosFound);

            self.photos()[1].name = 'zzz';
        },
        error: function (result) {
            ShowMessage(result.responseText, result.status);
        }
    });
}

ko.applyBindings(new PhotosViewModel());

$(function () {
    'use strict';

    var token = window.sessionStorage.getItem('token')
    var params = getUrlVars();

    var url = 'api/admin/fotos?buffetId=' + params['buffetId'],
        uploadButton = $('<button/>')
            .addClass('btn btn-primary')
            .prop('disabled', true)
            .text('Processing...')
            .on('click', function () {
                var $this = $(this),
                    data = $this.data();
                $this
                    .off('click')
                    .text('Abort')
                    .on('click', function () {
                        $this.remove();
                        data.abort();
                    });
                data.submit().always(function () {
                    $this.remove();
                });
            });
    $('#fileupload').fileupload({
        url: url,
        dataType: 'json',
        autoUpload: false,
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
        maxFileSize: 4000000,
        disableImageResize: /Android(?!.*Chrome)|Opera/.test(window.navigator.userAgent),
        previewMaxWidth: 100,
        previewMaxHeight: 100,
        previewCrop: true,
        beforeSend: function (xhr, data) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + token);
        },
    }).on('fileuploadadd', function (e, data) {
        data.context = $('<div/>').appendTo('#files');
        $.each(data.files, function (index, file) {
            var node = $('<p/>').append($('<span/>').text(file.name));
            if (!index) {
                node.append('<br>').append(uploadButton.clone(true).data(data));
            }
            node.appendTo(data.context);
        });
    }).on('fileuploadprocessalways', function (e, data) {
        var index = data.index,
            file = data.files[index],
            node = $(data.context.children()[index]);
        if (file.preview) {
            node
                .prepend('<br>')
                .prepend(file.preview);
        }
        if (file.error) {
            node
                .append('<br>')
                .append($('<span class="text-danger"/>').text(file.error));
        }
        if (index + 1 === data.files.length) {
            data.context.find('button')
                .text('Upload')
                .prop('disabled', !!data.files.error);
        }
    }).on('fileuploadprogressall', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $('#progress .progress-bar').css('width', progress + '%');

    }).on('fileuploaddone', function (e, data) {
        $.each(data.result.files, function (index, file) {
            if (file.url) {
                var link = $('<a>').attr('target', '_blank').prop('href', file.url);
                var linkDelete = $('<a>').prop('href', "javascript:callApi('" + file.deleteUrl + "', '" + file.id + "')");

                link[0].innerText = 'Visualizar';
                linkDelete[0].innerText = "Excluir";

                $(data.context)[0].id = file.id;
                $(data.context.children()[index].childNodes[2]).append('<br>');
                $(data.context.children()[index].childNodes[2]).append(link);
                $(data.context.children()[index].childNodes[2]).append('<br>');
                $(data.context.children()[index].childNodes[2]).append(linkDelete);
            } else if (file.error) {
                var error = $('<span class="text-danger"/>').text(file.error);
                $(data.context.children()[index])
                    .append('<br>')
                    .append(error);
            }
        });
    }).on('fileuploadfail', function (e, data) {
        $.each(data.files, function (index) {
            $(data.context.children()[index]).detach();
            ShowMessage(data._response.jqXHR.responseText, data._response.jqXHR.status);
        });
    }).prop('disabled', !$.support.fileInput).parent().addClass($.support.fileInput ? undefined : 'disabled');
});

function callApi(url, idDiv) {

    var token = window.sessionStorage.getItem('token');

    document.getElementById(idDiv).outerHTML = '';

    $.ajax(url, {
        type: "delete",
        contentType: "application/json",
        headers: { 'Authorization': 'Bearer ' + token },
        success: function (payload) {
        },
        error: function (result) {
            ShowMessage(result.responseText, result.status);
        }
    });
}