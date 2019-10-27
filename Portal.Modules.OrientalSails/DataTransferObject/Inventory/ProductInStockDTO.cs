using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class ProductInStockDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int StorageId { get; set; }
        public string StorageName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int WarningLimit { get; set; }
    }
}