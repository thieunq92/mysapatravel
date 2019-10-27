using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class SeriesManagerBLL
    {
        public SeriesRepository SeriesRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public PermissionBLL PermissionBLL { get; set; }
        public UserBLL UserBLL { get; set; }
        public BookingHistoryRepository BookingHistoryRepository { get; set; }

        public SeriesManagerBLL()
        {
            SeriesRepository = new SeriesRepository();
            BookingRepository = new BookingRepository();
            PermissionBLL = new PermissionBLL();
            UserBLL = new UserBLL();
            BookingHistoryRepository = new BookingHistoryRepository();
        }

        public void Dispose()
        {
            if (SeriesRepository != null)
            {
                SeriesRepository.Dispose();
                SeriesRepository = null;
            }

            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }

            if (PermissionBLL != null)
            {
                PermissionBLL.Dispose();
                PermissionBLL = null;
            }

            if (UserBLL != null)
            {
                UserBLL.Dispose();
                UserBLL = null;
            }

            if (BookingHistoryRepository != null)
            {
                BookingHistoryRepository.Dispose();
                BookingHistoryRepository = null;
            }
        }

        public IList<Series> SeriesBookingGetAllByQueryString(System.Collections.Specialized.NameValueCollection nvcQueryString, int pageSize, int currentPageIndex, out int count)
        {
            string partnerName = null;
            try
            {
                partnerName = nvcQueryString["p"];
            }
            catch { }

            string seriesCode = null;
            try
            {
                seriesCode = nvcQueryString["sc"];
            }
            catch { }

            int agencyId = -1;
            try
            {
                agencyId = Int32.Parse(nvcQueryString["ai"]);
            }
            catch { }

            int salesInCharge = -1;
            try
            {
                salesInCharge = Int32.Parse(nvcQueryString["sic"]);
            }
            catch { }

            var haveViewAllSeriesPermission = PermissionBLL.UserCheckPermission(UserBLL.UserGetCurrent().Id, (int)PermissionEnum.VIEWALLSERIES);
            if (!haveViewAllSeriesPermission)
            {
                salesInCharge = UserBLL.UserGetCurrent().Id;
            }

            return SeriesRepository.SeriesGetByQueryString(partnerName, seriesCode, agencyId, salesInCharge, pageSize, currentPageIndex, out count);
        }

        public Series SeriesGetById(int seriesId)
        {
            return SeriesRepository.SeriesGetById(seriesId);
        }

        public void BookingSaveOrUpdate(Booking booking)
        {
            var sessionBooking = BookingRepository.GetById(booking.Id);
            BookingRepository.SaveOrUpdate(sessionBooking);
        }

        public void BookingHistorySaveOrUpdate(BookingHistory bookingHistory)
        {
            BookingHistoryRepository.SaveOrUpdate(bookingHistory);
        }
    }
}