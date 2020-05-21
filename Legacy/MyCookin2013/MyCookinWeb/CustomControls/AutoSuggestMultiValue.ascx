<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoSuggestMultiValue.ascx.cs" Inherits="MyCookinWeb.CustomControls.AutoSuggestMultiValue" %>

<link rel="Stylesheet" href="/Styles/jQueryUiCss/AutoSuggest/autoSuggest.css" />
<script language="javascript" type="text/javascript" src="/Js/AutoSuggest/jquery.autoSuggest.js"></script>

<asp:HiddenField ID="hdWebServiceURL" runat="server" />
<asp:HiddenField ID="hdselectedItemProp" runat="server" />
<asp:HiddenField ID="hdsearchObjProps" runat="server" />
<asp:HiddenField ID="hdselectedValuesProp" runat="server" />
<asp:HiddenField ID="hdqueryParam" runat="server" />
<asp:HiddenField ID="hdqueryIDLangParam" runat="server" />
<asp:HiddenField ID="hdqueryIDLangValue" runat="server" />
<asp:HiddenField ID="hdMinChars" runat="server" />
<asp:HiddenField ID="hdPreFillValue" runat="server" />
<asp:HiddenField ID="hdValue" runat="server" />
<asp:HiddenField ID="hdMaxAllowedValues" runat="server" />
<asp:HiddenField ID="hdAddedValuesCount" runat="server" />
<asp:HiddenField ID="hdOtherQueryParameter" runat="server" />
<asp:HiddenField ID="hdStartText" runat="server" />
<asp:HiddenField ID="hdEmptyText" runat="server" />
<asp:HiddenField ID="hdLimitAllowedValuesText" runat="server" />

<asp:TextBox ID="txtAutoSuggest" runat="server"></asp:TextBox>

<script language="javascript" type="text/javascript">
    var data = { items: [
            <%=hdPreFillValue.Value %>
            ]
    };
    if ($("#" + '<%=hdPreFillValue.ClientID %>').val() != "") {
        $("#"+ '<%=txtAutoSuggest.ClientID %>').autoSuggest("http://" + WebServicesPath + '<%=hdWebServiceURL.Value %>',
        { selectedItemProp: '<%=hdselectedItemProp.Value %>',
            searchObjProps: '<%=hdsearchObjProps.Value %>',
            selectedValuesProp: '<%=hdselectedValuesProp.Value %>',
            queryParam: '<%=hdqueryParam.Value %>',
            queryIDLangParam: '<%=hdqueryIDLangParam.Value %>',
            queryIDLangValue: '<%=hdqueryIDLangValue.Value %>',
            minChars: '<%=hdMinChars.Value %>',
            preFill: data.items,
            extraParams:'<%=hdOtherQueryParameter.Value %>',
            CopyHiddenFiledID: '<%=hdValue.ClientID %>',
            MaxAllowedValues: '<%=hdMaxAllowedValues.Value %>',
            AddedValuesCountFielID:'<%=hdAddedValuesCount.ClientID %>',
            startText: '<%=hdStartText.Value %>',
            emptyText: '<%=hdEmptyText.Value %>',
            limitText: '<%=hdLimitAllowedValuesText.Value %>'
        });
    }
    else {
         $("#"+ '<%=txtAutoSuggest.ClientID %>').autoSuggest('<%=hdWebServiceURL.Value %>',
        { selectedItemProp: '<%=hdselectedItemProp.Value %>',
            searchObjProps: '<%=hdsearchObjProps.Value %>',
            selectedValuesProp: '<%=hdselectedValuesProp.Value %>',
            queryParam: '<%=hdqueryParam.Value %>',
            queryIDLangParam: '<%=hdqueryIDLangParam.Value %>',
            queryIDLangValue: '<%=hdqueryIDLangValue.Value %>',
            minChars: '<%=hdMinChars.Value %>',
            preFill: "",
            extraParams:'<%=hdOtherQueryParameter.Value %>',
            CopyHiddenFiledID: '<%=hdValue.ClientID %>',
            MaxAllowedValues: '<%=hdMaxAllowedValues.Value %>',
            AddedValuesCountFielID:'<%=hdAddedValuesCount.ClientID %>',
            startText: '<%=hdStartText.Value %>',
            emptyText: '<%=hdEmptyText.Value %>',
            limitText: '<%=hdLimitAllowedValuesText.Value %>'
        });
    }
    </script>
