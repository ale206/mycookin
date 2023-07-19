var searchResult = {
    init: function(app) {
        app.controller("searchResultController",
            [
                "$scope", "$rootScope", "searchService", "resourceService",
                function($scope, $rootScope, searchService, resourceService) {

                    const langId = resourceService.getCurrLanguage();

                    // GetResources
                    resourceService.getLabels(langId, "home", $scope);

                    // Search Results

                    const res = searchService.get();
                    $scope.recipes = res.response;

                    if (res.response) {
                        if (res.response.length === 0) {
                            console.log(res.response.length === 0);
                            console.log($scope.noRecipesText);
                            $scope.noRecipesText = true;
                        }
                    }

                    /// FUNC getFullStars
                    $scope.getFullStars = function(num) {
                        const floored = Math.floor(num);
                        return new Array(floored);
                    };

                    /// FUNC getEmptyStars
                    $scope.getEmptyStars = function(num) {
                        const floored = Math.floor(num);
                        return new Array(5 - floored);
                    };

                    if (res.input) {
                        $scope.searchEngine =
                        {
                            text: res.input.query,
                            isVegan: res.input.isVegan,
                            isVegetarian: res.input.isVegetarian,
                            isGlutenFree: res.input.isGlutenFree,
                            isLight: res.input.isLight,
                            isQuick: res.input.isQuick
                        };
                    }
                }
            ]);
    }
};

module.exports = {
    init: searchResult.init
};