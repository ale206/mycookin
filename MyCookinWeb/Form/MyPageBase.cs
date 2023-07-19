using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;
using MyCookin.Common;
using MyCookinWeb.Utilities;

namespace MyCookinWeb.Form
{
    public abstract class MyPageBase : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            
            if (Session["IDLanguage"] != null)
            {
                int _idLanguage;
                if (Request.QueryString["IDLanguage"] != null)
                {
                    try
                    {
                        _idLanguage = MyConvert.ToInt32(Request.QueryString["IDLanguage"].ToString(), 1);
                    }
                    catch
                    {
                        _idLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                    }
                }
                else
                {
                    _idLanguage = MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1);
                }

                MyCulture _culture = new MyCulture(_idLanguage);
                CultureInfo _cultureInfo = new CultureInfo(_culture.GetCompleteLanguageCodeByIDLang());
                Thread.CurrentThread.CurrentUICulture = _cultureInfo;
                Thread.CurrentThread.CurrentCulture = _cultureInfo;
            }
            base.InitializeCulture();
        }

        public static void NavHistoryClear()
        {
            try
            {
                HttpContext.Current.Session["navHistory"] = "";
            }
            catch
            {
            }
        }

        public static void NavHistoryAddUrl(string Url)
        {
            try
            {
                if (HttpContext.Current.Session["navHistory"].ToString().IndexOf(Url) > -1)
                {
                    HttpContext.Current.Session["navHistory"] = HttpContext.Current.Session["navHistory"].ToString().Replace(Url + "|-|", "");
                }
                HttpContext.Current.Session["navHistory"] += Url + "|-|";
            }
            catch
            {
            }
        }

        public static void NavHistoryRemoveUrlFrom(string Url)
        {
            try
            {
                if (HttpContext.Current.Session["navHistory"].ToString().IndexOf(Url) > -1)
                {
                    HttpContext.Current.Session["navHistory"] = HttpContext.Current.Session["navHistory"].ToString().Replace(Url + "|-|", "");
                }
            }
            catch
            {
            }
        }

        public static string NavHistoryGetPrevUrl(string CurrentUrl)
        {
            try
            {
                if (HttpContext.Current.Session["navHistory"].ToString().IndexOf("|-|") > -1)
                {
                    HttpContext.Current.Session["navHistory"] = HttpContext.Current.Session["navHistory"].ToString().Replace(CurrentUrl + "|-|", "");
                    string[] _separator = new string[] { "|-|" };
                    String[] _arrayUrl = HttpContext.Current.Session["navHistory"].ToString().Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                    string _lastUrl = _arrayUrl[_arrayUrl.Length - 1];
                    //HttpContext.Current.Session["navHistory"] = HttpContext.Current.Session["navHistory"].ToString().Replace(_lastUrl + "|-|", "");
                    return _lastUrl;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}