ko.validation.locale('pt-BR');

function Plan(item) {
    this.id = item.id;
    this.name = item.name;
    this.description = item.description;
    this.image = item.image;
    this.selectedPlan = false;
}

function RangePrice(item) {
    this.rangePriceText = item.text;
    this.rangePriceId = item.code;
}

function BuffetCategory(item) {
    this.buffetCategoryText = item.text;
    this.buffetCategoryId = item.code;
}

function BuffetEnvironment(item) {
    this.buffetEnvironmentText = item.text;
    this.buffetEnvironmentId = item.code;
}

function Buffet(data) {
    this.name = data.name();
    this.description = data.description();
    this.street = data.street();
    this.number = data.number();
    this.district = data.district();
    this.city = data.city();
    this.state = data.selectedState();
    this.cellphone = data.cellphone();
    this.facebook = data.facebook();
    this.instagram = data.instagram();
    this.selectedPlan = data.selectedPlan();
    this.selectedRangePrice = data.selectedRangePrice().rangePriceId;
    this.selectedBuffetCategory = data.selectedBuffetCategory().buffetCategoryId;
    this.selectedBuffetEnvironment = data.selectedBuffetEnvironment().buffetEnvironmentId;
}

function NewBuffetViewModel() {

    var self = this;

    self.plans = ko.observableArray([]);
    self.rangesPrice = ko.observableArray([]);
    self.buffetCategories = ko.observableArray([]);
    self.buffetEnvironments = ko.observableArray([]);

    self.name = ko.observable().extend({ required: true });
    self.description = ko.observable().extend({ required: true });
    self.street = ko.observable().extend({ required: true });
    self.number = ko.observable();
    self.district = ko.observable().extend({ required: true });
    self.city = ko.observable().extend({ required: true });
    self.cellphone = ko.observable();
    self.facebook = ko.observable();
    self.instagram = ko.observable();
    self.selectedPlan = ko.observable().extend({ required: true });
    self.selectedRangePrice = ko.observable().extend({ required: true });
    self.selectedBuffetCategory = ko.observable().extend({ required: true });
    self.selectedBuffetEnvironment = ko.observable().extend({ required: true });
    self.selectedState = ko.observable().extend({ required: true });

    loadPlans(self);
    loadRangePrice(self);
    loadBuffetCategories(self);
    loadBuffetEnvironment(self);

    self.token = ko.observable(window.sessionStorage.getItem('token'));
    self.errors = ko.validation.group(self);

    self.save = function () {

        if (self.errors().length > 0) {
            self.errors.showAllMessages();
            return;
        }

        $.ajax("/api/admin", {
            data: ko.toJSON(new Buffet(self)),
            type: "post",
            contentType: "application/json",
            headers: { 'Authorization': 'Bearer ' + self.token() },
            success: function (result) {
                window.location = 'usuario-detalhe-buffet-fotos.html?buffetId=' + result;
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

function loadRangePrice(self) {

    $.ajax("/api/dominio/faixas-de-preco", {
        type: "get",
        contentType: "application/json",
        success: function (payload) {
            var rangesPrice = $.map(payload, function (item) { return new RangePrice(item) });
            self.rangesPrice(rangesPrice);
        },
        error: function (result) {
            ShowMessage(result.responseText, result.status);
        }
    });
}

function loadBuffetEnvironment(self) {

    $.ajax("/api/dominio/ambientes", {
        type: "get",
        contentType: "application/json",
        success: function (payload) {
            var list = $.map(payload, function (item) { return new BuffetEnvironment(item) });
            self.buffetEnvironments(list);
        },
        error: function (result) {
            ShowMessage(result.responseText, result.status);
        }
    });
}

function loadBuffetCategories(self) {

    $.ajax("/api/dominio/categorias-buffet", {
        type: "get",
        contentType: "application/json",
        success: function (payload) {
            var list = $.map(payload, function (item) { return new BuffetCategory(item) });
            self.buffetCategories(list);
        },
        error: function (result) {
            ShowMessage(result.responseText, result.status);
        }
    });
}

function loadPlans(self) {

    $.ajax("/api/dominio/planos", {
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