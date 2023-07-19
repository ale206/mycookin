<%@ Page Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true"
    CodeBehind="AuditCheckPhoto.aspx.cs" Inherits="MyCookinWeb.MyAdmin.AuditCheckPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/MyManager.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server"></asp:Label>
    </asp:Panel>
    <%--<asp:Panel ID="pnlMyManager" CssClass="pnlMyManager" ClientIDMode="Static" runat="server">
    --%>
    <asp:UpdatePanel ID="upnlMyManager" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlMyManager" CssClass="pnlMyManager" ClientIDMode="Static" runat="server">
            <asp:HiddenField ID="hfIDAuditEvent" runat="server" />
            <asp:HiddenField ID="hfObjectID" runat="server" />
            <table style="width: 90%;">
                <tr>
                    <td style="text-align: left; font-size: medium;">
                        <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="/MyAdmin/MyManager.aspx"><-- Torna al Menu</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:Image ID="imgPhoto" runat="server" />
                        <br />
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </td>
                <tr>
                    <td style="text-align: center;">
                        <asp:HyperLink ID="hlPhotoPath" runat="server"></asp:HyperLink> ---- <asp:HyperLink ID="hlUser" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                
                </tr>
                <tr>
                    <td style="text-align: center;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:Button ID="btnImageOk" runat="server" Text="OK :)" OnClick="btnImageOk_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:Button ID="btnImageRemove" runat="server" Text="RIMUOVI !" OnClick="btnImageRemove_Click"
                            OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro?', 'SI', 'NO');" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        &nbsp;
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
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
