var routes = {
    init: function() {
        const routeModule = angular.module("routeModule", ["ngRoute"]);

        routeModule.config([
            "$routeProvider", "$locationProvider", function($routeProvider, $locationProvider) {
                $routeProvider.when("/",
                    {
                        templateUrl: "app/components/home/home.html",
                        controller: "homeController"
                    }).when("/termsAndConditions",
                    {
                        templateUrl: "app/components/termsAndConditions/termsAndConditions.html",
                        controller: "termsAndConditionsController"
                    }).when("/contact",
                    {
                        templateUrl: "app/components/contact/contact.html",
                        controller: "contactController"
                    }).when("/login",
                    {
                        templateUrl: "app/components/login/login.html",
                        controller: "loginController"
                    }).when("/logout",
                    {
                        templateUrl: "app/components/logout/logout.html",
                        controller: "logoutController"
                    }).when("/en/recipe/:recipeId",
                    {
                        templateUrl: "app/components/recipe/recipe.html",
                        controller: "recipeController"
                    }).when("/it/ricetta/:recipeId",
                    {
                        templateUrl: "app/components/recipe/recipe.html",
                        controller: "recipeController"
                    }).when("/es/receta/:recipeId",
                    {
                        templateUrl: "app/components/recipe/recipe.html",
                        controller: "recipeController"
                    }).when("/email/confirm",
                    {
                        templateUrl: "app/components/confirmEmail/confirmEmail.html",
                        controller: "confirmEmailController"
                    }).when("/password/forgot",
                    {
                        templateUrl: "app/components/forgotPassword/forgotPassword.html",
                        controller: "forgotPasswordController"
                    }).when("/password/reset",
                    {
                        templateUrl: "app/components/resetPassword/resetPassword.html",
                        controller: "resetPasswordController"
                    }).when("/about",
                    {
                        templateUrl: "app/components/about/about.html",
                        controller: "aboutController"
                    }).when("/bulletinBoard",
                    {
                        templateUrl: "app/components/bulletinBoard/bulletinBoard.html",
                        controller: "bulletinBoardController"
                    }).when("/searchResult",
                    {
                        templateUrl: "app/components/searchResult/searchResult.html",
                        controller: "searchResultController"
                    }).otherwise({
                    redirectTo: "/"
                });

                //To remove # from URL
                $locationProvider.html5Mode(true);

            }
        ]);
    }
};

module.exports = {
    init: routes.init
};