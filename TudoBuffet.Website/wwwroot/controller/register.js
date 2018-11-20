ko.validation.locale('pt-BR');

var mustEqual = function (val, other) {
    return val === other;
};

function UserRegister(data) {
    this.name = data.name();
    this.email = data.email();
    this.password = data.password();
    this.confirmationPassword = data.confirmationPassword();
}

function UserRegisterViewModel() {

    this.name = ko.observable().extend({ required: true, minLength: 2, maxLength: 120 });
    this.email = ko.observable().extend({ required: true, email: true });
    this.password = ko.observable().extend({ required: true, minLength: 2 });
    this.confirmationPassword = ko.observable().extend({
        required: true,
        validation: {
            validator: mustEqual,
            message: 'As senhas devem ser iguais',
            params: this.password
        }
    });

    self.errors = ko.validation.group(this);

    self.save = function () {

        if (errors().length > 0) {
            errors.showAllMessages();
            return;
        }

        $.ajax("/api/users", {
            data: ko.toJSON(new UserRegister(this)),
            type: "post",
            contentType: "application/json",
            success: function (result) {
                ShowMessage(result, 200);
            },
            error: function (result) {
                ShowMessage(result.responseText, result.status);
            }
        });

        this.name(null);
        this.email(null);
        this.password(null);
        this.confirmationPassword(null);
    };
}

ko.applyBindings(new UserRegisterViewModel(), document.getElementById("register"));