using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Common;

namespace MyCookin.WebServices.UserBoardWS
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class UsersLikes : System.Web.Services.WebService
    {

        [WebMethod]
        public List<MyUser> UsersLikesList(string ActionType, string IDUserActionFather)
        {
            List<MyUser> UsersLikesList = new List<MyUser>();

            try
            {
                Guid IDUserActionFatherGuid = new Guid(IDUserActionFather);
                
                UserBoard NewUserBoardAction = new UserBoard(MyConvert.ToInt32(ActionType, 3), IDUserActionFatherGuid);

                UsersLikesList = NewUserBoardAction.UsersByActionTypeAndActionFather();
            }
            catch
            { 
            }
 
            return UsersLikesList;
        }
    }
}
