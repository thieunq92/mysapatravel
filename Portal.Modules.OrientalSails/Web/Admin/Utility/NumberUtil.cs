using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Portal.Modules.OrientalSails.Web.Admin.Utility
{
    public static class NumberUtil
    {
        public static string FormatPhoneNumber(string phoneNum)
        {
            if (String.IsNullOrEmpty(phoneNum)) {
                return "";
            }
            if (phoneNum.IndexOf("&") > 0) {
                phoneNum = phoneNum.Substring(0, phoneNum.IndexOf("&"));
            }
            Regex regexObj = new Regex(@"[^\d]");
            phoneNum = regexObj.Replace(phoneNum, "");


            if (phoneNum.Length > 0)
            {
                phoneNum = Regex.Replace(phoneNum, @"(\d{4})(\d{3})(\d{1,})", "$1.$2.$3");
            }

            return phoneNum;
        }

        public static string FormatMoney(double money)
        {
            return money.ToString("#,##0.##");
        }
    }
}