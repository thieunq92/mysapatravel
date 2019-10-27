using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class ProductPriceDTO
    {
        public double Price { get; set; }
        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public int UnitId { get; set; }
    }
}