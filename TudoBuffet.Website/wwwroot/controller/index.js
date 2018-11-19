function Buffet(payload) {
    this.name = ko.observable(payload.name);
    this.category = ko.observable(payload.category);
    this.thumbprint = ko.observable(payload.thumbprint);
}

function BuffetsViewModel() {
    var self = this;

    self.buffets = ko.observableArray([]);

    $.getJSON("/api/buffets/destaques", function (allData) {
        var buffetsMapped = $.map(allData, function (item) { return new Buffet(item); });
        self.buffets(buffetsMapped);
    }); 
}

ko.applyBindings(new BuffetsViewModel());