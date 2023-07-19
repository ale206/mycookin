<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master"
    AutoEventWireup="true" CodeBehind="MailchimpUpdateLists.aspx.cs" Inherits="MyCookinWeb.MyAdmin.MailchimpUpdateLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/MyManager.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Panel ID="pnlMyManager" CssClass="pnlMyManager" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    </asp:Panel>

</asp:Content>

