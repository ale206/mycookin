

var searchEngine = {
    init: function() {

        const app = angular.module("searchEngine_module", []);

        app.directive("searchEngine",
            function() {
                return {
                    restrict: "E",
                    templateUrl: "/app/shared/directives/searchEngine.html",
                    scope: false,
                    require: ["^form", "ngModel", "noty"],

                    controller: function($scope,
                        $rootScope,
                        $http,
                        appConfig,
                        $window,
                        searchService,
                        $location,
                        $route,
                        resourceService,
                        noty) {

                        if ($scope.searchEngine) {
                            if ($scope.searchEngine.isVegan)
                                $("input[name='isVegan']").attr("checked", "checked");
                            if ($scope.searchEngine.isVegetarian)
                                $("input[name='isVegetarian']").attr("checked", "checked");
                            if ($scope.searchEngine.isGlutenFree)
                                $("input[name='isGlutenFree']").attr("checked", "checked");
                            if ($scope.searchEngine.isLight)
                                $("input[name='isLight']").attr("checked", "checked");
                            if ($scope.searchEngine.isQuick)
                                $("input[name='isQuick']").attr("checked", "checked");
                            if ($scope.searchEngine.isEmptyFridge)
                                $("input[name='isEmptyFridge']").attr("checked", "checked");
                        }

                        $(".i-check, .i-radio").iCheck({
                            checkboxClass: "i-check",
                            radioClass: "i-radio"
                        });

                        $scope.submitSearchEngine = function() {
                            $rootScope.loading = true;

                            var isVegan = $('input[name="isVegan"]').parent("div.i-check").hasClass("checked");
                            var isVegetarian = $('input[name="isVegetarian"]').parent("div.i-check")
                                .hasClass("checked");
                            var isGlutenFree = $('input[name="isGlutenFree"]').parent("div.i-check")
                                .hasClass("checked");
                            var isLight = $('input[name="isLight"]').parent("div.i-check").hasClass("checked");
                            var isQuick = $('input[name="isQuick"]').parent("div.i-check").hasClass("checked");
                            const isEmptyFridge = $('input[name="isEmptyFridge"]').parent("div.i-check")
                                .hasClass("checked");

                            var query = "";

                            if ($scope.searchEngine != undefined)
                                query = $scope.searchEngine.text;

                            const langId = resourceService.getCurrLanguage();


                            if (isEmptyFridge && (query.split(",").length < 3)) {

                                let emptyFridgeAlert =
                                    "Empty Fridge function is active: insert at least three ingredients separated by comma";

                                switch (langId) {
                                case 2:
                                    emptyFridgeAlert =
                                        "La funzione Svuota Frigo è attiva: inserisci almeno tre ingredienti separati da virgola";
                                    break;
                                case 3:
                                    emptyFridgeAlert =
                                        "La función Vacía Nevera es activa: introduzca al menos tres ingredientes separados por comas";
                                    break;
                                }

                                //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                                noty.show(emptyFridgeAlert, "warning", "center", true, null, null, null, null, 4000);

                                $rootScope.loading = false;
                                return;
                            }

                            $http({
                                method: "POST",
                                url: appConfig.apiBaseUrl + "recipe/search",
                                headers: { 'Content-Type': "application/json" },
                                data: JSON.stringify({
                                    "Search": query,
                                    "LanguageId": langId,
                                    "Vegan": isVegan,
                                    "Vegetarian": isVegetarian,
                                    "GlutenFree": isGlutenFree,
                                    "Light": isLight,
                                    "Quick": isQuick,
                                    "Count": 12, //TODO: To Configuration!
                                    "Offset": 0,
                                    "IsEmptyFridge": isEmptyFridge,
                                    "OrderBy": "",
                                    "IsAscendant": true,
                                    "includeIngredients": false,
                                    "includeSteps": false,
                                    "includeProperties": false
                                })
                            }).then(function(response) {
                                //console.log(response.data.length == 0);
                                //$scope.loadingRecipes = false;
                                $rootScope.loading = false;

                                const obj = {
                                    response: response.data,
                                    input: {
                                        "query": query,
                                        "isVegan": isVegan,
                                        "isVegetarian": isVegetarian,
                                        "isGlutenFree": isGlutenFree,
                                        "isLight": isLight,
                                        "isQuick": isQuick
                                    }
                                };
                                searchService.set(obj);

                                if ($location.path() === "/searchResult")
                                    $route.reload();
                                else {
                                    $location.path("/searchResult");
                                    $location.replace();
                                }

                            });
                        };
                    }
                };
            });
    }
};

module.exports = {
    init: searchEngine.init
};