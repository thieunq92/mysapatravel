using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Util
{
    public class IvProductUtil
    {
        public static double ConvertRateParentUnit(int quanity, IvUnit unit)
        {
            var result = 0.0;
            if (unit.Parent != null)
            {
                var rate = unit.Rate;
                if (unit.Math == "/")
                {
                    result = Convert.ToDouble(quanity) / Convert.ToDouble(rate);
                }
                else if (unit.Math == "*")
                    result = quanity * rate;
            }
            else
            {
                result = quanity;
            }
            return result;
        }
    }
}