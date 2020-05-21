<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="UserNotifications.aspx.cs" Inherits="MyCookinWeb.User.UserNotifications" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="/Styles/PageStyle/UserNotifications.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Panel ID="pnlNotificationsContainer" ClientIDMode="Static" 
        CssClass="pnlNotificationsContainer" runat="server" 
        meta:resourcekey="pnlNotificationsContainerResource1">
    

        <asp:GridView ID="gwNotifications" CssClass="gridview" runat="server" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
            GridLines="Horizontal" CellSpacing="6" PageSize="1" 
            meta:resourcekey="gwNotificationsResource1">
            <AlternatingRowStyle CssClass="gridViewAltRow" />
            <Columns>
                <asp:BoundField DataField="CreatedOn" DataFormatString="{0:F}" 
                    meta:resourcekey="BoundFieldResource1" SortExpression="CreatedOn" />
                <asp:BoundField DataField="UserNotification" HtmlEncode="False" 
                    meta:resourcekey="BoundFieldResource2" SortExpression="UserNotification" />
            </Columns>
            <HeaderStyle CssClass="gridViewHeader" />
            <PagerStyle CssClass="gridViewPager" />
            <RowStyle CssClass="gridViewRow" />
            <SelectedRowStyle CssClass="gridViewSelectedRow" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />

        </asp:GridView>

    </asp:Panel>
</asp:Content>
