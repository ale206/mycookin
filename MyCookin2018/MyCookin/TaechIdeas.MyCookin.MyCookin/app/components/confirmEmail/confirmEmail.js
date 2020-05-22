var confirmEmail = {
    init: function() {

        const app = angular.module("confirmEmail_module", ["ui.bootstrap", "notyModule"]);

        app.controller("confirmEmailController",
            [
                "$scope", "$rootScope", "$http", "$location", "$timeout", "$window", "appConfig", "noty",
                function($scope, $rootScope, $http, $location, $timeout, $window, appConfig, noty) {

                    $rootScope.loading = true;

                    //DEBUG
                    //console.log($location.search().Id);
                    //console.log($location.search().ConfirmationCode);

                    //Ip
                    var ip = "0.0.0.0";

                    $http.get("https://api.ipify.org?format=json").success(function(data) {
                        ip = data.ip;
                    });

                    $http({
                        method: "POST",
                        url: appConfig.apiCoreUrl + "user/activate",
                        headers: { 'Content-Type': "application/json" },
                        data: JSON.stringify({
                            UserId: $location.search().Id,
                            ConfirmationCode: $location.search().ConfirmationCode,
                            IpAddress: ip
                        })
                    }).then(function successCallback(response) {
                            // this callback will be called asynchronously
                            // when the response is available

                            $rootScope.loading = false;

                            const redirect = function() { $window.location.href = "/login"; };
                            //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                            noty.show("Your user is active now! You will be redirect to the login page.",
                                "success",
                                "center",
                                true,
                                null,
                                null,
                                null,
                                redirect);

                            //DEBUG
                            //console.log("Status: " + response.status);
                            //console.log("UserId: " + response.data["UserId"]);
                            //console.log("UserToken: " + response.data["UserToken"]);
                        },
                        function errorCallback(response) {
                            // called asynchronously if an error occurs
                            // or server returns response with an error status.

                            $rootScope.loading = false;

                            const redirect = function() { $window.location.href = "/"; };
                            //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                            noty.show(response.data["Message"], "warning", "center", true, null, null, null, redirect);

                            //DEBUG
                            //console.log("Status: " + response.status);
                            //console.log("Message: " + response.data['Message']);
                        });

                }
            ]);
    }
};

module.exports = {
    init: confirmEmail.init
};