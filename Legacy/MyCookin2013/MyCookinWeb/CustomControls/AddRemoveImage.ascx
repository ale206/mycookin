<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddRemoveImage.ascx.cs" Inherits="MyCookinWeb.CustomControls.AddRemoveImage" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
    <div id="uploadControl" >
                        <asp:AsyncFileUpload runat="server" ID="afuUploadMediaFile" ClientIDMode="Static"
                            OnUploadedComplete="afuUploadMediaFile_UploadedComplete" OnClientUploadComplete="CallJCrop" />
                            <asp:Label ID="lblImgInfo" runat="server" ClientIDMode="Static"></asp:Label></div>
                            <div id="ImageToCrop" >
                            <asp:Image ID="imgUploadedImg" ImageUrl="" runat="server" ClientIDMode="Static" />
                            </div>
                            <table border="0">
                            <tr>
                            <td><div id="DeleteBtn" style="visibility:hidden">
                            <asp:Button ID="btnDeleteImg" runat="server" Text="" 
                            onclick="btnDeleteImg_Click" /><asp:ImageButton AlternateText="Delete Image" 
                                    ID="btnDeleteImg2" runat="server" onclick="btnDeleteImg2_Click" /></div></td>
                            <td> <div id="btnCropContainer" style="visibility:hidden">
                            <asp:Button ID="btnCrop" runat="server" onclick="btnCrop_Click" 
                            Text="" ClientIDMode="Static" OnClientClick="" />
                                <asp:ImageButton AlternateText="Crop Image" ID="btnCrop2" runat="server" 
                                    onclick="btnCrop2_Click" /></div></td>
                            </tr>
                            </table>
                <div id="coords" class="coords" style="visibility:hidden; height:0px">
                <asp:TextBox ID="txtX1" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtY1" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtX2" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtY2" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtW" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtH" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtMinCropWidth" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtMinCropHeight" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtHaveImageLoad" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtCropComplete" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtImageSaved" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtCropAspectRatio" runat="server" ClientIDMode="Static"></asp:TextBox>
                <asp:TextBox ID="txtIDMedia" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtImgName" runat="server"></asp:TextBox>
                 </div>

         <script language="javascript" type="text/javascript">
                     if (document.getElementById('txtHaveImageLoad').value == 'YES') {
                         top.document.getElementById('uploadControl').style.visibility = 'hidden';
                         document.getElementById('DeleteBtn').style.visibility = 'visible';
                     }

</script>