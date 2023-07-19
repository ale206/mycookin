using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace AuthPack
{
    public class FacebookData
    {

    }
    [DataContract]
    public class FacebookMe
    {
        [DataMember]
        public string id;
        [DataMember]
        public string username;
        [DataMember]
        public string name;
        [DataMember]
        public string birthday;
        [DataMember]
        public string email;
        [DataMember]
        public string first_name;
        [DataMember]
        public string gender;
        [DataMember]
        public string last_name;
        [DataMember]
        public string link;
        [DataMember]
        public string locale;
        [DataMember]
        public string picture;
        [DataMember]
        public string verified_email;

    }
}