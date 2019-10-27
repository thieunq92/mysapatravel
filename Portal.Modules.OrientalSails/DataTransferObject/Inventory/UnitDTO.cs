using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class UnitDTO
    {
        public int Id { get; set; }
        public string Math { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int ParentId { get; set; }
        public int Rate { get; set; }
    }
}