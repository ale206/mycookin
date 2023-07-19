var forgotPassword = {
    init: function() {

        const app = angular.module("forgotPassword_module", ["ui.bootstrap", "notyModule"]);

        app.controller("forgotPasswordController",
            [
                "$scope", "$rootScope", "$http", "$location", "$timeout", "$window", "appConfig", "noty",
                function($scope, $rootScope, $http, $location, $timeout, $window, appConfig, noty) {

                    $scope.submitEmail = function() {

                        $rootScope.loading = true;

                        //DEBUG
                        //console.log($scope.forgotPassword.email);

                        //Ip
                        //var ip = "0.0.0.0";

                        //$http.get('https://api.ipify.org?format=json').
                        //        success(function (data) {
                        //            ip = data.ip;
                        //        });

                        $http({
                            method: "POST",
                            url: appConfig.apiCoreUrl + "user/resetpasswordprocess",
                            headers: { 'Content-Type': "application/json" },
                            data: JSON.stringify({
                                Email: $scope.forgotPassword.email
                            })
                        }).then(function successCallback(response) {
                                // this callback will be called asynchronously
                                // when the response is available

                                $rootScope.loading = false;

                                //DEBUG
                                //console.log("Result: " + response.data["ResetPasswordProcessCompleted"]);

                                //noty.show('Your user is active now! You will be redirect to the login page.', "success", "center");

                                var redirect;

                                if (response.data["ResetPasswordProcessCompleted"]) {

                                    redirect = function() { $window.location.href = "/login"; };
                                    //message, layout, OnOkClick, modal, onShow, afterShow, onClose, afterClose
                                    noty.confirm("We have sent you an email to reset your password.",
                                        "center",
                                        null,
                                        true,
                                        null,
                                        null,
                                        null,
                                        redirect);

                                } else {

                                    redirect = function() { $window.location.href = "/"; };
                                    //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                                    noty.show("Email not found.",
                                        "warning",
                                        "center",
                                        true,
                                        null,
                                        null,
                                        null,
                                        redirect);

                                }
                            },
                            function errorCallback(response) {
                                // called asynchronously if an error occurs
                                // or server returns response with an error status.

                                $rootScope.loading = false;

                                //DEBUG
                                //console.log("Status: " + response.status);
                                //console.log("Message: " + response.data['Message']);

                                const redirect = function() { $window.location.href = "/"; };
                                //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                                noty.show(response.data["Message"],
                                    "warning",
                                    "center",
                                    true,
                                    null,
                                    null,
                                    null,
                                    redirect);


                            });
                    };

                }
            ]);
    }
};

module.exports = {
    init: forgotPassword.init
};