$(document).ready(function () {
    try {
        var d = new Date();
        $("#hfOffsetMasterPage").val(d.getTimezoneOffset());
    }
    catch (err) {
    }
});