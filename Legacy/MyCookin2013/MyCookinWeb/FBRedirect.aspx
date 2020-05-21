<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FBRedirect.aspx.cs" Inherits="MyCookinWeb.FBRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <a href="https://www.mycookin.com" target="_blank">
                    <img alt="MyCookin" src="/Images/MyCookinPageTabFacebook.jpg" border="0" width="790"
                         height="500" />
                </a>
            </div>
        </form>
        <script type="text/javascript">

            var _gaq = _gaq || [];
            _gaq.push(['_setAccount', 'UA-39281629-1']);
            _gaq.push(['_setDomainName', 'mycookin.com']);
            _gaq.push(['_setAllowLinker', true]);
            _gaq.push(['_trackPageview']);

            (function() {
                var ga = document.createElement('script');
                ga.type = 'text/javascript';
                ga.async = true;
                ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js';
                var s = document.getElementsByTagName('script')[0];
                s.parentNode.insertBefore(ga, s);
            })();

        </script>
    </body>
</html>