using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class BusByDateRepository : RepositoryBase<BusByDate>
    {
        public RouteRepository RouteRepository { get; set; }
        public ExpenseRepository ExpenseRepository { get; set; }
        public CostTypeRepository CostTypeRepository { get; set; }
        public ExpenseServiceRepository ExpenseServiceRepository { get; set; }
        private Route routeAlias;
        public BusByDateRepository()
            : base()
        {
            RouteRepository = new RouteRepository();
            ExpenseRepository = new ExpenseRepository();
            CostTypeRepository = new CostTypeRepository();
            ExpenseServiceRepository = new ExpenseServiceRepository();
        }
        public BusByDateRepository(ISession session)
            : base(session)
        {
            RouteRepository = new RouteRepository();
            ExpenseRepository = new ExpenseRepository();
            CostTypeRepository = new CostTypeRepository();
            ExpenseServiceRepository = new ExpenseServiceRepository();
        }
        public new void Dispose()
        {
            if (RouteRepository != null)
            {
                RouteRepository.Dispose();
                RouteRepository = null;
            }
            if (ExpenseRepository != null)
            {
                ExpenseRepository.Dispose();
                ExpenseRepository = null;
            }
            if (CostTypeRepository != null)
            {
                CostTypeRepository.Dispose();
                CostTypeRepository = null;
            }
            if (ExpenseServiceRepository != null)
            {
                ExpenseServiceRepository.Dispose();
                ExpenseServiceRepository = null;
            }
            base.Dispose();
        }
        public IQueryOver<BusByDate, BusByDate> BusByDateGetAllByCriterion(DateTime? date, BusType busType, Route route)
        {
            var query = _session.QueryOver<BusByDate>();

            if (date.HasValue)
            {
                query = query.Where(x => x.Date == date);
            }
            BusType busTypeAlias = null;
            query = query.JoinAlias(x => x.BusType, () => busTypeAlias);
            if (busType != null && busType.Id > 0)
            {
                query = query.Where(() => busTypeAlias.Id == busType.Id);
            }
            query = query.JoinAlias(x => x.Route, () => routeAlias);
            if (route != null && route.Id > 0)
            {
                query = query.Where(() => routeAlias.Id == route.Id);
            }
            return query;
        }
        public BusByDate BusByDateGetById(int busByDateId)
        {
            return _session.QueryOver<BusByDate>().Where(x => x.Id == busByDateId).FutureValue().Value;
        }

        public IQueryOver<BusByDate, BusByDate> BusByDateGetAllByCriterion(DateTime? date, BusType busType, Route route, string way)
        {
            if (String.IsNullOrEmpty(way))
            {
                return BusByDateGetAllByCriterion(date, busType, route);
            }
            if (way == "All")
            {
                return BusByDateGetAllByCriterionAndAllWay(date, busType, route);
            }
            var query = BusByDateGetAllByCriterion(date, busType, route);
            query = query.Where(x => routeAlias.Way == way);
            return query;
        }

        public IQueryOver<BusByDate, BusByDate> BusByDateGetAllByCriterionAndAllWay(DateTime? date, BusType busType, Route route)
        {
            var query = _session.QueryOver<BusByDate>();
            var listRoute = RouteRepository.RouteGetAllById(route.Id).Future().ToList();
            var listRouteId = listRoute.Select(x => x.Id).ToList();
            if (date.HasValue)
            {
                query = query.Where(x => x.Date == date);
            }
            BusType busTypeAlias = null;
            query = query.JoinAlias(x => x.BusType, () => busTypeAlias);
            if (busType != null && busType.Id > 0)
            {
                query = query.Where(() => busTypeAlias.Id == busType.Id);
            }
            Route routeAlias = null;
            query = query.JoinAlias(x => x.Route, () => routeAlias);
            if (route != null && route.Id > 0)
            {
                query = query.AndRestrictionOn(() => routeAlias.Id).IsIn(listRouteId);
            }
            return query;
        }

        public IQueryOver<BusByDate, BusByDate> BusByDateGetAllByCriterion(DateTime? date, BusType busType, Route route, string way, int group)
        {
            if (group == -1)
            {
                return BusByDateGetAllByCriterion(date, busType, route, way);
            }
            return BusByDateGetAllByCriterion(date, busType, route, way).Where(x => x.Group == group);
        }

        public override void SaveOrUpdate(BusByDate busByDate)
        {
            if(busByDate == null){
                return;
            }
            var createOrUpdate = "";
            if (busByDate.Id <= 0) {
                createOrUpdate = "Create";
            }
            else
            {
                createOrUpdate = "Update";
            }
            var busByDateRouteBackRef = busByDate.BusByDateRouteBackRef;
            if (busByDateRouteBackRef != null)
            {
                busByDateRouteBackRef.Guide = busByDate.Guide;
                busByDateRouteBackRef.Group = busByDate.Group;
                base.SaveOrUpdate(busByDateRouteBackRef);
            }
            base.SaveOrUpdate(busByDate);
            if (createOrUpdate == "Create")
            {
                var driverExpense = new Expense()
                {
                    BusByDate = busByDate,
                    Type = "Drivers",
                    Date = busByDate.Date.Value,
                };
                ExpenseRepository.SaveOrUpdate(driverExpense);
                var listCostType = CostTypeRepository.CostTypeGetAll().Future().ToList();
                var expenseTypeNull = ExpenseRepository.ExpenseGetAllByCriterion(-1, busByDate.Date).Where(z => z.Type == null).FutureValue().Value;
                var expenseService = ExpenseServiceRepository.ExpenseServiceGetAllByCriterion(driverExpense.Id).FutureValue().Value;
                if (expenseService == null || expenseService.Id <= 0)
                {
                    expenseService = new ExpenseService();
                }
                expenseService.Cost = driverExpense.Cost;
                expenseService.Name = driverExpense.BusByDate != null ? driverExpense.BusByDate.Driver_Name : "";
                expenseService.Type = listCostType.Where(z => z.Name == "Driver").FirstOrDefault();
                expenseService.Expense = expenseTypeNull;
                expenseService.ExpenseIdRef = driverExpense.Id;
                ExpenseServiceRepository.SaveOrUpdate(expenseService);            
            }
        }

        public override void Delete(BusByDate busByDate)
        {
            var busByDateRouteBackRef = busByDate.BusByDateRouteBackRef;
            var busByDateRouteTo_HaveBusByDateRouteBackRef =
                _session.QueryOver<BusByDate>().Where(x => x.BusByDateRouteBackRef.Id == busByDate.Id).FutureValue().Value;
            if (busByDateRouteTo_HaveBusByDateRouteBackRef != null && busByDateRouteTo_HaveBusByDateRouteBackRef.Id > 0)
            {
                busByDateRouteTo_HaveBusByDateRouteBackRef.BusByDateRouteBackRef = null;
                SaveOrUpdate(busByDateRouteTo_HaveBusByDateRouteBackRef);
            }

            if (busByDateRouteBackRef != null)
            {
                base.Delete(busByDateRouteBackRef);
            }
            base.Delete(busByDate);
        }
    }
}