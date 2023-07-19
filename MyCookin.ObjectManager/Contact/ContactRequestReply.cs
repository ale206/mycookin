using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.Contact;
using System.Data.Objects;

namespace MyCookin.ObjectManager.ContactManager
{
    public class ContactRequestReply
    {
        #region Privatefields
        private Guid _IDContactRequestReply;
        private Guid _IDContactRequest;
        private Guid _IDUserWhoReplied;
        private string _Reply;
        private DateTime _ReplyDate;
        private string _IpAddress;

        private bool _IsError;
        private string _ResultExecutionCode;
        private string _USPReturnValue;
        #endregion

        #region PublicFields
        public Guid IDContactRequestReply
        {
        get { return _IDContactRequestReply;}
        set { _IDContactRequestReply = value;}
        }
        public Guid IDContactRequest
        {
        get { return _IDContactRequest;}
        set { _IDContactRequest = value;}
        }
        public Guid IDUserWhoReplied
        {
        get { return _IDUserWhoReplied;}
        set { _IDUserWhoReplied = value;}
        }
        public string Reply
        {
            get { return _Reply; }
            set { _Reply = value; }
        }
        public DateTime ReplyDate
        {
        get { return _ReplyDate;}
        set { _ReplyDate = value;}
        }
        public string IpAddress
        {
        get { return _IpAddress;}
        set { _IpAddress = value;}
        }
        public bool IsError
        {
            get { return _IsError; }
            set { _IsError = value; }
        }
        public string ResultExecutionCode
        {
            get { return _ResultExecutionCode; }
            set { _ResultExecutionCode = value; }
        }
        public string USPReturnValue
        {
            get { return _USPReturnValue; }
            set { _USPReturnValue = value; }
        }
        #endregion

        #region Constructors

        public ContactRequestReply()
        {

        }

        //To insert Reply
        public ContactRequestReply(Guid IDContactRequest, Guid IDUserWhoReplied, string Reply, DateTime ReplyDate, string IpAddress)
        {
            _IDContactRequest = IDContactRequest;
            _IDUserWhoReplied = IDUserWhoReplied;
            _Reply = Reply;
            _ReplyDate = ReplyDate;
            _IpAddress = IpAddress;
        }
        #endregion

        #region Methods

        #region InsertNewReply
        public List<ContactRequestReply> InsertNewReply()
        {
            List<ContactRequestReply> ContactRequestReplyList = new List<ContactRequestReply>();

            ContactEntities ent_Contact = new ContactEntities();

            try
            {
                ObjectResult<USPResult> FirstResultList =
                                ent_Contact.USP_InsertContactRequestReply(_IDContactRequest, _IDUserWhoReplied, _Reply, _ReplyDate, _IpAddress);

                USPResult _result = FirstResultList.First();

                ContactRequestReplyList.Add(
                    new ContactRequestReply()
                    {
                        _IsError = _result.isError,
                        _ResultExecutionCode = _result.ResultExecutionCode,
                        _USPReturnValue = _result.USPReturnValue
                    }
                );
            }
            catch (Exception ex)
            {
                ContactRequestReplyList.Add(
                        new ContactRequestReply()
                        {
                            _IsError = true,
                            _ResultExecutionCode = "",
                            _USPReturnValue = ex.Message
                        }
                    );
            }

            return ContactRequestReplyList;
        }
        #endregion

        #endregion
    }
}
