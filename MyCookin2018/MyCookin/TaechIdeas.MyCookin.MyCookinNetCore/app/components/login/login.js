var login = {
    init: function() {

        const app = angular.module("login_module", ["ui.bootstrap", "notyModule"]);

        app.config(function($httpProvider) {
            $httpProvider.defaults.headers.common = {};
            $httpProvider.defaults.headers.post = {};
            $httpProvider.defaults.headers.put = {};
            $httpProvider.defaults.headers.patch = {};
        });

        app.controller("loginController",
            [
                "$scope", "$rootScope", "$http", "$timeout", "$window", "$cookies", "appConfig", "resourceService",
                "noty",
                function($scope, $rootScope, $http, $timeout, $window, $cookies, appConfig, resourceService, noty) {

                    resourceService.getLabels($scope.currLang, "login", $rootScope);

                    $(".form-group").each(function() {
                        var self = $(this),
                            input = self.find("input");

                        input.focus(function() {
                            self.addClass("form-group-focus");
                        });

                        input.blur(function() {
                            if (input.val()) {
                                self.addClass("form-group-filled");
                            } else {
                                self.removeClass("form-group-filled");
                            }
                            self.removeClass("form-group-focus");
                        });
                    });

                    //Offset
                    const d = new Date();
                    var offset = d.getTimezoneOffset();

                    //Ip
                    var ip = "0.0.0.0";

                    $http.get("https://api.ipify.org?format=json").success(function(data) {
                        ip = data.ip;
                    });

                    //*******************************************
                    //LOGIN SUBMIT - START
                    //*******************************************
                    $scope.submitLogin = function() {

                        $rootScope.loading = true;

                        const languageId = resourceService.getCurrLanguage();

                        //DEBUG
                        //console.log($scope.login.email);
                        //console.log($scope.login.password);
                        //console.log(offset);
                        //console.log(ip);
                        //console.log(languageId);

                        $http({
                            method: "POST",
                            url: appConfig.apiCoreUrl + "user/login",
                            headers: { 'Content-Type': "application/json" },
                            data: JSON.stringify({
                                Email: $scope.login.email,
                                Password: $scope.login.password,
                                Offset: offset,
                                Ip: ip,
                                IsPasswordHashed: false,
                                LanguageId: languageId,
                                WebsiteId: 1
                            })
                        }).then(function successCallback(response) {
                                // this callback will be called asynchronously
                                // when the response is available
                                $rootScope.loading = false;

                                //COOKIE TO CHECK IF USER IS LOGGED 
                                const cookieExpirationDate = new Date();
                                cookieExpirationDate.setFullYear(cookieExpirationDate.getFullYear() + 1);

                                //$cookies.put('MyCookin_Logged', 'true');
                                $cookies.put("MyCookin_Logged",
                                    "true",
                                    {
                                        expires: cookieExpirationDate
                                    });
                                $cookies.put("MyCookin_Token",
                                    response.data["UserToken"],
                                    {
                                        expires: cookieExpirationDate
                                    });

                                $scope.login = $scope.initial;

                                $window.location.href = "/bulletinBoard";

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
                                noty.show(response.data["Message"],
                                    "warning",
                                    "center",
                                    true,
                                    null,
                                    null,
                                    null,
                                    null,
                                    2000);

                                //DEBUG
                                //console.log("Status: " + response.status);
                                //console.log("Message: " + response.data['Message']);
                            });

                    };
                    //*******************************************


                    //*******************************************
                    //REGISTRATION SUBMIT - START
                    //*******************************************
                    $scope.submitRegistration = function() {
                        $rootScope.loading = true;

                        const languageId = resourceService.getCurrLanguage();

                        //DEBUG
                        //console.log($scope.registration.name);
                        //console.log($scope.registration.surname);
                        //console.log($scope.registration.username);
                        //console.log($scope.registration.email);
                        //console.log($scope.registration.password);
                        //console.log($scope.calendarDob.dateOfBirth);
                        //console.log(offset);
                        //console.log(ip);
                        //console.log(languageId);
                        //console.log($scope.registration.contractSigned);

                        $http({
                            method: "POST",
                            url: appConfig.apiCoreUrl + "user/register",
                            headers: { 'Content-Type': "application/json" },
                            data: JSON.stringify({
                                Name: $scope.registration.name,
                                Surname: $scope.registration.surname,
                                Username: $scope.registration.username,
                                Password: $scope.registration.password,
                                ContractSigned: $scope.registration.contractSigned,
                                DateOfBirth: $scope.calendarDob.dateOfBirth,
                                Email: $scope.registration.email,
                                LanguageId: languageId,
                                CityId: 1,
                                Offset: offset,
                                Mobile: null,
                                Ip: ip,
                                GenderId: 1,
                                WebsiteId: 1

                            })
                        }).then(function successCallback(response) {
                                // this callback will be called asynchronously
                                // when the response is available
                                $rootScope.loading = false;

                                $scope.registration = $scope.initial;

                                const redirect = function() { $window.location.href = "/"; };
                                //message, layout, OnOkClick, modal, onShow, afterShow, onClose, afterClose
                                noty.confirm("Welcome! Please check your email to confirm your registration.",
                                    "center",
                                    null,
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
                                noty.show(response.data["Message"], "warning", "center", true);

                                //DEBUG
                                //console.log("Status: " + response.status);
                                //console.log("Message: " + response.data['Message']);
                            });

                    };
                    //*******************************************

                }
            ]);
    }
};

module.exports = {
    init: login.init
};