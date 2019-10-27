using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class CampaignDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDTO CreatedBy { get; set; }
        public ICollection<GoldenDayDTO> GoldenDays { get; set; }
    }
}