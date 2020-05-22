using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.Core.BusinessLogic.Common
{
    public class MySecurityManager : IMySecurityManager
    {
        public string GenerateSha1Hash(string textToHash)
        {
            string textHashed;

            var hash = SHA1.Create();
            var encoder = new ASCIIEncoding();
            var combined = encoder.GetBytes(textToHash);
            hash.ComputeHash(combined);
            textHashed = Convert.ToBase64String(hash.Hash);

            return textHashed;
        }

        public string GenerateMd5FileHash(string filePath)
        {
            var MD5HashValue = "";
            var fsFile = File.OpenRead(filePath);
            var hashMD5 = MD5.Create();
            var mHash = hashMD5.ComputeHash(fsFile);
            MD5HashValue = Convert.ToBase64String(mHash);
            return MD5HashValue;
        }

        public PasswordScore CheckPasswordStrength(string password, string username)
        {
            var score = 1;

            if (password.Length < 1)
            {
                return PasswordScore.Blank;
            }

            //if (password.Length < 5)
            //return PasswordScore.VeryWeak;
            if (password.Length >= 4)
            {
                score++;
            }

            //List of Stupid words

            #region StupidWords

            //Write words in Lower Case
            string[] StupidWordsArray = {"12345", "0000", "pippo", "paperino", "password", username};

            var pos = Array.IndexOf(StupidWordsArray, password.ToLower());
            if (pos > -1)
            {
                // the array contains the string and the pos variable
                // will have its position in the array
                return PasswordScore.VeryWeak;
            }

            #endregion

            //Win 1 point if you write more than 6 characters
            if (password.Length >= 6)
            {
                score++;
            }

            //Win 1 point if you write more than 12 characters
            if (password.Length >= 12)
            {
                score++;
            }

            //Win 1 point if you write at least one character (alphabet in uppercase or lowercase)
            if (Regex.Match(password, @".*[a-z].*", RegexOptions.ECMAScript).Success &&
                Regex.Match(password, @".*[A-Z].*", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            //Win 1 point if you write at least one number
            if (Regex.Match(password, @".*[0-9].*", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            //Win 1 point if you write at least one special char
            if (Regex.Match(password, @".*[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)].*", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            return (PasswordScore) score;
        }
    }
}