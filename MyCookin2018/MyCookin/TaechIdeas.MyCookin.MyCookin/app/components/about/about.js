var about = {
    init: function(app) {
        app.controller("aboutController",
            [
                "$scope", "$rootScope", function($scope, $rootScope) {
                }
            ]);
    }
};

module.exports = {
    init: about.init
};