using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class GoldenDayCreateCampaignBLL
    {
        public CampaignRepository CampaignRepository { get; set; }
        public UserRepository UserRepository { get; set; }
        public GoldenDayCreateCampaignBLL()
        {
            CampaignRepository = new CampaignRepository();
            UserRepository = new UserRepository();
        }
        public void Dispose()
        {
            if (CampaignRepository != null)
            {
                CampaignRepository.Dispose();
                CampaignRepository = null;
            }
            if (UserRepository != null)
            {
                UserRepository.Dispose();
                UserRepository = null;
            }
        }
        public void CampaignSaveOrUpdate(Campaign campaign)
        {
            CampaignRepository.SaveOrUpdate(campaign);
        }

        public Campaign CampaignGetById(int campaignId)
        {
            return CampaignRepository.GetById(campaignId);
        }

        public void CampaignMerge(Campaign campaign)
        {
            CampaignRepository.Merge(campaign);
        }

        public Campaign CampaignGetByMonthAndYear(int month, int year)
        {
            return CampaignRepository.CampaignGetByMonthAndYear(month, year);
        }

        public User UserGetById(int userId)
        {
            return UserRepository.GetById(userId);
        }
    }
}