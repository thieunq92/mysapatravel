using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class ExpenseRepository : RepositoryBase<Expense>
    {
        public ExpenseRepository() : base() { }
        public ExpenseRepository(ISession session) : base(session) { }

        public IQueryOver<Expense, Expense> ExpenseGetAllByCriterion(int cruiseId, DateTime? date)
        {
            var query = _session.QueryOver<Expense>();
            Cruise cruiseAlias = null;
            query = query.JoinAlias(x => x.Cruise, () => cruiseAlias);
            if (cruiseId != -1)
            {
                query = query.Where(x => x.Cruise.Id == cruiseId);
            }
            if (date != null)
            {
                query = query.Where(x => x.Date == date);
            }
            return query;
        }

        public Expense ExpenseGetById(int expenseId)
        {
            return _session.QueryOver<Expense>().Where(x => x.Id == expenseId).FutureValue().Value;
        }

        public IQueryOver<Expense, Expense> ExpenseGetAllByCriterion(Agency guide, DateTime? date, Route route)
        {
            var query = _session.QueryOver<Expense>();
            Agency guideAlias = null;
            query = query.JoinAlias(x => x.Guide, () => guideAlias);
            if (guide != null)
            {
                query = query.Where(() => guideAlias.Id == guide.Id);
            }
            if (date != null)
            {
                query = query.Where(x => x.Date == date);
            }
            Cruise cruiseAlias = null;
            query = query.JoinAlias(x => x.Cruise, () => cruiseAlias);
            CruiseRoute cruiseRouteAlias = null;
            query = query.JoinAlias(()=>cruiseAlias.ListCruiseRoute, ()=>cruiseRouteAlias);
            Route routeAlias = null;
            query = query.JoinAlias(()=>cruiseRouteAlias.Route, ()=>routeAlias);
            if (route != null)
            {
                query = query.Where(() => routeAlias.Id == route.Id);
            }
            return query;
        }

        public IQueryOver<Expense, Expense> ExpenseGetAllByCriterion(DateTime? date)
        {
            return ExpenseGetAllByCriterion(-1, date);
        }
    }
}