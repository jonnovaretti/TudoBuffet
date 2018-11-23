function Plan(item) {
    this.id = item.id;
    this.name = item.name;
    this.description = item.description;
    this.image = item.image;
    this.selectedPlan = false;
}

function Buffet(data) {
    this.name = data.name();
    this.description = data.description();
    this.street = data.street();
    this.number = data.number();
    this.district = data.district();
    this.city = data.city();
    this.state = data.state();
    this.celphone = data.celphone();
    this.facebook = data.facebook();
    this.instagram = data.instagram();
    this.selectedPlan = data.selectedPlan();
}

function NewBuffetViewModel() {

    var self = this;

    self.plans = ko.observableArray([]);
    self.selectedPlan = ko.observable();

    loadPlans(self);

    self.save = function () {

        if (errors().length > 0) {
            errors.showAllMessages();
            return;
        }

        $.ajax("/api/area-logada/buffet", {
            data: ko.toJSON(new Buffet(self)),
            type: "post",
            contentType: "application/json",
            success: function (result) {
                ShowMessage(result, 200);
            },
            error: function (result) {
                ShowMessage(result.responseText, result.status);
            }
        });
    };

    self.selected = function (id) {
        self.selectedPlan(id);
    }
}

ko.applyBindings(new NewBuffetViewModel());

function loadPlans(self) {

    $.ajax("/api/planos", {
        type: "get",
        contentType: "application/json",
        success: function (payload) {
            var plansFound = $.map(payload, function (item) { return new Plan(item) });
            self.plans(plansFound);
        },
        error: function (result) {
            ShowMessage(result.responseText, result.status);
        }
    });
}