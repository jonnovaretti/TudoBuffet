var nameBudgetListItem = 'budgetList';

$(document).ready(function () {
    loadBuffetsSelected();
});

function loadBuffetsSelected() {
    var currentList;
    var budgetList;
    var viewButtonBudgetList;

    currentList = window.localStorage.getItem(nameBudgetListItem);

    if (currentList !== null) {
        budgetList = currentList.split('|');

        viewButtonBudgetList = document.getElementById('viewButtonBudgetList');
        viewButtonBudgetList.innerText = budgetList.length + ' buffet(s) selecionado(s)';
    }
    else {
        viewButtonBudgetList = document.getElementById('viewButtonBudgetList');
        viewButtonBudgetList.innerText = '0 buffet selecionado';
        $('#icostar').hide();
    }
}

function addBuffetToBudgetList(buffetId) {
    var currentList;

    currentList = window.localStorage.getItem(nameBudgetListItem);

    if (currentList === null)
        currentList = buffetId;
    else {
        if (currentList.indexOf(buffetId) === -1)
            currentList += '|' + buffetId;
    }

    window.localStorage.setItem(nameBudgetListItem, currentList);
    loadBuffetsSelected();

    $('#icostar').show();

    return false;
}

function prepareBudgetList() {
    var currentList;
    var budgetList;
    var form = document.createElement("form");

    currentList = window.localStorage.getItem(nameBudgetListItem);

    if (currentList === undefined)
        return;
    else {
        var hiddenField = document.createElement("input");

        form = document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", '/orcamento/listar');

        hiddenField.setAttribute("type", "hidden");
        hiddenField.setAttribute("name", 'currentList');
        hiddenField.setAttribute("value", currentList);

        form.appendChild(hiddenField);
    }
    
    document.body.appendChild(form);
    form.submit();
}