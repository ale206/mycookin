<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Offline.aspx.cs" Inherits="MyCookinWeb.Offline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MyCookin - We are sorry</title>
    <link rel="Stylesheet" href="/Styles/SiteStyle/style.css" />
</head>
<body>
    <img src="/Images/Backgrounds/vino2.jpg" alt="Background Image" id="backgroundImage" />
    <form id="form1" runat="server">
    <asp:Panel ID="pnlErrorMessage" ClientIDMode="Static" runat="server">
        <p>
            <asp:HyperLink ID="lnkLogo" runat="server" ImageUrl="/Images/MyCookinLogo-105x105.png"
                NavigateUrl="#"></asp:HyperLink></p>
        <p>
            &nbsp;</p>
        <p>
             <br />
             &nbsp;&nbsp;MyCookin is currently offline<br />
             &nbsp;&nbsp;MyCookin non è momentaneamente disponibile
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;&nbsp;<br />
            &nbsp;&nbsp;</p>
            <p>
            &nbsp;</p>
        <p>
             &nbsp;&nbsp;Please try again in a few minutes. Thank you.<br />
             &nbsp;&nbsp;Riprova tra qualche minuto. Grazie.
        </p>
    </asp:Panel>
    </form>

</body>
</html>
