using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCookinWeb.Auth
{
    public class FbUserInfo
    {
        #region PrivateFields
        string _ID;
        string _username;
        string _name;
        string _first_name;
        string _last_name;
        string _link;
        DateTime? _birthday;
        int? _gender;
        string _locale;
        #endregion

        #region PublicFields
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        public string link
        {
            get { return _link; }
            set { _link = value; }
        }
        public DateTime? birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        public int? gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        public string locale
        {
            get { return _locale; }
            set { _locale = value; }
        }
        #endregion

        #region Constructors
        public FbUserInfo()
        {
            _ID = ID;
            _username = username;
            _name = name;                //FullName
            _first_name = first_name;
            _last_name = last_name;
            _link = link;
            _birthday = birthday;
            _gender = gender;
            _locale = locale;
        }
        #endregion
    }
}