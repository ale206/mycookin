var home = {
    init: function(app) {


        // private Func: getLastRecipes
        var getLastRecipes = function(baseUrl, lang, $scope, $http) {

            $scope.defaultImage = true;
            $scope.loadingGif = true;

            $http({
                method: "GET",
                cache: true,
                url: baseUrl +
                    "recipe/gettopbylanguage?languageId=" +
                    lang +
                    "&includeIngredients=false&includeSteps=false&includeProperties=false"
            }).then(function(response) {
                $scope.loadingGif = false;
                $scope.lastRecipes = response.data;
            });
        };

        // private Func: getBestRecipes
        var getBestRecipes = function(baseUrl, lang, $scope, $http) {
            $http({
                method: "GET",
                cache: true,
                url: baseUrl +
                    "recipe/getbestbylanguage?languageId=" +
                    lang +
                    "&includeIngredients=false&includeSteps=false&includeProperties=false"
            }).then(function(response) {
                //console.log(response);
                $scope.bestRecipes = response.data;
            });
        };

        app.controller("homeController",
            [
                "$scope", "$rootScope", "appConfig",
                "resourceService", "noty", "$timeout", "$cookies", "$http",
                function($scope, $rootScope, appConfig, resourceService, noty, $timeout, $cookies, $http) {

                    const langId = resourceService.getCurrLanguage();

                    //COOKIE LAW
                    var cookieExpirationDate = new Date();
                    cookieExpirationDate.setFullYear(cookieExpirationDate.getFullYear() + 1);

                    var putCookie = function() {
                        $cookies.put("MyCookin_CookieLawAccepted",
                            "true",
                            {
                                expires: cookieExpirationDate
                            });
                    };

                    //We have to pass a function with the scope to Noty..
                    $scope.setCookieLawAccepted = function() {
                        putCookie();
                    };

                    const cookieLawAccepted = $cookies.get("MyCookin_CookieLawAccepted");

                    if (!cookieLawAccepted) {

                        let cookieText =
                            'Cookies are good to eat. We use digital cookies to provide you with a better MyCookin.  Carry on browsing if you are happy with this, or have a look at the <a id=\"hlPrivacyIubenda\" class=\"iubenda-nostyle no-brand iubenda-embed\" href=\"https://www.iubenda.com/privacy-policy/264657\">Privacy Policy</a>.';

                        switch (langId) {
                        case 2:
                            cookieText =
                                "I cookies sono buoni da mangiare. Noi utiliziamo i cookies digitali per fornirti un MyCookin migliore. Se sei d'accordo chiudi questa finestra e continua la tua navigazione, o dai uno sguardo alla nostra <a id=\"hlPrivacyIubenda\" class=\"iubenda-nostyle no-brand iubenda-embed\" href=\"https://www.iubenda.com/privacy-policy/264657\">politica sulla riservatezza</a>.";
                            break;
                        case 3:
                            cookieText =
                                "Los cookies son buenos para comer. Nosotros utilizamos cookies digitales para hacer un MyCookin mejor. Si estás de acuerdo cierra est ventana o sigues navigando, o mira la nuestra <a id=\"hlPrivacyIubenda\" class=\"iubenda-nostyle no-brand iubenda-embed\" href=\"https://www.iubenda.com/privacy-policy/264657\">política de privacidad</a>.";
                            break;
                        }

                        //message, layout, OnOkClick, modal, onShow, afterShow, onClose, afterClose
                        noty.confirm(cookieText, "bottom", $scope.setCookieLawAccepted);
                    }

                    //For STARS
                    $scope.getNumber = function(num) {
                        return new Array(Math.round(num));
                    };


                    // GetResources
                    resourceService.getLabels(langId, "home", $scope);


                    // Get Last recipes.
                    getLastRecipes(appConfig.apiBaseUrl, langId, $scope, $http);

                    // Get Best recipes.
                    getBestRecipes(appConfig.apiBaseUrl, langId, $scope, $http);

                    $scope.$on("changeLang",
                        function(event, args) {
                            resourceService.getLabels(args.idLang, "home", $scope);

                            // Get Last recipes.
                            getLastRecipes(appConfig.apiBaseUrl, args.idLang, $scope, $http);

                            // Get Best recipes.
                            getBestRecipes(appConfig.apiBaseUrl, args.idLang, $scope, $http);
                        });

                    $timeout(function() {

                            $scope.defaultImage = false;

                            // Owl Carousel
                            const owlCarousel = $("#owl-carousel");
                            const owlItems = owlCarousel.attr("data-items");
                            const owlCarouselSlider = $("#owl-carousel-slider");
                            const owlNav = owlCarouselSlider.attr("data-nav");
                            // owlSliderPagination = owlCarouselSlider.attr('data-pagination');

                            owlCarousel.owlCarousel({
                                items: owlItems,
                                navigation: true,
                                navigationText: ["", ""]
                            });

                            owlCarouselSlider.owlCarousel({
                                slideSpeed: 200,
                                paginationSpeed: 800,
                                // pagination: owlSliderPagination,
                                singleItem: true,
                                navigation: false,
                                navigationText: ["", ""],
                                transitionStyle: "fade",
                                autoPlay: 15000
                            });
                        },
                        15000);
                }
            ]);
    }
};

module.exports = {
    init: home.init
};