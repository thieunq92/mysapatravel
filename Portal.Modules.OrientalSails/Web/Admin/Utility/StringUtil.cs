using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Web.Admin.Utility
{
    public class StringUtil
    {
        /// <summary>
        /// Lấy kí tự đầu tiền của mỗi từ trong chuỗi
        /// </summary>
        public static string GetFirstLetter(string str)
        {
            var output = "";
            str.Split(' ').ToList().ForEach(x =>
            {
                output += x[0] + " ";
            });
            return output;
        }
    }
}