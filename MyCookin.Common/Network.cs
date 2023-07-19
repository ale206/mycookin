using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MyCookin.Common
{
    public class Network
    {
        #region PrivateFields
        private string _from;
        private string _to;
        private string _cc;
        private string _bcc;
        private string _subject;
        private string _message;
        private string _clientSmtp;
        private int _clientSmtpPort;
        private string _smtpServerUsn;
        private string _smtpServerPsw;
        private string _htmlFilePath;
        #endregion

        #region PublicFields
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }
        
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }
        
        public string Cc
        {
            get { return _cc; }
            set { _cc = value; }
        }
        
        public string Bcc
        {
            get { return _bcc; }
            set { _bcc = value; }
        }
        
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string HtmlFilePath
        {
            get { return _htmlFilePath; }
            set { _htmlFilePath = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to Send Simple Email
        /// </summary>
        /// <param name="From">Sender Email Address</param>
        /// <param name="To">Email(s) Address of the message's recipient</param>
        /// <param name="Cc">Carbon Copy Email(s)</param>
        /// <param name="Bcc">Blind Carbon Copy Email(s)</param>
        /// <param name="Subject">A brief summary of the topic of the message</param>
        /// <param name="Message">Email Message - Let it Empty if use HtmlFilePath</param>
        /// <param name="HtmlFilePath">Url of the Html Page (Start with /) as Email Template in Body - Optional</param>
        public Network(string From, string To, string Cc, string Bcc, string Subject, string Message, string HtmlFilePath) 
        {
            _from = From;   
            _to = To;
            _cc = Cc;
            _bcc = Bcc;
            _subject = Subject;
            _message = Message;
            _htmlFilePath = HtmlFilePath;

            _clientSmtp = AppConfig.GetValue("ClientSmtp", AppDomain.CurrentDomain);
            _clientSmtpPort = Convert.ToInt32(AppConfig.GetValue("ClientSmtpPort", AppDomain.CurrentDomain));
            _smtpServerUsn = AppConfig.GetValue("SmtpServerUsn", AppDomain.CurrentDomain);
            _smtpServerPsw = AppConfig.GetValue("SmtpServerPsw", AppDomain.CurrentDomain);
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Send an Email
        /// </summary>
        /// <returns>Returns True if Success, False otherwise</returns>
        public bool SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_clientSmtp, _clientSmtpPort);

                mail.From = new MailAddress(_from);
                mail.To.Add(_to);
                mail.Subject = _subject;

                //Check if we are including an external page html as body
                if (String.IsNullOrEmpty(_htmlFilePath))
                {
                    mail.IsBodyHtml = true;
                    mail.Body = _message;
                }
                else
                {
                    mail.IsBodyHtml = true;

                    //READ FROM URL
                    string url = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + _htmlFilePath;
                    
                    WebRequest WebPageRequest = HttpWebRequest.Create(url);


                    // make request for web page
                    HttpWebResponse WebResponse = (HttpWebResponse)WebPageRequest.GetResponse();

                    StreamReader WebSource = new StreamReader(WebResponse.GetResponseStream());

                    string myPageSource = string.Empty;
                    myPageSource = WebSource.ReadToEnd();
                    
                    //FOR DEBUG: this write the text in a file
                    //StreamWriter srw = new StreamWriter(HttpContext.Current.Server.MapPath("temp.html"), true);
                    //srw.WriteLine(myPageSource);
                    //srw.Close();
                    WebSource.Close();

                    mail.Body = myPageSource;
                    }
                
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_smtpServerUsn, _smtpServerPsw);
                SmtpServer.EnableSsl = MyConvert.ToBoolean(AppConfig.GetValue("EnableSSL", AppDomain.CurrentDomain), true);

                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                string ResultSendEmail = ex.Message;
                return false;
            }
        }

        public void SendEmailThread()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_clientSmtp, _clientSmtpPort);

                mail.From = new MailAddress(_from);
                mail.To.Add(_to);
                mail.Subject = _subject;

                //Check if we are including an external page html as body
                if (String.IsNullOrEmpty(_htmlFilePath))
                {
                    mail.IsBodyHtml = true;
                    mail.Body = _message;
                }
                else
                {
                    mail.IsBodyHtml = true;

                    //READ FROM URL
                    string url = AppConfig.GetValue("WebUrl", AppDomain.CurrentDomain) + _htmlFilePath;

                    WebRequest WebPageRequest = HttpWebRequest.Create(url);


                    // make request for web page
                    HttpWebResponse WebResponse = (HttpWebResponse)WebPageRequest.GetResponse();

                    StreamReader WebSource = new StreamReader(WebResponse.GetResponseStream());

                    string myPageSource = string.Empty;
                    myPageSource = WebSource.ReadToEnd();

                    //FOR DEBUG: this write the text in a file
                    //StreamWriter srw = new StreamWriter(HttpContext.Current.Server.MapPath("temp.html"), true);
                    //srw.WriteLine(myPageSource);
                    //srw.Close();
                    WebSource.Close();

                    mail.Body = myPageSource;
                }

                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_smtpServerUsn, _smtpServerPsw);
                SmtpServer.EnableSsl = MyConvert.ToBoolean(AppConfig.GetValue("EnableSSL", AppDomain.CurrentDomain), true);

                SmtpServer.Send(mail);

                
            }
            catch (Exception ex)
            {
                string ResultSendEmail = ex.Message;
                
            }
        }

        /// <summary>
        /// Get Current Path Url
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPathUrl()
        {
            string host = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] words = host.Split('/');
            string basePath = "";
            for (int i = 0; i < (words.Length) - 1; i++)
            {
                basePath += words[i] + "/";
            }

            return basePath;
        }

                /// <summary>
        /// Get Current Path Url
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPageUrl()
        {
            string host = HttpContext.Current.Request.Url.AbsoluteUri;
            int baseSearch = host.IndexOf("//") + 3;
            int startPath = host.IndexOf("/", baseSearch);
            return host.Substring(startPath);
        }


        /// <summary>
        /// Get name of Current Page
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string currentPage = oInfo.Name;

            return currentPage.ToString();
        }

        /// <summary>
        /// Destroy Cookie
        /// </summary>
        public static void DestroyCookie()
        {
            HttpCookie myCookie = new HttpCookie(AppConfig.GetValue("CookieName", AppDomain.CurrentDomain));

            if (myCookie != null)
            {
                myCookie.Expires = DateTime.UtcNow.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }

        public static string GetIP()
        {
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
 
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
 
            IPAddress[] addr = ipEntry.AddressList;
 
            return addr[addr.Length-1].ToString();
        }

        #endregion
    }
}
