<%@ Page Title="Recipe DashBoard" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="RecipeDashBoard.aspx.cs" Inherits="MyCookinWeb.RecipeWeb.RecipeDashBoard" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
<link rel="Stylesheet" href="\Styles\SiteStyle\_OLD_\Recipe.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlMainTab" runat="server" ClientIDMode="Static">
        <ul>
            <li>
                <asp:HyperLink ID="lnkRecipeMainDash" runat="server" Text="Dashboard Recipe" NavigateUrl="#pnlRecipeMainDash"></asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="lnkMyRecipe" runat="server" Text="MyRecipe" NavigateUrl="#pnlMyRecipe"></asp:HyperLink></li>
        </ul>
        <asp:UpdatePanel ID="pnlRecipeMainDash" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p>
                    <asp:Label ID="lblTitleNumRecipeToDo" runat="server" Text="Ricette Da Controllare e Sistemare: "></asp:Label>
                    <asp:Label ID="lblNumRecipeToDo" runat="server" ClientIDMode="Static" Text="0"></asp:Label>
                </p>
                <asp:Label ID="lblTitleRecipeModByMe" runat="server" Text="Scegli una Ricetta da questa lista"></asp:Label><br />

                <asp:GridView ID="gvRecipeToDo" runat="server" AllowPaging="true" PageSize="25" 
                        AutoGenerateColumns="false" CssClass="mGrid" OnPageIndexChanging="gvRecipeToDo_PageIndexChanging" >
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDRecipe" OnDataBinding="lblIDRecipe_DataBinding" runat="server" Text='<%# Eval("IDRecipe") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RecipeName" HeaderText="Ricetta" SortExpression="RecipeName" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData1" runat="server" Text="Nessuna Ricetta da visualizzare"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlMyRecipe" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvMyRecipe" runat="server" AllowPaging="true" PageSize="25" 
                        AutoGenerateColumns="false" CssClass="mGrid" OnPageIndexChanging="gvMyRecipe_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDRecipe" OnDataBinding="lblIDRecipe_DataBinding" runat="server" Text='<%# Eval("IDRecipe") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RecipeName" HeaderText="Ricetta" SortExpression="RecipeName" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData1" runat="server" Text="Nessuna Ricetta da visualizzare"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
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
    <br /><br /><br />
</asp:Content>
