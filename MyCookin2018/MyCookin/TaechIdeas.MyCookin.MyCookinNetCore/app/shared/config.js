var config = {
    init: function() {
        const app = angular.module("config", []);

        app.constant("appConfig",
            {
                //apiBaseUrl: "http://www.TaechIdeas.MyCookin.cloud/mycookin/",
                apiBaseUrl: "http://localhost:50351/mycookin/",
                //apiCoreUrl: "http://www.TaechIdeas.MyCookin.cloud/core/",
                apiCoreUrl: "http://localhost:50351/core/",
                //facebookAppId: "476759249023668"
                facebookAppId: "1059302990769288" //test
            });
    }
};