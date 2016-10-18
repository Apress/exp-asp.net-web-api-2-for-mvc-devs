$(document).ready(function () {

    getProducts = function() {
        $.ajax("/api/products", {
            success: function (data) {
                products.removeAll();
                for (var i = 0; i < data.length; i++) {
                    products.push(data[i]);
                }
            }
        })
    };
    ko.applyBindings();
});
