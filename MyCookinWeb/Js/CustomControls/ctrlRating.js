function RateObject(WebMethodPath, IDObjectToRate, IDUserVoter, RateScore, ErrorMsg,pnlID) {
    //alert(WebMethodPath + ' | ' + IDObjectToRate + ' | ' + IDUserVoter + ' | ' + RateScore);

    if (RateScore == null) {
        RateScore = 0;
    }

    $.ajax({
        //data: "{ 'words': '" + request.term + "', 'LangCode': '<%=LanguageCode%>'}",
        data: "{ IDObjectToVote: '" + IDObjectToRate + "', 'IDUser': '" + IDUserVoter + "','VoteValue': '" + RateScore + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        //url: "http://" + WebServicesPath + "/City/FindCity.asmx/SearchCities",
        url: "http://" + WebServicesPath + WebMethodPath,
        crossDomain: true,
        dataFilter: function (data) { return data; },
        success: function (data) {
            //alert(RateScore);
            //            if (RateScore > 0) {
            //                $(pnlID).raty('readOnly', true);
            //            }
            //            else {
            //                $(pnlID).raty('readOnly', false);
            //            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            try {
                ShowJQuiBoxDialog('', 'ErrorMsg');
            }
            catch (ex) {
                alert(ErrorMsg);
            }
        }
    });
}