var viewModel = ko.observable({
    productID: 1,
    name: "Emergency Flare",
    price: 12.99
});

var response = ko.observable("Ready");
var gotError = ko.observable(false);

var sendRequest = function (requestType) {

    $.ajax("/api/products", {
        type: "POST",
        data: JSON.stringify(viewModel()),
        contentType: "application/json",
        success: function (data) {
            gotError(false);
            response("Success");
        },
        error: function (jqXHR) {
            gotError(true);
            response(jqXHR.status + " (" + jqXHR.statusText + ")");
        }
    });
};


$(document).ready(function () {
    ko.applyBindings();
});
