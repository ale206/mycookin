<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="ctrlShowRecipeIngredient.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlShowRecipeIngredient" %>
<asp:HiddenField ID="hfIDRecipeIngredient" runat="server"  />
<asp:HiddenField ID="hfIDRecipe" runat="server" />
<asp:HiddenField ID="hfIDIngredient" runat="server" />
<asp:HiddenField ID="hfIsPrincipal" runat="server" />
<asp:HiddenField ID="hfQuantityNotStd" runat="server" />
<asp:HiddenField ID="hfQuantityNotStdType" runat="server" />
<asp:HiddenField ID="hfQuantity" runat="server" />
<asp:HiddenField ID="hfQuantityType" runat="server" />
<asp:HiddenField ID="hfQuantityNotSpecified" runat="server" />
<asp:HiddenField ID="hfRecipeIngredientGroupNumber" runat="server" />
<asp:HiddenField ID="hfRecipeIngredientGroupNumberChange" runat="server" />
<asp:HiddenField ID="hfIDLanguage" runat="server" />
<asp:HiddenField ID="hfShowPrincipalIngr" Value="false" runat="server" />
<asp:HiddenField ID="hfShowInvalidIngr"  Value="false" runat="server" />
<asp:HiddenField ID="hfShowEditButton" Value="false" runat="server" />
<asp:HiddenField ID="hfEnableEditIngredientRelevance" Value="false" runat="server" />
<asp:HiddenField ID="hfControlLoaded" Value="false" runat="server" />
<asp:HiddenField ID="hfShowInfoPanel" Value="false" runat="server" />
<asp:HiddenField ID="hfLoadInfoPanelAsync" Value="false" runat="server" />

<script type="text/javascript" language="javascript" src="/Js/CustomControls/ctrlShowlRecipeIngredient.js"></script>
<%--<script type="text/javascript" language="javascript" src="/Js/PopBox/popbox.min.js"></script>--%>

<asp:Panel ID="pnlIngredientInfoBox" runat="server" 
    CssClass="pnlIngredientInfoBox popbox" 
    meta:resourcekey="pnlIngredientInfoBoxResource1">
    <asp:Panel ID="pnlIngrGroup" CssClass="pnlIngredientGroupSeparator" 
        runat="server" meta:resourcekey="pnlIngrGroupResource1">
        <asp:Label ID="lblIngredientGroup" CssClass="lblIngredientGroup" 
            runat="server" meta:resourcekey="lblIngredientGroupResource1"></asp:Label>
    </asp:Panel>
        <asp:Label ID="lblIngrQta" runat="server" 
            CssClass="lblRecipeIngredientQtaTitle" meta:resourcekey="lblIngrQtaResource1"></asp:Label>
        <asp:HyperLink ID="lnkIngredient" runat="server" 
            CssClass="linkPopBox lnkIngredient" NavigateUrl="#" 
            meta:resourcekey="lnkIngredientResource1"></asp:HyperLink>
    <asp:Image ID="imgInvalidIngr" runat="server" ImageUrl="/Images/attention_icon.png"
        Width="18px" Height="18px" Visible="False" 
        ToolTip="Ingrediente non valido" meta:resourcekey="imgInvalidIngrResource1" />
    <asp:Label ID="lblIngredientNote" runat="server" 
        CssClass="lblRecipeIngredientNote" 
        meta:resourcekey="lblIngredientNoteResource1"></asp:Label>
    <asp:CheckBox ID="chkPrincipal" runat="server" Enabled="False" 
        meta:resourcekey="chkPrincipalResource1" />
    <asp:DropDownList ID="ddlIngredientRelevance" runat="server" 
        meta:resourcekey="ddlIngredientRelevanceResource1">
    </asp:DropDownList>
     <asp:ImageButton ID="btnDeleteIngredient" ImageUrl="/Images/deleteX.png" 
        runat="server" Width="16px" Height="16px" 
        onclick="btnDeleteIngredient_Click" 
        meta:resourcekey="btnDeleteIngredientResource1" />
    <div class="collapse">
        <asp:Panel ID="pnlBoxInternal" CssClass="box" runat="server" 
            meta:resourcekey="pnlBoxInternalResource1">
            <a href="#" class="closePopBox">x</a>
            <div id="pnlIngredientInfoBoxContent" class="pnlIngredientInfoBoxContent">
                <asp:Panel ID="pnlInfoHead" runat="server" CssClass="pnlInfoHead" 
                    meta:resourcekey="pnlInfoHeadResource1">
                    <asp:Panel ID="pnlInfoHeadLeft" runat="server" CssClass="pnlInfoHeadLeft" 
                        meta:resourcekey="pnlInfoHeadLeftResource1">
                        <asp:Image ID="imgIngredientPhoto" runat="server" 
                            CssClass="imgIngredientPhoto"  Width="80px" Height="80px" 
                            meta:resourcekey="imgIngredientPhotoResource1"/>
                    </asp:Panel>
                    <asp:Panel ID="pnlInfoHeadRight" runat="server" CssClass="pnlInfoHeadRight" 
                        meta:resourcekey="pnlInfoHeadRightResource1">
                        <asp:HyperLink ID="lnkGoToIngredient" runat="server" 
                            CssClass="lnkGoToIngredient"><asp:Label ID="lblIngreadientName" runat="server"></asp:Label></asp:HyperLink><br />
                        <asp:Label ID="lblKcal" runat="server" Text="{0} Kcal per 100g" 
                            CssClass="lblKcal" meta:resourcekey="lblKcalResource1"></asp:Label>
                    </asp:Panel>
                </asp:Panel>
                <div id="pnlIngredientBasicInfo" class="pnlIngredientBasicInfo">
                    <asp:Label ID="lblIngredientBasicInfo" runat="server" Width="350px" 
                        CssClass="lblRecipeIngredientBasicInfo" 
                        meta:resourcekey="lblIngredientBasicInfoResource1"></asp:Label>
                </div>
                <asp:Panel ID="pnlAltIngredient" CssClass="pnlAltIngredient" runat="server" 
                    Visible="False" meta:resourcekey="pnlAltIngredientResource1">
                    <asp:Label ID="lblAltIngrTitle" CssClass="lblRecipeIngredientNote" 
                        runat="server" Width="350px" 
                        Text="In questa ricetta al posto di questo ingrediente potresti usare:" 
                        meta:resourcekey="lblAltIngrTitleResource1"></asp:Label>
                    <br /><br />
                    <asp:Label ID="lblAltIngr" CssClass="lblRecipeIngredientQtaTitle" 
                        runat="server" meta:resourcekey="lblAltIngrResource1"></asp:Label>
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
    <script language="javascript" type="text/javascript">
        $('#<%=ddlIngredientRelevance.ClientID%>').live('change', function (e) {
            //alert("{ IDRecipeIngredient: '" + '<%=hfIDRecipeIngredient.Value%>' + "', IngredientRelevance: '" + $('#<%=ddlIngredientRelevance.ClientID%>').val() + "'}");
            $.ajax({
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                url: "http://" + WebServicesPath + "/Recipe/ManageRecipe.asmx/ChangeIngredientRelevance",
                data: "{ IDRecipeIngredient: '" + '<%=hfIDRecipeIngredient.Value%>' + "', IngredientRelevance: '" + $('#<%=ddlIngredientRelevance.ClientID%>').val() + "'}",
                success: function (msg) {
                    //console.log('ok');
                },
                error: function (msg) {
                    console.log('Ops...');
                }
            });
        });
    </script>
</asp:Panel>

