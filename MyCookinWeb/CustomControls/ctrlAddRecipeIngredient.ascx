<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlAddRecipeIngredient.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlAddRecipeIngredient" %>

<div id="HiddenFieldInfo" style="visibility:hidden; height:0px">
    <asp:HiddenField ID="txtObjectID" runat="server" />
    <asp:HiddenField ID="txtIsError" runat="server" />
    <asp:HiddenField ID="txtErrorMessage" runat="server" />
    <asp:HiddenField ID="txtObjectLabelIdentifier" runat="server" />
    <asp:HiddenField ID="txtObjectIDIdentifier" runat="server" />
    <asp:HiddenField ID="hfLanguageCode" runat="server" />
    <asp:HiddenField ID="hfLangFieldLabel" runat="server" />
    <asp:HiddenField ID="hfWordFieldLabel" runat="server" />
    <asp:HiddenField ID="hfMethodName" runat="server" />
    <asp:HiddenField ID="hfMinLenght" runat="server" />
    <asp:HiddenField ID="hfObjectLabelText" runat="server" />
    <asp:HiddenField ID="hfAlternativeIngredient" runat="server" />
    <asp:HiddenField ID="hfIDRecipe" runat="server" />
    <asp:HiddenField ID="hfRecipeIngredientGroupNumber" runat="server" />
    <asp:HiddenField ID="hfRecipeIngredientGroupName" runat="server" />
    <%--//Output Message--%>
    <asp:HiddenField ID="hfInsertRecipeIngredientIsError" runat="server" />
    <asp:HiddenField ID="hfInsertRecipeIngredientMessage" runat="server" />
    <asp:HiddenField ID="hfInsertAltRecipeIngredientIsError" runat="server" />
    <asp:HiddenField ID="hfInsertAltRecipeIngredientMessage" runat="server" />
</div>
<script type="text/javascript" language="javascript" src="/Js/CustomControls/ctrlRecipeIngredient.js"></script>

