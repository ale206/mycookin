<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlUserBoardStatus.ascx.cs"
    Inherits="MyCookinWeb.CustomControls.ctrlUserBoardStatus" %>
<asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">

<script type="text/javascript">

    var WallStatusPlaceHolderLang = {
        '2': 'Cosa stai pensando?',
        '3': 'En qué piensas?',
        '1': 'What are you thinking?',
        '4': 'Que pensez-vous?',
        '5': 'Was denken Sie?'
    };

    function GetWallStatusCorrectPlaceHolder(idLanguage) {
    
        WallStatusPlaceHolder = WallStatusPlaceHolderLang[idLanguage];
        if (WallStatusPlaceHolder == "") {
            WallStatusPlaceHolder = WallStatusPlaceHolderLang['1'];
        }
        return WallStatusPlaceHolder;
    }

    $(document).ready(function () {
        $("#<%=txtStatus.ClientID%>").attr("placeholder", GetWallStatusCorrectPlaceHolder('<%=hfIDLanguage.Value %>'));
    });

    </script>

    <asp:HiddenField ID="hfIDLanguage" runat="server" />

    <asp:Label ID="lblStatus" runat="server" meta:resourcekey="lblStatusResource1"></asp:Label>
    <asp:TextBox ID="txtStatus" CssClass="AutoSizeTextArea" runat="server" 
        TextMode="MultiLine" PlaceHolder=""
        Width="764px" Rows="1" meta:resourcekey="txtStatusResource1"></asp:TextBox>
    <asp:ImageButton ID="ibtnStatus" runat="server" 
        ImageUrl="/Images/icon-forward.png" OnClick="ibtnStatus_Click" Height="34px" 
        Width="34px" meta:resourcekey="ibtnStatusResource1" />
</asp:Panel>
