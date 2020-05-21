<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlRating.ascx.cs" Inherits="MyCookinWeb.CustomControls.ctrlRating" %>
<link href="/Styles/jQueryUiCss/Rating/application.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="/Js/Rating/jquery.raty.js"></script>
<script type="text/javascript" language="javascript" src="/Js/CustomControls/ctrlRating.js"></script>

<asp:HiddenField ID="hfStartScore" runat="server" />
<asp:HiddenField ID="hfIDVote" runat="server" />
<asp:HiddenField ID="hfImageOffPath" runat="server" />
<asp:HiddenField ID="hfImageOnPath" runat="server" />
<asp:HiddenField ID="hfImageHalfPath" runat="server" />
<asp:HiddenField ID="hfStarNumber" runat="server" />
<asp:HiddenField ID="hfReadOnly" runat="server" />
<asp:HiddenField ID="hfEnableCancel" runat="server" />
<asp:HiddenField ID="hfCancelToolTip" Value="Delete this rate" runat="server" />
<asp:HiddenField ID="hfCancelImageOffPath" runat="server" />
<asp:HiddenField ID="hfCancelImageOnPath" runat="server" />
<asp:HiddenField ID="hfWidth" runat="server" />
<asp:HiddenField ID="hfRatedValue" runat="server" />
<asp:HiddenField ID="hfRatingWebMethodPath" runat="server" />
<asp:HiddenField ID="hfIDObjectToRate" runat="server" />
<asp:HiddenField ID="hfIDUserVoter" runat="server" />
<asp:HiddenField ID="hfRateResult" runat="server" />
<asp:HiddenField ID="hfNoRate" runat="server" Value="Fai il login per votare questa ricetta." />
<asp:HiddenField ID="hfMessageOnError" runat="server" Value="Error saving your rate, please try later." />
<asp:HiddenField ID="hfReadOnlyMsg" runat="server" Value="You have already rate this object." />
<asp:HiddenField ID="hfRated" runat="server" Value="false" />

<asp:Panel ID="pnlRate" runat="server" meta:resourcekey="pnlRateResource1">
</asp:Panel>
<script type="text/javascript">
    function StartRaty() {
        $('#<%=pnlRate.ClientID%>').raty({
            score: parseFloat('<%=hfStartScore.Value%>'),
            starOff: '<%=hfImageOffPath.Value%>',
            starOn: '<%=hfImageOnPath.Value%>',
            starHalf: '<%=hfImageHalfPath.Value%>',
            number: parseFloat('<%=hfStarNumber.Value%>'),
            readOnly: Boolean.parse('<%=hfReadOnly.Value%>'),
            cancel: Boolean.parse('<%=hfEnableCancel.Value%>'),
            cancelOff: '<%=hfCancelImageOffPath.Value%>',
            cancelOn: '<%=hfCancelImageOnPath.Value%>',
            cancelHint: '<%=hfCancelToolTip.Value%>',
            width: parseFloat('<%=hfWidth.Value%>'),
            noRatedMsg: '<%=hfNoRate.Value%>',
            targetType: 'number',
            hints: '',
            half: true,
            click: function (score, evt) {
                //alert('ID: ' + $(this).attr('id') + "\nscore: " + score + "\nevent: " + evt);
                $('#<%=hfRatedValue.ClientID%>').val(score);
                RateObject('<%=hfRatingWebMethodPath.Value%>',
                        '<%=hfIDObjectToRate.Value%>',
                        '<%=hfIDUserVoter.Value%>',
                        score,
                        '<%=hfMessageOnError.Value%>',
                        '#<%=pnlRate.ClientID%>');
            }
        });
    }
</script>


