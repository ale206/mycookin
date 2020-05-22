var utils = {
    init: function(app) {

        // RESOURCE Service
        app.factory("resourceService",
            [
                "$resource", "$cookies", function($resource, $cookies) {
                    const serviceInstance = {};

                    // FUNCTION getCurrLanguage
                    serviceInstance.getCurrLanguage = function() {
                        var langId = 1;

                        if ($cookies.get("_culture") != undefined)
                            langId = parseInt($cookies.get("_culture"));
                        else {
                            const lang = navigator.language || navigator.userLanguage;
                            if (lang === "it-IT") {
                                return 2;
                            } else if (lang === "es-ES") {
                                return 3;
                            } else {
                                return 1;
                            }
                        }
                        return langId;
                    };

                    // FUNCTION getLabels
                    serviceInstance.getLabels = function(idLang, section, $scope) {
                        switch (idLang) {
                        case 2:
                            language = "it";
                            break;
                        case 3:
                            language = "es";
                            break;
                        default:
                            language = "en";
                            break;
                        }

                        const languageFilePath = `/assets/resources/${section}/${language}.json`;

                        $resource(languageFilePath).get(function(data) {
                            $scope.labels = data;
                        });
                    };

                    return serviceInstance;
                }
            ]);

        // SEARCH Service
        app.factory("searchService",
            [
                function() {
                    var saveData = {};

                    const set = function(data) {
                        saveData = data;
                    };

                    const get = function() {
                        return saveData;
                    };

                    return {
                        set: set,
                        get: get
                    };
                }
            ]);
    }
};

module.exports = {
    init: utils.init
};