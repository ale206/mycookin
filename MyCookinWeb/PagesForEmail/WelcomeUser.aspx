<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/PagesForEmail.Master" AutoEventWireup="true" CodeBehind="WelcomeUser.aspx.cs" Inherits="MyCookinWeb.PagesForEmail.WelcomeUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainEmail" runat="server">

    <asp:Panel ID="pnlEmailMessage" ClientIDMode="Static" runat="server" 
            style="background-color: #fff; margin: 10px; font-family:Verdana; font-size:1.1em; color:#70644F;">      
        <p>
            <asp:HyperLink ID="lnkMessage" runat="server" 
                style="color:#70644F; text-decoration:none;"></asp:HyperLink>
        </p>
        <p>
            <asp:Label ID="lblLinkText" runat="server" Text=""
                style="color:#70644F; text-decoration:none;"></asp:Label></p>
        <p>&nbsp;</p>
        <p>
            <asp:Label ID="lblNoReply" runat="server" Text=""
                style="font-size:0.8em; color:#70644F;"></asp:Label></p>
    </asp:Panel>

</asp:Content>
