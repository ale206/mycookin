<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master"
    AutoEventWireup="true" CodeBehind="MyManager.aspx.cs" Inherits="MyCookinWeb.MyAdmin.MyManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/MyManager.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Panel ID="pnlMyManager" CssClass="pnlMyManager" ClientIDMode="Static" runat="server">
        <asp:HyperLink ID="hlAllIngredientChecked" runat="server" NavigateUrl="/MyAdmin/AllIngredientChecked.aspx">Lista Ingredienti</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlAuditCheckPhoto" runat="server" NavigateUrl="/MyAdmin/AuditCheckPhoto.aspx">Foto da controllare</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlAuditSpammersList" runat="server" NavigateUrl="/MyAdmin/AuditSpammersList.aspx">Utenti segnalati come Spam</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlErrorsList" runat="server" NavigateUrl="/MyAdmin/ErrorsList.aspx">Lista Errori</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlUsersGroups" runat="server" NavigateUrl="/MyAdmin/UsersGroups.aspx">Gestione Gruppi e Permessi</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlMediaManager" runat="server" NavigateUrl="/MyAdmin/MediaManager.aspx">Media Manager</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlIngredientTranslations" runat="server" NavigateUrl="/MyAdmin/IngredientTranslations.aspx">Traduzione Ingredienti</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlRecipeTranslations" runat="server" NavigateUrl="/MyAdmin/RecipeTranslations.aspx">Traduzione Ricette</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlMailchimpUpdateLists" runat="server" NavigateUrl="/MyAdmin/MailchimpUpdateLists.aspx">Aggiorna Liste Mailchimp</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlSendTestEmail" runat="server" NavigateUrl="/MyAdmin/SendTestEmail.aspx">Invio email di prova</asp:HyperLink>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="pnlNoAuth" Visible="false" ClientIDMode="Static" runat="server">
        <br />
        <p>
            <asp:Label ID="lblNoAuth" runat="server" CssClass="lblIngredientTitle" Text="Non sei autorizzato a visualizzare questa pagina."></asp:Label></p>
        <br />
        <p>
            <asp:HyperLink ID="lnkBackToHome" CssClass="linkStandard" NavigateUrl="~/Default.aspx"
                runat="server">Torna a MyCookin</asp:HyperLink></p>
    </asp:Panel>
</asp:Content>