<asp:Panel ID="pnlIngredient"  CssClass="pnlIngredientQta" runat="server" 
    meta:resourcekey="pnlIngredientResource1">
    <asp:UpdatePanel ID="upnIngr" runat="server">
        <ContentTemplate>
    <div style="display:block; margin-top:5px; margin-bottom:10px;">
        <div style="display:inline-block;">
            <asp:Label ID="lblIngrGroup" runat="server" Text="Gruppo Ingrediente"  meta:resourcekey="lblIngrGroupNameResource"></asp:Label>
        </div>
        <div style="display:inline-block;">
            <asp:DropDownList ID="ddlIngredientGroup" runat="server">
                <asp:ListItem Text="Ricetta" Value="0" meta:resourcekey="ddlIngredientGroupList0"/>
                <asp:ListItem Text="Per l'impasto" Value="1" meta:resourcekey="ddlIngredientGroupList1"/>
                <asp:ListItem Text="Per la farcitura" Value="2" meta:resourcekey="ddlIngredientGroupList2"/>
                <asp:ListItem Text="Per il ripieno" Value="3" meta:resourcekey="ddlIngredientGroupList3"/>
                <asp:ListItem Text="Per il condimento" Value="4" meta:resourcekey="ddlIngredientGroupList4"/>
                <asp:ListItem Text="Per la decorazione" Value="5" meta:resourcekey="ddlIngredientGroupList5"/>
            </asp:DropDownList>
        </div>
    </div>
    <asp:Label ID="lblObjectName" runat="server" meta:resourcekey="lblObjectNameResource1"></asp:Label><br />
    <asp:TextBox ID="txtObjectName" runat="server" 
                meta:resourcekey="txtObjectNameResource1"></asp:TextBox>
    <asp:UpdatePanel ID="pnlIngredientQtaType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Panel ID="pnlIngredientQtaTypeInt" CssClass="pnlIngredientQta" runat="server" 
                meta:resourcekey="pnlIngredientQtaTypeIntResource1">
            <asp:Label ID="lblIngrNote" runat="server" 
                Text="Note Ingrediente (fresche, grandi, mature)" 
                meta:resourcekey="lblIngrNoteResource1"></asp:Label><br />
            <asp:TextBox ID="txtIngrNote" runat="server" 
                meta:resourcekey="txtIngrNoteResource1"></asp:TextBox><br />
            <asp:Label ID="lblIngredientQtaType" runat="server" 
                meta:resourcekey="lblIngredientQtaTypeResource1"></asp:Label><br />
            <asp:DropDownList ID="ddlQtaType" runat="server" 
                meta:resourcekey="ddlQtaTypeResource1">
            </asp:DropDownList>
            </asp:Panel>          
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlIngredientQta" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Panel ID="pnlIngredientQtaInt" CssClass="pnlIngredientQta" runat="server" 
                meta:resourcekey="pnlIngredientQtaIntResource1">
            <asp:Label ID="lblQta" runat="server" CssClass="lblQta"
                Text="Select a value or specify a quantity" meta:resourcekey="lblQtaResource1"></asp:Label>
            <asp:Panel ID="pnlQtaNotStd" runat="server" CssClass="pnlQtaStd"
                meta:resourcekey="pnlQtaNotStdResource1">
                <asp:DropDownList ID="ddlQtaNotStd" runat="server" 
                    meta:resourcekey="ddlQtaNotStdResource1">
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="pnlQtaStd" CssClass="pnlQtaStd" runat="server" 
                Style="display: none" meta:resourcekey="pnlQtaStdResource1">
                <asp:TextBox ID="txtQtaStd" runat="server" Style="width: 50px;" MaxLength="4" onpaste="return false"
                    onkeypress="return isNumberKey(event)" 
                    meta:resourcekey="txtQtaStdResource1"></asp:TextBox>
            </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlPrincipalForRecipe" CssClass="pnlPrincipalForRecipe" runat="server" 
                Style="display: none" meta:resourcekey="pnlPrincipalForRecipeResource1">
        <asp:CheckBox ID="chkPrincipalForRecipe" runat="server" Visible="False" 
            meta:resourcekey="chkPrincipalForRecipeResource1" />
        <asp:DropDownList ID="ddlIngredientRelevance" runat="server" 
            meta:resourcekey="ddlIngredientRelevanceResource1">
        </asp:DropDownList>
        <asp:Label ID="lblPrincipalForRecipe" runat="server" 
            Text="<br />Esempio: nella parmigiana di melenzane l'ingrediente PRINCIPALE è la melanzana, <br />il pomodoro è NECESSARIO, mentre il pepe è OPZIONALE." 
            meta:resourcekey="lblPrincipalForRecipeResource1"></asp:Label>
    </asp:Panel>
    <br /><br />
     <asp:Panel ID="pnlIngredientAlternatives" CssClass="pnlPrincipalForRecipe" 
                runat="server" Style="display: none" 
                meta:resourcekey="pnlIngredientAlternativesResource1">
         <asp:Label ID="lblIngredientAlternatives" runat="server" 
             Text="Alternative Ingredient:" 
             meta:resourcekey="lblIngredientAlternativesResource1"></asp:Label><br />
         <asp:Panel ID="pnlIngredientAlternativesInternal" 
             CssClass="pnlPrincipalForRecipe" runat="server" 
             meta:resourcekey="pnlIngredientAlternativesInternalResource1">
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlAddButton" CssClass="pnlAddIngredient" runat="server">
        <div style="display:inline-block;top:10px;vertical-align:middle;margin-bottom:5px;">
            <img src="/Images/icon-add.png" alt="Add" width="15" height="15"/>
        </div>
        <div style="display:inline-block;">
            <asp:Button ID="btnAddIngredient" runat="server" Text="Aggiungi Ingrediente" CssClass="MyButtonSmall"
            ToolTip="Aggiungi Ingrediente" OnClick="btnAddIngredient_Click" 
                meta:resourcekey="btnAddIngredientResource1" />
            <asp:UpdateProgress ID="upInsertingIngredient" runat="server" 
                AssociatedUpdatePanelID="upnIngr">
                <ProgressTemplate>
                    <asp:Label ID="lblSaving" runat="server" Text="Aggiunta ingrediente in corso" 
                        meta:resourcekey="lblSavingResource1"></asp:Label>
                    <asp:Image ID="imgLoading" runat="server" 
                        ImageUrl="/Images/loadingLineOrange.gif" Height="20px" Width="220px" 
                        meta:resourcekey="imgLoadingResource1" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </asp:Panel>

    <style type="text/css">
        .ui-autocomplete
        {
            height: 200px;
            overflow:auto;
            padding-right: 20px;
        }
    </style>
    <script type="text/javascript">
        function AddRecipeIngredientStartAutoComplete() {
        /*
        * jQuery UI Autocomplete Select First Extension
        *
        * Copyright 2010, Scott González (http://scottgonzalez.com)
        * Dual licensed under the MIT or GPL Version 2 licenses.
        *
        * http://github.com/scottgonzalez/jquery-ui-extensions
        */
        (function ($) {

            $(".ui-autocomplete-input").live("autocompleteopen", function () {
                var autocomplete = $(this).data("autocomplete"),
		menu = autocomplete.menu;

                menu.activate($.Event({ type: "mouseenter" }), menu.element.children().first());
            });

        } (jQuery));
        /*
        *END jQuery UI Autocomplete Select First Extension
        */

        $(function () {
            $('#<%=hfAlternativeIngredient.ClientID%>').val('');
            $('#<%=txtObjectName.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        //data: "{ 'words': '" + request.term + "', 'LangCode': '<%=LanguageCode%>'}",
                        data: "{ '<%=WordFieldLabel%>': '" + request.term.replace("'", "\\'") + "', '<%=LangFieldLabel%>': '<%=LanguageCode%>'}",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        //url: "http://" + WebServicesPath + "/City/FindCity.asmx/SearchCities",
                        url: "http://" + WebServicesPath + "<%=MethodName%>",
                        crossDomain: true,
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item['<%=txtObjectLabelIdentifier.Value%>'],
                                    value: item['<%=txtObjectIDIdentifier.Value%>']
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(textStatus);
                            $('#<%=txtIsError.ClientID%>').val('true');
                            $('#<%=txtErrorMessage.ClientID%>').val(textStatus);
                        }
                    });
                },
                minLength: '<%=MinLenght%>',

                //Prevent showing ID instead of Label.
                focus: function (event, ui) {
                    //this.value = ui.item.label;
                    event.preventDefault(); // Prevent the default focus behavior.
                },

                //Triggered when an item is selected from the menu.
                select: function (event, ui) {

                    //If item selected, return his value, else return input (what has been written in the textbox)
                    //ShowItemSelected(ui.item ? ui.item.value : this.value)
                    ShowItemSelected(ui.item, '#<%=txtObjectID.ClientID%>', '#<%=txtObjectName.ClientID%>')
                    //DDL allowed Quantity Type
                    ShowQtaTypeDDL(ui.item.value, '<%=LanguageCode%>', '#<%=ddlQtaType.ClientID%>',
                                '#<%=pnlIngredientQtaType.ClientID%>', '#<%=pnlIngredientQta.ClientID%>',
                                '#<%=ddlQtaNotStd.ClientID%>', '#<%=pnlQtaNotStd.ClientID%>', '<%=LangFieldLabel%>', '<%=LanguageCode%>', '#<%=pnlPrincipalForRecipe.ClientID%>')
                    ShowQtaStdPanel();

                    GetAlternativesIngredient('#<%=pnlIngredientAlternatives.ClientID%>', '#<%=pnlIngredientAlternativesInternal.ClientID%>', ui.item.value, '<%=LangFieldLabel%>', '<%=LanguageCode%>', '#<%=hfAlternativeIngredient.ClientID%>')
                    return false;
                }
            });
        });

        //Reset DDL and Panel settings
        //====================================================
        $('#<%=ddlQtaType.ClientID%>').html("");
        $('#<%=ddlQtaNotStd.ClientID%>').html("");
        $('#<%=pnlQtaStd.ClientID%>').css({ 'display': 'none' });
        $('#<%=pnlAddButton.ClientID%>').css({ 'display': 'none' });
        //$('#<%=pnlQtaStd.ClientID%>').css({ 'height': '0' });
        $('#<%=pnlQtaNotStd.ClientID%>').css({ 'display': 'none' });
        $('#<%=pnlQtaNotStd.ClientID%>').css({ 'height': '0' });
        $('#<%=pnlPrincipalForRecipe.ClientID%>').css({ 'display': 'none' });
        //$('#<%=pnlPrincipalForRecipe.ClientID%>').css({ 'height': '0' });
        $('#<%=pnlIngredientQtaType.ClientID%>').css({ 'display': 'none' });
        $('#<%=pnlIngredientQtaType.ClientID%>').css({ 'height': '0' });
        $('#<%=pnlIngredientQta.ClientID%>').css({ 'display': 'none' });
        //$('#<%=pnlIngredientQta.ClientID%>').css({ 'height': '0' });
        $('#<%=txtQtaStd.ClientID%>').val('');
        $('#<%=txtIngrNote.ClientID%>').val('');
        $('#<%=chkPrincipalForRecipe.ClientID%>').attr('checked', false);
        //$('#<%=pnlIngredient.ClientID%>').css({ 'float': 'none' });
    }
