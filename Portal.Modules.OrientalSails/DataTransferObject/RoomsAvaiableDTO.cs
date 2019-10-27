using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class RoomsAvaiableDTO
    {
        public int CruiseId { get; set; }
        public string CruiseName { get; set; }
        public int TotalRoom { get; set; }
        public int NoRUsing { get; set; }
        public int NoRAvaiable { get; set; }
        public DateTime Date { get; set; }
    }
}