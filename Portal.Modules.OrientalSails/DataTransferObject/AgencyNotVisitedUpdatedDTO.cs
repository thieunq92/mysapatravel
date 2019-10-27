using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class AgencyNotVisitedUpdatedDTO
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public DateTime? LastMeetingDate { get; set; }
        public string MeetingDetails { get; set; }
    }
}