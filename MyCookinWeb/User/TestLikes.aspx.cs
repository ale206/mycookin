using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.UserBoardManager;
using MyCookin.Common;

namespace MyCookinWeb.User
{
    public partial class TestLikes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersLikesList("12", "a302b6d1-a744-4722-83d8-e86f8fc5effc");
        }

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