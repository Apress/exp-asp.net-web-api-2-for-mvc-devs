$(document).ready(function () {

    deleteProduct = function (data) {
        $.ajax("/api/products/" + data.ProductID, {
            type: "DELETE",
            success: function () {
                products.remove(data);
            }
        })
    };

    getProducts = function () {
        errors.removeAll();
        $.ajax("/api/products", {
            headers: { "X-UseProductFormat": "true" },
            //dataType: "text",
            accepts: {
                "*": "application/x.product"
            },
            success: function (data) {
                products.removeAll();
                var arr = data.split(",");
                for (var i = 0; i < arr.length; i += 3) {
                    products.push({
                        ProductID: arr[i],
                        Name: arr[i + 1],
                        Price: arr[i + 2]
                    });
                }
            },
            error: function (jqXHR) {
                switch (jqXHR.status) {
                    case 406:
                        errors.push("Request not accepted by server");
                        break;
                }
            }
        })
    };
    ko.applyBindings();
});
