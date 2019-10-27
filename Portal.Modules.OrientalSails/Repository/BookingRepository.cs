using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using NHibernate.Criterion;
using System.Linq.Expressions;
using Portal.Modules.OrientalSails.Utils;
using NHibernate.Transform;
using CMS.Core.Domain;
using NHibernate.Dialect.Function;

namespace Portal.Modules.OrientalSails.Repository
{
    public class BookingRepository : RepositoryBase<Booking>
    {
        public BookingRepository() : base() { }

        public BookingRepository(ISession session) : base(session) { }

        public int BookingCountByStatusAndDate(Web.Util.StatusType statusType, DateTime date)
        {
            return _session.QueryOver<Booking>()
                .Where(x => x.Deleted == false)
                .Where(x => x.Status == statusType && x.StartDate > date)
                .Select(Projections.RowCount()).FutureValue<int>().Value;
        }

        public int MyBookingPendingCount()
        {
            var userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            return _session.QueryOver<Booking>()
                .Where(x => x.Deleted == false)
                .Where(x => x.Status == StatusType.Pending && x.Deadline >= DateTime.Now)
                .Where(x => x.CreatedBy.Id == userId || x.Sale.Id == userId)
                .Select(Projections.RowCount()).FutureValue<int>().Value;
        }

        public int MyTodayBookingPendingCount()
        {
            var userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            return _session.QueryOver<Booking>()
                .Where(x => x.Deleted == false)
                .Where(x => x.Status == StatusType.Pending && x.Deadline >= DateTime.Now)
                .Where(x => x.CreatedBy.Id == userId || x.Sale.Id == userId)
                .Where(x => x.Deadline >= DateTime.Now && x.Deadline <= DateTime.Now.AddHours(36))
                .Select(Projections.RowCount()).FutureValue<int>().Value;
        }

        public int SystemBookingPendingCount()
        {
            return _session.QueryOver<Booking>()
                .Where(x => x.Deleted == false)
                .Where(x => x.Status == StatusType.Pending && x.Deadline >= DateTime.Now)
                .Select(Projections.RowCount()).FutureValue<int>().Value;
        }

        public IList<Booking> BookingListBLL_BookingSearchBy(int bookingId, int tripId, int cruiseId, int status,
            DateTime? startDate, string customerName, int agencyId, int batchId,
            int pageSize, int currentPageIndex, out int count)
        {
            var query = QueryOver.Of<Booking>().Where(x => x.Deleted == false);

            if (bookingId > -1)
                query = query.Where(x => x.Id == bookingId);

            SailsTrip sailsTripAlias = null;
            query = query.JoinAlias(x => x.Trip, () => sailsTripAlias);
            if (tripId > -1)
                query = query.Where(x => x.Trip.Id == tripId);

            Cruise cruiseAlias = null;
            query = query.JoinAlias(x => x.Cruise, () => cruiseAlias);
            if (cruiseId > -1)
                query = query.Where(x => x.Cruise.Id == cruiseId);

            if (status > -1)
            {
                query = query.Where(x => x.Status == (StatusType)status);
            }

            if (startDate != null)
            {
                query = query.And(
                    Restrictions.Eq(
                    Projections.SqlFunction("date",
                    NHibernateUtil.Date,
                    Projections.Property<Booking>(x => x.StartDate)
                    ), startDate.Value.Date));
            }

            BookingRoom bookingRoomAlias = null;
            Customer customerAlias = null;
            if (!string.IsNullOrEmpty(customerName))
            {
                query = query
                    .JoinAlias(x => x.BookingRooms, () => bookingRoomAlias)
                    .JoinAlias(() => bookingRoomAlias.Customers, () => customerAlias)
                    .Where(Restrictions.Like("customerAlias.Fullname", customerName, MatchMode.Anywhere));

            }

            Agency agencyAlias = null;
            query = query.JoinAlias(x => x.Agency, () => agencyAlias);

            if (agencyId > -1)
                query = query.Where(x => agencyAlias.Id == agencyId);

            if (batchId > -1)
                query = query.Where(x => x.Batch.Id == batchId);

            query = query.Select(Projections.Distinct(Projections.Property<Booking>(x => x.Id)));

            var mainQuery = _session.QueryOver<Booking>().WithSubquery.WhereProperty(x => x.Id).In(query);
            mainQuery = mainQuery.OrderBy(x => x.StartDate).Desc;
            count = mainQuery.RowCount();
            return mainQuery.Skip(currentPageIndex * pageSize).Take(pageSize).Future<Booking>().ToList();
        }

