<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlRecipeIngredient.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlRecipeIngredient" %>

<div id="HiddenFieldInfo" style="visibility:hidden; height:0px">
    <asp:HiddenField ID="txtObjectID" runat="server" />
    <asp:HiddenField ID="txtIsError" runat="server" />
    <asp:HiddenField ID="txtErrorMessage" runat="server" />
    <asp:HiddenField ID="txtObjectLabelIdentifier" runat="server" />
    <asp:HiddenField ID="txtObjectIDIdentifier" runat="server" />
</div>
<script type="text/javascript" language="javascript" src="/Js/CustomControls/ctrlRecipeIngredient.js"></script>
<asp:Panel ID="pnlIngredient" runat="server">
    <table style=" vertical-align:middle; border:0px">
        <tr>
            <td>
                <asp:Label ID="lblObjectName" runat="server"></asp:Label><br />
                <asp:TextBox ID="txtObjectName" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Panel ID="pnlIngredientQtaType" runat="server" Style="visibility: hidden">
                <asp:Label ID="lblIngredientQtaType" runat="server" Text="Quantity type"></asp:Label><br />
                    <asp:DropDownList ID="ddlQtaType" runat="server">
                    </asp:DropDownList>
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="pnlIngredientQta" runat="server" Style="visibility: hidden">
                <asp:Label ID="lblQta" runat="server" Text="Select a value or specify a quantity"></asp:Label>
                    <table style=" vertical-align:middle; border:0px">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlQtaNotStd" runat="server" Style="visibility: hidden">
                                    <asp:DropDownList ID="ddlQtaNotStd" runat="server">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtQtaStd" runat="server" Style="width: 40px;" MaxLength="5" onpaste="return false"
                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="pnlPrincipalForRecipe" runat="server" Style="visibility: hidden">
                    <asp:Label ID="lblPrincipalForRecipe" runat="server" Text="Principal Ingredient"></asp:Label><br />
                    <asp:CheckBox ID="chkPrincipalForRecipe" ToolTip="" runat="server" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <style type="text/css">
        .ui-autocomplete
        {
            height: 200px;
            overflow:auto;
            padding-right: 20px;
        }
    </style>
    
    <script type="text/javascript">
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
            $('#<%=txtObjectName.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        //data: "{ 'words': '" + request.term + "', 'LangCode': '<%=LanguageCode%>'}",
                        data: "{ '<%=WordFieldLabel%>': '" + request.term + "', '<%=LangFieldLabel%>': '<%=LanguageCode%>'}",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
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
                    return false;
                }
            });
        });

        //Compile DDL allowed Quantity Not Standard 
        //====================================================
        $('#<%=ddlQtaType.ClientID%>').change(function () {
            //alert('<%=LanguageCode%>');
            ShowQtaNotStdDDL($('#<%=ddlQtaType.ClientID%>').val(), '<%=LanguageCode%>', '#<%=ddlQtaNotStd.ClientID%>', '#<%=pnlQtaNotStd.ClientID%>', '<%=LangFieldLabel%>');
        });

    </script>
</asp:Panel>
