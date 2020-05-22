using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class FbUserInfo
    {
        #region PrivateFields

        #endregion

        #region PublicFields

        public string ID { get; set; }

        public string username { get; set; }

        public string name { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string link { get; set; }

        public DateTime? birthday { get; set; }

        public int? gender { get; set; }

        public string locale { get; set; }

        #endregion

        #region Constructors

        public FbUserInfo()
        {
            ID = ID;
            username = username;
            name = name; //FullName
            first_name = first_name;
            last_name = last_name;
            link = link;
            birthday = birthday;
            gender = gender;
            locale = locale;
        }

        #endregion
    }
}