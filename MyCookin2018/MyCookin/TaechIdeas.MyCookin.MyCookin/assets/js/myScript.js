var myScript = {
    init: function() {

        // Sidebar for Number of People
        $("#numPeople-slider").ionRangeSlider({
            min: 1,
            max: 8,
            type: "single",
            prefix: "",
            from: 4,
            // maxPostfix: "+",
            prettify: false,
            hasGrid: false,
            grid: true,
            grid_num: 8,
            step: 1,
            grid_snap: true,

            onStart: function(data) {
                console.log(data.from);
            },
            onChange: function(data) {
                console.log(data.from);
            },
            onFinish: function(data) {
                console.log(data.from);
            },
            onUpdate: function(data) {
                console.log(data.from);
            }
        });

        // Sidebar for Difficulty
        $("#difficulty-slider").ionRangeSlider({
            min: 1,
            max: 3,
            type: "single",
            prefix: "",
            from: 2,
            // maxPostfix: "+",
            prettify: false,
            hasGrid: false,
            grid: true,
            grid_num: 3,
            step: 1,
            grid_snap: true,

            onStart: function(data) {
                console.log(data.from);
            },
            onChange: function(data) {
                console.log(data.from);
            },
            onFinish: function(data) {
                console.log(data.from);
            },
            onUpdate: function(data) {
                console.log(data.from);
            }
        });


        $("input.preparation-time-pick").timepicker({
            minuteStep: 1,
            showInpunts: false,
            showMeridian: false,
            defaultTime: "00:15"
        });

        $("input.cooking-time-pick").timepicker({
            minuteStep: 1,
            showInpunts: false,
            showMeridian: false,
            defaultTime: "00:15"
        });

        $("input.cooling-time-pick").timepicker({
            minuteStep: 1,
            showInpunts: false,
            showMeridian: false,
            defaultTime: "00:00"
        });
    }
};

module.exports = {
    init: myScript.init
};