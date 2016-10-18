var setView = function (view) {
    adminModel.currentView(view);
}

var setListMode = function (mode) {
    console.log("Mode: " + mode);
    adminModel.listMode(mode);
}

var authenticateUser = function () {
    authenticate(function () {
        setView("productList");
        getProducts();
        getOrders();
    });
}

var createProduct = function () {
    saveProduct(adminModel.newProduct, function () {
        setListMode("products");
    })
}

var removeProduct = function (product) {
    deleteProduct(product.Id);
}

var removeOrder = function (order) {
    deleteOrder(order.Id);
}
