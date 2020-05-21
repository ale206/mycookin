<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Default.Master" AutoEventWireup="true" CodeBehind="TestScroll.aspx.cs" Inherits="MyCookinWeb.Message.TestScroll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">


<script type="text/javascript" src="/Js/Scrollbar/jquery.mCustomScrollbar.concat.min.js"></script>

<script type="text/javascript">

    function ActivateScrollerWithCallback(DivId) {
        $("#" + DivId).mCustomScrollbar({
            autoHideScrollbar: true,
            theme: "light-thin",
            callbacks: { onTotalScroll: function () { alert('ciao'); } }
        });
    };

    $(document).ready(function () {
        ActivateScrollerWithCallback("pnlScroll")    
    });

</script>


    <asp:Panel ID="pnlScroll" ClientIDMode="Static" style="height:100px; overflow:auto;" runat="server">
    Scroll down!<br />
    Scroll down!<br />
    Scroll down!<br />
    Scroll down!<br />
    Scroll down!<br />
    Scroll down!<br />
    Scroll down!<br />
    Scroll down!<br />
    Scroll down!<br />
    </asp:Panel>



</asp:Content>
