var config = {
    init: function() {
        const app = angular.module("config", []);

        app.constant("appConfig",
            {
                apiBaseUrl: "#{WebApiUrl}",
                facebookAppId: "#{facebook_appid}"
            });
    }
};