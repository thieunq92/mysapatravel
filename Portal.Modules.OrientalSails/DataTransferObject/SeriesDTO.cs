using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class SeriesDTO
    {
        public int Id { get; set; }

        public string SeriesCode { get; set; }

        public int CutoffDate { get; set; }

        public string NoOfDays { get; set; }

        public string NoOfDaysTrip { get;set; }

        public string BookerName { get; set; }

        public AgencyDTO Agency { get; set; }

        public BookerDTO Booker { get; set; }

        public string SpecialRequest { get; set; }

        public class AgencyDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class BookerDTO
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public string CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string AgencyName { get; set; }

        public string SalesInChargeName { get; set; }

        public string NoOfBooking { get; set; }

        public string Status { get; set; }
    }
}