using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class GoldenDayDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Policy { get; set; }
        public string DateAsString
        {
            get
            {
                return Date.ToString("dd/MM/yyyy");
            }
        }
    }
}