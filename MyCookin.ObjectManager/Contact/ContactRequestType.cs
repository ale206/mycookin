using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCookin.ObjectManager.ContactManager
{
    public enum TypeOfMessage : int
    {
        NotDefined = 0,
        GenericHelp = 1,
        RequestInformation = 2,
        CommercialInformations = 3,
        ReportBug = 4,
        Advertising = 5
    }

    public class ContactRequestType
    {
        #region Privatefields
        private int _IDContactRequestType;
        private string _RequestType;
        private DateTime _RequestTypeAddedOn;
        private bool _Enabled;
        #endregion

        #region PublicFields
        public int IDContactRequestType
        {
        get { return _IDContactRequestType;}
        set { _IDContactRequestType = value;}
        }
        public string RequestType
        {
        get { return _RequestType;}
        set { _RequestType = value;}
        }
        public DateTime RequestTypeAddedOn
        {
        get { return _RequestTypeAddedOn;}
        set { _RequestTypeAddedOn = value;}
        }
        public bool Enabled
        {
        get { return _Enabled;}
        set { _Enabled = value;}
        }

        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion
        

    }
}
