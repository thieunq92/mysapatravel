using AutoMapper;
using CMS.Core.Domain;
using Newtonsoft.Json;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for GoldenDayCreateCampaign
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class GoldenDayCreateCampaignWebService : System.Web.Services.WebService
    {
        private GoldenDayCreateCampaignBLL goldenDayCreateCampaignBLL;
        private UserBLL userBLL;
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                    userBLL = new UserBLL();
                return userBLL;
            }
        }
        public GoldenDayCreateCampaignBLL GoldenDayCreateCampaignBLL
        {
            get
            {
                if (goldenDayCreateCampaignBLL == null)
                {
                    goldenDayCreateCampaignBLL = new GoldenDayCreateCampaignBLL();
                }
                return goldenDayCreateCampaignBLL;
            }
        }
        public void Dispose()
        {
            if (goldenDayCreateCampaignBLL != null)
            {
                goldenDayCreateCampaignBLL.Dispose();
                goldenDayCreateCampaignBLL = null;
            }
        }
        [WebMethod]
        public string CampaignSaveOrUpdate(int month, int year)
        {
            var campaign = GoldenDayCreateCampaignBLL.CampaignGetByMonthAndYear(month, year);
            if (campaign == null || campaign.Id == 0) campaign = new Campaign() { CreatedBy = UserBLL.UserGetCurrent() };
            campaign.Month = month;
            campaign.Year = year;
            GoldenDayCreateCampaignBLL.CampaignSaveOrUpdate(campaign);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Campaign, CampaignDTO>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<GoldenDay, GoldenDayDTO>();
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            var campaignDTO = mapper.Map<Campaign, CampaignDTO>(campaign);
            return JsonConvert.SerializeObject(campaignDTO);
        }
        [WebMethod]
        public void GoldenDaySaveOrUpdate(CampaignDTO campaignDTO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CampaignDTO, Campaign>().ForMember(c => c.CreatedBy, mo => mo.Ignore()).AfterMap((cDTO, c) =>
                {
                    foreach (var goldenDay in c.GoldenDays)
                    {
                        goldenDay.Campaign = c;
                    }
                });
                cfg.CreateMap<GoldenDayDTO, GoldenDay>().ForMember(g => g.Campaign, mo => mo.Ignore());
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            var campaign = mapper.Map<CampaignDTO, Campaign>(campaignDTO);
            campaign.CreatedBy = GoldenDayCreateCampaignBLL.UserGetById(campaignDTO.CreatedBy.Id);
            GoldenDayCreateCampaignBLL.CampaignMerge(campaign);
            Dispose();
        }
        [WebMethod]
        public string CampaignGetById(int campaignId)
        {
            var campaign = GoldenDayCreateCampaignBLL.CampaignGetById(campaignId);
            if (campaign == null || campaign.Id == 0) return "";
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Campaign, CampaignDTO>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<GoldenDay, GoldenDayDTO>();
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            var campaignDTO = mapper.Map<Campaign, CampaignDTO>(campaign);
            Dispose();
            return JsonConvert.SerializeObject(campaignDTO);
        }
    }
}
