<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlEditImage.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlEditImage" %>
<%--Hidden Fields for control--%>
<asp:HiddenField ID="hfIDMedia" runat="server" />
<asp:HiddenField ID="hfMediaSizeType" runat="server" />
<asp:HiddenField ID="hfMediaChanged" runat="server" />
<asp:HiddenField ID="hfMediaType" runat="server" />
<asp:HiddenField ID="hfEnableEditing" runat="server" />
<asp:HiddenField ID="hfEnableUpload" runat="server" />
<asp:HiddenField ID="hfIsFirstLoad" runat="server" />
<%--Hidden Fields for crop--%>
<asp:HiddenField ID="hfX1" runat="server" />
<asp:HiddenField ID="hfY1" runat="server" />
<asp:HiddenField ID="hfX2" runat="server" />
<asp:HiddenField ID="hfY2" runat="server" />
<asp:HiddenField ID="hfWidth" runat="server" />
<asp:HiddenField ID="hfHeight" runat="server" />
<asp:HiddenField ID="hfMinCropWidth" runat="server" />
<asp:HiddenField ID="hfMinCropHeight" runat="server" />
<asp:HiddenField ID="hfCropAspectRatio" runat="server" />
<asp:HiddenField ID="hfPopUpTitle" runat="server" />
<asp:Panel ID="pnlMainCtrlEditImage" runat="server" 
    meta:resourcekey="pnlMainCtrlEditImageResource1">
    <asp:UpdatePanel ID="upnMainCtrlEditImage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlShowedImage" runat="server" CssClass="showedImageBox" 
                meta:resourcekey="pnlShowedImageResource1">
                <asp:Image ID="imgShowedImage" runat="server" 
                    meta:resourcekey="imgShowedImageResource1" />
                <asp:Panel ID="pnlEditButton" runat="server" CssClass="editButtonBox" 
                    meta:resourcekey="pnlEditButtonResource1">
                    <asp:Button ID="btnEditImage" runat="server" Text="Edit" CssClass="editButton" 
                        meta:resourcekey="btnEditImageResource1" />
                </asp:Panel>
                <asp:Panel ID="pnlUpdateProgress" runat="server" CssClass="updatingBox" 
                    meta:resourcekey="pnlUpdateProgressResource1">
                <asp:UpdateProgress ID="upImageUpdating" runat="server" 
                        AssociatedUpdatePanelID="upnMainCtrlEditImage">
                    <ProgressTemplate>
                        <asp:Image ID="imgLoading" runat="server" 
                            ImageUrl="/Images/Loader/ajax-loader_blu01.gif" 
                            meta:resourcekey="imgLoadingResource1"></asp:Image>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlEditImage" runat="server" 
                meta:resourcekey="pnlEditImageResource1">
                <asp:Image ID="imgImageToCrop" runat="server" 
                    meta:resourcekey="imgImageToCropResource1" />
                <div style="display: none;">
                    <asp:Button ID="btnCrop" runat="server" Text="Crop" OnClick="btnCrop_Click" 
                        meta:resourcekey="btnCropResource1" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
