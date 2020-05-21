<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MyCookinWeb.User.Contact" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/Contact.min.css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<script type="text/javascript" src="/Js/Pages/Contact.js"></script>

        <asp:Panel ID="pnlContactForm" CssClass="pnlContactForm" ClientIDMode="Static" 
            runat="server" meta:resourcekey="pnlContactFormResource1">
            <p class="titleRow">
                <asp:Label ID="lblTitle" CssClass="lblTitle" runat="server" Text="Contattaci" 
                    meta:resourcekey="lblTitleResource1"></asp:Label>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:DropDownList ID="ddlContactRequestType" ClientIDMode="Static" 
                    runat="server" meta:resourcekey="ddlContactRequestTypeResource1">
                    <asp:ListItem Text="Generic Help" Value="1" Selected="True" 
                        meta:resourcekey="ListItemResource1"></asp:ListItem>
                    <asp:ListItem Text="Request Information" Value="2" 
                        meta:resourcekey="ListItemResource2"></asp:ListItem>
                    <asp:ListItem Text="Commercial Informations" Value="3" 
                        meta:resourcekey="ListItemResource3"></asp:ListItem>
                    <asp:ListItem Text="Report Bug" Value="4" meta:resourcekey="ListItemResource4"></asp:ListItem>
                    <asp:ListItem Text="Advertising" Value="5" meta:resourcekey="ListItemResource5"></asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:TextBox ID="txtFirstName" runat="server" PlaceHolder="Nome" Width="400px" Rows="1"
                    MaxLength="50" ClientIDMode="Static" 
                    onkeypress="return clickButton(event,'btnSendNewRequestContact')" 
                    meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:TextBox ID="txtLastName" runat="server" PlaceHolder="Cognome" 
                    Width="400px" Rows="1"
                    MaxLength="50" ClientIDMode="Static" 
                    onkeypress="return clickButton(event,'btnSendNewRequestContact')" 
                    meta:resourcekey="txtLastNameResource1"></asp:TextBox>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:TextBox ID="txtEmail" runat="server" PlaceHolder="Email" Width="400px" Rows="1"
                    MaxLength="70" ClientIDMode="Static" 
                    onkeypress="return clickButton(event,'btnSendNewRequestContact')" 
                    meta:resourcekey="txtEmailResource1"></asp:TextBox>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:TextBox ID="txtRequestText" runat="server" TextMode="MultiLine" MaxLength="800"
                    PlaceHolder="Come possiamo aiutarti?" Width="400px" Rows="2" ClientIDMode="Static"
                    onkeypress="return clickButton(event,'btnSendNewRequestContact')" 
                    meta:resourcekey="txtRequestTextResource1"></asp:TextBox>
            </p>
            <p>
                &nbsp;</p>
            <p>
                <asp:CheckBox ID="chkPrivacy" ClientIDMode="Static" runat="server" 
                    meta:resourcekey="chkPrivacyResource1" />
                <asp:HyperLink ID="hlPrivacyIubendaContact" NavigateUrl="https://www.iubenda.com/privacy-policy/264657"
                    CssClass="iubenda-nostyle no-brand iubenda-embed lblPrivacyLink" 
                    runat="server" meta:resourcekey="hlPrivacyIubendaContactResource1"><asp:Label 
                    ID="lblPrivacy" runat="server" 
                    Text="Dichiaro di aver letto l'informativa sulla privacy" 
                    meta:resourcekey="lblPrivacyResource1"></asp:Label>
</asp:HyperLink>
            </p>
            <p>
                &nbsp;</p>
            <p class="btnAlign">
                <asp:Button ID="btnSendNewRequestContact" CssClass="MyButton" 
                    ClientIDMode="Static" runat="server" Text="Invia"
                    
                    OnClientClick="$('#imgSendNewContactRequestLoader').show(); SendNewContactRequest(); return false;" 
                    meta:resourcekey="btnSendNewRequestContactResource1" /><br />
                <asp:Image ID="imgSendNewContactRequestLoader" ClientIDMode="Static" AlternateText="Loading"
                    ImageUrl="~/Images/icon_loader.gif" Width="16px" Height="16px" 
                    runat="server" meta:resourcekey="imgSendNewContactRequestLoaderResource1" />
                <asp:Label ID="lblContactFormError" ClientIDMode="Static" runat="server" 
                    Text="* All fields are mandatory" 
                    meta:resourcekey="lblContactFormErrorResource1"></asp:Label>
                <asp:Label ID="lblContactEmailError" ClientIDMode="Static" runat="server" 
                    Text="* Please check your email" 
                    meta:resourcekey="lblContactEmailErrorResource1"></asp:Label>
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlSendResult" ClientIDMode="Static" runat="server" CssClass="pnlSendResult" 
            meta:resourcekey="pnlSendResultResource1">
            <p class="titleRow">
                <asp:Label ID="lblTitleThanks" CssClass="lblTitle" runat="server" Text="Grazie" 
                    meta:resourcekey="lblTitleThanksResource1"></asp:Label>
            </p>
            <p>
                &nbsp;</p>
            <asp:Label ID="lblThanks" ClientIDMode="Static" runat="server" 
                Text="Grazie di averci contattato. Ti risponderemo il prima possibile" 
                meta:resourcekey="lblThanksResource1"></asp:Label>
            <asp:Label ID="lblError" ClientIDMode="Static" runat="server" 
                Text="Si è verificato un errore. Ti preghiamo di riporvare più tardi." 
                meta:resourcekey="lblErrorResource1"></asp:Label>
            <p>
                &nbsp;</p>
                <p>
                &nbsp;</p>
                <p>
                    <asp:HyperLink ID="hlBackHome" NavigateUrl="/Default.aspx" runat="server" 
                        meta:resourcekey="hlBackHomeResource1">
</asp:HyperLink></p>
        </asp:Panel>

</asp:Content>
