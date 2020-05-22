(function(angular, $) {
    return angular.module("notyModule", []).provider("noty",
        function() {


            $.noty.defaults = {
                layout: "bottomRight",
                theme: "defaultTheme", //relax, defaultTheme
                type: "information",
                text: "",
                dismissQueue: true, // If you want to use queue feature set this true
                template:
                    '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
                animation: {
                    //open: { height: 'toggle' },
                    //close: { height: 'toggle' },
                    //easing: 'swing',
                    //speed: 500 // opening & closing animation speed

                    //open  : 'animated bounceInRight',
                    //close : 'animated bounceOutRight'
                    open: "animated flipInX", //Need animate.css https://daneden.github.io/animate.css/
                    close: "animated flipOutX" //Need animate.css

                },
                timeout: 5000, // delay for closing event. Set "false" (not boolean) for sticky notifications
                force: false, // adds notification to the beginning of queue when set to true
                modal: false,
                closeWith: ["click"], // ['click', 'button', 'hover']
                callback: {
                    onShow: function() {},
                    afterShow: function() {},
                    onClose: function() {},
                    afterClose: function() {}
                },
                buttons: false // an array of buttons
            };

            var settings = $.noty.defaults;

            return {
                settings: settings,
                $get: function() {
                    var callNoty = function(newSettings) {
                        return noty(newSettings || {});
                    };

                    return {
                        show: function(message, type, layout, modal, onShow, afterShow, onClose, afterClose, ms) {
                            callNoty({
                                text: message || settings.text,
                                type: type || settings.type,
                                layout: layout || settings.layout,
                                modal: modal || settings.modal,
                                timeout:
                                    ms || settings.timeout, //set to "false" (not boolean) to do not close automatically
                                callback: {
                                    onShow: function() { onShow == null ? function() {} : onShow() },
                                    afterShow: function() { afterShow == null ? function() {} : afterShow() },
                                    onClose: function() { onClose == null ? function() {} : onClose() },
                                    afterClose: function() { afterClose == null ? function() {} : afterClose() }
                                }
                            });
                        },
                        confirm: function(message, layout, onOkClick, modal, onShow, afterShow, onClose, afterClose) {
                            callNoty({
                                text: message || settings.text,
                                type: "confirm",
                                layout: layout || settings.layout,
                                modal: modal || settings.modal,
                                closeWith: ["button"],
                                callback: {
                                    onShow: function() { onShow == null ? function() {} : onShow() },
                                    afterShow: function() { afterShow == null ? function() {} : afterShow() },
                                    onClose: function() { onClose == null ? function() {} : onClose() },
                                    afterClose: function() { afterClose == null ? function() {} : afterClose() }
                                },
                                buttons: [
                                    {
                                        addClass: "btn btn-primary",
                                        text: "Ok",
                                        onClick: function($noty) {
                                            // this = button element
                                            // $noty = $noty element
                                            //console.log(onOkClick);
                                            onOkClick == null ? $noty.close() : onOkClick();
                                            $noty.close();
                                        }
                                    }
                                ]
                            });
                        },
                        confirmWithCancel: function(message,
                            layout,
                            onOkClick,
                            modal,
                            onShow,
                            afterShow,
                            onClose,
                            afterClose) {
                            callNoty({
                                text: message || settings.text,
                                type: "confirm",
                                layout: layout || settings.layout,
                                modal: modal || settings.modal,
                                closeWith: ["button"],
                                callback: {
                                    onShow: function() { onShow == null ? function() {} : onShow() },
                                    afterShow: function() { afterShow == null ? function() {} : afterShow() },
                                    onClose: function() { onClose == null ? function() {} : onClose() },
                                    afterClose: function() { afterClose == null ? function() {} : afterClose() }
                                },
                                buttons: [
                                    {
                                        addClass: "btn btn-primary",
                                        text: "Ok",
                                        onClick: function($noty) {
                                            // this = button element
                                            // $noty = $noty element
                                            onOkClick == null ? $noty.close() : onOkClick();
                                            $noty.close();
                                        }
                                    },
                                    {
                                        addClass: "btn btn-primary",
                                        text: "Cancel",
                                        onClick: function($noty) {
                                            $noty.close();
                                        }
                                    }
                                ]
                            });
                        },
                        //showAlert: function (message, layout) {
                        //    callNoty({ text: message || settings.text, type: "alert", layout: layout || settings.layout });
                        //},

                        //showSuccess: function (message, layout) {
                        //    callNoty({ text: message || settings.text, type: "success", layout: layout || settings.layout });
                        //},

                        //showError: function (message, layout) {
                        //    callNoty({ text: message, type: "error", layout: layout || settings.layout });
                        //},

                        closeAll: function() {
                            return $.noty.closeAll();
                        },
                        clearShowQueue: function() {
                            return $.noty.clearQueue();
                        }.bind(this)
                    };
                }

            };
        });
}(angular, jQuery));