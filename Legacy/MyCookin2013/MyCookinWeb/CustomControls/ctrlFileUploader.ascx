<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlFileUploader.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlFileUploader" %>
<script type="text/javascript" src="http://bp.yahooapis.com/2.4.21/browserplus-min.js"></script>

<script type="text/javascript" src="/js/PlUpload/plupload.js"></script>
<script type="text/javascript" src="/js/PlUpload/plupload.gears.js"></script>
<script type="text/javascript" src="/js/PlUpload/plupload.silverlight.js"></script>
<script type="text/javascript" src="/js/PlUpload/plupload.flash.js"></script>
<script type="text/javascript" src="/js/PlUpload/plupload.browserplus.js"></script>
<script type="text/javascript" src="/js/PlUpload/plupload.html4.js"></script>
<script type="text/javascript" src="/js/PlUpload/plupload.html5.js"></script>

<asp:HiddenField ID="hfEndPointPath" runat="server" />
<asp:HiddenField ID="hfUploadButtonText" runat="server" />
<asp:HiddenField ID="hfImageName" runat="server" />
<asp:HiddenField ID="hfUploadImgPath" runat="server" />
<asp:HiddenField ID="hfUploadImgMaxSize" runat="server" />
<asp:HiddenField ID="hfAllowMulti" runat="server" Value="false" />
<asp:HiddenField ID="hfMaxNumItem" runat="server" Value="15" />
<asp:HiddenField ID="hfMaxFileNumError" runat="server" Value="Hai superato il numero massimo di file consentiti (residui " />
<asp:HiddenField ID="hfUploadAllowedFileType" runat="server" />
<asp:HiddenField ID="hfIDMedia" runat="server" />
<asp:HiddenField ID="hfIDMediaOwner" runat="server" />
<asp:HiddenField ID="hfMD5Hash" runat="server" />
<asp:HiddenField ID="hfImageMediaType" runat="server" />
<asp:HiddenField ID="hfCropErrorBoxTitle" runat="server" />
<asp:HiddenField ID="hfCropErrorBoxMsg" runat="server" />
<asp:HiddenField ID="hfCropAspectRatio" runat="server" />
<asp:HiddenField ID="hfDeleteWarningMsg" runat="server" />
<asp:HiddenField ID="hfDeleteConfirm" runat="server" />
<asp:HiddenField ID="hfDeleteUndo" runat="server" />
<asp:HiddenField ID="hfBaseFileName" runat="server" />
<asp:HiddenField ID="hfUploadFailText" runat="server" />
<asp:HiddenField ID="hfDragAndDropZoneText" runat="server" />
<asp:HiddenField ID="hfIDLanguage" runat="server" />


<%--NO PUBLIC HIDDEN FILEDS--%>
<asp:HiddenField ID="hfTypeError" runat="server" />
<asp:HiddenField ID="hfSizeError" runat="server" />
<asp:HiddenField ID="hfMinSizeError" runat="server" />
<asp:HiddenField ID="hfEmptyError" runat="server" />
<asp:HiddenField ID="hfNoFilesError" runat="server" />
<asp:HiddenField ID="hfTooManyItemsError" runat="server" />
<%--=======================--%>

<div style="width: auto">
    <div id="boxSelectFile" style="display: inline-block">
        <asp:ImageButton ID="btnSelectFile" runat="server" ImageUrl="/Images/icon_Browse.jpg" Width="42px"/></div>
    <div id="boxUploadFile" style="display: inline-block">
        <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="/Images/icon_upload.png" Width="42px"/></div>
</div>

<div id="containerFileList">
    <div id="filelist"></div>
</div>

<script type="text/javascript">
    // Custom example logic
    function $(id) {
        return document.getElementById(id);
    }

    var fileUploaded = 0;
    var fileAdded = 0;
    var MaxFileToUpload = parseInt('<%=hfMaxNumItem.Value%>');

    var uploader = new plupload.Uploader({
        runtimes: 'gears,html5,flash,silverlight,browserplus,html4',
        browse_button: '<%=btnSelectFile.ClientID%>',
        container: 'containerFileList',
        max_file_size: '<%=hfUploadImgMaxSize.Value%>mb',
        multi_selection: true,
        unique_names: true,
        url: '<%=hfEndPointPath.Value%>',
        //resize : {width : 320, height : 240, quality : 90},
        flash_swf_url: '/js/PlUpload/plupload.flash.swf',
        silverlight_xap_url: '/js/PlUpload/plupload.silverlight.xap',
        filters: [
		{ title: "Allowed Files", extensions: "<%=hfUploadAllowedFileType.Value%>" }
	]
    });

    uploader.bind('Init', function (up, params) {
        $('filelist').innerHTML = "<div>Current runtime: " + params.runtime + "</div>";
    });

    uploader.init();

    uploader.bind('FilesAdded', function (up, files) {
        if (files.length <= MaxFileToUpload) {
            for (var i in files) {
                fileAdded++;
                MaxFileToUpload--;
                $('filelist').innerHTML += '<div id="' + files[i].id + '">' + files[i].name + ' (' + plupload.formatSize(files[i].size) + ') <b></b></div>';
            }
        }
        else {
            for (var i in files) {
                uploader.removeFile(files[i]);
            }
            alert(files.length);
            $('filelist').innerHTML += '<div id="boxMaxFile"><%=hfMaxFileNumError.Value%> ' + MaxFileToUpload + ') <b></b></div>';
        }
    });

    uploader.bind('UploadProgress', function (up, file) {
        $(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
    });

    uploader.bind('Error', function (up, args) {
        //        var props = '';
        //        for (property in args) {
        //            props += property + ': ' + args[property] + '\n';
        //        }
        //        alert(props);
        // IMPLEMENT ERROR MANAGER!
    });

    uploader.bind('FileUploaded', function (up, file, info) {
        if (info['response'].indexOf("Error:") > -1) {
            $(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + info['response'].replace("Error:", "") + "</span>";
        }
        else {
            $(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
        }
        fileUploaded++;
        if (fileUploaded == fileAdded) {
            alert("Finito");
            fileUploaded = 0;
            fileAdded = 0;
        }
    });

    $('uploadfiles').onclick = function () {
        uploader.start();
        return false;
    };


</script>