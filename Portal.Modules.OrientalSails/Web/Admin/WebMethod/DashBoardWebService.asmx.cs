using AutoMapper;
using CMS.Core.Domain;
using Newtonsoft.Json;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for DashBoardWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class DashBoardWebService : System.Web.Services.WebService
    {
        private DashBoardBLL dashBoardBLL;
        public DashBoardBLL DashBoardBLL
        {
            get
            {
                if (dashBoardBLL == null)
                    dashBoardBLL = new DashBoardBLL();
                return dashBoardBLL;
            }
        }
        public new void Dispose()
        {
            if (dashBoardBLL != null)
            {
                dashBoardBLL.Dispose();
                dashBoardBLL = null;
            }
        }
        [WebMethod]
        public string AgencyContactGetByAgencyId(string ai)
        {
            var agencyId = 0;
            try
            {
                agencyId = Int32.Parse(ai);
            }
            catch { }
            var agencyContacts = DashBoardBLL.AgencyContactGetByAgencyId(agencyId).Select(x => new { Id = x.Id, Name = x.Name, Position = x.Position });
            Dispose();
            return JsonConvert.SerializeObject(agencyContacts);
        }
        [WebMethod]
        public string GoldenDayGetAllInMonthByDate(DateTime date)
        {
            var firstDateOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            var goldenDays = DashBoardBLL.GoldenDayGetAllByDateRange(firstDateOfMonth, lastDateOfMonth);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GoldenDay, GoldenDayDTO>();
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            var goldenDaysDTO = mapper.Map<IEnumerable<GoldenDay>, IEnumerable<GoldenDayDTO>>(goldenDays);
            Dispose();
            return JsonConvert.SerializeObject(goldenDaysDTO);
        }
        [WebMethod]
        public string ActivityGetById(int activityId)
        {
            var activity = DashBoardBLL.ActivityGetById(activityId);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Activity, ActivityDTO>()
                    .ForMember(x => x.AgencyId, opt => opt.Ignore())
                    .ForMember(x => x.AgencyName, opt => opt.Ignore())
                    .ForMember(x => x.ContactId, opt => opt.Ignore())
                    .ForMember(x => x.ContactName, opt => opt.Ignore())
                    .ForMember(x => x.ContactPosition, opt => opt.Ignore())
                    .ForMember(x => x.CruiseId , opt=>opt.Ignore());
                cfg.CreateMap<User, UserDTO>();
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            var activityDTO = mapper.Map<Activity,ActivityDTO>(activity);
            var agency = DashBoardBLL.AgencyGetById(Int32.Parse(activity.Params));
            activityDTO.AgencyId = agency.Id;
            activityDTO.AgencyName = agency.Name;
            var contact = DashBoardBLL.AgencyContactGetById(activity.ObjectId);
            activityDTO.ContactId = contact.Id;
            activityDTO.ContactName = contact.Name;
            activityDTO.ContactPosition = contact.Position;
            activityDTO.CruiseId = activity.Cruise != null ? activity.Cruise.Id : 0;
            Dispose();
            return JsonConvert.SerializeObject(activityDTO);
        }
    }
}