//    function pageLoad() {
//        AddRecipeIngredientStartAutoComplete();
//    }

        //Compile DDL allowed Quantity Not Standard 
        //====================================================
        $('#<%=ddlQtaType.ClientID%>').live('change', function (e) {
            //alert('<%=LanguageCode%>');
            ShowQtaNotStdDDL($('#<%=ddlQtaType.ClientID%>').val(), '<%=LanguageCode%>', '#<%=ddlQtaNotStd.ClientID%>', '#<%=pnlQtaNotStd.ClientID%>', '<%=LangFieldLabel%>');
            ShowQtaStdPanel();
        });

        //Manage the visibility of Qta text field
        //=====================================================
        $('#<%=ddlQtaNotStd.ClientID%>').live('change', function (e) {
            ShowQtaStdPanel();
            //alert('<%=LanguageCode%>');
        });

        //Manage panel qta visibility
        //=====================================================
        function ShowQtaStdPanel() {
            if ($('#<%=ddlQtaNotStd.ClientID%>').val() == '' || $('#<%=ddlQtaNotStd.ClientID%>').val() == null) {
                $('#<%=pnlQtaStd.ClientID%>').css({ 'display': 'inline-block' });
                //$('#<%=pnlAddButton.ClientID%>').css({ 'display': 'block' });
                $('#<%=pnlQtaStd.ClientID%>').animate({height: '30px'},500);
            }
            else {
                //$('#<%=pnlQtaStd.ClientID%>').animate({ height: 'toggle' }, 500);
                $('#<%=pnlQtaStd.ClientID%>').css({ 'display': 'none' });
                //$('#<%=pnlAddButton.ClientID%>').css({ 'display': 'none' });
            }
            $('#<%=pnlAddButton.ClientID%>').css({ 'display': 'block' });
        }
    </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
