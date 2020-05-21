<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllIngredientChecked.aspx.cs" Inherits="MyCookinWeb.MyAdmin.AllIngredientChecked" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


.mGrid { background-color: #fff; margin: 5px 0 10px 0; border: 0px; border-collapse:collapse; }
table {border-collapse: separate;border-spacing: 0;}
caption, th, td {text-align: left;font-weight: normal;}
        .style1
        {
            border-collapse: collapse;
            font-weight: inherit;
            font-style: inherit;
            font-size: 100%;
            font-family: inherit;
            vertical-align: baseline;
            border-style: none;
            border-color: inherit;
            border-width: 0;
            margin: 0;
            padding: 0;
        }
        .style2
        {
            font-weight: inherit;
            font-style: inherit;
            font-size: 100%;
            font-family: inherit;
            vertical-align: baseline;
            border-style: none;
            border-color: inherit;
            border-width: 0;
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="pnlMain" ClientIDMode="Static" runat="server">
        <asp:Label ID="lblIngrChecked" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:GridView ID="gvIngrChecked" runat="server" AllowPaging="true" 
                    PageSize="25" AutoGenerateColumns="false" CssClass="mGrid" 
                        onpageindexchanging="gvIngrModByMe_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblIDIngredient"  OnDataBinding="lblIDIngredient_DataBinding" 
                                        runat="server" Text='<%# Eval("IDIngredient") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IngredientSingular" HeaderText="Ingrediente" 
                                SortExpression="IngredientSingular" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lblNoData2" runat="server" 
                                Text="Non hai ancora modificato nessun Ingrediente"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>

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
    </div>
    </form>
</body>
</html>
