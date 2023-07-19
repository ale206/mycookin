// Routing
var routes = require("./app.routes.js");

var utils = require("../app/shared/utils");

//Directives
var calendarDob = require("../app/shared/directives/calendarDob.js");
var facebookButton = require("../app/shared/directives/facebookButton.js");
var searchEngine = require("../app/shared/directives/searchEngine.js");
var googleAdsenseBanner = require("../app/shared/directives/googleAdsenseBanner.js");

// Components
var home = require("../app/components/home/home");
var recipe = require("../app/components/recipe/recipe");
var login = require("../app/components/login/login");
var logout = require("../app/components/logout/logout");
var termsAndConditions = require("../app/components/termsAndConditions/termsAndConditions");
var contact = require("../app/components/contact/contact");
var confirmEmail = require("../app/components/confirmEmail/confirmEmail");
var forgotPassword = require("../app/components/forgotPassword/forgotPassword");
var resetPassword = require("../app/components/resetPassword/resetPassword");
var bulletinBoard = require("../app/components/bulletinBoard/bulletinBoard");
var about = require("../app/components/about/about");
var searchResult = require("../app/components/searchResult/searchResult");

// Main.
var app = angular.module("mycookin_main",
    [
        "routeModule", "config", "ngCookies", "ngResource",
        "calendarDob_module", "facebookButton_module", "searchEngine_module", "googleAdsenseBanner_module",
        "login_module", "logout_module", "confirmEmail_module", "forgotPassword_module", "resetPassword_module"
    ]);

app.controller("mainController",
    [
        "$scope", "$cookies", "$rootScope", "resourceService",
        function($scope, $cookies, $rootScope, resourceService) {
            this.currentYear = new Date().getFullYear();
            $rootScope.currLang = 1;

            //CHECK IF USER IS LOGGED
            const userLogged = $cookies.get("MyCookin_Logged");
            //console.log(userLogged);
            userLogged ? $rootScope.isLogged = true : $rootScope.isLogged = false;


            // private function: getFlags.
            var getFlags = function() {
                var retObj = [];

                const flags = [
                    {
                        id: 1,
                        src: "assets/img/flags/32/uk.png",
                        alt: "English",
                        title: "EnglishFlag",
                        content: "ENG"
                    },
                    {
                        id: 2,
                        src: "assets/img/flags/32/it.png",
                        alt: "Italian",
                        title: "ItalianFlag",
                        content: "ITA"
                    },
                    {
                        id: 3,
                        src: "assets/img/flags/32/es.png",
                        alt: "Spanish",
                        title: "SpanishFlag",
                        content: "ESP"
                    }
                ];

                if ($rootScope.currLang == 2)
                    retObj = [flags[1], flags[2], flags[0]];
                else if ($rootScope.currLang == 3)
                    retObj = [flags[2], flags[0], flags[1]];
                else
                    retObj = [flags[0], flags[1], flags[2]];

                return retObj;
            };

            //  FUNC: changeLang(idLang)
            $scope.changeLang = function(idLang) {
                $rootScope.currLang = idLang;

                $cookies.put("_culture", idLang);

                //recipeService.getBestRecipes();
                //recipeService.getLastRecipes();

                $scope.flags = getFlags();

                //$scope.$broadcast('changeLang', { idLang: idLang });
                location.reload();
            };

            $rootScope.currLang = resourceService.getCurrLanguage();

            $scope.flags = getFlags();

            resourceService.getLabels($rootScope.currLang, "index", $rootScope);
        }
    ]);

routes.init();
config.init();

//Directives
calendarDob.init();
facebookButton.init();
searchEngine.init();
googleAdsenseBanner.init();

//Components
utils.init(app);
home.init(app);
termsAndConditions.init(app);
contact.init(app);
login.init();
logout.init();
recipe.init(app);
confirmEmail.init();
forgotPassword.init();
resetPassword.init();
bulletinBoard.init(app);
about.init(app);
searchResult.init(app);