using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class GuideDTO : AgencyDTO
    {
        [JsonIgnore]
        ICollection<BusByDateGuideDTO> BusByDatesGuidesDTO { get; set; }
        public GuideDTO(){
            BusByDatesGuidesDTO = new List<BusByDateGuideDTO>();
        }
    }
}