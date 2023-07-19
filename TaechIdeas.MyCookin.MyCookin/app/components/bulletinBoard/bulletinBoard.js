

var bulletinBoard = {
    init: function(app) {
        app.controller("bulletinBoardController",
            [
                "$scope", "$rootScope", "resourceService", "noty", function($scope, $rootScope, resourceService, noty) {

                    const languageId = resourceService.getCurrLanguage();

                    var message = "Hi! This is what you will find here soon. I will keep you updated!";

                    switch (languageId) {
                    case 2:
                        message =
                            "Ecco quello che vedrai presto in questa sezione. Torna a trovarci tra qualche giorno!";
                        break;
                    case 3:
                        message = "Esto es lo que verás pronto en esta sección. Vuelve en unos días!";
                        break;
                    }

                    //message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms
                    noty.show(message, "success", "center", true, null, null, null, null, 8000);
                }
            ]);
    }
};

module.exports = {
    init: bulletinBoard.init
};