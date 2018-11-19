ko.validation.locale('pt-BR');

var mustEqual = function (val, other) {
    return val === other;
};

function User(data) {
    this.name = data.name();
    this.email = data.email();
    this.password = data.password();
    this.confirmationPassword = data.confirmationPassword();
}

function UserViewModel() {

    this.name = ko.observable().extend({ required: true, minLength: 2, maxLength: 120 });
    this.email = ko.observable().extend({ required: true, email: true });
    this.password = ko.observable().extend({ required: true, minLength: 2 });
    this.confirmationPassword = ko.observable().extend({
        required: true,
        validation: {
            validator: mustEqual,
            message: 'As senhas devem ser iguais',
            params: this.password
        } });

    self.errors = ko.validation.group(this);

    self.save = function () {

        if (errors().length > 0) {
            errors.showAllMessages();
            return;
        }
        
        $.ajax("/api/users", {
            data: ko.toJSON(new User(this)),
            type: "post", contentType: "application/json",
            success: function (result) { alert(result); }
        });
    };
}

ko.applyBindings(new UserViewModel());