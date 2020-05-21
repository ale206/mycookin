using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.Message;
using System.Data.Objects;
using MyCookin.Log;
using MyCookin.Common;
using System.Web;

namespace MyCookin.ObjectManager.MessageManager
{
    public enum MessageType : int
    {
        Message = 1,
        Chat = 2
    }

    public class MyMessageType
    {
        #region Privatefields
        private MessageType _IDMessageType;
        private string _MessageType;
        private int _MessageMaxLength;
        private bool _MessageTypeEnabled;
        #endregion

        #region PublicFields
        public MessageType IDMessageType
        {
            get { return _IDMessageType; }
            set { _IDMessageType = value; }
        }
        public string MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; }
        }
        public int MessageMaxLength
        {
            get { return _MessageMaxLength; }
            set { _MessageMaxLength = value; }
        }
        public bool MessageTypeEnabled
        {
            get { return _MessageTypeEnabled; }
            set { _MessageTypeEnabled = value; }
        }
        #endregion

        #region Constructors
        public MyMessageType()
        { 
        }

        /// <summary>
        /// Select TypeOfMessage Informations according to ID
        /// </summary>
        /// <param name="IDMessageType"></param>
        public MyMessageType(MessageType IDMessageType)
        {
            _IDMessageType = IDMessageType;
        }
        #endregion

        #region Methods

        #region GetTypeOfMessageInfoByID
        /// <summary>
        /// Get TypeOfMessage Informations according to ID
        /// </summary>
        /// <returns></returns>
        public List<MyMessageType> GetTypeOfMessageInfoByID()
        {
            List<MyMessageType> MessageTypeList = new List<MyMessageType>();

            try
            {
                DBMessageChatEntity ent_MessageChat = new DBMessageChatEntity();

                ObjectResult<MessagesTypes> ResultList = ent_MessageChat.USP_GetTypeOfMessageInfoByID((int)_IDMessageType);

                foreach (MessagesTypes t in ResultList)
                {
                    MessageTypeList.Add(
                        new MyMessageType()
                        {
                            _IDMessageType = (MessageType)t.IDMessageType,
                            _MessageType = t.MessageType,
                            _MessageMaxLength = t.MessageMaxLength,
                            _MessageTypeEnabled = t.MessageTypeEnabled
                        });
                }
            }
            catch (Exception ex)
            {
                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRowForLog = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "", "Error in GetTypeOfMessageInfoByID(): " + ex.Message, HttpContext.Current.Session["IDUser"].ToString(), true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRowForLog);
                    LogManager.WriteFileLog(LogLevel.Errors, true, NewRowForLog);
                }
                catch { }
            }
            return MessageTypeList;
        }
        #endregion

        #endregion


    }
}
