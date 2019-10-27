using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Utils
{
    public class MiscUtility
    {
        public static string StringIndent(int level)
        {
            var str = string.Empty;
            for (var i = 0; i < level; i++)
            {
                //str = str + "...........";
                str = str +
                      HttpUtility.HtmlDecode(".&#160;&#160;&#160;");
            }
            return str;
        }
    }
}