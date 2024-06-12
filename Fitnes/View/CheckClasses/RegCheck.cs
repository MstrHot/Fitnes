using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fitnes.View.CheckClasses
{
    public class RegCheck
    {
        public static int PasswordCheck(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return 0;
            }
            int result = 0;
            if (password.Length>= 7) 
            {
                result ++;
            }
            if (Regex.Match(password, "[0-9]").Success)
            {
                result ++;
            }
            if (Regex.Match(password, "[A-Z]").Success)
            {
                result ++;
            }
            if (Regex.Match(password, "[a-z]").Success)
            {
                result ++;
            }
            if (Regex.Match(password, "[\\!\\@\\#\\%\\^\\*\\(\\)\\{\\}\\[\\]\\;\\:]").Success)
            {
                result ++;
            }
            if (Regex.Match(password, "[а-яА-яёЁ]").Success)
            {
                throw new Exception("Пароль не может модержать кириллические символы");
            }
            return result;
        }
        public static int TelephoneCheck(string telephone)
        {
            if(string.IsNullOrEmpty(telephone)) 
            {
                return 0;
            }
            if (telephone.Any(x => !Char.IsDigit(x)))
            {
                return 0;
            }

            int result = 0;
           
            if (telephone.Length >= 11)
            {
                result++;
            }
            if(telephone.Length<= 11 & (Regex.Match(telephone, "[^8]").Success || telephone.Length <=11 & Regex.Match(telephone,"[^7]").Success))
            {
                result++;
            }
           


            return  result;
        }
    }
}
