<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlMultiUpload.ascx.cs" Inherits="PlUploadTest.ctrlMultiUpload" %>
<div style="display:none">
<asp:Button ID="btnPostBackEvent" runat="server" Text="btnPostBackEvent" 
    onclick="btnPostBackEvent_Click" 
        meta:resourcekey="btnPostBackEventResource1" />
  <asp:Button ID="btnReset" runat="server" Text="btnPostBackEventReset" 
    onclick="btnReset_Click" meta:resourcekey="btnResetResource1" />  
    </div>

<script type="text/javascript" src="/Js/PlUpload/plupload.js"></script>
<script type="text/javascript" src="/Js/PlUpload/plupload.gears.js"></script>
<script type="text/javascript" src="/Js/PlUpload/plupload.silverlight.js"></script>
<script type="text/javascript" src="/Js/PlUpload/plupload.flash.js"></script>
<script type="text/javascript" src="/Js/PlUpload/plupload.browserplus.js"></script>
<script type="text/javascript" src="/Js/PlUpload/plupload.html4.js"></script>
<script type="text/javascript" src="/Js/PlUpload/plupload.html5.js"></script>


<asp:HiddenField ID="hfMaxFileNumber" runat="server" />
<asp:HiddenField ID="hfMaxFileSizeInMB" runat="server" />
<asp:HiddenField ID="hfAllowedFileTypes" runat="server" />
<asp:HiddenField ID="hfUploadConfig" runat="server" />
<asp:HiddenField ID="hfBaseFileName" runat="server" />
<asp:HiddenField ID="hfMaxFileNumErrorMessage" runat="server" />
<asp:HiddenField ID="hfFileCreatedIDsList" runat="server" />
<asp:HiddenField ID="hfUploadHandlerURL" runat="server" />
<asp:HiddenField ID="hfMediaOwner" runat="server" />
<asp:HiddenField ID="hfErrorReportFromServer" runat="server" />

<asp:Panel ID="pnlUploadContainer" runat="server" 
    meta:resourcekey="pnlUploadContainerResource1">
    <asp:Panel ID="pnlFileList" runat="server" 
        meta:resourcekey="pnlFileListResource1">
        <asp:Label ID="lblErrorMessage" runat="server" 
            Text="Error on browser upload module, update your browser or choose onother one." 
            meta:resourcekey="lblErrorMessageResource1"></asp:Label></asp:Panel>
    <asp:Panel ID="pnlErrorAndWarning" runat="server" 
        meta:resourcekey="pnlErrorAndWarningResource1"></asp:Panel>
    <asp:Label ID="lblTextToDisplay" runat="server" 
        meta:resourcekey="lblTextToDisplayResource1"></asp:Label>
    <br />
    <asp:HyperLink ID="lnkSelectFiles" runat="server" NavigateUrl="#" 
        meta:resourcekey="lnkSelectFilesResource1"></asp:HyperLink>
    <asp:Panel ID="pnlUploadButton" runat="server" 
        meta:resourcekey="pnlUploadButtonResource1">
        <asp:HyperLink ID="lnkUploadFiles" runat="server" NavigateUrl="#" 
            meta:resourcekey="lnkUploadFilesResource1"></asp:HyperLink>
    </asp:Panel>
    <asp:Panel ID="pnlUploading" runat="server" 
        meta:resourcekey="pnlUploadingResource1">
        <asp:Label ID="lblUploading" runat="server" Text="Sto caricando la tua foto" 
            CssClass="lblLoading" meta:resourcekey="lblUploadingResource1"></asp:Label>
        <br /><asp:Image ID="imgLoading" runat="server" ImageUrl="/Images/loadingLineOrange.gif"
        Height="20px" Width="220px" CssClass="btnSaveRecipe" 
            meta:resourcekey="imgLoadingResource1"/>
    </asp:Panel>
</asp:Panel>
