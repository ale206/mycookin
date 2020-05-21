using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.DAL.Contact;
using System.Data.Objects;

namespace MyCookin.ObjectManager.ContactManager
{
    public class ContactRequest
    {
        #region Privatefields
        private Guid _IDContactRequest;
        private int _IDLanguage;
        private string _FirstName;
        private string _LastName;
        private string _Email;
        private string _RequestText;
        private bool _PrivacyAccept;
        private DateTime _RequestDate;
        private string _IpAddress;
        private TypeOfMessage _IDContactRequestType;
        private bool _IsRequestClosed;
        private bool _JustNotClosedRequests;

        private bool _IsError;
        private string _ResultExecutionCode;
        private string _USPReturnValue;
        #endregion

        #region PublicFields
        public Guid IDContactRequest
        {
        get { return _IDContactRequest;}
        set { _IDContactRequest = value;}
        }
        public int IDLanguage
        {
        get { return _IDLanguage;}
        set { _IDLanguage = value;}
        }

        public string FirstName
        {
        get { return _FirstName;}
        set { _FirstName = value;}
        }
        public string LastName
        {
        get { return _LastName;}
        set { _LastName = value;}
        }
        public string Email
        {
        get { return _Email;}
        set { _Email = value;}
        }
        public string RequestText
        {
        get { return _RequestText;}
        set { _RequestText = value;}
        }
        public bool PrivacyAccept
        {
        get { return _PrivacyAccept;}
        set { _PrivacyAccept = value;}
        }
        public DateTime RequestDate
        {
        get { return _RequestDate;}
        set { _RequestDate = value;}
        }
        public string IpAddress
        {
        get { return _IpAddress;}
        set { _IpAddress = value;}
        }
        public TypeOfMessage IDContactRequestType
        {
            get { return _IDContactRequestType; }
            set { _IDContactRequestType = value; }
        }
        public bool IsRequestClosed
        {
            get { return _IsRequestClosed; }
            set { _IsRequestClosed = value; }
        }
        public bool JustNotClosedRequests
        {
            get { return _JustNotClosedRequests; }
            set { _JustNotClosedRequests = value; }
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
        public ContactRequest()
        { 
        }

        //To insert new request
        public ContactRequest(int IDLanguage, string FirstName, string LastName, string Email, string RequestText, bool PrivacyAccept,
                                DateTime RequestDate, string IpAddress, TypeOfMessage IDContactRequestType)
        {
            _IDLanguage = IDLanguage;
            _FirstName = FirstName;
            _LastName = LastName;
            _Email = Email;
            _RequestText = RequestText;
            _PrivacyAccept = PrivacyAccept;
            _RequestDate = RequestDate;
            _IpAddress = IpAddress;
            _IDContactRequestType = IDContactRequestType;

            _IsRequestClosed = false;
        }

        //To insert new request from webservice
        public ContactRequest(int IDLanguage, string FirstName, string LastName, string Email, string RequestText, bool PrivacyAccept,
                                DateTime RequestDate, string IpAddress, int IDContactRequestType)
        {
            _IDLanguage = IDLanguage;
            _FirstName = FirstName;
            _LastName = LastName;
            _Email = Email;
            _RequestText = RequestText;
            _PrivacyAccept = PrivacyAccept;
            _RequestDate = RequestDate;
            _IpAddress = IpAddress;
            _IDContactRequestType = (TypeOfMessage)IDContactRequestType;

            _IsRequestClosed = false;
        }

        //To get requests
        public ContactRequest(TypeOfMessage IDContactRequestType, bool JustNotClosedRequests)
        {
            _JustNotClosedRequests = JustNotClosedRequests;
            _IDContactRequestType = IDContactRequestType;
        }
        #endregion

        #region Methods

        #region InsertNewRequest
        //Insert new request
        public List<ContactRequest> InsertNewRequest()
        {
            List<ContactRequest> ContactRequestList = new List<ContactRequest>();

            ContactEntities ent_Contact = new ContactEntities();

            try
            {
                ObjectResult<USPResult> FirstResultList =
                                ent_Contact.USP_InsertContactRequest(_IDLanguage, (int)_IDContactRequestType, _FirstName, _LastName, _Email, _RequestText, _PrivacyAccept, _RequestDate, _IpAddress, _IsRequestClosed);

                USPResult _result = FirstResultList.First();

                ContactRequestList.Add(
                    new ContactRequest()
                    {
                        _IsError = _result.isError,
                        _ResultExecutionCode = _result.ResultExecutionCode,
                        _USPReturnValue = _result.USPReturnValue       
                    }
                );
            }
            catch (Exception ex)
            {
                ContactRequestList.Add(
                        new ContactRequest()
                        {
                            _IsError = true,
                            _ResultExecutionCode = "",
                            _USPReturnValue = ex.Message
                        }
                    );
            }

            return ContactRequestList;
        }
        #endregion

        //Get Requests
        #region GetRequestMessages
        public List<ContactRequest> GetRequestMessages()
        {
            List<ContactRequest> ContactRequestList = new List<ContactRequest>();

            ContactEntities ent_Contact = new ContactEntities();

            try
            {
                ObjectResult<RequestMessages> ResultList =
                                ent_Contact.USP_GetContactRequests((int)_IDContactRequestType, _JustNotClosedRequests);

                foreach (RequestMessages t in ResultList)
                {
                    ContactRequestList.Add(
                        new ContactRequest()
                        {
                            _IDContactRequest = t.IDContactRequest, 
                            _IDLanguage = t.IDLanguage, 
                            _FirstName = t.FirstName, 
                            _LastName = t.LastName, 
                            _Email = t.Email, 
					        _RequestText = t.RequestText, 
                            _RequestDate = t.RequestDate, 
                            _IpAddress = t.IpAddress,
                            _IDContactRequestType = (TypeOfMessage)t.IDContactRequestType 
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                ContactRequestList.Add(
                        new ContactRequest()
                        {
                            _IsError = true,
                            _ResultExecutionCode = "",
                            _USPReturnValue = ex.Message
                        }
                    );
            }

            return ContactRequestList;
        }
        #endregion

        #endregion
    }
}
