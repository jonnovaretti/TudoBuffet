(function (factory) {
    // Module systems magic dance.
    /*global require,ko.validation,define,module*/
    if (typeof require === 'function' && typeof exports === 'object' && typeof module === 'object') {
        // CommonJS or Node
        module.exports = factory(require('../'));
    } else if (typeof define === 'function' && define['amd']) {
        // AMD anonymous module
        define(['knockout.validation'], factory);
    } else {
        // <script> tag: use the global `ko.validation` object
        factory(ko.validation);
    }
}(function (kv) {
    if (!kv || typeof kv.defineLocale !== 'function') {
        throw new Error('Knockout-Validation is required, please ensure it is loaded before this localization file');
    }
    return kv.defineLocale('pt-BR', {
        required: 'Este campo é obrigatório.',
        min: 'Esse campo deve conter uma quantidade de caracters maior ou igual a {0}.',
        max: 'Esse campo deve conter uma quantidade de caracters menor ou igual a {0}.',
        minLength: 'Informe ao menos {0} caracteres.',
        maxLength: 'Informe no máximo {0} caracteres.',
        pattern: 'Este campo está fora do padrão',
        step: 'O valor deve ser incrementado por {0}',
        email: 'Informe um e-mail válido.',
        date: 'Informe uma data válida.',
        dateISO: 'Informe uma data válida (ISO).',
        number: 'Informe um número válido.',
        digit: 'Utilize somente dígitos.',
        phoneUS: 'Informe um telefone válido',
        equal: 'Os valores devem ser iguais',
        notEqual: 'Informe outro valor',
        unique: 'Verifique se o valor é único'
    });
}));