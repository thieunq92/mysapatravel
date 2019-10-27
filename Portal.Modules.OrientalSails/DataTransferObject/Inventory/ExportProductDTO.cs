using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class ExportProductDTO
    {
        public int Discount { get; set; }
        public int DiscountType { get; set; }
        public int ExportId { get; set; }
        public int ExportProductId { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double QuanityRateParentUnit { get; set; }
        public int Quantity { get; set; }
        public int StorageId { get; set; }
        public double Total { get; set; }
        public double UnitPrice { get; set; }
        public int UnitId { get; set; }
    }
}