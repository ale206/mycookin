<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MyCookinWeb.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MyCookin - We are sorry</title>
    <link rel="Stylesheet" href="/Styles/SiteStyle/style.css" />
</head>
<body>
    <img src="/Images/Backgrounds/vino2.jpg" alt="Background Image" id="backgroundImage" />
    <form id="form1" runat="server">
    <asp:Panel ID="pnlErrorMessage" ClientIDMode="Static" runat="server">
        <p>
            <asp:HyperLink ID="lnkLogo" runat="server" ImageUrl="/Images/MyCookinLogo-105x105.png"
                NavigateUrl="/default.aspx"></asp:HyperLink></p>
        <p>
            &nbsp;</p>
        <p>
            Ops! <br />
             &nbsp;&nbsp;Something Wrong<br />
             &nbsp;&nbsp;Qualcosa è andato storto
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;&nbsp;We are already working to fix this small issue.<br />
            &nbsp;&nbsp;Siamo già al lavoro per risolvere questo piccolo problema.</p>
            <p>
            &nbsp;</p>
        <p>
             &nbsp;&nbsp;Please Try again later. Thank you.<br />
             &nbsp;&nbsp;Riprova più tardi. Grazie.
        </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/default.aspx">Fai click qui per tornare alla Home Page.</asp:HyperLink></p>
    </asp:Panel>
    </form>

    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-39281629-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>

</body>
</html>
