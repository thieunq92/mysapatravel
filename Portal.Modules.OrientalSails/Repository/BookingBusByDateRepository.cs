using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class BookingBusByDateRepository : RepositoryBase<BookingBusByDate>
    {
        public BookingBusByDateRepository() { }
        public BookingBusByDateRepository(ISession session) : base(session) { }

        public IQueryOver<BookingBusByDate, BookingBusByDate> BookingBusByDateGetAllByCriterion(Booking booking, BusByDate busByDate)
        {
            var query = _session.QueryOver<BookingBusByDate>();
            if (booking != null)
            {
                query = query.Where(x => x.Booking == booking);
            }
            BusByDate busByDateAlias = null;
            query = query.JoinAlias(x => x.BusByDate, () => busByDateAlias);
            if (busByDate != null)
            {
                query = query.Where(() => busByDateAlias.Id == busByDate.Id);
            }
            return query;
        }

        public IQueryOver<BookingBusByDate, BookingBusByDate> BookingBusByDateGetAllByCriterion(Route route, BusType busType, int group)
        {
            var query = _session.QueryOver<BookingBusByDate>();
            BusByDate busByDateAlias = null;
            query = query.JoinAlias(x=>x.BusByDate,()=>busByDateAlias);
            Route routeAlias = null;
            query = query.JoinAlias(()=>busByDateAlias.Route,()=>routeAlias);
            if (route != null && route.Id > 0) {
                query = query.Where(() => routeAlias.Id == route.Id);
            }
            BusType busTypeAlias = null;
            query = query.JoinAlias(() => busByDateAlias.BusType, () => busTypeAlias);
            if (busType != null && busType.Id > 0)
            {
                query = query.Where(() => busTypeAlias.Id == busType.Id);
            }
            if (group > 0)
            {
                query = query.Where(() => busByDateAlias.Group == group);
            }
            return query;
        }
    }
}