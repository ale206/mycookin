<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Private.Master" AutoEventWireup="true" CodeBehind="CreateRecipe.aspx.cs" Inherits="MyCookinWeb.RecipeWeb.CreateRecipe" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register TagName="multiUp" TagPrefix="MyCtrl" Src="~/CustomControls/ctrlMultiUpload.ascx" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="head" runat="server">
 <link rel="Stylesheet" href="/Styles/PageStyle/ShowRecipe.min.css" />
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="cphMain" runat="server">
    <asp:HiddenField ID="hfAddPhotoText" runat="server" Value="Aggiungi una bella foto" />
    <asp:Panel ID="pnlResult" ClientIDMode="Static" runat="server" 
        meta:resourcekey="pnlResultResource1">
        <asp:Label ID="lblResult" ClientIDMode="Static" runat="server" 
            meta:resourcekey="lblResultResource1"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnlMain" runat="server" CssClass="pnlMain" 
        meta:resourcekey="pnlMainResource1">
     <asp:Panel ID="pnlBackground" runat="server" CssClass="pnlBackground" 
            meta:resourcekey="pnlBackgroundResource1">
            <div id="bgHeadBottom" class="bgHeadBottom">
            </div>
            <div id="bgHead" class="bgHead">
                <div id="bgHeadLeft" class="bgHeadLeft">
                </div>
                <div id="bgHeadRight" class="bgHeadRight">
                </div>
            </div>
            <div id="bgCenter" class="bgCenter">
                <div id="bgCenterLeft" class="bgCenterLeft">
                </div>
                <div id="bgCenterRight" class="bgCenterRight">
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlContent" ClientIDMode="Static" runat="server" 
            CssClass="pnlContent" meta:resourcekey="pnlContentResource1">
            <asp:Panel ID="pnlNewRecipe" runat="server" CssClass="pnlNewRecipe" 
                meta:resourcekey="pnlNewRecipeResource1">
                <asp:Panel ID="pnlRecipeName" runat="server" 
                    meta:resourcekey="pnlRecipeNameResource1">
                    <asp:Label ID="lblRecipeTitle" runat="server" 
                        Text="Dai un nome alla tua nuova ricetta" CssClass="lblRecipeTitle" 
                        meta:resourcekey="lblRecipeTitleResource1"></asp:Label>
                    <asp:TextBox ID="txtRecipeName" runat="server"
                        onkeypress="return isSpecialHTMLChar(event)"  
                        ClientIDMode="Static" meta:resourcekey="txtRecipeNameResource1"></asp:TextBox>
                </asp:Panel>
                <asp:Panel ID="pnlPhoto" runat="server" ClientIDMode="Static" 
                    CssClass="pnlPhoto" meta:resourcekey="pnlPhotoResource1">
                    <MyCtrl:multiUp ID="multiup" runat="server" OnFilesUploaded="FileUploaded" SelectFilesCssClass="selectPhoto" BaseFileNameClientIDMode="Static" />
                </asp:Panel>
                <asp:Panel ID="pnlSaving" runat="server" CssClass="pnlSaving" 
                    meta:resourcekey="pnlSavingResource1">
                    <asp:UpdateProgress ID="upSaving" runat="server">
                        <ProgressTemplate>
                            <asp:Label ID="lblSaving" runat="server" Text="Salvataggio ricetta in corso" 
                                meta:resourcekey="lblSavingResource1"></asp:Label>
                            <asp:Image ID="imgLoading" runat="server" 
                                ImageUrl="/Images/loadingLineOrange.gif" Height="20px" Width="220px" 
                                meta:resourcekey="imgLoadingResource1" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
        function RecipeNameChange() {
            if (ReplaceSpecialChar($('#txtRecipeName').val(),"") != "") {
                $('#pnlPhoto').css("display", "block");
                $('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height() + 250);
                $('#hfBaseFileName').val(ReplaceSpecialChar($('#txtRecipeName').val(), "_"));
            }
            else {
                $('#pnlPhoto').css("display", "none");
                $('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height() + 10);
            }
            $('#hfBaseFileName').val($('#txtRecipeName').val());
        }
        function pageLoad() {
            $('#<%=pnlBackground.ClientID%>').height($('#<%=pnlContent.ClientID%>').height() + 10);
            RecipeNameChange();
        }
        $('#txtRecipeName').live('change keyup paste', function (e) {
            RecipeNameChange();
        });
    </script>
</asp:Content>
