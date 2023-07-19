<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="MediaManager.aspx.cs" Inherits="MyCookinWeb.MyAdmin.MediaManager" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/MyManager.min.css" />
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Panel ID="pnlMyManager" CssClass="pnlMyManagerLarge" ClientIDMode="Static" runat="server">
    <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="/MyAdmin/MyManager.aspx"><-- Torna al Menu</asp:HyperLink>
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        <p>
            &nbsp;</p>
        <asp:Panel ID="pnlSettings" runat="server" CssClass="DisplayBlock">
            <asp:Label ID="lblMediaType" runat="server" Text="MediaType:"></asp:Label>
            <asp:TextBox ID="txtMediaType" runat="server" placeholder="MediaType"></asp:TextBox><br /><br />
            <asp:Label ID="lblNumMedia" runat="server" Text="Number of media to manage:"></asp:Label>
            <asp:TextBox ID="txtNumMedia" runat="server" placeholder="Number of media to manage"></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="pnlImageSize" runat="server"  CssClass="DisplayBlock">
            <asp:Button ID="btnCreateSmallSizeMedia" runat="server" Text="Create Small Size"  onclick="btnCreateSmallSizeMedia_Click" />
            <asp:Button ID="btnCreateOriginaResizedMedia" runat="server" Text="Create OriginalResized Size"  onclick="btnCreateOriginaResizedMedia_Click"/>
        </asp:Panel>
        <asp:Panel ID="pnlMoveOnCDN" runat="server"  CssClass="DisplayBlock">
            <asp:Button ID="btnMoveMediaOnCDN" runat="server" Text="Move Media Files on CDN" onclick="btnMoveMediaOnCDN_Click"/>
            <asp:Button ID="btnMoveSmallMediaOnCDN" runat="server" Text="Move Small Size Media on CDN" onclick="btnMoveSmallSizeOnCDN_Click"/>
            <asp:Button ID="btnMoveOriginalResizedOnCDN" runat="server" Text="Move OriginalResized Media on CDN" onclick="btnMoveOriginalResizedOnCDN_Click" Visible="false"/>
        </asp:Panel>
        <asp:UpdateProgress ID="upsTransfering" runat="server">
            <ProgressTemplate>
                <asp:Label ID="lblTransfering" runat="server" Text="Transfering, please wait..."></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:Panel ID="pnlResult" runat="server">
            <asp:Label ID="lblResult" runat="server" Text="" CssClass="lblResult"></asp:Label>
        </asp:Panel>
    </asp:Panel>
</asp:Content>