        public IList<Booking> PaymentReportBLL_BookingSearchBy(string spay, DateTime? from, DateTime? to,
            string agencyName, int cruiseId, int tripId, int agencyId, int bookingId, int salesId)
        {

            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false)
                .And(Restrictions.Or(Restrictions.Where<Booking>(x => x.Status == StatusType.Approved), Restrictions.Where<Booking>(x => x.Status == StatusType.Cancelled && x.CancelPay > 0)));
            if (spay == "1")
                query = query.Where(x => x.IsPaid != true);

            if (from != null)
                query = query.Where(x => x.StartDate >= from);

            if (to != null)
                query = query.Where(x => x.StartDate <= to);

            Agency agencyAlias = null;
            query.JoinAlias(x => x.Agency, () => agencyAlias);
            if (!string.IsNullOrEmpty(agencyName))
            {
                query = query.Where(x => agencyAlias.Name.IsLike(agencyName, MatchMode.Anywhere));
            }

            BookingSale bookingSaleAlias = null;
            query.JoinAlias(x => x.BookingSale, () => bookingSaleAlias);
            if (salesId > -1)
            {
                if (salesId > 0)
                    query = query.Where(x => bookingSaleAlias.Sale.Id == salesId);

                if (salesId == 0)
                    query = query.Where(x => agencyAlias.Sale == null);
            }

            if (cruiseId > -1)
            {
                query = query.Where(x => x.Cruise.Id == cruiseId);
            }

            if (bookingId > -1)
            {
                query = query.Where(x => x.Id == bookingId);
            }

            if (tripId > -1)
            {
                query = query.Where(x => x.Trip.Id == tripId);
            }

            if (agencyId > -1)
            {
                query = query.Where(x => x.Agency.Id == agencyId);
            }

            return query.OrderBy(x => x.StartDate).Asc.List<Booking>();
        }

        public Booking BookingGetById(int bookingId)
        {
            return _session.QueryOver<Booking>().Where(x => x.Deleted == false)
                .Where(x => x.Id == bookingId).FutureValue<Booking>().Value;
        }

