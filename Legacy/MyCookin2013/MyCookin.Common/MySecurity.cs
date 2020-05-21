using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace MyCookin.Common
{
    public class MySecurity
    {
        public static string GenerateSHA1Hash(string textToHash)
        {

            string textHashed;

            SHA1 hash = SHA1.Create();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] combined = encoder.GetBytes(textToHash);
            hash.ComputeHash(combined);
            textHashed = Convert.ToBase64String(hash.Hash);

            return textHashed;
        }

        public static string GenerateMD5FileHash(string filePath)
        {
            string MD5HashValue="";
            FileStream fsFile = File.OpenRead(filePath);
            MD5 hashMD5 = MD5.Create();
            byte[] mHash = hashMD5.ComputeHash(fsFile);
            MD5HashValue = Convert.ToBase64String(mHash);
            return MD5HashValue;
        }


        public class PasswordAdvisor
        {
            public enum PasswordScore
            {
                Blank = 0,
                VeryWeak = 1,
                Weak = 2,
                Medium = 3,
                Strong = 4,
                VeryStrong = 5
            }

            public static PasswordScore CheckPasswordStrength(string password, string username)
            {
                int score = 1;

                if (password.Length < 1)
                    return PasswordScore.Blank;
                //if (password.Length < 5)
                    //return PasswordScore.VeryWeak;
                if (password.Length >= 4)
                score++;

                //List of Stupid words
                #region StupidWords
                //Write words in Lower Case
                string[] StupidWordsArray = { "12345", "0000", "pippo", "paperino", "password", username };

                int pos = Array.IndexOf(StupidWordsArray, password.ToLower());
                if (pos > -1)
                {
                    // the array contains the string and the pos variable
                    // will have its position in the array
                    return PasswordScore.VeryWeak;
                }
                #endregion

                //Win 1 point if you write more than 6 characters
                if (password.Length >= 6)
                    score++;
                //Win 1 point if you write more than 12 characters
                if (password.Length >= 12)
                    score++;
                //Win 1 point if you write at least one character (alphabet in uppercase or lowercase)
                if (Regex.Match(password, @".*[a-z].*", RegexOptions.ECMAScript).Success &&
                    Regex.Match(password, @".*[A-Z].*", RegexOptions.ECMAScript).Success)
                    score++;
                //Win 1 point if you write at least one number
                if (Regex.Match(password, @".*[0-9].*", RegexOptions.ECMAScript).Success)
                    score++;
                //Win 1 point if you write at least one special char
                if (Regex.Match(password, @".*[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)].*", RegexOptions.ECMAScript).Success)
                    score++;

                return (PasswordScore)score;
            }
        }
    }
}
