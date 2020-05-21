<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="AuditSpammersList.aspx.cs" Inherits="MyCookinWeb.MyAdmin.AuditSpammersList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="Stylesheet" href="/Styles/PageStyle/MyManager.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
 <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server"></asp:Label>
    </asp:Panel>

<asp:Panel ID="pnlMyManager" CssClass="pnlMyManager" ClientIDMode="Static" runat="server">

                <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="/MyAdmin/MyManager.aspx"><-- Torna al Menu</asp:HyperLink>
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
<p>&nbsp;</p>

<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" 
        DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" Width="500" 
        PageSize="12">
        <Columns>
           <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnUserNoSpam" runat="server" CausesValidation="false" 
                            CommandName="" Text="X" OnClick="lbtnUserNoSpam_Click" CommandArgument='<%# Bind("ObjectID") %>'
                            OnClientClick="return JCOnfirm(this, 'Remove', 'Sicuro', 'SI', 'NO');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            <asp:TemplateField HeaderText="IDUser" SortExpression="ObjectID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ObjectID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl='<%# "/User/UserProfile.aspx?IDUserRequested=" + (Eval("ObjectID")) %>' 
                        Target="_blank" Text='<%# Bind("ObjectID") %>'></asp:HyperLink>
                    <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("ObjectID") %>'></asp:Label>--%>
                </ItemTemplate>
                <ItemStyle Width="95%" />
            </asp:TemplateField>
            <asp:BoundField DataField="NumberOfEveniences" HeaderText="n" 
                SortExpression="NumberOfEveniences" >
            <ItemStyle Width="5%" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
                
                
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                 
                OldValuesParameterFormatString="original_{0}" 
                SelectMethod="GetObjectIDNumberOfEveniences" 
                TypeName="MyCookin.DAL.Audit.ds_AuditTableAdapters.GetAuditEventDAL" 
                UpdateMethod="Update">
                
                <SelectParameters>
                    <asp:Parameter DefaultValue="UserSpam" Name="ObjectType" Type="String" />
                </SelectParameters>
                
            </asp:ObjectDataSource>

</asp:Panel>

<asp:Panel ID="pnlNoAuth" Visible="false" ClientIDMode="Static" runat="server">
            <br />
            <p>
                <asp:Label ID="lblNoAuth" runat="server" CssClass="lblIngredientTitle" Text="Non sei autorizzato a visualizzare questa pagina."></asp:Label></p>
            <br />
        </asp:Panel>
</asp:Content>
