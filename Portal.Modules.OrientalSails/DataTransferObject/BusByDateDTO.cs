using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class BusByDateDTO
    {
        public int Id { get; set; }
        public int Group { get; set; }
        public int? SupplierId { get; set; }
        public string Driver_Name { get; set; }
        public string Driver_Phone { get; set; }
        public bool Deleted { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Baby { get; set; }
        public string PaxString
        {
            get
            {
                if (Child <= 0 && Baby <= 0)
                {
                    return (Adult + Child + Baby) + " pax";
                }
                return String.Format("{0} ({1}a,{2}b,{3}c)", (Adult + Child + Baby), Adult, Child, Baby);
            }
        }
        public string RouteName { get; set; }
        public string BusTypeName { get; set; }
        public ICollection<BusByDateGuideDTO> BusByDatesGuidesDTO { get; set; }
        public BusByDateDTO()
        {
            BusByDatesGuidesDTO = new List<BusByDateGuideDTO>();
        }
    }
}