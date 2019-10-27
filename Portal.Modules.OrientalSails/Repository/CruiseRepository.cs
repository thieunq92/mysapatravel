using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using NHibernate.Linq;
using Portal.Modules.OrientalSails.DataTransferObject;

namespace Portal.Modules.OrientalSails.Repository
{
    public class CruiseRepository : RepositoryBase<Cruise>
    {
        public CruiseRepository() : base() { }

        public CruiseRepository(ISession session) : base(session) { }

        public IEnumerable<Cruise> CruiseGetAll()
        {
            var query = _session.QueryOver<Cruise>();
            query = query.Where(x => x.Deleted == false);
            var list = query.Future().ToList();
            return list.OrderBy(c=>c.Order);
        }

        public IQueryOver<Cruise, Cruise> QueryOverCruiseGetAll()
        {
            return _session.QueryOver<Cruise>().Where(x => x.Deleted == false);
        }

        public IList<Cruise> CruiseGetAllByTrip(SailsTrip trip)
        {
            return _session.Query<Cruise>().Where(x => x.Deleted == false)
                .Where(x => x.Trips.Contains(trip)).ToFuture().ToList();
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return _session.QueryOver<Cruise>().Where(x => x.Deleted == false)
                .Where(x => x.Id == cruiseId)
                .FutureValue()
                .Value;
        }

        public IEnumerable<RoomsAvaiableDTO> CruiseGetRoomsAvaiableInDateRange(DateTime from, DateTime to)
        {
            var query = _session.CreateSQLQuery("exec dbo.sp_getroomsavaiableindaterange :from, :to");
            query.SetParameter("from", from);
            query.SetParameter("to", to);
            return query.List<object[]>().Select(x => new RoomsAvaiableDTO { 
                  CruiseId = (int)x[0]
                , CruiseName = (string)x[1]
                , TotalRoom = (int)x[2]
                , NoRUsing = (int)x[3]
                , NoRAvaiable = (int)x[4]
                , Date = (DateTime)x[5]
            });
        }
    }
}