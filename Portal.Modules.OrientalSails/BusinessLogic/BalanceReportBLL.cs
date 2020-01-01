using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class BalanceReportBLL
    {
        public BookingRepository BookingRepository {get;set;}
        public BalanceReportBLL()
        {
            BookingRepository = new BookingRepository();
        }
        public void Dispose()
        {
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
        }

        public List<Booking> BookingGetAllFromDateToDate(DateTime fromDate, DateTime toDate)
        {
            return BookingRepository.BookingGetAllFromDateToDate(fromDate, toDate);
        }
    }
}