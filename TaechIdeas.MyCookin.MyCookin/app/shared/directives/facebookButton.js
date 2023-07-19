//Source:
//https://github.com/Ciul/angular-facebook

var facebookButton = {
    init: function() {

        const app = angular.module("facebookButton_module", ["facebook"])
            .config(function(FacebookProvider, appConfig) {
                FacebookProvider.init(appConfig.facebookAppId);
            });

        app.directive("facebookButton",
            function() {
                return {
                    restrict: "E",
                    templateUrl: "/app/shared/directives/facebookButton.html",
                    scope: false,
                    controller: function($scope, $http, Facebook, appConfig, $window, $cookies) {

                        //console.log('In the controller');


                        // Define user empty data :/
                        $scope.user = {};

                        // Defining user logged status
                        $scope.logged = false;

                        // And some fancy flags to display messages upon user status change
                        $scope.byebye = false;
                        $scope.salutation = false;

                        /**
                         * Watch for Facebook to be ready.
                         * There's also the event that could be used
                         */
                        $scope.$watch(
                            function() {
                                return Facebook.isReady();
                            },
                            function(newVal) {
                                if (newVal)
                                    $scope.facebookReady = true;

                            }
                        );

                        var userIsConnected = false;

                        Facebook.getLoginStatus(function(response) {
                            if (response.status == "connected") {
                                userIsConnected = true;
                            }


                        });

                        //IntentLogin
                        $scope.IntentLogin = function() {
                            if (!userIsConnected) {
                                //console.log('Doing login..');
                                $scope.login();
                            } else {
                                //console.log('Already Logged');
                            }

                            $scope.loginRegistrationProcess();
                            return;
                        };


                        $scope.login = function() {
                            Facebook.login(function(response) {
                                    if (response.status == "connected") {
                                        $scope.status = "yes";
                                        $scope.IntentLogin();
                                    } else {
                                        $scope.status = "no";
                                    }
                                },
                                { scope: "email,user_birthday,user_friends,user_location,publish_actions" });
                        };

                        //me
                        $scope.me = function() {
                            Facebook.api("/me",
                                function(response) {
                                    //Using $scope.$apply since this happens outside angular framework.
                                    $scope.$apply(function() {
                                        $scope.user = response;
                                    });

                                });
                        };

                        //Logout
                        $scope.logout = function() {
                            Facebook.logout(function() {
                                $scope.$apply(function() {
                                    $scope.user = {};
                                    $scope.logged = false;
                                });
                            });
                        };

                        //loginRegistrationProcess
                        $scope.loginRegistrationProcess = function() {
                            Facebook.api("/me",
                                function(response) {
                                    //Using $scope.$apply since this happens outside angular framework.
                                    $scope.$apply(function() {
                                        $scope.user = response;

                                        //console.log('in loginRegistrationProcess');

                                        //console.log(response);

                                        if (response["id"] == undefined)
                                            return;

                                        //console.log(response["id"]);
                                        //console.log(response["birthday"]);
                                        //console.log(response["email"]);
                                        //console.log(response["first_name"]);
                                        //console.log(response["gender"]);
                                        //console.log(response["hometown"] == undefined ? null : response["hometown"]["name"]);
                                        //console.log(response["last_name"]);
                                        //console.log(response["link"]);
                                        //console.log(response["locale"]);
                                        //console.log(response["name"]);
                                        //console.log(response["timezone"]);
                                        //console.log(response["verified"]);


                                        //var id = response["id"];
                                        //var birthday = response["birthday"];
                                        //var email = response["email"];
                                        //var firstName = response["first_name"];
                                        //var gender = response["gender"];
                                        //var hometown = response["hometown"] == undefined ? null : response["hometown"]["name"];
                                        //var lastName = response["last_name"];
                                        //var link = response["link"];
                                        //var locale = response["locale"];
                                        //var name = response["name"];    //full name
                                        //var timezone = response["timezone"];
                                        //var verified = response["verified"];

                                        //Ip
                                        var ip = "0.0.0.0";

                                        $http.get("https://api.ipify.org?format=json").success(function(data) {
                                            ip = data.ip;
                                        });

                                        $http({
                                            method: "POST",
                                            url: appConfig.apiCoreUrl + "user/facebooklogin",
                                            headers: { 'Content-Type': "application/json" },
                                            data: JSON.stringify({
                                                Id: response["id"],
                                                Birthday: response["birthday"],
                                                Email: response["email"],
                                                FirstName: response["first_name"],
                                                Gender: response["gender"],
                                                Hometown: response["hometown"] == undefined
                                                    ? null
                                                    : response["hometown"]["name"],
                                                LastName: response["last_name"],
                                                Link: response["link"],
                                                Locale: response["locale"],
                                                Name: response["name"], //full name
                                                Timezone: response["timezone"],
                                                Verified: response["verified"],
                                                Ip: ip
                                            })
                                        }).then(function successCallback(response) {
                                                // this callback will be called asynchronously
                                                // when the response is available

                                                $scope.login = $scope.initial;

                                                //COOKIE TO CHECK IF USER IS LOGGED 
                                                const cookieExpirationDate = new Date();
                                                cookieExpirationDate.setFullYear(cookieExpirationDate.getFullYear() +
                                                    1);

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

                                                //DEBUG
                                                //console.log("Status: " + response.status);
                                                //console.log("UserId: " + response.data["UserId"]);
                                                //console.log("UserToken: " + response.data["UserToken"]);

                                                $window.location.href = "/bulletinBoard";
                                            },
                                            function errorCallback(response) {
                                                // called asynchronously if an error occurs
                                                // or server returns response with an error status.

                                                //DEBUG
                                                console.log(`Status: ${response.status}`);
                                                console.log(`Message: ${response.data["Message"]}`);
                                            });
                                    });
                                });
                        };
                    }
                };
            });


    }
};

module.exports = {
    init: facebookButton.init
};