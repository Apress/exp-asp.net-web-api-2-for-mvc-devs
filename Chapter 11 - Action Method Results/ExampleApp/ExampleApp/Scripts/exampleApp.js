$(document).ready(function () {

    deleteProduct = function (data) {
        $.ajax("/api/products/" + data.ProductID, {
            type: "DELETE",
            success: function () {
                products.remove(data);
            }
        })
    };

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
