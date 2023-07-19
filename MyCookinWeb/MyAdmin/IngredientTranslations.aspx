<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="IngredientTranslations.aspx.cs" Inherits="MyCookinWeb.MyAdmin.IngredientTranslations" %>
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
            <asp:Label ID="lblFromIDLanguage" runat="server" Text="From IDLanguage:"></asp:Label>
            <asp:TextBox ID="txtFromIDLanguage" runat="server" placeholder=""></asp:TextBox><br /><br />
            <asp:Label ID="lblToIDLanguage" runat="server" Text="To IDLanguage:"></asp:Label>
            <asp:TextBox ID="txtToIDLanguage" runat="server" placeholder=""></asp:TextBox><br /><br />
            <asp:Label ID="lblNumIngredients" runat="server" Text="Number Ingredients to translate:"></asp:Label>
            <asp:TextBox ID="txtNumIngredients" runat="server" placeholder=""></asp:TextBox>
        </asp:Panel>
        <asp:Panel ID="pnlStartTranslate" runat="server"  CssClass="DisplayBlock">
            <asp:Button ID="btnStartTranslate" runat="server" Text="Start Translate"  onclick="btnStartTranslate_Click" />
        </asp:Panel>
        <asp:UpdateProgress ID="upsTranslating" runat="server">
            <ProgressTemplate>
                <asp:Label ID="lblTranslating" runat="server" Text="Translating, please wait..."></asp:Label>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:Panel ID="pnlResult" runat="server">
            <asp:Label ID="lblResult" runat="server" Text="" CssClass="lblResult"></asp:Label>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
