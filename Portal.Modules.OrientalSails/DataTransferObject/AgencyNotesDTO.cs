using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class AgencyNotesDTO
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int AgencyId { get; set; }
        public int RoleId { get; set; }
    }
}