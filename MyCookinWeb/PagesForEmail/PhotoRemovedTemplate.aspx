<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/PagesForEmail.Master" AutoEventWireup="true" CodeBehind="PhotoRemovedTemplate.aspx.cs" Inherits="MyCookinWeb.PagesForEmail.PhotoRemoved" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainEmail" runat="server">

<asp:Panel ID="pnlEmailMessage" ClientIDMode="Static" runat="server" 
            style="background-color: #fff; margin: 10px; font-family:Verdana; font-size:1.1em; color:#70644F;">       
        <p>
            <asp:Label ID="lblMessage" runat="server" Text="Ci dispiace informarti che una tua foto è stata rimossa dalle nostre pagine in quanto non rispettava le nostre condizioni d'uso."></asp:Label>
        </p>
        <p>&nbsp;</p>
        <p>
            <asp:HyperLink ID="lnkMessage" runat="server" 
                style="color:#70644F; text-decoration:none;">
                    <asp:Label ID="lblLinkTermsAndConditions" runat="server" Text="Visualizza le condizioni d'uso di MyCookin"></asp:Label>
                </asp:HyperLink>
        </p>
        <p>&nbsp;</p>
        <p>
            <asp:Label ID="lblNoReply" runat="server" Text=""
                style="font-size:0.8em; color:#70644F;"></asp:Label></p>
    </asp:Panel>

</asp:Content>
