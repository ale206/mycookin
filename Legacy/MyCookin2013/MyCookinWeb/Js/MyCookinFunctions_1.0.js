// Hello.
//
// This is JSHint, a tool that helps to detect errors and potential
// problems in your JavaScript code.
//
// To start, simply enter some JavaScript anywhere on this page. Your
// report will appear on the right side.
//
// Additionally, you can toggle specific options in the Configure
// menu.

//WebServices site path
var WebServicesPath = "localhost";
var WebServicesMySqlPath = "localhost";
jQuery.support.cors = true;

/********************************************
********************************************/

//To know Browser version:
/********************************************
//alert($.browser.msie);    //Return true if IE, undefined otherwise
//alert($.browser.version);

*/
/********************************************
General JS Functions - MyCookin
********************************************/

//Hide scroll bars
function HideScrollbars() {
    try {
        document.documentElement.style.overflow = 'hidden';  // firefox, chrome
        document.body.scroll = "no"; // ie only
    } catch (err) {
        console.log("Ops..: " + err);
    }
}
//==========================================

//MouseOver MouseOut

function MOver(picimage)//funzione che si attiva con OnMouseOver
{
    try {
        Picture_Over = eval(picimage + "On.src");
        document[picimage].src = Picture_Over;
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function MOut(picimage)//funzione che si attiva con OnMouseOut
{
    try {
        Picture_Out = eval(picimage + "Off.src");
        document[picimage].src = Picture_Out;
    } catch (err) {
        console.log("Ops..: " + err);
    }
}

//==========================================

/********************************************
********************************************/

/********************************************
BOX DIALOG - JQUERY

//Show JQueryUi BoxDialog - JS: ShowJQuiBoxDialog(Title, Text)
string BoxTitle = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-9999");
string BoxText = RetrieveMessage.RetrieveDBMessage(IDLanguage, "US-IN-0011");
ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialog('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "');", true);
********************************************/
function ShowJQuiBoxDialog(BoxTitle, BoxText) {
    try {
        //Add text to show in label lblResult
        $get('lblResult').innerHTML = BoxText.toString();
        //Show BoxDialog
        $("#pnlResult").dialog({
            modal: true,
            resizable: false,
            title: BoxTitle.toString(),
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

////Show JQueryUi BoxDialog With Redirect On Close - JS: ShowJQuiBoxDialogWithRedirect(Title, Text, RedirectUrl)
//string BoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
//string BoxText = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-0005");
//string RedirectUrl = AppConfig.GetValue("HomePage", AppDomain.CurrentDomain);
//ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowJQuiBoxDialogWithRedirect('" + BoxTitle.Replace("'", "\\'") + "','" + BoxText.Replace("'", "\\'") + "', '" + RedirectUrl + "');", true);

function ShowJQuiBoxDialogWithRedirect(BoxTitle, BoxText, RedirectUrl) {
    try {
        //Add text to show in label lblResult
        $get('lblResult').innerHTML = BoxText.toString();
        //Show BoxDialog
        $("#pnlResult").dialog({ modal: true,
            resizable: false,
            title: BoxTitle.toString(),
            close: function () { window.location.href = RedirectUrl; },
            buttons: {
                Ok: function () {
                    window.location.href = RedirectUrl;
                }
            }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

/********************************************
POPBOX INFO PANEL
********************************************/
function StartPopBox(pnlMainName, lnkName, pnlBoxName) {
    try {
        $(pnlMainName).popbox({
            'open': lnkName,
            'box': pnlBoxName,
            'arrow': '.arrow',
            'arrow-border': '.arrow-border',
            'close': '.close',
            'closefunction': ''
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function StartPopBoxAsync(pnlMainName, lnkName, pnlBoxName) {
    try {
        $(pnlMainName).popbox({
            'open': lnkName,
            'box': pnlBoxName,
            'arrow': '.arrow',
            'arrow-border': '.arrow-border',
            'close': '.close',
            'closefunction': ''
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function StartPopBoxWithCloseFunction(pnlMainName, lnkName, pnlBoxName) {
    try {
        $(pnlMainName).popbox({
            'open': lnkName,
            'box': pnlBoxName,
            'arrow': '.arrow',
            'arrow-border': '.arrow-border',
            'close': '.close',
            'closefunction': 'StartTimer'
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

/********************************************
********************************************/

/********************************************
PANEL WITH TAB - JQUERY
********************************************/
$(function () {
    try {
        $("#pnlMainTab").tabs({ fx: { height: 'toggle', opacity: 'toggle'} });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
});

/********************************************
ADULT CALENDAR - JQUERY
********************************************/

//function AdultCalendar(FieldID) {
//    $('input[id$=' + FieldID + ' ]').datepicker( {
//        changeYear: true,
//        changeMonth: false,
//        dateFormat: 'dd/mm/yy',
//        firstDay: 1,
//        maxDate: '-18y',
//        minDate: '-100y',
//        yearRange: 'c-100:c'
//        //showOn: 'button',
//        //buttonImage: '../../Images/calendar.gif',
//        //buttonImageOnly: true
//    });
//};

//Set Localization for Calendar
jQuery(function ($) {
    try {
        $.datepicker.setDefaults($.datepicker.regional['it']);
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
});


function AdultCalendar(FieldID, LanguageCode) {
    try {
        var CurrentDate = new Date();
        $('input[id$=' + FieldID + ' ]').datepicker({
            format: 'dd/mm/yyyy',
            weekStart: 1, //inizia da lunedì
            startView: 'decade',
            startDate: Date.DateAdd('yyyy', -118, CurrentDate),
            endDate: Date.DateAdd('yyyy', -18, CurrentDate),
            language: LanguageCode
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

/********************************************
VALIDATION SUMMARY IN BOX DIALOG - JQUERY
//Show ValidationSummary in JQuery BoxDialog
string SummaryBoxTitle = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-WN-9999");
ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ValidationSummaryInBoxDialog('" + btnLogin.ClientID + "', '" + vsLogin.ClientID + "', '" + pnlResult.ClientID + "', '" + SummaryBoxTitle + "');", true);
********************************************/
function ValidationSummaryInBoxDialog(ButtonID, ValidationSummaryID, DivResultID, BoxTitle) {
    try {
        $(document).ready(function () {
            $('#' + ButtonID).click(function () {
                document.TimeID = setTimeout('CheckMessage(\'' + ValidationSummaryID + '\', \'' + DivResultID + '\',\' ' + BoxTitle + '\');', 5);
            });
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function CheckMessage(ValidationSummaryID, DivResultID, BoxTitle) {
    try {
        if ($('div#' + ValidationSummaryID + ' UL' + '').length > 0) {
            //Empty Label (if you generated an error before. Ex. Wrong Usn or Psw)
            $get('lblResult').innerHTML = '';

            //Show BoxDialog With OnCloseEvent to Empty ResultPanel
            $("#pnlResult").dialog({ modal: true,
                resizable: false,
                title: BoxTitle.toString(),
                close: function () { $('div#' + ValidationSummaryID).empty(); }
            });

            clearTimeout(document.TimeID);
        }
        else {
            document.TimeID = setTimeout('CheckMessage(\'' + ValidationSummaryID + '\', \'' + DivResultID + '\',\' ' + BoxTitle + '\');', 5);
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

/********************************************
VALIDATION SUMMARY WITH TOOLTIP TIPSY
//Show ValidationSummary in JQuery BoxDialog 
*********************************************/
function ValidationSummaryWithTipsyTooltip(ButtonID, ValidationSummaryID, DivResultID, BoxTitle) {
    try {
        $(document).ready(function () {
            $('#' + ButtonID).click(function () {
                document.TimeID = setTimeout('CheckMessageForTooltip(\'' + ValidationSummaryID + '\', \'' + DivResultID + '\',\' ' + BoxTitle + '\');', 5);
            });
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}


function CheckMessageForTooltip(ValidationSummaryID, DivResultID, BoxTitle) {
    try {
        if ($('div#' + ValidationSummaryID + ' UL' + '').length > 0) {
            //Empty Label (if you generated an error before. Ex. Wrong Usn or Psw)
            $get('lblResult').innerHTML = '';

            $("#pnlResult").hide();

            //document.TimeID = setTimeout("\$('.tipsyTooltipError').tipsy('show');", 5);
            //$('.tipsyTooltipError').tipsy('show');
            $('.tipsyTooltipError').each(
                function () {
                    $(this).tipsy('show');
                }
            );

            //Hide ALL tooltip shown on the top left corner
            $('.tipsy[style*="left: 0px"]').each(
            function () {
                $(this).hide();
            }
            );

            clearTimeout(document.TimeID);
        }
        else {
            document.TimeID = setTimeout('CheckMessageForTooltip(\'' + ValidationSummaryID + '\', \'' + DivResultID + '\',\' ' + BoxTitle + '\');', 5);
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function HideAllTipsyTooltipError() {
    try {
        $('.tipsyTooltipError').each(
            function () {
                $(this).tipsy('hide');
            }
        );
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}


/********************************************
Crop Image - JQUERY . JCrop
********************************************/
function CallJCrop() {
    try {
        var minWidth;
        var minHeight;
        var CropAspectRatio;

        try {
            minWidth = $('#txtMinCropWidth').val();
            minHeight = $('#txtMinCropHeight').val();
            //alert(minWidth + ' ' + minHeight);
        }
        catch (e) {
            minWidth = 150;
            minHeight = 150;
        }

        try {
            CropAspectRatio = $('#txtCropAspectRatio').val();
        }
        catch (e) {
            CropAspectRatio = 1;
        }

        // The variable jcrop_api will hold a reference to the
        // Jcrop API once Jcrop is instantiated.
        var jcrop_api;

        // In this example, since Jcrop may be attached or detached
        // at the whim of the user, I've wrapped the call into a function
        initJcrop();

        // The function is pretty simple
        function initJcrop() {
            //set jcrop target
            jcrop_api = $.Jcrop('#imgUploadedImg', { onChange: showCoords,
                onSelect: showCoords,
                onRelease: clearCoords,
                aspectRatio: CropAspectRatio,
                minSize: [minWidth, minHeight]
            });

            //animate base selection
            jcrop_api.animateTo([50, 50, 300, 300]);

        }

        function showCoords(c) {
            $('#txtX1').val(parseInt(c.x.toString(), 10));
            $('#txtY1').val(parseInt(c.y.toString(), 10));
            $('#txtX2').val(parseInt(c.x2.toString(), 10));
            $('#txtY2').val(parseInt(c.y2.toString(), 10));
            $('#txtW').val(parseInt(c.w.toString(), 10));
            $('#txtH').val(parseInt(c.h.toString(), 10));
        }

        function clearCoords() {
            $('#coords input').val('');
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function CheckCrop(Title, Message) {
    try {
        if ($('#txtH').val() !== "" || $('#txtCropComplete').val() == "YES") {
            return true;
        }
        else {
            ShowJQuiBoxDialog(Title, Message);
            return false;
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
        return false;
    }
}

/********************************************
Modal Confirmation - JQUERY UI
********************************************/
var confirmed = false;

function JCOnfirm(objButton, BoxTitle, BoxText, ButtonOkTxt, ButtonNoTxt) {
    try {
        var btns = {};
        btns[ButtonOkTxt] = function () {
            $(this).dialog("close");
            confirmed = true;
            objButton.click();
        };
        btns[ButtonNoTxt] = function () {
            $(this).dialog("close");
            confirmed = false;
        };

        if (!confirmed) {
            $get('lblResult').innerHTML = BoxText.toString();

            $("#pnlResult").dialog({
                resizable: false,
                modal: true,
                title: BoxTitle.toString(),
                buttons: btns
            });
            return false;
        }
        else {
            confirmed = false;
            return true;
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
        return false;
    }
}

/********************************************
Check for write only number in a textbox
********************************************/

function isNumberKey(evt) {
    try {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if ((charCode > 47 && charCode < 58) || charCode == 44 || evt.keyCode == 46 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39)
            return true;

        return false;
    }
    catch (err) {
        console.log("Ops..: " + err);
        return false;
    }
}

/********************************************
Check for NO write special chars in a textbox
********************************************/
function isSpecialKey(evt) {
    try {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode < 48 && charCode != 46) || (charCode >= 58 && charCode <= 64) || (charCode >= 91 && charCode <= 96) || (charCode >= 123))

            return false;

        return true;
    }
    catch (err) {
        console.log("Ops..: " + err);
        return false;
    }
}
function isSpecialHTMLChar(evt) {
    try {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode == 34) || (charCode == 35) || (charCode == 36) || (charCode == 38) || (charCode == 47) || (charCode == 60) || (charCode == 62) || (charCode == 36) || (charCode == 92) || (charCode == 124)||(charCode == 126))
            return false;
        return true;
    }
    catch (err) {
        console.log("Ops..: " + err);
        return false;
    }
}
/*********************************************
Avoid writing numbers in a textbox
*********************************************/
function noWrite(evt) {
    return false;
}

/*********************************************
Focus on one textbox on page load
*********************************************/
function FocusOnLoad(IDTextBox) {
    try {
        $(document).ready(function () {
            $('#' + IDTextBox).focus();
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

/*********************************************
JS For Social Buttons
*********************************************/
//$(function () {
//    $(".login-buttons").tabs({ fx: { height: 'toggle', opacity: 'toggle'} }).show();
//});

function ShowSocialButtons() {
    try {
        //    function onLinkedInAuth() {
        //        $.ajax({
        //            //SSL Only
        //            url: "https://" + document.domain + "/auth/auth.aspx",
        //            dataType: 'jsonp',
        //            complete: function () {
        //                location.reload();
        //            }
        //        });
        //    };

        window.fbAsyncInit = function () {
            FB.init({
                appId: "476759249023668",     //App ID
                //channelUrl : '//WWW.YOUR_DOMAIN.COM/channel.html', // Channel File
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                oauth: true, // enable OAuth 2.0
                xfbml: true  // parse XFBML
            });
            $("fblogin").show();
        };

        (function (d) {
            var js, id = 'facebook-jssdk'; if (d.getElementById(id)) { return; }
            js = d.createElement('script'); js.id = id; js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";
            d.getElementsByTagName('head')[0].appendChild(js);
        } (document));
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

//In a repeater set the correct default button in a textbox (Ex.: UserProfile Insert new Comment)
//Ex.: ctrlUserBoardBlock.ascx.cs - txtNewComment.Attributes.Add("onkeypress", "return clickButton(event,'" + ibtnComment.ClientID + "')");
function clickButton(e, buttonid) {
    try {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {
            if (evt.keyCode == 13) {
                bt.click();
                return false;
            }
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
        return false;
    }
}

//Activate JqueryScroller (Ex.: UserProfile List of Comments)
//Ex.: ctrlUserBoardBlock.ascx.cs
//Notice: Insert in the page: <script type="text/javascript" src="/Js/Scrollbar/jquery.mCustomScrollbar.concat.min.js"></script>
//<asp:Panel runat="server" ID="pnlComments" CssClass="content"></Panel>
function ActivateScroller(DivId) {
    try {
        $("#" + DivId).mCustomScrollbar({
            autoHideScrollbar: true,
            advanced: { updateOnContentResize: true }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

//Auto resize for TextArea
function TextAreaAutoGrow(DivId) {
    try {
        $("#" + DivId).autosize();
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

//Noty
//*************************************
$.noty.defaults = {
    layout: 'bottomRight',
    theme: 'defaultTheme',
    type: 'information',
    text: '',
    dismissQueue: true, // If you want to use queue feature set this true
    template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
    animation: {
        open: { height: 'toggle' },
        close: { height: 'toggle' },
        easing: 'swing',
        speed: 500 // opening & closing animation speed
    },
    timeout: 5000, // delay for closing event. Set "false" for sticky notifications
    force: false, // adds notification to the beginning of queue when set to true
    modal: false,
    closeWith: ['click'], // ['click', 'button', 'hover']
    callback: {
        onShow: function () { },
        afterShow: function () { },
        onClose: function () { },
        afterClose: function () { }
    },
    buttons: false // an array of buttons
};

//Tooltip
//Activate Jquery Tooltip where the class MyTooltip is set
//********************************************************
$(document).ready(function () {
    try {
        $(".MyTooltip").tooltip({
            position: {
                my: "center bottom-20",
                at: "center top",
                using: function (position, feedback) {
                    $(this).css(position);
                    $("<div>").addClass("arrow").addClass(feedback.vertical).addClass(feedback.horizontal).appendTo(this);
                }
            }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
});
//*********************************************************

//Tooltip Tipsy
//Activate Jquery Tooltip where the class tipsyTooltip is set
//********************************************************
$(document).ready(function () {
    try {
        //DOC.: http://onehackoranother.com/projects/jquery/tipsy/
        $('.tipsyTooltip').tipsy({
            delayIn: 0, // delay before showing tooltip (ms)
            delayOut: 0, // delay before hiding tooltip (ms)
            fade: true, // fade tooltips in/out?
            //fallback: '',// fallback text to use when no tooltip text
            gravity: 'w', // gravity - nw | n | ne | w | e | sw | s | se
            html: true, // is tooltip content HTML?
            live: false, // use live event support?
            offset: 0, // pixel offset of tooltip from element
            opacity: 0.9, // opacity of tooltip
            //title: '',// attribute/callback containing tooltip text
            trigger: 'focus'// how tooltip is triggered - hover | focus | manual
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
});

//Tooltip for Show Error
$(document).ready(function () {
    try {
        //DOC.: http://onehackoranother.com/projects/jquery/tipsy/
        $('.tipsyTooltipError').tipsy({
            delayIn: 0, // delay before showing tooltip (ms)
            delayOut: 0, // delay before hiding tooltip (ms)
            fade: true, // fade tooltips in/out?
            //fallback: '',// fallback text to use when no tooltip text
            gravity: 'w', // gravity - nw | n | ne | w | e | sw | s | se
            html: true, // is tooltip content HTML?
            live: false, // use live event support?
            offset: 0, // pixel offset of tooltip from element
            opacity: 0.9, // opacity of tooltip
            //title: '',// attribute/callback containing tooltip text
            trigger: 'manual'// how tooltip is triggered - hover | focus | manual
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
});

//Tooltip for Note
$(document).ready(function () {
    try {
        //DOC.: http://onehackoranother.com/projects/jquery/tipsy/
        $('.tipsyInfoNoteW').tipsy({
            delayIn: 0, // delay before showing tooltip (ms)
            delayOut: 0, // delay before hiding tooltip (ms)
            fade: true, // fade tooltips in/out?
            //fallback: '',// fallback text to use when no tooltip text
            gravity: 'w', // gravity - nw | n | ne | w | e | sw | s | se
            html: true, // is tooltip content HTML?
            live: false, // use live event support?
            offset: 0, // pixel offset of tooltip from element
            opacity: 1, // opacity of tooltip
            //title: '',// attribute/callback containing tooltip text
            trigger: 'manual'// how tooltip is triggered - hover | focus | manual
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
});

//Tooltip for Note
$(document).ready(function () {
    try {
        //DOC.: http://onehackoranother.com/projects/jquery/tipsy/
        $('.tipsyInfoNote').tipsy({
            delayIn: 0, // delay before showing tooltip (ms)
            delayOut: 0, // delay before hiding tooltip (ms)
            fade: true, // fade tooltips in/out?
            //fallback: '',// fallback text to use when no tooltip text
            gravity: 'nw', // gravity - nw | n | ne | w | e | sw | s | se
            html: true, // is tooltip content HTML?
            live: false, // use live event support?
            offset: 0, // pixel offset of tooltip from element
            opacity: 1, // opacity of tooltip
            //title: '',// attribute/callback containing tooltip text
            trigger: 'manual'// how tooltip is triggered - hover | focus | manual
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
});
//********************************************************

//CHECK EMAIL
//********************************************************
function checkEmail(email) {
    try {
        if (email === "") {
            return false;
        }

        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

        if (emailReg.test(email)) {
            return true;
        }
        else {
            return false;
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
        return false;
    }
}
//********************************************************

function hasSpecialChar_v1(stringToCheck) {
    try {
        //Insert here Valid characters
        var RegEx = /^[a-zA-Z0-9_.-]*$/;

        if (RegEx.test(stringToCheck)) {
            return true;
        }
        else {
            return false;
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
        return false;
    }
}

function hasSpecialChar_v2(stringToCheck) {
    try {
        var RegEx = /^[a-zA-Z0-9ÀÖäëïöüâêîôûáàéèíìóòúù ']*$/;

        if (stringToCheck.trim() != "") {
            if (RegEx.test(stringToCheck)) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }
    catch (err) {
        return false;
    }
}

//Html Encode function
function htmlEncode(value) {
    try {
        return $('<div/>').text(value).html();
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function ShowJQuiModalPopUp(PanelID, TitleText, ImgToCropID, ButtoCropID) {
    try {
        //Show BoxDialog
        var PopUpWidth = 700;
        try {
            PopUpWidth = $(ImgToCropID).width() + 35;
        } catch (err) {
            PopUpWidth = 700;
        }

        $(PanelID).dialog({
            resizable: false,
            draggable: false,
            width: PopUpWidth,
            position: { my: "center center", at: "center", of: window },
            title: TitleText.toString(),
            close: function () { DestroyJCropNew(ImgToCropID); },
            closeOnEscape: true,
            modal: true,
            buttons: {
                Ok: function () {
                    __doPostBack(ButtoCropID, "");
                    $(this).dialog('close');
                }
            }
        });
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}


/********************************************
Crop Image - JQUERY . JCrop New Function
********************************************/
function DestroyJCropNew(ImgToCropID, ImageLoadedCheck) {
    try {
        var jcrop_api = $(ImgToCropID).data('Jcrop');
        try {
            jcrop_api.destroy();
        }
        catch (err) {
        }
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

function EditImageToCrop(ImgToCropID, IDMedia, MediaSizeType, FromBackupServer, AppendGuid, MinCropWidthID, MinCropHeightID, CropAspectRatioID,
                        CropX1ID, CropY1ID, CropX2ID, CropY2ID, CropWidth, CropHeight, PanelID, TitleText, ImgToCropID, ButtoCropID) {
    try {
        CallJCropNew(MinCropWidthID, MinCropHeightID, CropAspectRatioID, ImgToCropID,
                            CropX1ID, CropY1ID, CropX2ID, CropY2ID, CropWidth, CropHeight);
        ShowJQuiModalPopUp(PanelID, TitleText, ImgToCropID, ButtoCropID);
    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

//===============================
//CallJCropNew Globlal variables
//===============================
var g_minWidth;
var g_minHeight;
var g_CropAspectRatio;
var g_CropX1ID;
var g_CropX2ID;
var g_CropY1ID;
var g_CropY2ID;
var g_CropWidth;
var g_CropHeight;
var g_ImgToCropID;

function CallJCropNew(MinCropWidthID, MinCropHeightID, CropAspectRatioID, ImgToCropID,
                        CropX1ID, CropY1ID, CropX2ID, CropY2ID, CropWidth, CropHeight) {
    try {
        var minWidth;
        var minHeight;
        var CropAspectRatio;

        try {
            //minWidth = $(MinCropWidthID).val();
            //minHeight = $(MinCropHeightID).val();
            g_minWidth = $(MinCropWidthID).val();
            g_minHeight = $(MinCropHeightID).val();
            //alert(minWidth + ' ' + minHeight);
        }
        catch (e) {
            //minWidth = 150;
            //minHeight = 150;
            g_minWidth = 150;
            g_minHeight = 150;
        }

        try {
            //CropAspectRatio = $(CropAspectRatioID).val();
            g_CropAspectRatio = $(CropAspectRatioID).val();
        }
        catch (e) {
            //CropAspectRatio = 1;
            g_CropAspectRatio = 1;
        }

        // The variable jcrop_api will hold a reference to the
        // Jcrop API once Jcrop is instantiated.
        var jcrop_api;

        g_CropX1ID = CropX1ID;
        g_CropX2ID = CropX2ID;
        g_CropY1ID = CropY1ID;
        g_CropY2ID = CropY2ID;
        g_CropWidth = CropWidth;
        g_CropHeight = CropHeight;
        g_ImgToCropID = ImgToCropID;
        // In this example, since Jcrop may be attached or detached
        // at the whim of the user, I've wrapped the call into a function
        initJcrop();

        //        // The function is pretty simple
        //        function initJcrop() {
        //            //set jcrop target
        //            jcrop_api = $.Jcrop(ImgToCropID, {
        //                onChange: showCoords,
        //                onSelect: showCoords,
        //                onRelease: clearCoords,
        //                aspectRatio: CropAspectRatio,
        //                minSize: [minWidth, minHeight]
        //            });

        //            //animate base selection
        //            jcrop_api.animateTo([50, 50, 300, 300]);

        //        };

        //        function showCoords(c) {
        //            $(CropX1ID).val(parseInt(c.x.toString()));
        //            $(CropY1ID).val(parseInt(c.y.toString()));
        //            $(CropX2ID).val(parseInt(c.x2.toString()));
        //            $(CropY2ID).val(parseInt(c.y2.toString()));
        //            $(CropWidth).val(parseInt(c.w.toString()));
        //            $(CropHeight).val(parseInt(c.h.toString()));
        //        };

        //        function clearCoords() {
        //            $(CropX1ID).val('');
        //            $(CropY1ID).val('');
        //            $(CropX2ID).val('');
        //            $(CropY2ID).val('');
        //            $(CropWidth).val('');
        //            $(CropHeight).val('');
        //        };

    }
    catch (err) {
        console.log("Ops..: " + err);
    }
}

// The function is pretty simple
function initJcrop() {
    //set jcrop target
    jcrop_api = $.Jcrop(g_ImgToCropID, {
        onChange: showCoords,
        onSelect: showCoords,
        onRelease: clearCoords,
        aspectRatio: g_CropAspectRatio,
        minSize: [g_minWidth, g_minHeight]
    });

    //animate base selection
    jcrop_api.animateTo([50, 50, 300, 300]);

}

function showCoords(c) {
    $(g_CropX1ID).val(parseInt(c.x.toString(), 10));
    $(g_CropY1ID).val(parseInt(c.y.toString(), 10));
    $(g_CropX2ID).val(parseInt(c.x2.toString(), 10));
    $(g_CropY2ID).val(parseInt(c.y2.toString(), 10));
    $(g_CropWidth).val(parseInt(c.w.toString(), 10));
    $(g_CropHeight).val(parseInt(c.h.toString(), 10));
}

function clearCoords() {
    $(g_CropX1ID).val('');
    $(g_CropY1ID).val('');
    $(g_CropX2ID).val('');
    $(g_CropY2ID).val('');
    $(g_CropWidth).val('');
    $(g_CropHeight).val('');
}

function ReplaceSpecialChar(TextToCheck, ReplaceChar) {
    return TextToCheck.replace(/[^a-zA-Z0-9]/g, ReplaceChar);
}
function ReplaceSpecialChar2(TextToCheck, ReplaceChar) {
    return TextToCheck.replace(/[$^|&:;\{\}\[\]\\\/\<\>]/gi, ReplaceChar);
}
function CheckMinuteField(txtHoursID, txtMinuteID) {
    var _minutes = parseInt($('#' + txtMinuteID).val(),10);
    if (_minutes > 59) {
        var _hours = parseInt($('#' + txtHoursID).val(), 10);
        _hours = _hours + 1;
        _minutes = 0;
        $('#' + txtMinuteID).val(_minutes);
        $('#' + txtHoursID).val(_hours);
    }
}
function InizializeMultiUpload(maxFileToUpload, maxFileSizeInMB, lnkSelectFiles, pnlUploadButton, pnlUploading,
                                    pnlUploadContainer, hfUploadHandlerURL, hfUploadConfig, hfBaseFileName,
                                    hfMediaOwner, hfAllowedFileTypes, pnlFileList,
                                    pnlErrorAndWarning, hfMaxFileNumErrorMessage, hfErrorReportFromServer,
                                    btnReset, hfFileCreatedIDsList, btnPostBackEvent, lnkUploadFiles) {

    var fileUploaded = 0;
    var fileAdded = 0;
    var MaxFileToUpload = maxFileToUpload;
    var MaxFileSizeInMB = maxFileSizeInMB;
    try {
        MaxFileToUpload = parseInt(MaxFileToUpload);
    }
    catch (er) {
        MaxFileToUpload = 10;
    }

    try {
        MaxFileSizeInMB = parseInt(MaxFileSizeInMB);
    }
    catch (er) {
        MaxFileSizeInMB = 3;
    }
    $('#' + lnkSelectFiles).css({ 'display': 'inline-block' });
    $(pnlUploadButton).css({ 'display': 'none' });
    $(pnlUploading).css({ 'display': 'none' });
    var uploader = new plupload.Uploader({
        runtimes: 'html5,silverlight,flash,gears,html4',
        browse_button: lnkSelectFiles,
        container: pnlUploadContainer,
        max_file_size: MaxFileSizeInMB.toString() + 'mb',
        multi_selection: true,
        unique_names: true,
        url: hfUploadHandlerURL + '?UploadConfig=' + hfUploadConfig + '&baseFileName=' + hfBaseFileName + '&IDMediaOwner=' + hfMediaOwner,
        //resize : {width : 320, height : 240, quality : 90},
        flash_swf_url: '/Js/PlUpload/plupload.flash.swf',
        silverlight_xap_url: '/Js/PlUpload/plupload.silverlight.xap',
        filters: [{ title: "Files", extensions: hfAllowedFileTypes }]
    });
    uploader.bind('Init', function (up, params) {
        GetElementByID(pnlFileList).innerHTML = "";
        $(pnlUploadButton).css({ 'display': 'none' });
    });

    uploader.init();

    uploader.bind('FilesAdded', function (up, files) {
        $(pnlUploadButton).css({ 'display': 'inline-block' });
        GetElementByID(pnlErrorAndWarning).innerHTML = "";
        var fileToAdd = MaxFileToUpload
        for (var i in files) {
            if (i < fileToAdd) {
                fileAdded++;
                MaxFileToUpload--;
                GetElementByID(pnlFileList).innerHTML += '<div id="' + files[i].id + '">' + files[i].name + ' (' + plupload.formatSize(files[i].size) + ') <b></b></div>';
            }
            else {
                uploader.removeFile(files[i]);
            }
        }
        if (files.length >= MaxFileToUpload || 1 == 1) {
            $('#' + lnkSelectFiles).css({ 'display': 'none' });
            //uploader.settings.url = hfUploadHandlerURL + '?UploadConfig=' + hfUploadConfig + '&baseFileName=' + hfBaseFileName + '&IDMediaOwner=' + hfMediaOwner;
            $(pnlUploading).css({ 'display': 'inline-block' });
            if (hfBaseFileName == "") {
                try {
                    hfBaseFileName = $('#hfBaseFileName').val();
                }
                catch (err)
                { }
                uploader.settings.url = hfUploadHandlerURL + '?UploadConfig=' + hfUploadConfig + '&baseFileName=' + hfBaseFileName + '&IDMediaOwner=' + hfMediaOwner;
            }
            uploader.start();
            $(pnlUploadButton).css({ 'display': 'none' });
        }
    });

    uploader.bind('UploadProgress', function (up, file) {
        GetElementByID(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
    });

    uploader.bind('FileUploaded', function (up, file, info) {
        if (info['response'].indexOf("Error:") > -1) {
            GetElementByID(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + info['response'].replace("Error:", "") + "</span>";
            $(hfErrorReportFromServer).val($(hfErrorReportFromServer).val() + info['response'].replace("Error:", "") + htmlEncode("<br/>"));
            fileUploaded++;
            ShowJQuiBoxDialog('Error', info['response'].replace("Error:", ""));
            if (fileUploaded == fileAdded) {
                __doPostBack(btnReset, "");
            }
        }
        else {
            GetElementByID(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "% </span>";
            $(hfFileCreatedIDsList).val($(hfFileCreatedIDsList).val() + info['response'].replace("Success:", "") + ",");
            fileUploaded++;
            if (fileUploaded == fileAdded) {
                $(pnlUploading).css({ 'display': 'none' });
                __doPostBack(btnPostBackEvent, "");
                fileUploaded = 0;
                fileAdded = 0;
            }
        }

    });

    GetElementByID(lnkUploadFiles).onclick = function () {
        $(pnlUploading).css({ 'display': 'inline-block' });
        if (hfBaseFileName == "") {
            try {
                hfBaseFileName = $('#hfBaseFileName').val();
            }
            catch (err)
            { }
            uploader.settings.url = hfUploadHandlerURL + '?UploadConfig=' + hfUploadConfig + '&baseFileName=' + hfBaseFileName + '&IDMediaOwner=' + hfMediaOwner;
        }
        uploader.start();
        $(pnlUploadButton).css({ 'display': 'none' });
        return false;
    };
}
function GetElementByID(id) {
    return document.getElementById(id);
}