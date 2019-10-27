using AutoMapper;
using Newtonsoft.Json;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for AgencyViewWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class AgencyViewWebService : System.Web.Services.WebService
    {
        private AgencyViewBLL agencyViewBLL;
        public AgencyViewBLL AgencyViewBLL
        {
            get
            {
                if (agencyViewBLL == null)
                    agencyViewBLL = new AgencyViewBLL();
                return agencyViewBLL;
            }
        }
        public new void Dispose()
        {
            if (agencyViewBLL != null)
            {
                agencyViewBLL.Dispose();
                agencyViewBLL = null;
            }
        }
        [WebMethod]
        public string AgencyNotesGetById(int agencyNotesId)
        {
            var agencyNotes = AgencyViewBLL.AgencyNotesGetById(agencyNotesId);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AgencyNotes, AgencyNotesDTO>()
                    .ForMember(and => and.AgencyId, opt => opt.MapFrom(an => an.Agency.Id))
                    .ForMember(and => and.RoleId, opt => opt.MapFrom(an => an.Role.Id));
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            var agencyNotesDTO = mapper.Map<AgencyNotes, AgencyNotesDTO>(agencyNotes);
            Dispose();
            return JsonConvert.SerializeObject(agencyNotesDTO);
        }
    }
}
