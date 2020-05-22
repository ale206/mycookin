using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using AutoMapper;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Network.Dto;

namespace TaechIdeas.Core.BusinessLogic.Network
{
    public class NetworkManager : INetworkManager
    {
        private readonly INetworkRepository _networkRepository;
        private readonly INetworkConfig _networkConfig;
        private readonly IMapper _mapper;

        public NetworkManager(INetworkRepository networkRepository, INetworkConfig networkConfig, IMapper mapper)
        {
            _networkRepository = networkRepository;
            _networkConfig = networkConfig;
            _mapper = mapper;
        }

        public SaveEmailToSendOutput SaveEmailToSend(SaveEmailToSendInput saveEmailToSendInput)
        {
            if (string.IsNullOrEmpty(saveEmailToSendInput.From))
            {
                throw new Exception("Field FROM Empty!");
            }

            if (string.IsNullOrEmpty(saveEmailToSendInput.To))
            {
                throw new Exception("Field TO Empty!");
            }

            return _mapper.Map<SaveEmailToSendOutput>(_networkRepository.SaveEmailToSend(_mapper.Map<SaveEmailToSendIn>(saveEmailToSendInput)));
        }

        public IEnumerable<EmailsToSendOutput> EmailsToSend(EmailsToSendInput emailsToSendInput)
        {
            return _mapper.Map<IEnumerable<EmailsToSendOutput>>(_networkRepository.EmailsToSend(_mapper.Map<EmailsToSendIn>(emailsToSendInput)));
        }

        public SendEmailOutput SendEmail(SendEmailInput sendEmailInput)
        {
            if (string.IsNullOrEmpty(sendEmailInput.From))
            {
                throw new Exception("Field FROM Empty!");
            }

            if (string.IsNullOrEmpty(sendEmailInput.To))
            {
                throw new Exception("Field TO Empty!");
            }

            var updateEmailStatusIn = _mapper.Map<UpdateEmailStatusIn>(sendEmailInput);

            try
            {
                var clientSmtp = _networkConfig.ClientSmtp;
                var clientSmtpPort = _networkConfig.ClientSmtpPort;
                var smtpServerUsn = _networkConfig.SmtpServerUsn;
                var smtpServerPsw = _networkConfig.SmtpServerPsw;

                var mail = new MailMessage();
                var smtpServer = new SmtpClient(clientSmtp, clientSmtpPort);

                mail.From = new MailAddress(sendEmailInput.From);
                mail.To.Add(sendEmailInput.To);
                mail.Subject = sendEmailInput.Subject;

                //Check if we are including an external page html as body
                if (string.IsNullOrEmpty(sendEmailInput.HtmlFilePath))
                {
                    mail.IsBodyHtml = true;
                    mail.Body = sendEmailInput.Message;
                }
                else
                {
                    mail.IsBodyHtml = true;

                    //READ FROM URL
                    var url = Path.Combine(_networkConfig.WebUrl, sendEmailInput.HtmlFilePath);

                    var webPageRequest = WebRequest.Create(url);

                    // make request for web page
                    var webResponse = (HttpWebResponse) webPageRequest.GetResponse();

                    var webSource = new StreamReader(webResponse.GetResponseStream());

                    var myPageSource = webSource.ReadToEnd();

                    //FOR DEBUG: this write the text in a file
                    //StreamWriter srw = new StreamWriter(HttpContext.Current.Server.MapPath("temp.html"), true);
                    //srw.WriteLine(myPageSource);
                    //srw.Close();
                    webSource.Close();

                    mail.Body = myPageSource;
                }

                smtpServer.UseDefaultCredentials = true;
                smtpServer.Credentials = new NetworkCredential(smtpServerUsn, smtpServerPsw);
                smtpServer.EnableSsl = _networkConfig.EnableSsl;

                smtpServer.Send(mail);

                updateEmailStatusIn.EmailStatus = (int) EmailStatus.Sent;
            }
            catch (Exception ex)
            {
                updateEmailStatusIn.EmailStatus = (int) EmailStatus.Error;
            }

            return _mapper.Map<SendEmailOutput>(_networkRepository.UpdateEmailStatus(updateEmailStatusIn));
        }

        /// <summary>
        ///     Get Current Path Url
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPathUrl()
        {
            throw new NotImplementedException();

            //var host = HttpContext.Current.Request.Url.AbsoluteUri;
            //var words = host.Split('/');
            //var basePath = "";
            //for (var i = 0; i < (words.Length) - 1; i++)
            //{
            //    basePath += words[i] + "/";
            //}

            //return basePath;
        }

        /// <summary>
        ///     Get Current Path Url
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPageUrl()
        {
            throw new NotImplementedException();

            //var host = HttpContext.Current.Request.Url.AbsoluteUri;
            //var baseSearch = host.IndexOf("//") + 3;
            //var startPath = host.IndexOf("/", baseSearch);
            //return host.Substring(startPath);
        }

        /// <summary>
        ///     Get name of Current Page
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPageName()
        {
            throw new NotImplementedException();
            //var sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            //var oInfo = new System.IO.FileInfo(sPath);
            //var currentPage = oInfo.Name;

            //return currentPage.ToString();
        }

        /// <summary>
        ///     Destroy Cookie
        /// </summary>
        public void DestroyCookie()
        {
            throw new NotImplementedException();

            //var myCookie = new HttpCookie(_networkConfig.CookieName); //new HttpCookie(_appConfigManager.GetValue("CookieName", AppDomain.CurrentDomain));

            //if (myCookie != null)
            //{
            //    myCookie.Expires = DateTime.UtcNow.AddDays(-1);
            //    HttpContext.Current.Response.Cookies.Add(myCookie);
            //}
        }

        public string GetIp()
        {
            var strHostName = "";
            strHostName = Dns.GetHostName();

            var ipEntry = Dns.GetHostEntry(strHostName);

            var addr = ipEntry.AddressList;

            return addr[addr.Length - 1].ToString();
        }
    }
}