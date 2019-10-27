using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class ExportDTO
    {
        public int BookingRoomId { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public bool Deleted { get; set; }
        public string Detail { get; set; }
        public int ExportId { get; set; }
        public int Id { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Name { get; set; }
        public double Pay { get; set; }
        public int RoomId { get; set; }
        public int CruiseId { get; set; }
        public IvExportType Status { get; set; }
        public int StorageId { get; set; }
        public double Total { get; set; }
        public virtual DateTime ExportDate { get; set; }

        public List<ExportProductDTO> ExportProducts { get; set; }
        public bool IsDebt { get; set; }
        public string Agency { get; set; }

    }
}