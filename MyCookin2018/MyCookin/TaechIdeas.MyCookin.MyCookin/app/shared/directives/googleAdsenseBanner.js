var googleAdsenseBanner = {
    init: function() {

        const app = angular.module("googleAdsenseBanner_module", []);

        app.directive("googleAdsenseBanner",
            function() {
                return {
                    restrict: "E",
                    templateUrl: "/app/shared/directives/googleAdsenseBanner.html",
                    scope: false,
                    controller: function($scope) {
                        ($scope.adsbygoogle = window.adsbygoogle || []).push({});
                    }
                };
            });


    }
};

module.exports = {
    init: googleAdsenseBanner.init
};