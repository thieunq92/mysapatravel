using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class SalesMonthSummaryDTO
    {
        public int SalesId { get; set; }
        public string SalesUserName { get; set; }
        public int NumberOfBookings { get; set; }
        public int NumberOfPax2Days { get; set; }
        public int NumberOfPax3Days { get; set; }
        public decimal Revenue { get; set; }
        public int MeetingReports { get; set; }
    }
}