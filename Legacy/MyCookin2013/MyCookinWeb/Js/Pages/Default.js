var boxFrigoInfo;
var BaseSearchPlaceHolder = "";
var FreeFridgeSearchPlaceHolder = "";

var BaseSearchPlaceHolderLang = {
    '2': 'Ho voglia di qualcosa di dolce...',
    '3': 'Quiero algo de dulce ...',
    '1': 'I want something sweet ...',
    '4': 'Je veux quelque chose de sucré ...',
    '5': 'Ich möchte etwas Süßes'
};

var FreeFridgeSearchPlaceHolderLang = {
    '2': 'pasta, tonno, pomodoro',
    '3': 'pasta, atùn, tomate',
    '1': 'pasta, tuna, tomato',
    '4': 'pâtes, thon, tomate',
    '5': 'Nudeln, Thunfisch, Tomaten'
};

function GetBaseSearchPlaceHolder(idLanguage) {
    BaseSearchPlaceHolder = BaseSearchPlaceHolderLang[idLanguage];
    if (BaseSearchPlaceHolder == "") {
        BaseSearchPlaceHolder = BaseSearchPlaceHolderLang['1'];
    }
    return BaseSearchPlaceHolder;
}
function GetFreeFridgeSearchPlaceHolder(idLanguage) {
    FreeFridgeSearchPlaceHolder = FreeFridgeSearchPlaceHolderLang[idLanguage];
    if (FreeFridgeSearchPlaceHolder == "") {
        FreeFridgeSearchPlaceHolder = FreeFridgeSearchPlaceHolderLang['1'];
    }
    return FreeFridgeSearchPlaceHolder;
}