var customerModel = {
    productCategories: ko.observableArray([]),
    filteredProducts: ko.observableArray([]),
    selectedCategory: ko.observable(null),
    cart: ko.observableArray([]),
    cartTotal: ko.observable(0),
    cartCount: ko.observable(0),
    currentView: ko.observable("list")
}