        public IList<Booking> BookingReportBLL_BookingSearchBy(DateTime? startDate, int cruiseId, int bookingStatus)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);

            if (cruiseId > -1)
            {
                query = query.Where(x => x.Cruise.Id == cruiseId);
            }

            if (bookingStatus > -1)
            {
                query = query.Where(x => x.Status == (StatusType)bookingStatus);
            }

            if (startDate != null)
            {
                query = query.Where(Restrictions.Eq(
                    Projections.SqlFunction("date",
                    NHibernateUtil.Date,
                    Projections.Property<Booking>(x => x.StartDate)
                    ), startDate.Value.Date));
            }

            return query.Future<Booking>().ToList();
        }

        public IList<Booking> BookingGetBySeries(int seriesId)
        {
            return _session.QueryOver<Booking>().Where(x => x.Deleted == false)
                .Where(x => x.Series.Id == seriesId).Future().ToList();
        }

        public IList<Booking> SeriesViewBLL_BookingSearchBy(int seriesId, string taCode, int bookingCode, DateTime? startDate)
        {

            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);

            if (seriesId > -1)
            {
                query = query.Where(x => x.Series.Id == seriesId);
            }

            if (!String.IsNullOrEmpty(taCode))
            {
                query = query.Where(x => x.AgencyCode == taCode);
            }

            if (bookingCode > -1)
            {
                query = query.Where(x => x.Id == bookingCode);
            }

            if (startDate != null)
            {
                query = query.Where(Restrictions.Eq(
                  Projections.SqlFunction("date",
                  NHibernateUtil.Date,
                  Projections.Property<Booking>(x => x.StartDate)
                  ), startDate.Value.Date));
            }

            return query.Future<Booking>().ToList();
        }

        public IList<Booking> ViewActivitiesBLL_BookingGetAllBy(int userId, DateTime? from, DateTime? to)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
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
                query = query.Where(x => x.CreatedDate <= to || x.ModifiedDate <= to);
            }

            return query.Future().ToList();
        }

        public IList<Booking> BookingGetAllBy(DateTime? startDate, int bookingStatus, bool isLimousine)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);

            if (bookingStatus > -1)
            {
                query = query.Where(x => x.Status == (StatusType)bookingStatus);
            }

            if (startDate != null)
            {
                query = query.Where(Restrictions.Eq(
                    Projections.SqlFunction("date",
                    NHibernateUtil.Date,
                    Projections.Property<Booking>(x => x.StartDate)
                    ), startDate.Value.Date));
            }

            return query.Future<Booking>().ToList();
        }

        public IQueryOver<Booking, Booking> BookingGetAllByCriterionTransfer(BusType busType, Route route, string way, DateTime? date)
        {
            var query = _session.QueryOver<Booking>();
            BusType busTypeAlias = null;
            query.JoinAlias(x => x.Transfer_BusType, () => busTypeAlias);
            if (busType != null && busType.Id > 0)
            {
                query = query.Where(() => busTypeAlias.Id == busType.Id);
            }
            Cruise cruiseAlias = null;
            query.JoinAlias(x => x.Cruise, () => cruiseAlias);
            CruiseRoute cruiseRouteAlias = null;
            query.JoinAlias(() => cruiseAlias.ListCruiseRoute, () => cruiseRouteAlias);
            Route routeAlias = null;
            query.JoinAlias(() => cruiseRouteAlias.Route, () => routeAlias);
            if (route != null)
            {
                query = query.Where(() => cruiseRouteAlias.Route.Id == route.Id);
            }
            if (!String.IsNullOrEmpty(way))
            {
                query = query.Where(() => routeAlias.Way == way);
            }
            if (date != null)
            {
                if (way == "To")
                {
                    query = query.Where(x => x.Transfer_DateTo == date);
                }
                if (way == "Back")
                {
                    query = query.Where(x => x.Transfer_DateBack == date);
                }
            }
            return query;
        }

        public IQueryOver<Booking, Booking> BookingGetAllByCriterionTransfer(BusType busType, Route route, DateTime? date)
        {
            var way = "";
            if (route != null)
            {
                way = route.Way;
            }
            return BookingGetAllByCriterionTransfer(busType, route, way, date);
        }

        public IQueryOver<Booking, Booking> BookingGetByCriterion(DateTime? date, Cruise cruise)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
            if (date != null)
            {
                query = query.Where(x => x.StartDate == date);
            }
            if (cruise != null)
            {
                query = query.Where(x => x.Cruise == cruise);
            }
            query = query.Where(x => x.Deleted == false);
            return query;
        }

        public IEnumerable<Booking> BookingGetAllByCriterion(User user, DateTime? date, Cruise cruise, IEnumerable<StatusType> listStatus)
        {
            var cruises = CruiseGetByUser(user);
            if (cruises.Count > 0)
            {
                var query = QueryOver.Of<Booking>().Where(x => x.Deleted == false);
                if (date != null)
                {
                    query.Where(r => (r.EndDate > date && r.StartDate > date.Value.AddDays(-1) &&
                                      r.StartDate < date.Value.AddDays(1)) || (r.StartDate < date && r.EndDate > date));
                }
                if (cruise != null)
                {
                    query = query.Where(x => x.Cruise == cruise);
                }
                else
                {
                    query = query.Where(x => x.Cruise.IsIn(cruises.ToList()));
                }
                query = query.WhereRestrictionOn(x => x.Status).IsIn(listStatus.ToArray());
                query = query.Where(x => x.Deleted == false);
                query = query.Fetch(x => x.Trip).Eager;
                query = query.Fetch(x => x.Agency).Eager;
                //                var bookingRoomQuery = _session.QueryOver<BookingRoom>().WithSubquery.WhereProperty(x => x.Book)
                //                    .In<Booking>(query)
                //                    .Fetch(x => x.Customers).Eager
                //                    .Future();
                var bookingQuery = _session.QueryOver<Booking>().WithSubquery.WhereProperty(x => x.Id)
                    .In<Booking>(query.Select(x => x.Id))
                    .Fetch(x => x.BookingRooms).Eager;
                bookingQuery = bookingQuery.TransformUsing(
                    Transformers.DistinctRootEntity);
                return bookingQuery.Future();
            }
            else return new List<Booking>();

        }
        public IList<Cruise> CruiseGetAll2()
        {
            var query = _session.QueryOver<Cruise>();
            query = query.Where(x => x.Deleted == false);
            return query.List<Cruise>();
        }
        public IList<Cruise> CruiseGetByUser(User user)
        {
            if (user.HasPermission(AccessLevel.Administrator))
                return CruiseGetAll2();
            else
            {
                var query = _session.QueryOver<Cruise>();
                query = query.Where(x => x.Deleted == false);
                IvRoleCruise roleCruise = null;
                query = query.Left.JoinAlias(x => x.ListRoleCruises, () => roleCruise);
                query = query.Where(x => roleCruise.User == user || roleCruise.Role.IsIn(user.Roles));
                return query.TransformUsing(Transformers.DistinctRootEntity).List();
            }
        }
        public IEnumerable<Booking> ShadowBookingGetByDate(DateTime Date)
        {
            var query = QueryOver.Of<BookingHistory>();
            Booking bookingAlias = null;
            query = query.JoinAlias(x => x.Booking, () => bookingAlias);
            query = query.And(Restrictions.Or(
                Restrictions.Where<BookingHistory>(x => x.StartDate != bookingAlias.StartDate),
                Restrictions.Where<BookingHistory>(x => x.Status == StatusType.Cancelled && bookingAlias.Status == StatusType.Cancelled)));
            query = query.Where(() => bookingAlias.Deleted == false);
            query = query.Where(x => x.StartDate == Date);
            query.Select(Projections.Distinct(Projections.Property<BookingHistory>(x => x.Booking.Id)));

            var mainQuery = _session.QueryOver<Booking>().WithSubquery.WhereProperty(x => x.Id).In<BookingHistory>(query);
            mainQuery = mainQuery.Fetch(x => x.BookingRooms).Eager;
            mainQuery = mainQuery.Fetch(x => x.BookingRooms.First().Book).Eager;
            mainQuery = mainQuery.Fetch(x => x.Trip).Eager;
            mainQuery = mainQuery.Fetch(x => x.Agency).Eager;
            mainQuery = mainQuery.TransformUsing(
                 Transformers.DistinctRootEntity);
            var list = mainQuery.Future().ToList();
            return list;
        }

        public ICollection<Booking> BookigBookingGetAllTodayBookings(User user)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
            BookingSale bookingSalesAlias = null;
            query = query.JoinAlias(x => x.BookingSale, () => bookingSalesAlias);
            query = query.Where(() => bookingSalesAlias.Sale == user);
            query = query.Where(x => x.StartDate == DateTime.Today);
            query = query.Where(x => x.Status == StatusType.Approved);
            return query.Future().ToList();
        }

        public IEnumerable<Booking> BookingGetAllNewBookings(User user)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
            BookingSale bookingSalesAlias = null;
            query = query.JoinAlias(x => x.BookingSale, () => bookingSalesAlias);
            if (user != null)
            {
                query = query.Where(() => bookingSalesAlias.Sale == user);
            }
            //Kiểm tra điều kiện booking được tạo trong ngày hôm nay
            query = query.Where(x => x.CreatedDate >= DateTime.Today && x.CreatedDate <= DateTime.Today.Add(new TimeSpan(23, 59, 59)));
            //--
            query = query.Where(x => x.Status == StatusType.Pending);
            return query.Future().ToList();
        }

        public IEnumerable<Booking> BookingGetAllBookingsInMonth(User user)
        {
            var firstDateOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
            BookingSale bookingSalesAlias = null;
            query = query.JoinAlias(x => x.BookingSale, () => bookingSalesAlias);
            query = query.Where(() => bookingSalesAlias.Sale == user);
            query = query.Where(x => x.StartDate >= firstDateOfMonth && x.StartDate <= lastDateOfMonth);
            query = query.Where(x => x.Status == StatusType.Approved);
            return query.Future().ToList();
        }

        public int BookingGetNumberOfBookingsInMonth(int month, int year, User user)
        {
            var firstDateOfMonth = new DateTime(year, month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
            BookingSale bookingSalesAlias = null;
            query = query.JoinAlias(x => x.BookingSale, () => bookingSalesAlias);
            if (user != null)
            {
                query = query.Where(() => bookingSalesAlias.Sale == user);
            }
            query = query.Where(x => x.StartDate >= firstDateOfMonth && x.StartDate <= lastDateOfMonth);
            query = query.Where(x => x.Status == StatusType.Approved);
            query = query.Select(Projections.RowCount());
            return query.FutureValue<int>().Value;
        }

        public double BookingGetTotalRevenueInMonth(int month, int year, User user)
        {
            var firstDateOfMonth = new DateTime(year, month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
            BookingSale bookingSalesAlias = null;
            query = query.JoinAlias(x => x.BookingSale, () => bookingSalesAlias);
            if (user != null)
            {
                query = query.Where(() => bookingSalesAlias.Sale == user);
            }
            query = query.Where(x => x.StartDate >= firstDateOfMonth && x.StartDate <= lastDateOfMonth);
            query = query.Where(x => x.Status == StatusType.Approved);
            query = query.Select(
                Projections.Sum(
                    Projections.Conditional(
                        Restrictions.Where<Booking>(x => x.IsTotalUsd),
                        Projections.SqlFunction(
                            new VarArgsSQLFunction("(", "*", ")"),
                            NHibernateUtil.Double,
                            Projections.Property<Booking>(x => x.Total),
                            Projections.Constant(23000)
                        ),
                        Projections.Property<Booking>(x => x.Total)
                    )
                ));
            return query.Take(1).SingleOrDefault<double>();
        }

        public IEnumerable<Booking> BookingGetAllStartInDate(DateTime date, Cruise cruise)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
            query = query.Where(x => x.Status == StatusType.Approved);
            if (date != null)
            {
                query = query.Where(x => x.StartDate == date);
            }
            if (cruise != null)
            {
                query = query.Where(x => x.Cruise == cruise);
            }
            query = query.Where(x => x.Deleted == false);
            return query.Future().ToList();
        }

        public IEnumerable<Booking> BookingGetAllNewBookingsByCampaign(Campaign campaign)
        {
            var query = _session.QueryOver<Booking>().Where(b => b.CreatedDate > campaign.CreatedDate);
            query = query.AndRestrictionOn(b => b.StartDate).IsIn(campaign.GoldenDays.Select(gd => new DateTime(gd.Date.Year, gd.Date.Month, gd.Date.Day)).ToList());
            return query.Future().ToList();
        }

        public IEnumerable<Booking> BookingGetAllNewBookings()
        {
            return BookingGetAllNewBookings(null);
        }

        public IEnumerable<Booking> BookingGetAllNewBookings(DateTime date)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false);
            //Kiểm tra điều kiện booking được tạo trong ngày hôm nay
            query = query.Where(x => x.CreatedDate >= date && x.CreatedDate <= date.Add(new TimeSpan(23, 59, 59)));
            //--
            query = query.Where(x => x.Status == StatusType.Pending || x.Status == StatusType.Approved);
            return query.Future().ToList();
        }

        public IEnumerable<Booking> BookingGetAllCancelledBookingOnDate(DateTime date)
        {
            var query = _session.QueryOver<Booking>();
            BookingHistory bookingHistory = null;
            query = query.JoinAlias(b => b.BookingHistories, () => bookingHistory);
            query = query.Where(() => bookingHistory.Date >= date && bookingHistory.Date <= date.Add(new TimeSpan(23, 59, 59)));
            query = query.Where(() => bookingHistory.Status == StatusType.Cancelled);
            return query.TransformUsing(new DistinctRootEntityResultTransformer()).Future().ToList();
        }

        public IQueryOver<Booking, Booking> BookingGetAllByListId(List<int> listBookingId)
        {
            var query = _session.QueryOver<Booking>();
            query = query.AndRestrictionOn(x => x.Id).IsIn(listBookingId);
            return query;
        }
        public IList<Booking> GetBookingDebtReceivables(DateTime to, int agencyId, int pageSize, int pageIndex)
        {
            var query = _session.QueryOver<Booking>().Where(x => x.Deleted == false && x.StartDate <= to);
            query = query.Where(Restrictions.Or(Restrictions.Where<Booking>(x => x.Status == StatusType.Approved && x.Paid != x.Total && x.Total != 0), Restrictions.Where<Booking>(x => x.Status == StatusType.Cancelled && x.CancelPay > 0)));
            query = query.Where(x => x.IsPaid != true);
            //query = query.Where(x => x.Status == StatusType.Approved && x.Paid < x.Total || x.Status == StatusType.Cancelled && x.Paid < x.CancelPay);

            Agency agencyAlias = null;
            query = query.JoinAlias(x => x.Agency, () => agencyAlias);
            if (agencyId != -1)
            {
                query = query.Where(x => agencyAlias.Id == agencyId);
            }
            query = query.OrderBy(b => b.CreatedDate).Asc;
            return query.Take(pageSize).Skip(pageSize * pageIndex).List<Booking>();
        }
    }
}