<%@ Page Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" 
AutoEventWireup="true" CodeBehind="UsersGroups.aspx.cs" Inherits="MyCookinWeb.MyAdmin.UsersGoups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/MyManager.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

        <asp:Panel ID="pnlMain" CssClass="pnlMyManagerLarge" ClientIDMode="Static" runat="server">
        <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="/MyAdmin/MyManager.aspx"><-- Torna al Menu</asp:HyperLink>
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        <p>
            &nbsp;</p>

            <asp:ObjectDataSource ID="odsUser" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="MyCookin.DAL.AdminDAL.ds_AdminUsersGroupsMemeberShipTableAdapters.UsersTableAdapter"
                UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter DbType="Guid" Name="Original_IDUser" />
                    <asp:Parameter Name="Original_Name" Type="String" />
                    <asp:Parameter Name="Original_Surname" Type="String" />
                    <asp:Parameter Name="Original_UserName" Type="String" />
                    <asp:Parameter Name="Original_UserDomain" Type="Int32" />
                    <asp:Parameter Name="Original_UserType" Type="Int32" />
                    <asp:Parameter Name="Original_PasswordHash" Type="String" />
                    <asp:Parameter Name="Original_LastPasswordChange" Type="DateTime" />
                    <asp:Parameter Name="Original_PasswordExpireOn" Type="DateTime" />
                    <asp:Parameter Name="Original_ChangePasswordNextLogon" Type="Boolean" />
                    <asp:Parameter Name="Original_ContractSigned" Type="Boolean" />
                    <asp:Parameter Name="Original_BirthDate" Type="DateTime" />
                    <asp:Parameter Name="Original_eMail" Type="String" />
                    <asp:Parameter Name="Original_MailConfirmedOn" Type="DateTime" />
                    <asp:Parameter Name="Original_Mobile" Type="String" />
                    <asp:Parameter Name="Original_MobileConfirmationCode" Type="String" />
                    <asp:Parameter Name="Original_MobileConfirmedOn" Type="DateTime" />
                    <asp:Parameter Name="Original_IDLanguage" Type="Int32" />
                    <asp:Parameter Name="Original_IDCity" Type="Int32" />
                    <asp:Parameter DbType="Guid" Name="Original_IDProfilePhoto" />
                    <asp:Parameter Name="Original_UserEnabled" Type="Boolean" />
                    <asp:Parameter Name="Original_UserLocked" Type="Boolean" />
                    <asp:Parameter Name="Original_MantainanceMode" Type="Boolean" />
                    <asp:Parameter Name="Original_IDSecurityQuestion" Type="Int32" />
                    <asp:Parameter Name="Original_SecurityAnswer" Type="String" />
                    <asp:Parameter Name="Original_DateMembership" Type="DateTime" />
                    <asp:Parameter Name="Original_AccountExpireOn" Type="DateTime" />
                    <asp:Parameter Name="Original_LastLogon" Type="DateTime" />
                    <asp:Parameter Name="Original_LastProfileUpdate" Type="DateTime" />
                    <asp:Parameter Name="Original_UserIsOnLine" Type="Boolean" />
                    <asp:Parameter Name="Original_LastIpAddress" Type="String" />
                    <asp:Parameter Name="Original_IDVisibility" Type="Int32" />
                    <asp:Parameter Name="Original_IDGender" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter DbType="Guid" Name="IDUser" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Surname" Type="String" />
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="UserDomain" Type="Int32" />
                    <asp:Parameter Name="UserType" Type="Int32" />
                    <asp:Parameter Name="PasswordHash" Type="String" />
                    <asp:Parameter Name="LastPasswordChange" Type="DateTime" />
                    <asp:Parameter Name="PasswordExpireOn" Type="DateTime" />
                    <asp:Parameter Name="ChangePasswordNextLogon" Type="Boolean" />
                    <asp:Parameter Name="ContractSigned" Type="Boolean" />
                    <asp:Parameter Name="BirthDate" Type="DateTime" />
                    <asp:Parameter Name="eMail" Type="String" />
                    <asp:Parameter Name="MailConfirmedOn" Type="DateTime" />
                    <asp:Parameter Name="Mobile" Type="String" />
                    <asp:Parameter Name="MobileConfirmationCode" Type="String" />
                    <asp:Parameter Name="MobileConfirmedOn" Type="DateTime" />
                    <asp:Parameter Name="IDLanguage" Type="Int32" />
                    <asp:Parameter Name="IDCity" Type="Int32" />
                    <asp:Parameter DbType="Guid" Name="IDProfilePhoto" />
                    <asp:Parameter Name="UserEnabled" Type="Boolean" />
                    <asp:Parameter Name="UserLocked" Type="Boolean" />
                    <asp:Parameter Name="MantainanceMode" Type="Boolean" />
                    <asp:Parameter Name="IDSecurityQuestion" Type="Int32" />
                    <asp:Parameter Name="SecurityAnswer" Type="String" />
                    <asp:Parameter Name="DateMembership" Type="DateTime" />
                    <asp:Parameter Name="AccountExpireOn" Type="DateTime" />
                    <asp:Parameter Name="LastLogon" Type="DateTime" />
                    <asp:Parameter Name="LastProfileUpdate" Type="DateTime" />
                    <asp:Parameter Name="UserIsOnLine" Type="Boolean" />
                    <asp:Parameter Name="LastIpAddress" Type="String" />
                    <asp:Parameter Name="IDVisibility" Type="Int32" />
                    <asp:Parameter Name="IDGender" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="Surname" Type="String" />
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="UserDomain" Type="Int32" />
                    <asp:Parameter Name="UserType" Type="Int32" />
                    <asp:Parameter Name="PasswordHash" Type="String" />
                    <asp:Parameter Name="LastPasswordChange" Type="DateTime" />
                    <asp:Parameter Name="PasswordExpireOn" Type="DateTime" />
                    <asp:Parameter Name="ChangePasswordNextLogon" Type="Boolean" />
                    <asp:Parameter Name="ContractSigned" Type="Boolean" />
                    <asp:Parameter Name="BirthDate" Type="DateTime" />
                    <asp:Parameter Name="eMail" Type="String" />
                    <asp:Parameter Name="MailConfirmedOn" Type="DateTime" />
                    <asp:Parameter Name="Mobile" Type="String" />
                    <asp:Parameter Name="MobileConfirmationCode" Type="String" />
                    <asp:Parameter Name="MobileConfirmedOn" Type="DateTime" />
                    <asp:Parameter Name="IDLanguage" Type="Int32" />
                    <asp:Parameter Name="IDCity" Type="Int32" />
                    <asp:Parameter DbType="Guid" Name="IDProfilePhoto" />
                    <asp:Parameter Name="UserEnabled" Type="Boolean" />
                    <asp:Parameter Name="UserLocked" Type="Boolean" />
                    <asp:Parameter Name="MantainanceMode" Type="Boolean" />
                    <asp:Parameter Name="IDSecurityQuestion" Type="Int32" />
                    <asp:Parameter Name="SecurityAnswer" Type="String" />
                    <asp:Parameter Name="DateMembership" Type="DateTime" />
                    <asp:Parameter Name="AccountExpireOn" Type="DateTime" />
                    <asp:Parameter Name="LastLogon" Type="DateTime" />
                    <asp:Parameter Name="LastProfileUpdate" Type="DateTime" />
                    <asp:Parameter Name="UserIsOnLine" Type="Boolean" />
                    <asp:Parameter Name="LastIpAddress" Type="String" />
                    <asp:Parameter Name="IDVisibility" Type="Int32" />
                    <asp:Parameter Name="IDGender" Type="Int32" />
                    <asp:Parameter DbType="Guid" Name="Original_IDUser" />
                    <asp:Parameter Name="Original_Name" Type="String" />
                    <asp:Parameter Name="Original_Surname" Type="String" />
                    <asp:Parameter Name="Original_UserName" Type="String" />
                    <asp:Parameter Name="Original_UserDomain" Type="Int32" />
                    <asp:Parameter Name="Original_UserType" Type="Int32" />
                    <asp:Parameter Name="Original_PasswordHash" Type="String" />
                    <asp:Parameter Name="Original_LastPasswordChange" Type="DateTime" />
                    <asp:Parameter Name="Original_PasswordExpireOn" Type="DateTime" />
                    <asp:Parameter Name="Original_ChangePasswordNextLogon" Type="Boolean" />
                    <asp:Parameter Name="Original_ContractSigned" Type="Boolean" />
                    <asp:Parameter Name="Original_BirthDate" Type="DateTime" />
                    <asp:Parameter Name="Original_eMail" Type="String" />
                    <asp:Parameter Name="Original_MailConfirmedOn" Type="DateTime" />
                    <asp:Parameter Name="Original_Mobile" Type="String" />
                    <asp:Parameter Name="Original_MobileConfirmationCode" Type="String" />
                    <asp:Parameter Name="Original_MobileConfirmedOn" Type="DateTime" />
                    <asp:Parameter Name="Original_IDLanguage" Type="Int32" />
                    <asp:Parameter Name="Original_IDCity" Type="Int32" />
                    <asp:Parameter DbType="Guid" Name="Original_IDProfilePhoto" />
                    <asp:Parameter Name="Original_UserEnabled" Type="Boolean" />
                    <asp:Parameter Name="Original_UserLocked" Type="Boolean" />
                    <asp:Parameter Name="Original_MantainanceMode" Type="Boolean" />
                    <asp:Parameter Name="Original_IDSecurityQuestion" Type="Int32" />
                    <asp:Parameter Name="Original_SecurityAnswer" Type="String" />
                    <asp:Parameter Name="Original_DateMembership" Type="DateTime" />
                    <asp:Parameter Name="Original_AccountExpireOn" Type="DateTime" />
                    <asp:Parameter Name="Original_LastLogon" Type="DateTime" />
                    <asp:Parameter Name="Original_LastProfileUpdate" Type="DateTime" />
                    <asp:Parameter Name="Original_UserIsOnLine" Type="Boolean" />
                    <asp:Parameter Name="Original_LastIpAddress" Type="String" />
                    <asp:Parameter Name="Original_IDVisibility" Type="Int32" />
                    <asp:Parameter Name="Original_IDGender" Type="Int32" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsGroup" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="MyCookin.DAL.AdminDAL.ds_AdminUsersGroupsMemeberShipTableAdapters.SecurityGroupsTableAdapter"
                UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter DbType="Guid" Name="Original_IDSecurityGroup" />
                    <asp:Parameter Name="Original_SecurityGroup" Type="String" />
                    <asp:Parameter Name="Original_Enabled" Type="Boolean" />
                    <asp:Parameter Name="Original_Description" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter DbType="Guid" Name="IDSecurityGroup" />
                    <asp:Parameter Name="SecurityGroup" Type="String" />
                    <asp:Parameter Name="Enabled" Type="Boolean" />
                    <asp:Parameter Name="Description" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SecurityGroup" Type="String" />
                    <asp:Parameter Name="Enabled" Type="Boolean" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter DbType="Guid" Name="Original_IDSecurityGroup" />
                    <asp:Parameter Name="Original_SecurityGroup" Type="String" />
                    <asp:Parameter Name="Original_Enabled" Type="Boolean" />
                    <asp:Parameter Name="Original_Description" Type="String" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsMembership" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetGroupsMembership"
                TypeName="MyCookin.DAL.AdminDAL.ds_AdminUsersGroupsMemeberShipTableAdapters.SecurityGroupsUserMembersTableAdapter"
                UpdateMethod="Update">
                <InsertParameters>
                    <asp:Parameter DbType="Guid" Name="IDSecurityGroupUserMember" />
                    <asp:Parameter DbType="Guid" Name="IDSecurityGroup" />
                    <asp:Parameter DbType="Guid" Name="IDUser" />
                    <asp:Parameter Name="MembershipDate" Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter DbType="Guid" Name="IDSecurityGroup" />
                    <asp:Parameter DbType="Guid" Name="IDUser" />
                    <asp:Parameter Name="MembershipDate" Type="DateTime" />
                    <asp:Parameter DbType="Guid" Name="Original_IDSecurityGroupUserMember" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <asp:GridView ID="gvUserGroup" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataKeyNames="IDSecurityGroupUserMember" DataSourceID="odsMembership">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="IDSecurityGroupUserMember" HeaderText="IDSecurityGroupUserMember"
                        ReadOnly="True" SortExpression="IDSecurityGroupUserMember" Visible="False" />
                    <asp:TemplateField HeaderText="SecurityGroup" SortExpression="IDSecurityGroup">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlViewSecGroup" runat="server" DataSourceID="odsGroup" DataTextField="SecurityGroup"
                                DataValueField="IDSecurityGroup" Enabled="False" SelectedValue='<%# Bind("IDSecurityGroup") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlModSecGroup" runat="server" DataSourceID="odsGroup" DataTextField="SecurityGroup"
                                DataValueField="IDSecurityGroup" SelectedValue='<%# Bind("IDSecurityGroup") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User" SortExpression="IDUser">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlViewUser" runat="server" DataSourceID="odsUser" DataTextField="UserName"
                                DataValueField="IDUser" Enabled="False" SelectedValue='<%# Bind("IDUser") %>'>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlModUser" runat="server" DataSourceID="odsUser" DataTextField="UserName"
                                DataValueField="IDUser" SelectedValue='<%# Bind("IDUser") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MembershipDate" HeaderText="MembershipDate" SortExpression="MembershipDate" />
                </Columns>
            </asp:GridView>
            <asp:FormView ID="frmUserGroup" runat="server" DataKeyNames="IDSecurityGroupUserMember"
                DataSourceID="odsMembership" DefaultMode="Insert" OnItemInserting="frmUserGroup_ItemInserting">
                <InsertItemTemplate>
                    <table class="style1">
                        <tr>
                            <td>
                                SecurityGroup:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAddGroup" runat="server" DataSourceID="odsGroup" DataTextField="SecurityGroup"
                                    DataValueField="IDSecurityGroup" SelectedValue='<%# Bind("IDSecurityGroup") %>'>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAddUser" runat="server" DataSourceID="odsUser" DataTextField="UserName"
                                    DataValueField="IDUser" SelectedValue='<%# Bind("IDUser") %>'>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="Insert" />
                            </td>
                            <td>
                                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                </InsertItemTemplate>
            </asp:FormView>
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
