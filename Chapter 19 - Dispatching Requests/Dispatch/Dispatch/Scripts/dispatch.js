var viewModel = ko.observable({
    productId: 100, name: "Bananas", price: 12.34
});

var products = ko.observableArray();
var response = ko.observable("Ready");
var gotError = ko.observable(false);

var getAll = function () {
    sendRequest("GET");
}

var getOne = function () {
    sendRequest("GET", 2);
}

var postOne = function () {
    sendRequest("POST");
}

var sendRequest = function (verb, id) {

    var url = "/api/products/" + (id || "");

    var config = {
        type: verb || "GET",
        data: verb == "POST" ? viewModel() : null,
        success: function (data) {
            gotError(false);
            response("Success");
            products.removeAll();
            if (Array.isArray(data)) {
                data.forEach(function (product) {
                    products.push(product);
                });
            } else {
                products.push(data);
            }
        },
        error: function (jqXHR) {
            gotError(true);
            products.removeAll();
            response(jqXHR.status + " (" + jqXHR.statusText + ")");
        }
    }

    if (verb != "GET" && verb != "POST") {
        config.type = "POST";
        config.headers = {
            "X-HTTP-Method-Override": verb
        };
    }

    $.ajax(url, config);
};

$(document).ready(function () {
    ko.applyBindings();
});