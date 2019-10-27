using CMS.Core.Domain;
using NHibernate;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class AgencyRepository : RepositoryBase<Agency>
    {
        public AgencyRepository() : base() { }

        public AgencyRepository(ISession session) : base(session) { }

        public IList<Agency> AgencyGetAll()
        {
            return _session.QueryOver<Agency>().Where(x => x.Deleted == false).Future().ToList();
        }

        public Agency AgencyGetById(int agencyId)
        {
            return _session.QueryOver<Agency>().Where(x => x.Deleted == false)
                .Where(x => x.Id == agencyId).FutureValue().Value;
        }

        public IList<Agency> ViewActivitiesBLL_AgencyGetAllBy(int userId, DateTime? from, DateTime? to)
        {
            var query = _session.QueryOver<Agency>();
            if (userId > -1)
            {
                query = query.Where(x => x.CreatedBy.Id == userId || x.ModifiedBy.Id == userId);
            }

            if (from != null)
            {
                query = query.Where(x => x.CreatedDate >= from || x.ModifiedDate >= from);
            }

            if (to != null)
            {
                query = query.Where(x => x.CreatedDate <= to || x.ModifiedDate <= from);
            }

            return query.Future().ToList();
        }
        public IQueryOver<Agency, Agency> AgencyGetAllByRole(Role role)
        {
            return _session.QueryOver<Agency>().Where(x => x.Deleted == false).Where(x => x.Role == role);
        }

        public IQueryOver<Agency, Agency> Guide_AgencyGetAllGuideInDay(Role role, DateTime? date, Route route)
        {
            var query = _session.QueryOver<Agency>();
            Role roleAlias = null;
            query = query.JoinAlias(x => x.Role, () => roleAlias);
            query = query.Where(() => roleAlias.Id == role.Id);
            if (!date.HasValue)
                date = DateTime.Today;
            Expense expenseAlias = null;
            query = query.JoinAlias(x => x.ListExpense, () => expenseAlias);
            query = query.Where(() => expenseAlias.Date == date.Value);
            Cruise cruiseAlias = null;
            query = query.JoinAlias(() => expenseAlias.Cruise, () => cruiseAlias);
            CruiseRoute cruiseRouteAlias = null;
            query = query.JoinAlias(() => cruiseAlias.ListCruiseRoute, () => cruiseRouteAlias);
            Route routeAlias = null;
            query = query.JoinAlias(() => cruiseRouteAlias.Route, () => routeAlias);
            query = query.Where(() => routeAlias.Id == route.Id);
            return query;
        }

        public object AgencyGetAllAgenciesSendNoBookingsLast3Month(User user)
        {
            var last3month = DateTime.Today.AddMonths(-3);
            var agenciesSendBookingsLast3Month = QueryOver.Of<Booking>().Where(x => x.CreatedDate > last3month)
                .Select(x => x.Agency.Id)
                .Select(Projections.Distinct(Projections.Property<Booking>(x => x.Agency.Id)));
            var agenciesSendNoBookingsLast3Month = QueryOver.Of<Agency>()
                .Where(x => x.Sale == user).WithSubquery.WhereProperty(x => x.Id)
                .NotIn(agenciesSendBookingsLast3Month)
                .Select(x => x.Id);
            Agency agencyAlias = null;
            Booking bookingAlias = null;
            var findLastestSendBookingQuery = QueryOver.Of<Booking>().Where(x => x.Agency == bookingAlias.Agency && x.CreatedDate > bookingAlias.CreatedDate).Select(Projections.RowCount());
            var query = _session.QueryOver<Booking>(() => bookingAlias).JoinAlias(x => x.Agency, () => agencyAlias)
                .WithSubquery.WhereProperty(x => x.Agency.Id).In(agenciesSendNoBookingsLast3Month)
                .WithSubquery.WhereValue(1).Gt(findLastestSendBookingQuery)
                .OrderBy(x => x.CreatedDate).Desc;
            return query.Future().ToList();
        }

        public object AgencyGetAllAgenciesNotVisitedInLast2Month(User user)
        {
            var last2month = DateTime.Today.AddMonths(-2);
            Activity activityAlias = null;
            var findLastestVisitedQuery = QueryOver.Of<Activity>()
                .Where(x => x.Params == activityAlias.Params && x.DateMeeting > activityAlias.DateMeeting)
                .Select(Projections.RowCount());
            var agenciesNotVisitedInLast2MonthQuery = QueryOver.Of<Activity>(() => activityAlias)
                .Where(x => x.DateMeeting <= last2month)
                .WithSubquery.WhereValue(1).Gt(findLastestVisitedQuery).Select(x => x.Params);
            var agenciesVisitedInLast2MonthQuery = QueryOver.Of<Activity>(() => activityAlias)
                .Where(x => x.DateMeeting > last2month)
                .WithSubquery.WhereValue(1).Gt(findLastestVisitedQuery).Select(x => x.Params);
            var agenciesNotVisitedAnyTime = _session.QueryOver<Agency>()
                .Where(x => x.Sale == user)
                .WithSubquery.WhereProperty(x => x.Id).NotIn(agenciesNotVisitedInLast2MonthQuery)
                .WithSubquery.WhereProperty(x => x.Id).NotIn(agenciesVisitedInLast2MonthQuery)
                .Future().ToList();
            var activitiesOfAgenciesNotVisitedInLast2Month = _session.QueryOver<Activity>(() => activityAlias).Where(x => x.DateMeeting <= last2month && x.User == user)
                .WithSubquery.WhereValue(1).Gt(findLastestVisitedQuery)
                .OrderBy(x => x.DateMeeting).Asc
                .Future().ToList();
            var agenciesNotUpdateOrNotVisitedAnyTime = new List<object>();
            foreach (var agencyNotVisitedAnyTime in agenciesNotVisitedAnyTime)
            {
                agenciesNotUpdateOrNotVisitedAnyTime.Add(new
                {
                    AgencyId = agencyNotVisitedAnyTime.Id,
                    Name = agencyNotVisitedAnyTime.Name,
                    LastMeeting = "",
                    Note = "",
                });
            }
            foreach (var activityOfAgencyNotVisitedInLast2Month in activitiesOfAgenciesNotVisitedInLast2Month)
            {
                var agencyId = 0;
                try
                {
                    agencyId = Int32.Parse(activityOfAgencyNotVisitedInLast2Month.Params);
                }
                catch { }
                var agency = _session.QueryOver<Agency>().Where(x => x.Id == agencyId).FutureValue().Value;
                var agencyNotUpdate = new
                {
                    AgencyId = agencyId,
                    Name = agency != null ? agency.Name : "",
                    LastMeeting = activityOfAgencyNotVisitedInLast2Month.DateMeeting.ToString("dd/MM/yyyy"),
                    Note = activityOfAgencyNotVisitedInLast2Month.Note
                };
                agenciesNotUpdateOrNotVisitedAnyTime.Add(agencyNotUpdate);
            }
            return agenciesNotUpdateOrNotVisitedAnyTime;
        }

        public object AgencyGetTop10(User user)
        {
            var firstDateOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            var query = _session.QueryOver<Customer>();
            query = query.Where(x => x.Type == CustomerType.Adult || x.Type == CustomerType.Children);
            BookingRoom bookingRoomAlias = null;
            query = query.JoinAlias(x => x.BookingRooms, () => bookingRoomAlias);
            Booking bookingAlias = null;
            query = query.JoinAlias(() => bookingRoomAlias.Book, () => bookingAlias);
            BookingSale bookingSalesAlias = null;
            query = query.JoinAlias(() => bookingAlias.BookingSale, () => bookingSalesAlias);
            if (user != null)
            {
                query = query.Where(() => bookingSalesAlias.Sale == user);
            }
            query = query.Where(() => bookingAlias.Deleted == false);
            query = query.Where(() => bookingAlias.StartDate >= firstDateOfMonth && bookingAlias.StartDate <= lastDateOfMonth);
            query = query.Where(() => bookingAlias.Status == StatusType.Approved);
            Agency agencyAlias = null;
            query = query.JoinAlias(() => bookingAlias.Agency, () => agencyAlias);

            query = query.Select(
                    Projections.Property(()=>agencyAlias.Id),
                    Projections.Property(()=>agencyAlias.Name),
                    Projections.RowCount(),
                    Projections.Group(()=>agencyAlias.Id),
                    Projections.Group(()=>agencyAlias.Name)
                );
            return query.OrderBy(Projections.RowCount()).Desc.Take(10).Future<object[]>().ToList()
                .Select(x=> new { AgencyId = x[0], AgencyName = x[1], NumberOfPax = x[2] });
        }
    }
}