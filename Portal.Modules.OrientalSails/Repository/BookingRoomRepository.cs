using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class BookingRoomRepository : RepositoryBase<BookingRoom>
    {
        public BookingRoomRepository() : base() { }

        public BookingRoomRepository(ISession session) : base(session) { }


        public BookingRoom BookingRoomGetById(int bookingRoomId)
        {
            return _session.QueryOver<BookingRoom>().Where(x => x.Id == bookingRoomId).FutureValue().Value;
        }

        public IEnumerable<BookingRoom> BookingRoomGetAllByBooking(Booking booking)
        {
            var query = _session.QueryOver<BookingRoom>();
            if (booking != null)
            {
                query = query.Where(x => x.Book == booking);
            }
            return query.Future().ToList();
        }

        public IEnumerable<BookingRoom> BookingRoomGetAllByCriterion(Cruise cruise, DateTime? date)
        {
            var query = _session.QueryOver<BookingRoom>();
            Booking bookingAlias = null;
            query = query.JoinAlias(x => x.Book, () => bookingAlias);

            if (cruise != null)
            {
                query = query.Where(() => bookingAlias.Cruise == cruise);
            }
            if (date != null)
            {
                query = query.Where(() => (bookingAlias.EndDate > date && bookingAlias.StartDate > date.Value.AddDays(-1) && bookingAlias.StartDate < date.Value.AddDays(1)) || (bookingAlias.StartDate < date && bookingAlias.EndDate > date));
            }
            query = query.Where(() => bookingAlias.Deleted == false);
            query = query.Where(() => bookingAlias.Status != StatusType.Cancelled && bookingAlias.Status != StatusType.CutOff);
            return query.Future().ToList();
        }

        public int BookingRoomGetRowCountByCriterion(Cruise cruise, DateTime? date) {
            var query = _session.QueryOver<BookingRoom>();
            Booking bookingAlias = null;
            query = query.JoinAlias(x => x.Book, () => bookingAlias);

            if (cruise != null)
            {
                query = query.Where(() => bookingAlias.Cruise == cruise);
            }
            if (date != null)
            {
                query = query.Where(() => (bookingAlias.EndDate > date && bookingAlias.StartDate > date.Value.AddDays(-1) && bookingAlias.StartDate < date.Value.AddDays(1)) || (bookingAlias.StartDate < date && bookingAlias.EndDate > date));
            }
            query = query.Where(() => bookingAlias.Deleted == false);
            query = query.Where(() => bookingAlias.Status != StatusType.Cancelled && bookingAlias.Status != StatusType.CutOff);
            query = query.Select(Projections.RowCount());
            return query.FutureValue<int>().Value;
        }
    }
}