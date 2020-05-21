<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoComplete.ascx.cs" Inherits="MyCookinWeb.CustomControls.AutoComplete" %>

<div id="HiddenFieldInfo" style="visibility:hidden; height:0px">
<asp:TextBox ID="txtObjectID" runat="server"></asp:TextBox>
<asp:TextBox ID="txtIsError" runat="server"></asp:TextBox>
<asp:TextBox ID="txtErrorMessage" runat="server"></asp:TextBox>
<asp:TextBox ID="txtObjectLabelIdentifier" runat="server"></asp:TextBox>
<asp:TextBox ID="txtObjectIDIdentifier" runat="server"></asp:TextBox>
</div>
<%-- Panel for Search --%>
<asp:Panel ID="pnlSearch" ClientIDMode="Static" runat="server">

    <asp:Label ID="lblObjectName" runat="server"></asp:Label>
    <asp:TextBox ID="txtObjectName" ClientIDMode="Static" runat="server"></asp:TextBox>
    <style type="text/css">
        .ui-autocomplete
        {
            height: 200px;
            overflow:auto;
            padding-right: 20px;
        }
    </style>
    <script type="text/javascript">
        function AutoCompleteStartAutoComplete() {
            /*
            * jQuery UI Autocomplete Select First Extension
            *
            * Copyright 2010, Scott González (http://scottgonzalez.com)
            * Dual licensed under the MIT or GPL Version 2 licenses.
            *
            * http://github.com/scottgonzalez/jquery-ui-extensions
            */
            (function ($) {

                $(".ui-autocomplete-input").on("autocompleteopen", function () {
                    var autocomplete = $(this).data("autocomplete"),
		                menu = autocomplete.menu;

                    menu.activate($.Event({ type: "mouseenter" }), menu.element.children().first());
                });

            } (jQuery));
            /*
            *END jQuery UI Autocomplete Select First Extension
            */

            $(function () {

                function ShowItemSelected(itemSelected) {
                    $('#<%=txtObjectID.ClientID%>').val(itemSelected.value);
                    $('#<%=txtObjectName.ClientID%>').val(itemSelected.label);
                }

                $("#txtObjectName").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            //data: "{ 'words': '" + request.term + "', 'LangCode': '<%=LanguageCode%>'}",
                            data: "{ '<%=WordFieldLabel%>': '" + request.term.replace("'","\\'") + "', '<%=LangFieldLabel%>': '<%=LanguageCode%>'}",
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
                                        label: item['<%=txtObjectLabelIdentifier.Text%>'],
                                        value: item['<%=txtObjectIDIdentifier.Text%>']
                                    }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                //alert(textStatus);
                                $('#txtIsError').val('true');
                                $('#txtErrorMessage').val(textStatus);
                            }
                        });
                    },
                    minLength: '<%=MinLenght%>',
                    //Prevent showing ID instead of Label.
                    focus: function (event, ui) {
                        // this.value = ui.item.label;
                        event.preventDefault(); // Prevent the default focus behavior.
                    },
                    //Triggered when an item is selected from the menu.
                    select: function (event, ui) {
                        //If item selected, return his value, else return input (what has been written in the textbox)
                        //ShowItemSelected(ui.item ? ui.item.value : this.value)
                        ShowItemSelected(ui.item)
                        return false;
                    }
                });
            });
        }
        AutoCompleteStartAutoComplete();
    </script>
</asp:Panel>
