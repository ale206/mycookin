<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master" AutoEventWireup="true" CodeBehind="ImageCrop.aspx.cs" Inherits="MyCookinWeb.Utilities.ImageCrop" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/ImageCrop.min.css" />
    <link href="/Styles/jQueryUiCss/JCrop/jquery.Jcrop.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
 <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <scripts>
            <asp:ScriptReference Path="/Js/JCrop/jquery.color.js" />
            <asp:ScriptReference Path="/Js/JCrop/jquery.Jcrop.js" />
        </scripts>
    </asp:ScriptManagerProxy>
    <asp:HiddenField ID="hfX1" runat="server" />
    <asp:HiddenField ID="hfY1" runat="server" />
    <asp:HiddenField ID="hfX2" runat="server" />
    <asp:HiddenField ID="hfY2" runat="server" />
    <asp:HiddenField ID="hfWidth" runat="server" />
    <asp:HiddenField ID="hfHeight" runat="server" />
    <asp:HiddenField ID="hfMinCropWidth" runat="server" />
    <asp:HiddenField ID="hfMinCropHeight" runat="server" />
    <asp:HiddenField ID="hfCropAspectRatio" runat="server" />
    <asp:Panel ID="pnlMain" runat="server" CssClass="pnlMain">
        <asp:Panel ID="pnlMainInternal" runat="server" CssClass="pnlMainInternal">
            <asp:Label ID="lblInfoCrop" runat="server" Text="Ritaglia la tua foto e clicca ok" CssClass="lblInfoCrop"></asp:Label>
            <asp:Image ID="imgToCrop" runat="server" CssClass="imgToCrop" />
            <asp:Button ID="btnCrop" runat="server" Text="OK" CssClass="btnCrop" OnClick="btnCrop_Click" />
        </asp:Panel>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        $(window).load(function () {
            //Start Jcrop
            CallJCropNew('#<%=hfMinCropWidth.ClientID%>', '#<%=hfMinCropHeight.ClientID%>', '#<%=hfCropAspectRatio.ClientID%>', '#<%=imgToCrop.ClientID%>', '#<%=hfX1.ClientID%>', '#<%=hfY1.ClientID%>', '#<%=hfX2.ClientID%>', '#<%=hfY2.ClientID%>', '#<%=hfWidth.ClientID%>', '#<%=hfHeight.ClientID%>');
        });
    </script>
</asp:Content>
