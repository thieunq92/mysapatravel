using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class BusByDateGuideDTO
    {
        public int Id { get; set; }
        [JsonIgnore]
        public BusByDateDTO BusByDateDTO { get; set; }
        public GuideDTO GuideDTO { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}