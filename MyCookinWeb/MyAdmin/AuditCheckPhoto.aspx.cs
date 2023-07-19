using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.AuditManager;
using MyCookin.ObjectManager;
using MyCookin.Common;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ErrorAndMessage;

namespace MyCookinWeb.MyAdmin
{
    public partial class AuditCheckPhoto :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Check Authorization to Visualize this Page
           * If not authorized, redirect to login.
          //*****************************************/
            PageSecurity SecurityPage = new PageSecurity(Session["IDUser"].ToString(), Network.GetCurrentPageName());

            if (!SecurityPage.CheckAuthorization())
            {
                Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl()).ToLower(), true);
            }
            //******************************************

            //Check if user belong group authorized to view this page
            if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("72f85f59-37e9-4bdd-ade2-ea45127f774b") >= 0)
            {
                pnlMyManager.Visible = true;
                pnlNoAuth.Visible = false;
            }
            else
            {
                pnlMyManager.Visible = false;
                pnlNoAuth.Visible = true;
            }

            if (!IsPostBack)
            {
                LoadPictureToCheck();
            }
            

        }

        protected void LoadPictureToCheck()
        {
            List<Audit> AuditList = new List<Audit>();

            Audit AuditObj = new Audit(ObjectType.Photo, 1);

            AuditList = AuditObj.GetAuditEventToCheck();

            lblTitle.Text = "Foto da Controllare " + "(" + AuditObj.NumberOfEventsToCheck() + ")";

            if (AuditList.Count > 0)
            {
                hfIDAuditEvent.Value = AuditList[0].IDAuditEvent.ToString();
                hfObjectID.Value = AuditList[0].ObjectID.ToString();

                imgPhoto.ImageUrl = AuditList[0].ObjectTxtInfo;
                imgPhoto.Height = 180;

                lblMessage.Text = "";

                try
                {
                    Photo PhotoInfo = new Photo(new Guid(AuditList[0].ObjectID.ToString()));
                    PhotoInfo.QueryMediaInfo();

                    MyUser UserOwner = new MyUser(PhotoInfo.MediaOwner);
                    UserOwner.GetUserBasicInfoByID();

                    hlUser.Text = UserOwner.Name + " " + UserOwner.Surname;
                    hlUser.NavigateUrl = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + "/User/" + UserOwner.UserName;
                    hlUser.Target = "_blank";
                }
                catch
                { }

                try
                {
                    string[] words = AuditList[0].ObjectTxtInfo.Split('/');

                    hlPhotoPath.Text = words[2];
                    hlPhotoPath.NavigateUrl = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + AuditList[0].ObjectTxtInfo;
                    hlPhotoPath.Target = "_blank";
                }
                catch
                { }

            }
            else
            {
                lblMessage.Text = "Nessuna Foto da controllare. :)";

                btnImageOk.Visible = false;
                btnImageRemove.Visible = false;

                imgPhoto.Visible = false;
                hlPhotoPath.Visible = false;
                hlUser.Visible = false;

            }
        }


        protected void btnImageOk_Click(object sender, EventArgs e)
        {
            try
            {
                //Update Audit Event
                Audit AuditObj = new Audit(new Guid(hfIDAuditEvent.Value), new Guid(Session["IDUser"].ToString()), false);

                AuditObj.UpdateEvent();

                LoadPictureToCheck();
            }
            catch(Exception ex)
            {
                lblMessage.Text = "Errore nell'aggiornamento dell'evento: " + ex.Message;
            }

        }

        protected void btnImageRemove_Click(object sender, EventArgs e)
        {
            Media MediaAction = new Media(new Guid(hfObjectID.Value));

            //Photo Owner
            Guid MediaOwner = MediaAction.MediaOwner;

            MyUser OwnerUser = new MyUser(MediaOwner);
            OwnerUser.GetUserBasicInfoByID();

            //Delete Photo
            //***********************
            Photo _photo = new Photo(new Guid(hfObjectID.Value));
            _photo.QueryMediaInfo();

            _photo.Checked = true;
            _photo.CheckedByUser = new Guid(Session["IDUser"].ToString());
            _photo.MediaDisabled = true;

            _photo.DeletePhoto();
            //***********************

            //Update Audit Event
            //***********************
            Audit AuditObj = new Audit(new Guid(hfIDAuditEvent.Value), new Guid(Session["IDUser"].ToString()), false);
            AuditObj.UpdateEvent();
            //***********************

            //Set user as spam
            //***********************
            Audit UserSpam = new Audit("User Spam Reported", MediaOwner, ObjectType.UserSpam, Session["IDUser"].ToString(), AuditEventLevel.Hight, DateTime.UtcNow);

            //SP to Add event to DB Audit
            ManageUSPReturnValue resultReportSpam = UserSpam.AddEvent();
            //***********************

            //Send Email
            //***********************
            string From = AppConfig.GetValue("EmailFromProfileUser", AppDomain.CurrentDomain);
            string To = OwnerUser.eMail;
            string Subject = RetrieveMessage.RetrieveDBMessage((int)OwnerUser.IDLanguage, "US-IN-0068");
            string url = "/PagesForEmail/PhotoRemovedTemplate.aspx";

            Network Mail = new Network(From, To, "", "", Subject, "", url);

            System.Threading.Thread ThreadMail = new System.Threading.Thread(new System.Threading.ThreadStart(Mail.SendEmailThread));
            ThreadMail.IsBackground = true;
            ThreadMail.Start();
            //***********************

            LoadPictureToCheck();
        }
    }
}