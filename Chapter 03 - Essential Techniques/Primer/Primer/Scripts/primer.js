var modelData = ko.observable("(Ready)");

var sendRequest = function () {
    $.ajax("/api/pagesize", {
        type: "GET",
        success: function (data) {
            modelData("Response: " + data + " bytes");
        }
    });
}

$(document).ready(function () {
    ko.applyBindings();
});
