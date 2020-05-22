var recipe = {
    init: function(app) {
        app.controller("recipeController",
            [
                "$scope", "$http", "appConfig", "resourceService", "$routeParams", "$window", "noty",
                function($scope, $http, appConfig, resourceService, $routeParams, $window, noty) {

                    $scope.recipe = {};

                    const recipeId = $routeParams.recipeId;

                    // GET getByFriendlyId
                    $http({
                        method: "GET",
                        url: appConfig.apiBaseUrl +
                            "recipe/getbyfriendlyid?friendlyId=" +
                            recipeId +
                            "&includeIngredients=true&includeSteps=true&includeProperties=true"
                    }).then(function(response) {

                            //console.log(response.data);

                            if (response.data == undefined) {
                                $window.location.href = "/";
                            }

                            $scope.recipe = response.data;

                            const groups = $scope.recipe.Properties.PropertiesByRecipeAndLanguageGroups;
                            //console.log(groups[0]);

                            $scope.calculateBarSizes(response.data);

                            //USER INFO
                            //**************************************/
                            $http({
                                method: "GET",
                                url: appConfig.apiCoreUrl +
                                    "user/getuserbyid?userId=" +
                                    response.data.RecipeOwner.UserId
                            }).then(function(response) {
                                    // success
                                    $scope.recipe.RecipeOwner.ImageUrl = response.data.ImageUrl;
                                },
                                function() {
                                    // error
                                });
                            //**************************************/

                        },
                        function errorCallback(response) {
                            //Error calling getbyfriendlyid;
                            const redirect = function() { $window.location.href = "/"; };

                            //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                            noty.show("Someone ate this recipe card! We haven't got it anymore :(",
                                "warning",
                                "center",
                                false,
                                null,
                                null,
                                null,
                                redirect,
                                3000);
                        });

                    // FUNCTION getAvgRating
                    $scope.getAvgRating = function(num) {
                        if (num != undefined)
                            return new Array(Math.floor(num));
                        else
                            return new Array(0);
                    };

                    // GetResources
                    const langId = resourceService.getCurrLanguage();
                    resourceService.getLabels(langId, "recipe", $scope);

                    $scope.calculateBarSizes = function(recipe) {
                        const proteinsRecipeThreshold = 15;
                        const fatRecipeThreshold = 30;
                        const alcoholRecipeThreshold = 20;
                        const carbohydratesRecipeThreshold = 65;

                        const proteinPercentage =
                            Math.round((recipe.RecipePortionProteins / proteinsRecipeThreshold) * 100);
                        const fatsPercentage = Math.round((recipe.RecipePortionFats / fatRecipeThreshold) * 100);
                        const alcoholPercentage =
                            Math.round((recipe.RecipePortionAlcohol / alcoholRecipeThreshold) * 100);
                        const carbohydratesPercentage =
                            Math.round((recipe.RecipePortionCarbohydrates / carbohydratesRecipeThreshold) * 100);

                        $scope.sizeBarProtein = {
                            "width": `${String((proteinPercentage > 100 ? 100 : proteinPercentage))}%`
                        };

                        $scope.sizeBarFats = {
                            "width": `${String((fatsPercentage > 100 ? 100 : fatsPercentage))}%`
                        };

                        $scope.sizeBarAlcohol = {
                            "width": `${String((alcoholPercentage > 100 ? 100 : alcoholPercentage))}%`
                        };

                        $scope.sizeBarCarbohydrates = {
                            "width": `${String((carbohydratesPercentage > 100 ? 100 : carbohydratesPercentage))}%`
                        };

                    };


                }
            ]);
    }
};

module.exports = {
    init: recipe.init
};