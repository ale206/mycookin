using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace MyCookin.API.Helpers
{
    public static class OpenIdHelper
    {
        public static string GetJwks(TokenValidationParameters tokenValidationParameters)
        {
            // This line in Fargate doesn't work as it doesn't have access to the internet so it is temporarly a copy and paste from https://cognito-idp.eu-west-1.amazonaws.com/eu-west-1_Zrq7io2kN/.well-known/jwks.json
            //return new WebClient().DownloadString(tokenValidationParameters.ValidIssuer + \"/.well-known/jwks.json\");
            return "{\"keys\":[{\"alg\":\"RS256\",\"e\":\"AQAB\",\"kid\":\"By1xxyTDJUZT7yr5XjQQN4/UYWprm2vieENlrC99PC4=\",\"kty\":\"RSA\",\"n\":\"i3VxFc69AE6bNHrwjM3N1s1dRVm8ecaeS59UfW9Z_sSIJbB_ZEoZQ2t2IjG25XfqkPsp5wd2NpbIdmQQGSiM4Q0Md5PV12y1ApFTEoPpC4AWYG6tZq2IgXwQlzenP2riIHJsoVuskpedNQFaFoDKIQNvbSK4hvtsYoFO7Gs3KLtYXqJkObg4SAVPdhiXsG2UiQT5Pc4rR5-r7NpSPpG_aRkiU_A5prX6gUzX11kwRB-SM_7CySAdgkD0t3Es-2tdMJgraVQ9yUn4xRxO0pTunFcTnroZIgyXYI7aYq15Eq0mLw9tHB8XMrD5ZDxgKL0RjpnaoPhauaOePzxAeK6qlQ\",\"use\":\"sig\"},{\"alg\":\"RS256\",\"e\":\"AQAB\",\"kid\":\"6taOMd1DU36oao9XbCk1j58IFdeSMV/5GwzMJ/DpJTg=\",\"kty\":\"RSA\",\"n\":\"p0QthlbkPt-8GJBOiR1H5116qAr5CG2CiYGjLo4_q5REYOR2CWNdqlhGAj1ISoBK5ymMyKx8dgoTE4lsJJC2qrjLAGTuwn9gYrdtjF9Y3d_KgGfpQ3KlLL-WFIfaS1SfSl2hjVRJz0Ob_Ifa3j_QJ62UtDBAurIMh_XrkV6sCD6QJMFt_PbadTmtVsVvZ1ue2R4dheHNW3B26RjeQ0LfxyGmS8vQGC44gkFLTkRlExG0GLDe1bmaI1tQnssacmev3hvAIY7XvhQWmgxf744RjDUx00B7CP2wsH6Pv_EC9rnS8EEt_VW6Ry37bsESjli23lmr_9_tJc4-5DpksTLvlQ\",\"use\":\"sig\"}]}";
        }
    }
}