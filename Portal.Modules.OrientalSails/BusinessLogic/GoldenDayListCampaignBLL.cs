using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class GoldenDayListCampaignBLL
    {
        public CampaignRepository CampaignRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public GoldenDayListCampaignBLL()
        {
            CampaignRepository = new CampaignRepository();
            BookingRepository = new BookingRepository();
        }

        public void Dispose()
        {
            if (CampaignRepository != null)
            {
                CampaignRepository.Dispose();
                CampaignRepository = null;
            }
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
        }

        public IEnumerable<Campaign> CampaignGetAll()
        {
            return CampaignRepository.CampaignGetAll();
        }

        public IEnumerable<Booking> BookingGetAllNewBookingsByCampaign(Campaign campaign)
        {
            return BookingRepository.BookingGetAllNewBookingsByCampaign(campaign);
        }

        public Campaign CampaginGetById(int campaignId)
        {
            return CampaignRepository.GetById(campaignId);
        }

        public IEnumerable<Campaign> CampaignGetAllPaged(int pageSize, int pageIndex, out int count)
        {
            return CampaignRepository.CampaignGetAllPaged(pageSize, pageIndex, out count);
        }

        public void CampaignDelete(Campaign campaign)
        {
            CampaignRepository.Delete(campaign);
        }
    }
}