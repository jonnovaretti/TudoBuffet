function ShowMessage(message, status) {
    if (status >= 500) {
        window.location = "404.html";
    }

    $('#message-body')[0].innerText = message;
    $('#message-modal').modal();
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function cleanQueryParamCityAndState() {
    let currentLink = window.location.href;
    let params = getUrlVars();
    let valuesParam = ['cidade', 'uf'];
    let finalLink;

    for (var i = 0; i < valuesParam.length; i++) {
        let valueParam = params[valuesParam[i]];

        if (params.length > 0 && currentLink.indexOf('?')) {
            if (params.length == 1)
                finalLink = currentLink.replace('?' + valuesParam[i] + '=' + valueParam, '');
            else
                finalLink = currentLink.replace(valuesParam[i] + '=' + valueParam + '&', '').replace('&' + valuesParam[i] + '=' + valueParam, '').replace('&' + valuesParam[i] + '=' + valueParam + '&', '');

            currentLink = finalLink;
        }
    }
    window.location = finalLink;

}

function cleanQueryParam(paramName) {
    let currentLink = window.location.href;
    let params = getUrlVars();
    let valueParam;
    let finalLink;

    valueParam = params[paramName];

    if (params.length > 0 && currentLink.indexOf('?')) {
        if (params.length == 1)
            finalLink = currentLink.replace('?' + paramName + '=' + valueParam, '');
        else
            finalLink = currentLink.replace(paramName + '=' + valueParam + '&', '').replace('&' + paramName + '=' + valueParam, '').replace('&' + paramName + '=' + valueParam + '&', '');
    }

    window.location = finalLink;
}

function filterByName(nameParam) {
    var name = document.getElementById('name');
    var params = getUrlVars();
    var nameInLink;
    var finalHref;
    var separator = '?';
    var currentLink = window.location.href;

    if (name.value === '' || name.value === undefined) {
        cleanQueryParam(nameParam);
    }
    else {
        if (currentLink.indexOf(nameParam + '=') >= 0) {
            nameInLink = params[nameParam];
            finalHref = currentLink.replace(nameInLink, name.value);
        }

        if (name.value !== undefined && name.value !== '' && currentLink.indexOf(nameParam + '=') == -1) {
            if (currentLink.indexOf('?') >= 0)
                separator = '&';
            finalHref = currentLink + separator + nameParam + '=' + name.value;
        }

        window.location = finalHref;
    }
}

function filterBy(cityParam, stateParam) {
    var city = document.getElementById('city');
    var state = document.getElementById('state');
    var params = getUrlVars();
    var cityInLink, stateInLink;
    var finalHref;
    var separator = '?';
    var currentLink = window.location.href;

    if (city.value === '') {
        cleanQueryParam('cidade');
        return;
    }

    if (state.value === '') {
        cleanQueryParam('uf');
        return;
    }

    if (currentLink.indexOf(stateParam + '=') >= 0 && currentLink.indexOf(cityParam + '=') >= 0) {
        stateInLink = params[stateParam];
        cityInLink = params[cityParam];
        finalHref = currentLink.replace(stateInLink, state.value).replace(cityInLink, city.value);
    }

    if (currentLink.indexOf(cityParam + '=') >= 0) {
        cityInLink = params[cityParam];
        finalHref = currentLink.replace(cityInLink, city.value);
    }
    else if (currentLink.indexOf(stateParam + '=') >= 0) {
        stateInLink = params[stateParam];
        finalHref = currentLink.replace(stateInLink, state.value);
    }

    if (city.value !== undefined && city.value !== '' && currentLink.indexOf(cityParam + '=') == -1) {
        if (currentLink.indexOf('?') >= 0)
            separator = '&';

        finalHref = currentLink + separator + cityParam + '=' + city.value;
    }

    if (state.value !== undefined && state.value !== '' && currentLink.indexOf(stateParam + '=') == -1) {
        if (currentLink.indexOf('?') >= 0)
            separator = '&';

        if (city.value !== undefined && city.value !== '' && currentLink.indexOf(cityParam + '=') == -1) {
            finalHref += separator + stateParam + '=' + state.value;
        }
        else {
            finalHref = currentLink + separator + stateParam + '=' + state.value;
        }
    }

    window.location = finalHref;
}