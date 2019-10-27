using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class Top10AgenciesDTO
    {
        public int NumberOfPax { get; set; }
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
    }
}