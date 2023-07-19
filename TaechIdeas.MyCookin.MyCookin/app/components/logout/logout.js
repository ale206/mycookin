var logout = {
    init: function() {

        const app = angular.module("logout_module", ["ui.bootstrap", "notyModule"]);

        app.config(function($httpProvider) {
            $httpProvider.defaults.headers.common = {};
            $httpProvider.defaults.headers.post = {};
            $httpProvider.defaults.headers.put = {};
            $httpProvider.defaults.headers.patch = {};
        });

        app.controller("logoutController",
            [
                "$scope", "$rootScope", "$http", "$timeout", "$window", "$cookies", "appConfig", "resourceService",
                "noty",
                function($scope, $rootScope, $http, $timeout, $window, $cookies, appConfig, resourceService, noty) {

                    //*******************************************
                    //LOGOUT
                    //*******************************************

                    $rootScope.loading = true;

                    //var languageId = resourceService.getCurrLanguage();

                    const token = $cookies.get("MyCookin_Token");

                    if (token == undefined) {
                        $cookies.remove("MyCookin_Logged");
                        $cookies.remove("MyCookin_Token");
                        $window.location.href = "/";
                    }

                    $http({
                        method: "POST",
                        url: appConfig.apiCoreUrl + "user/logout",
                        headers: { 'Content-Type': "application/json" },
                        data: JSON.stringify({
                            UserToken: token
                        })
                    }).then(function successCallback(response) {
                            // this callback will be called asynchronously
                            // when the response is available
                            $rootScope.loading = false;

                            $cookies.remove("MyCookin_Logged");
                            $cookies.remove("MyCookin_Token");

                            $window.location.href = "/";
                        },
                        function errorCallback(response) {
                            // called asynchronously if an error occurs
                            // or server returns response with an error status.
                            $rootScope.loading = false;

                            const redirect = function() { $window.location.href = "/"; };
                            //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                            noty.show(response.data["Message"],
                                "warning",
                                "center",
                                true,
                                null,
                                null,
                                null,
                                redirect,
                                2000);
                        });

                    //*******************************************
                }
            ]);
    }
};

module.exports = {
    init: logout.init
};