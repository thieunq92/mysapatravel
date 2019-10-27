using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class RouteRepository : RepositoryBase<Route>
    {
        public RouteRepository() : base() { }
        public RouteRepository(ISession session) : base(session) { }

        public IQueryOver<Route, Route> RouteGetAll()
        {
            return _session.QueryOver<Route>();
        }

        public Route RouteGetById(int routeId)
        {
            return _session.QueryOver<Route>().Where(x => x.Id == routeId).FutureValue().Value;
        }

        public Route RouteBackGetByRouteTo(Route route)
        {
            if (route != null && route.Id > 0)
            {
                return _session.QueryOver<Route>().Where(x => x.Group == route.Group).Where(x => x.Way == "Back").FutureValue().Value;
            }
            return null;
        }

        public IQueryOver<Route> RouteGetAllById(int routeId)
        {
            var route = RouteGetById(routeId);
            if (route != null && route.Id > 0)
            {
                return _session.QueryOver<Route>().Where(x => x.Group == route.Group);
            }
            return null;
        }

        public Route RouteToGetByRouteBack(Route route)
        {
            if (route != null && route.Id > 0)
            {
                return _session.QueryOver<Route>().Where(x => x.Group == route.Group).Where(x => x.Way == "To").FutureValue().Value;
            }
            return null;
        }
    }
}