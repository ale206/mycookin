var resetPassword = {
    init: function() {

        const app = angular.module("resetPassword_module", ["ui.bootstrap", "notyModule"]);

        app.controller("resetPasswordController",
            [
                "$scope", "$rootScope", "$http", "$location", "$timeout", "$window", "appConfig", "noty",
                function($scope, $rootScope, $http, $location, $timeout, $window, appConfig, noty) {

                    $rootScope.loading = true;

                    //DEBUG
                    //console.log($location.search().Id);
                    //console.log($location.search().ConfirmationCode);

                    if ($location.search().Id == undefined || $location.search().ConfirmationCode == undefined) {
                        $window.location.href = "/";
                        return;
                    }

                    //CHECK
                    //************************************************************

                    $http({
                        method: "POST",
                        url: appConfig.apiCoreUrl + "user/checkpasswordprocess",
                        headers: { 'Content-Type': "application/json" },
                        data: JSON.stringify({
                            UserId: $location.search().Id,
                            ConfirmationCode: $location.search().ConfirmationCode
                        })
                    }).then(function successCallback(response) {
                            // this callback will be called asynchronously
                            // when the response is available

                            $rootScope.loading = false;

                            //DEBUG
                            //console.log("IsValid: " + response.data["IsValid"]);

                            if (response.data["IsValid"]) {
                                //Show form...
                                $scope.isValid = response.data["IsValid"];

                            } else {

                                const redirect = function() { $window.location.href = "/"; };
                                //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                                noty.show("Not valid Id or ConfirmationCode.",
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

                            //noty.show(response.data['Message'], "warning", "center");
                            noty.show("Something has gone wrong. Please try later.", "warning", "center");

                            //DEBUG
                            //console.log("Status: " + response.status);
                            //console.log("Message: " + response.data['Message']);
                        });
                    //************************************************************


                    //UPDATE PASSWORD
                    $scope.submitPassword = function() {

                        $rootScope.loading = true;

                        //DEBUG
                        //console.log($scope.resetPassword.password);

                        //Ip
                        //var ip = "0.0.0.0";

                        //$http.get('https://api.ipify.org?format=json').
                        //        success(function (data) {
                        //            ip = data.ip;
                        //        });


                        $http({
                            method: "POST",
                            url: appConfig.apiCoreUrl + "user/password/update",
                            headers: { 'Content-Type': "application/json" },
                            data: JSON.stringify({
                                UserId: $location.search().Id,
                                NewPassword: $scope.resetPassword.password,
                                ConfirmationCode: $location.search().ConfirmationCode
                            })
                        }).then(function successCallback(response) {
                                // this callback will be called asynchronously
                                // when the response is available

                                $rootScope.loading = false;

                                //DEBUG
                                //console.log("PasswordUpdated: " + response.data["PasswordUpdated"]);

                                //noty.show('Your user is active now! You will be redirect to the login page.', "success", "center");

                                var redirect;

                                if (response.data["PasswordUpdated"]) {

                                    redirect = function() { $window.location.href = "/#/login"; };
                                    //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                                    noty.show("Your password has been updated.",
                                        "success",
                                        "center",
                                        true,
                                        null,
                                        null,
                                        null,
                                        redirect);
                                } else {

                                    redirect = function() { $window.location.href = "/"; };
                                    //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                                    noty.show("Something has gone wrong.",
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
    init: resetPassword.init
};