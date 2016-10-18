var viewModel = ko.observable({ first: 2, second: 5 });
var response = ko.observable("Ready");
var gotError = ko.observable(false);

var sendRequest = function (requestType) {
    $.ajax("/api/bindings/sumnumbers", {
        type: "POST",
        data: requestType == "sum"
            ? viewModel() : { value1: viewModel().first, value2: viewModel().second },
        success: function (data) {
            gotError(false);
            response("Total: " + data);
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
