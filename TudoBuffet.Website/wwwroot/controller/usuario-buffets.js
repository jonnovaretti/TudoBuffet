function PurchasedPlan(item) {
    this.id = ko.observable(item.id);
    this.name = ko.observable(item.name);
    this.activedAt = ko.observable(item.activedAt);
    this.namePlan = ko.observable(item.namePlan);
    this.status = ko.observable(item.status);
}

function AdsViewModel() {

    var self = this;

    self.purchasedPlan = ko.observableArray([]);

    self.token = ko.observable(window.sessionStorage.getItem('token'));

    if (self.token() === null) {
        window.location = "/signup.html";
    }

    $.ajax("/api/area-logada/buffet/planos-contratados", {
        type: "get",
        contentType: "application/json",
        headers: { 'Authorization': 'Bearer ' + self.token() },
        success: function (payload) {

            var adsFound = $.map(payload, function (item) { return new PurchasedPlan(item) });

            self.purchasedPlan(adsFound);
        },
        error: function (result) {
            ShowMessage(result.responseText, result.status);
        }
    });
}

ko.applyBindings(new AdsViewModel());