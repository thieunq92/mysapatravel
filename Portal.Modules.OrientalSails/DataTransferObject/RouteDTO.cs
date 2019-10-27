using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class RouteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<BusTypeDTO> ListBusTypeDTO { get; set; }
        public string Way { get; set; }
    }
}