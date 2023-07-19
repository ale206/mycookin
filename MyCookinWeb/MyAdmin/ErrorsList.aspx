<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master"
    AutoEventWireup="true" CodeBehind="ErrorsList.aspx.cs" Inherits="MyCookinWeb.MyAdmin.ErrorsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/MyManager.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
 <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlMyManager" CssClass="pnlMyManagerLarge" ClientIDMode="Static" runat="server">
        <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="/MyAdmin/MyManager.aspx"><-- Torna al Menu</asp:HyperLink>
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        <p>
            &nbsp;</p>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" PageSize="7" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDeleteError" runat="server" CausesValidation="false" 
                            CommandName="" Text="X" OnClick="lbtnDeleteError_Click" CommandArgument='<%# Bind("ErrorMessage") %>'
                            OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro', 'SI', 'NO');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NumberOfEveniences" HeaderText="n" SortExpression="NumberOfEveniences" />
                <asp:BoundField DataField="ErrorMessage" HeaderText="Error" SortExpression="ErrorMessage" />
                <asp:BoundField DataField="FileOrigin" HeaderText="FileOrigin" SortExpression="FileOrigin" />
                <asp:BoundField DataField="ErrorLine" HeaderText="Line" SortExpression="ErrorLine" />
                <asp:BoundField DataField="ErrorMessageCode" HeaderText="Code" SortExpression="ErrorMessageCode" />
                <asp:CheckBoxField DataField="isStoredProcedureError" HeaderText="SP" SortExpression="isStoredProcedureError" />
                <asp:CheckBoxField DataField="isTriggerError" HeaderText="TR" SortExpression="isTriggerError" />
                <asp:CheckBoxField DataField="isApplicationError" HeaderText="APP" SortExpression="isApplicationError" />
                <asp:CheckBoxField DataField="isApplicationLog" HeaderText="LOG" SortExpression="isApplicationLog" />
                <asp:BoundField DataField="ErrorSeverity" HeaderText="Severity" SortExpression="ErrorSeverity" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetErrorsList" TypeName="MyCookin.DAL.ErrorAndMessage.ds_ErrorAndMessageTableAdapters.ErrorsLogsTableAdapter">
        </asp:ObjectDataSource>
    </asp:Panel>
    <asp:Panel ID="pnlNoAuth" Visible="false" ClientIDMode="Static" runat="server">
        <br />
        <p>
            <asp:Label ID="lblNoAuth" runat="server" CssClass="lblIngredientTitle" Text="Non sei autorizzato a visualizzare questa pagina."></asp:Label></p>
        <br />
    </asp:Panel>
</asp:Content>
