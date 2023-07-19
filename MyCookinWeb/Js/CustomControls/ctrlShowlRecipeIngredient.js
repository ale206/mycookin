//PopBox Info panel
function StartPopBox(pnlMainName, lnkName, pnlBoxName) {
    $(pnlMainName).popbox({
        'open': lnkName,
        'box': pnlBoxName,
        'arrow': '.arrow',
        'arrow-border': '.arrow-border',
        'close': '.closePopBox'
    });
}


function StartPopBoxAsync(pnlMainName, lnkName, pnlBoxName) {
    $(pnlMainName).popbox({
        'open': lnkName,
        'box': pnlBoxName,
        'arrow': '.arrow',
        'arrow-border': '.arrow-border',
        'close': '.closePopBox'
    });
}