<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Auth.aspx.cs" Inherits="AuthPack.Auth" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:HiddenField ID="hfOffset" runat="server" />
    
    <%--Offset--%>

    

    </form>
</body>

<script type="text/javascript" language="javascript">

    try {
        var d = new Date();
        document.getElementById("hfOffset").value = d.getTimezoneOffset();
    }
    catch (err) {
    }       
    </script>

</html>



