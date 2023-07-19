

var termsAndConditions = {
    init: function(app) {
        app.controller("termsAndConditionsController",
            [
                "$scope", "$rootScope", "resourceService", function($scope, $rootScope, resourceService) {

                    const languageId = resourceService.getCurrLanguage();

                    $scope.en = false;
                    $scope.it = false;
                    $scope.es = false;

                    switch (languageId) {
                    case 1:
                        $scope.en = true;
                        break;
                    case 2:
                        $scope.it = true;
                        break;
                    case 3:
                        $scope.es = true;
                        break;
                    default:
                        $scope.en = true;
                        break;
                    }
                }
            ]);
    }
};

module.exports = {
    init: termsAndConditions.init
};