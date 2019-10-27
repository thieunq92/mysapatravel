using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class MeetingDTO
    {
        public string UpdateTime { get; set; }
        public string DateMeeting { get; set; }
        public string SalesName { get; set; }
        public string MeetWith { get; set; }
        public string Position { get; set; }
        public string BelongToAgency { get; set; }
        public string Note { get; set; }
    }
}