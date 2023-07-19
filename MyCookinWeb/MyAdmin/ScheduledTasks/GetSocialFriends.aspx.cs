using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using System.Data;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Log;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb.MyAdmin.ScheduledTasks
{
    public partial class GetSocialFriends :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GetSocialNetworkFriends())
            {
                lblExecutionResult.Text = "ok";
            }
            else
            {
                lblExecutionResult.Text = "errors";
            }

        }

        public static bool GetSocialNetworkFriends()
        {
            try
            {
                //Check Users from table SocialLogins Where FriendsRetrievedOn is null or quite old

                DataTable DT_IDUsersWithOldFriendsRetrievedOn = new DataTable();

                DT_IDUsersWithOldFriendsRetrievedOn = MyUserSocial.GetIDUsersWithOldFriendsRetrievedOn();

                //For each User, instantiate class MyUserSocial, get Token, get friends.
                if (DT_IDUsersWithOldFriendsRetrievedOn.Rows.Count > 0)
                {
                    string ExecutionResult = String.Empty;

                    Guid IDUser = new Guid();
                    int IDSocialNetwork;

                    int i = 0;

                    foreach (DataRow row in DT_IDUsersWithOldFriendsRetrievedOn.Rows)
                    {
                        try
                        {
                            IDUser = DT_IDUsersWithOldFriendsRetrievedOn.Rows[i].Field<Guid>("IDUser");
                            IDSocialNetwork = DT_IDUsersWithOldFriendsRetrievedOn.Rows[i].Field<int>("IDSocialNetwork");

                            MyUserSocial UserSocial = new MyUserSocial(IDUser, IDSocialNetwork);

                            UserSocial.GetUserFriendsFromSocialNetwork(UserSocial.AccessToken, UserSocial.RefreshToken);

                            //WRITE A ROW IN STATISTICS DB
                            MyStatistics NewStatisticUser = new MyStatistics(IDUser, null, StatisticsActionType.SC_SocialFriendsRetrieved, "Social Friends Retrieved", Network.GetCurrentPageName(), "", "");
                            NewStatisticUser.InsertNewRow();

                        }
                        catch
                        {
                            //WRITE A ROW IN LOG FILE AND DB
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0019", "Error on Social Friends Retrieving ", IDUser.ToString(), true, false);
                            LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                            LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                        }
                        
                        i += 1;
                       
                    }
                }

                return true;
            }
            catch
            {
                //WRITE A ROW IN LOG FILE AND DB
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-0019", "Generic Error on Social Friends Retrieving ", "", true, false);
                LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                LogManager.WriteFileLog(LogLevel.Errors, true, NewRow);

                return false;
            }
        }

    }
}