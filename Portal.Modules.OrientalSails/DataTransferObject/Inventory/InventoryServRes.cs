using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class InventoryServRes
    {
        public string Status { get; set; }
        public string Exceptions { get; set; }
        public List<int> BillFails { get; set; }
        public Dictionary<int, int> BillSuccess { get; set; }
        public List<int> ProductFails { get; set; }
        public Dictionary<int, int> BillProductFails { get; set; }
        public Dictionary<int, int> BillProductSuccess { get; set; }
    }
}