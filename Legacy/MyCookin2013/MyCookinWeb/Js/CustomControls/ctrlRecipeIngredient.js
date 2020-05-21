/********************************************
ctrlRecipeIngredient.ascx - MyCookin
JS code for control only purpose
********************************************/

function ShowItemSelected(itemSelected,objID,objName) {
    $(objID).val(itemSelected.value);
    $(objName).val(itemSelected.label);
}

//Compile DDL allowed Quantity Type 
//====================================================
function ShowQtaTypeDDL(IDIngredient, IDLang, ddlQta, pnlQtaType, pnlQta, ddlNoStd, pnlQtaNoStd,LangField,LangCode,pnlPrincipalIngr) {
    //alert(IDIngredient + " - " + IDLang);
    $(ddlQta).html("");
    $(pnlQtaType).css({ 'display': 'block' });
    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/Ingredient/IngredientInfo.asmx/IngredientsQuantityType",
        data: "{ IDIngredient: '" + IDIngredient + "', " + LangField + ": '" + LangCode + "'}",
        success: function (msg) {
            BindQtaTypeDDL(msg.d, pnlQta, ddlNoStd, pnlQtaType, pnlQtaNoStd, ddlQta, LangField, LangCode, pnlPrincipalIngr);
        }
    });
}

function BindQtaTypeDDL(msg, pnlQta, ddlNoStd, pnlQtaType, pnlQtaNoStd, ddlQta, LangField, LangCode, pnlPrincipalIngr) {
    $.each(msg, function () {

        $(ddlQta).append($("<option></option>").val(this['IDIngredientQuantityType']).html(this['IngredientQuantityTypeSingular']));

    });
    //Compile first value of DDL allowed Quantity Not Standard 
    //alert($('#<%=ddlQtaType.ClientID%>').val());
    if ($(ddlQta).val() != null) {
        ShowQtaNotStdDDL($(ddlQta).val(), LangCode, ddlNoStd, pnlQtaNoStd, LangField);
        $(pnlQta).css({ 'display': 'block' });
        $(pnlPrincipalIngr).css({ 'display': 'block' });
        //$(pnlQta).animate({ height: '50px' }, 500);
        //$(pnlPrincipalIngr).animate({ height: '50px' }, 500);

    }
    else {
       // $(pnlPrincipalIngr).animate({ height: '0px' }, 500);
        //$(pnlQta).animate({ height: '0px' }, 500);
        //$(pnlQtaType).animate({ height: '0px' }, 500);
        $(ddlNoStd).html("");
        $(pnlQtaType).css({ 'display': 'none' });
        $(pnlQta).css({ 'display': 'none' });
        $(pnlPrincipalIngr).css({ 'display': 'none' });
    }
}
//====================================================


//Compile DDL allowed Quantity Not Standard 
//====================================================

function ShowQtaNotStdDDL(IDQtaType, LangCode, ddlQtaNoStd, pnlQtaNoStd, LangField) {
    //alert("{ IDIngredientQuantityType : '" + IDQtaType + "', " + LangField + ": '" + LangCode + "'}");
    $(ddlQtaNoStd).html("");
    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/Ingredient/IngredientInfo.asmx/IngredientAllowedQuantityNotStd",
        data: "{ IDIngredientQuantityType : '" + IDQtaType + "', " + LangField + ": '" + LangCode + "'}",
        success: function (msg) {
            BindQtaNotStdDDL(msg.d, ddlQtaNoStd, pnlQtaNoStd, LangCode)
        }
    });
}

function BindQtaNotStdDDL(msg, ddlQtaNoStd, pnlQtaNoStd, LangCode) {
    $(ddlQtaNoStd).append($("<option></option>").val("").html(GetQtaNoStdEmpty(LangCode)));
    $.each(msg, function () {
        $(ddlQtaNoStd).append($("<option></option>").val(this['IDQuantityNotStd']).html(this['QuantityNotStdSingular']));
    });

    if ($(ddlQtaNoStd).val() != null && msg != '') {
        $(pnlQtaNoStd).css({ 'display': 'inline-block' });
        $(pnlQtaNoStd).animate({ height: '30px' }, 500);
    }
    else {
        $(pnlQtaNoStd).css({ 'display': 'none' });
    }
}
var ddlQtaNoStdEmptyVal = {
    '2': 'Quantità',
    '3': 'Cantidad',
    '1': 'Quantity',
    '4': 'Quantity',
    '5': 'Quantity'
};
var ddlQtaNoStdEmpty = "";
function GetQtaNoStdEmpty(idLanguage) {
    ddlQtaNoStdEmpty = ddlQtaNoStdEmptyVal[idLanguage];
    if (ddlQtaNoStdEmpty == "") {
        ddlQtaNoStdEmpty = ddlQtaNoStdEmptyVal['1'];
    }
    return ddlQtaNoStdEmpty;
}
//====================================================

//Compile AlternativeIngredient Panel
//====================================================

function GetAlternativesIngredient(pnlIngredientAlternatives, pnlIngredientAlternativesInternal, IDIngredient, LangField, LangCode, ResultValueField) {
    var CountItem = 0;
    $(pnlIngredientAlternativesInternal).html("");
    $(pnlIngredientAlternatives).css({ 'display': 'block' });
    $.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "http://" + WebServicesPath + "/Ingredient/IngredientInfo.asmx/IngredientAlternative",
        data: "{ IDIngredientMain: '" + IDIngredient + "', " + LangField + ": '" + LangCode + "'}",
        success: function (msg) {
            $.each(msg.d, function () {
                $(pnlIngredientAlternativesInternal).append($("<input type=\"checkbox\" id=\"" + this['IDIngredient'] + "\" class=\"chkIngrAlt\" ></input>" + this['IngredientSingular'] + "<br/>").val(this['IDIngredient']).html(this['IngredientSingular']));
                CountItem++;
                $("#" + this['IDIngredient']).click(function () {
                    if ($(this).is(':checked')) {
                        //alert($(this).val());
                        AddAltIngrValue($(this).val(), ResultValueField);
                    }
                    else {
                        //alert("Unchecked");
                        removeAltIngrValue($(this).val(), ResultValueField);
                    }
                });
                //END ForEach
            });
            //alert(CountItem);
            if (CountItem == 0) {
               // alert(CountItem);
                $(pnlIngredientAlternatives).css({ 'display': 'none' });
            }
        }
    });
}

function AddAltIngrValue(IngrID, ValueField) {
    if ($(ValueField).val() == "") {
        $(ValueField).val($(ValueField).val() + "," + IngrID + ",");
    }
    else {
        $(ValueField).val($(ValueField).val() + IngrID + ",");
    }
    //alert($(ValueField).val());
}

function removeAltIngrValue(IngrID, ValueField) {
    $(ValueField).val($(ValueField).val().replace("," + IngrID + ",", ","));
    if ($(ValueField).val() == ",") {
        $(ValueField).val("");
    }
    //alert($(ValueField).val());
}

//====================================================