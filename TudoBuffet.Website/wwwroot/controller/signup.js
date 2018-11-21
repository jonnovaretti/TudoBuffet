ko.validation.locale('pt-BR');

function UserSignup(data) {
    this.email = data.emailSignup();
    this.password = data.passwordSignup();
}

function UserSignupViewModel() {

    this.emailSignup = ko.observable().extend({ required: true, email: true });
    this.passwordSignup = ko.observable().extend({ required: true, minLength: 2 });

    self.errors = ko.validation.group(this);

    self.signup = function () {

        if (errors().length > 0) {
            errors.showAllMessages();
            return;
        }

        $.ajax("/api/usuarios/entrar", {
            data: ko.toJSON(new UserSignup(this)),
            type: "post", contentType: "application/json",
            success: function (payload) {

                window.sessionStorage.setItem('token', payload.authenticatedUser.token);
                window.sessionStorage.setItem('id', payload.authenticatedUser.id);

                window.location = "usuario-buffets.html";
            },
            error: function (result) {
                ShowMessage(result.responseText, result.status);
            }
        });
    };
}

ko.applyBindings(new UserSignupViewModel(), document.getElementById("signup"));