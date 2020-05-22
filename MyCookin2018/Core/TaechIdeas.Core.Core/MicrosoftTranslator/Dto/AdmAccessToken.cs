using System.Runtime.Serialization;

namespace TaechIdeas.Core.Core.MicrosoftTranslator.Dto
{
    [DataContract]
    public class AdmAccessToken
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string token_type { get; set; }

        [DataMember]
        public string expires_in { get; set; }

        [DataMember]
        public string scope { get; set; }
    }
}