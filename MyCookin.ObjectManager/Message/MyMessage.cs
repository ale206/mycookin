using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.ObjectManager.MessageManager;
using MyCookin.DAL.Message;
using System.Data.Objects;
using MyCookin.ObjectManager.UserBoardNotificationsManager;
using MyCookin.ObjectManager.UserBoardManager;
using System.Web;
using MyCookin.ObjectManager.UserManager;
using MyCookin.Log;
using MyCookin.Common;

namespace MyCookin.ObjectManager.MessageManager
{
    public class MyMessage
    {
        #region Privatefields
        private Guid _IDMessage;
        private MessageType _IDMessageType;
        private string _Message;

        #endregion

        #region PublicFields
        public Guid IDMessage
        {
            get { return _IDMessage;}
            set { _IDMessage = value;}
        }
        public MessageType IDMessageType
        {
            get { return _IDMessageType; }
            set { _IDMessageType = value; }
        }
        public string Message
        {
            get { return _Message;}
            set { _Message = value;}
        }
        #endregion

        #region Constructors
        public MyMessage()
        { 
        }

        /// <summary>
        /// Insert New Message and Recipients
        /// </summary>
        /// <param name="IDMessageType"></param>
        /// <param name="Message"></param>
        public MyMessage(MessageType IDMessageType, string Message)
        {
            _IDMessageType = IDMessageType;
            _Message = Message;
        }

        /// <summary>
        /// Select Or Delete
        /// </summary>
        /// <param name="IDMessage"></param>
        public MyMessage(Guid IDMessage)
        {
            _IDMessage = IDMessage;
        }
        #endregion

        #region Methods

        #region InsertNewMessage
        /// <summary>
        /// Insert New Message
        /// </summary>
        /// <returns></returns>
        public Guid InsertNewMessage()
        {
            Guid IDMessage = new Guid();

            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<USPResult> ResultList = ent_MessageChat.USP_InsertMessage((int)_IDMessageType, _Message);
                USPResult _result = ResultList.First();

                bool IsError = _result.isError;
                string ResultExecutionCode = _result.ResultExecutionCode;
                string USPReturnValue = _result.USPReturnValue;

                IDMessage = new Guid(USPReturnValue);
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in ViewConversation(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return IDMessage;
        }
        #endregion

        #region GetMessageInfoByID
        /// <summary>
        /// Get Message Info By ID
        /// </summary>
        /// <returns></returns>
        public List<MyMessage> GetMessageInfoByID()
        {
            List<MyMessage> MessagesList = new List<MyMessage>();

            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<Messages> ResultList = ent_MessageChat.USP_GetMessageInfoByID(_IDMessage);

                foreach (Messages t in ResultList)
                {
                    MessagesList.Add(
                        new MyMessage()
                        {
                            _IDMessage = t.IDMessage,
                            _IDMessageType = (MessageType)t.IDMessageType,
                            _Message = t.Message
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in GetMessageInfoByID(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }

            return MessagesList;
        }
        #endregion

        #endregion
    }
}
