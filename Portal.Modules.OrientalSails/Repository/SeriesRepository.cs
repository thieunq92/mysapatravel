using NHibernate;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace Portal.Modules.OrientalSails.Repository
{
    public class SeriesRepository : RepositoryBase<Series>
    {
        public SeriesRepository() : base() { }

        public SeriesRepository(ISession session) : base(session) { }


        public Series SeriesGetById(int seriesId)
        {
            return _session.QueryOver<Series>().Where(x => x.Id == seriesId).FutureValue().Value;
        }

        public IList<Series> SeriesGetByQueryString(string partnerName, string seriesCode, int agencyId, int salesInChargeId, int pageSize, int currentPageIndex, out int count)
        {
            var query = QueryOver.Of<Series>();

            Agency agencyAlias = null;
            query = query.JoinAlias(x => x.Agency, () => agencyAlias);
            if (!string.IsNullOrEmpty(partnerName))
            {
                query = query.Where(Restrictions.Like("agencyAlias.Name", partnerName, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(seriesCode))
            {
                query = query.Where(Restrictions.Like("SeriesCode", seriesCode, MatchMode.Anywhere));
            }

            if (agencyId > -1)
            {
                query = query.Where(x => agencyAlias.Id == agencyId);
            }

            if (salesInChargeId > -1)
            {
                query = query.Where(x => agencyAlias.Sale.Id == salesInChargeId);
            }

            //Booking bookingAlias = null;
            //query.JoinAlias(x => x.ListBooking, () => bookingAlias);
            ////query = query.Where(x => bookingAlias.Deleted == false)
            ////    .Where(x => bookingAlias.StartDate >= DateTime.Now.Date)
            ////    .Where(x => bookingAlias.Status != Web.Util.StatusType.Cancelled);

            query = query.Select(Projections.Distinct(Projections.Property<Series>(x => x.Id)));
            var mainQuery = _session.QueryOver<Series>().WithSubquery.WhereProperty(x => x.Id).In(query);
            mainQuery = mainQuery.OrderBy(x => x.Id).Desc;

            count = mainQuery.RowCount();
            return mainQuery.Skip(currentPageIndex * pageSize).Take(pageSize).Future().ToList();
        }

        public bool CheckDuplicateSeriesCode(string txtSeriesCode, int seriesId)
        {
            var query = _session.Query<Series>().Where(x => x.SeriesCode.ToLower() == txtSeriesCode.ToLower());
            if (seriesId > -1)
            {
                query = query.Where(x => x.Id != seriesId);
            }
            var series = query.ToFutureValue().Value;
            return series != null;
        }

        public Series SeriesGetBySeriesCode(string seriesCode)
        {
            Series series = _session.Query<Series>().Where(x => x.SeriesCode.ToLower() == seriesCode.ToLower()).ToFutureValue().Value;
            return series;
        }

        public IList<Series> ViewActivitiesBLL_SeriesGetAllBy(int userId, DateTime? from, DateTime? to)
        {
            var query = _session.QueryOver<Series>();
            if (userId > -1)
            {
                query = query.Where(x => x.CreatedBy.Id == userId);
            }

            if (from != null)
            {
                query = query.Where(x => x.CreatedDate >= from);
            }

            if (to != null)
            {
                query = query.Where(x => x.CreatedDate <= to);
            }

            return query.Future().ToList();
        }
    }
}