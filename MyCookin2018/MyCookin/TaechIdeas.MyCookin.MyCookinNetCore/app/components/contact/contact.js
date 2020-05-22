var contact = {
    init: function(app) {
        app.controller("contactController",
            [
                "$scope", "$rootScope", "$http", function($scope, $rootScope, $http) {
                }
            ]);
    }
};

module.exports = {
    init: contact.init
};