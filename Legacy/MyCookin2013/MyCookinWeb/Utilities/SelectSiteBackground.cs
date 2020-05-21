using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookinWeb.Utilities
{
    public class SelectSiteBackground
    {
        public static string GetBackgroundURL(int hour)
        {
            MediaType TypeToUse = MediaType.NotSpecified;

            switch (hour)
            {
                case 6:
                case 7:
                case 8:
                case 9:
                    TypeToUse = MediaType.SiteBackgroundBreakfast;
                    break;
                case 10:
                case 11:
                    TypeToUse = MediaType.SiteBackgroundMorning;
                    break;
                case 12:
                case 13:
                case 14:
                    TypeToUse = MediaType.SiteBackgroundLunch;
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                    TypeToUse = MediaType.SiteBackgroundAfternoon;
                    break;
                case 19:
                case 20:
                case 21:
                case 22:
                    TypeToUse = MediaType.SiteBackgroundDinner;
                    break;
                case 23:
                case 0:
                case 1:
                case 2:
                case 3:
                    TypeToUse = MediaType.SiteBackgroundNight;
                    break;
                case 4:
                case 5:
                    TypeToUse = MediaType.SiteBackgroundLateNight;
                    break;
                  
            }
            string _return = "";
            try
            {
                _return = Media.GetOneRandomMediaByMediaType(TypeToUse).Rows[0]["MediaPath"].ToString();
            }
            catch
            {
            }
            return _return;
        }
    }
}