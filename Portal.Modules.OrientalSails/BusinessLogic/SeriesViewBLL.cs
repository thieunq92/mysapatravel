using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class SeriesViewBLL
    {
        public SeriesRepository SeriesRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public BookingHistoryRepository BookingHistoryRepository { get; set; }

        public SeriesViewBLL()
        {
            SeriesRepository = new SeriesRepository();
            BookingRepository = new BookingRepository();
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

            if (BookingHistoryRepository != null)
            {
                BookingHistoryRepository.Dispose();
                BookingHistoryRepository = null;
            }
        }

        public IList<Booking> BookingGetBySeries(int seriesId)
        {
            return BookingRepository.BookingGetBySeries(seriesId);
        }

        public Series SeriesGetById(int seriesId)
        {
            return SeriesRepository.SeriesGetById(seriesId);
        }

        public Booking BookingGetById(int bookingId)
        {
            return BookingRepository.BookingGetById(bookingId);
        }

        public void BookingSaveOrUpdate(Booking booking)
        {
            BookingRepository.SaveOrUpdate(booking);
        }

        public IList<Booking> BookingGetAllByQueryString(NameValueCollection nvcQueryString)
        {
            string taCode = null;
            try
            {
                taCode = nvcQueryString["tc"];
            }
            catch { }

            int bookingCode = -1;
            try
            {
                bookingCode = Int32.Parse(nvcQueryString["bc"].Replace("OS", ""));
            }
            catch { }

            DateTime? startDate = null;
            try
            {
                startDate = DateTime.ParseExact(nvcQueryString["sd"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var seriesId = -1;
            try
            {
                seriesId = Int32.Parse(nvcQueryString["si"]);
            }
            catch { }

            return BookingRepository.SeriesViewBLL_BookingSearchBy(seriesId, taCode, bookingCode, startDate);
        }

        public void BookingHistorySaveOrUpdate(BookingHistory bookingHistory)
        {
            BookingHistoryRepository.SaveOrUpdate(bookingHistory);
        }
    }
}