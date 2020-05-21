<%@ Page Title="Ingredient DashBoard" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="IngredientDashBoard.aspx.cs" Inherits="MyCookinWeb.IngredientWeb.IngredientDashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlMainTab" runat="server" ClientIDMode="Static">
        <ul>
            <li>
                <asp:HyperLink ID="lnkIngrMainDash" runat="server" Text="Dashboard Ingredient" NavigateUrl="#pnlIngrMainDash"></asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="lnkMyIngr" runat="server" Text="MyIngredient" NavigateUrl="#pnlMyIngr"></asp:HyperLink></li>
        </ul>
        <asp:UpdatePanel ID="pnlIngrMainDash" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <p>
                <asp:Label ID="lblTitleNumIngrToDo" runat="server" Text="Ingredienti Da Controllare e Sistemare: "></asp:Label>
                <asp:Label ID="lblNumIngrToDo" runat="server" ClientIDMode="Static" Text="0"></asp:Label>
                </p>
                <p>&nbsp;</p>
                <p>

                <table width="90%" cellpadding="10" cellspacing="10" >
                <tr>
                <td width="60%">
                    <asp:Label ID="lblTitleIngrModByMe" runat="server" Text="Scegli un ingrediente da questa lista"></asp:Label><br />
                    <asp:GridView ID="gvIngrToDo" runat="server" AllowPaging="true" PageSize="25" 
                        AutoGenerateColumns="false" 
                        onpageindexchanging="gvIngrToDo_PageIndexChanging" CssClass="mGrid" >
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDIngredient" OnDataBinding="lblIDIngredient_DataBinding" runat="server" Text='<%# Eval("IDIngredient") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IngredientSingular" HeaderText="Ingrediente" SortExpression="IngredientSingular" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData1" runat="server" Text="Nessun Ingrediente da visualizzare"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
                <td width="40%" valign="top">In questa pagina, Dashboard Ingredient, trovi la lista 
                    di tutti gli ingredienti che devono essere controllati.
                    <br />
                    <br />
                    <br />
                    Di ognuno dovremo inserire un&#39;immagine, controllare se il nome è scritto bene e 
                    completare alcune informazioni.<br />
                    <br />
                    <br />
                    Non dovrai per forza compilare tutto subito: potrai salvare e aggiungere altre 
                    informazioni in seguito andando nella pagina MyIngredient.<br />
                    <br />
                    <br />
                    Se non conosci qualcosa di questa lista fai una breve ricerca su internet. Se 
                    non è un ingrediente potrai segnalarlo nella sua pagina di modifica.</td>
                </tr>
                
                </table>

                
                </p>

            </ContentTemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="gvIngrToDo" EventName="PageIndexChanged" />
            </triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlMyIngr" ClientIDMode="Static" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <p>

            <table width="90%" cellpadding="10" cellspacing="10" >
                <tr>
                <td width="60%">

                    <asp:Label ID="lblTitleNumIngrDoByUser" runat="server" 
                        Text="Ingredienti che hai già aggiornato: "></asp:Label>
                    <asp:Label ID="lblNumIngrDoByUser" runat="server" ClientIDMode="Static" 
                        Text="0"></asp:Label>
                    <br />
                    <br />

                <asp:Label ID="lbTitlelIngrToDo" runat="server" Text="Ingredienti aggiornati: "></asp:Label><br />
                    <asp:GridView ID="gvIngrModByMe" runat="server" AllowPaging="true" 
                    PageSize="25" AutoGenerateColumns="false" CssClass="mGrid" 
                        onpageindexchanging="gvIngrModByMe_PageIndexChanging">
                    <Columns>
                            <asp:TemplateField HeaderText="" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDIngredient"  OnDataBinding="lblIDIngredient_DataBinding" runat="server" Text='<%# Eval("IDIngredient") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IngredientSingular" HeaderText="Ingrediente" SortExpression="IngredientSingular" />
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData2" runat="server" Text="Non hai ancora modificato nessun Ingrediente"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
                <td width="40%" valign="top">In questa pagina, MyIngredient, trovi tutti gli 
                    ingredienti che hai modificato.<br />
                    <br />
                    Puoi rivederli e modificarli quando vuoi.</td>
                </tr>
                
                </table>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnlNoAuth" Visible="false" ClientIDMode="Static" runat="server">
        <br />
        <p><asp:Label ID="lblNoAuth" runat="server" CssClass="lblIngredientTitle" Text="Non sei autorizzato a visualizzare questa pagina."></asp:Label></p><br />
        <p><asp:HyperLink ID="lnkBackToHome" CssClass="linkStandard" NavigateUrl="~/Default.aspx" runat="server">Torna a MyCookin</asp:HyperLink></p>
    </asp:Panel>
</asp:Content>
