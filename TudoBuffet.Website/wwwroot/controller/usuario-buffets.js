
$(document).ready(function () {
    var token, id;

    token = window.localStorage.getItem('token');
    id = window.localStorage.getItem('id');

    if (token === undefined) {
        window.location = "/";
    }
});

function Ad(item) {
    this.Id = ko.observable(item.id);
    this.Name = ko.observable(item.name);
    this.ActivedAt = ko.observable(item.activedAt);
    this.Plan = ko.observable(item.plan);
    this.Status = ko.observable(item.status);
}

function AdsViewModel() {

    this.ads = ko.observableArray([]);
    this.displayMessage: ko.observable(false);

    $.getJSON("/api/buffets/" + id, function (payload) {
        var adsFound = $.map(payload, function (item) { return new Ad(item) });

        if (adsFound.lenght == 0)
            displayMessage = true;
        else
            this.ads(adsFound);
    });
}

ko.applyBindings(new AdsViewModel());