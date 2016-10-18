var viewModel = ko.observable({
    first: 2, second: 5, op: { add: true, double: true }
});
var response = ko.observable("Ready");
var gotError = ko.observable(false);

var sendRequest = function (requestType) {
    $.ajax("/api/bindings/sumnumbers", {
        type: "POST",
        data: "<Numbers>"
                + "<First>" + viewModel().first + "</First>"
                + "<Op>"
                    + "<Add>" + viewModel().op.add + "</Add>"
                    + "<Double>" + viewModel().op.double + "</Double>"
                + "</Op>"
                + "<Second>" + viewModel().second + "</Second>"
                + "</Numbers>",
        contentType: "application/xml",
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
