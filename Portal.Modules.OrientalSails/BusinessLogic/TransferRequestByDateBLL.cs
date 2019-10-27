using CMS.Core.Domain;
using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class TransferRequestByDateBLL
    {
        public RouteRepository RouteRepository { get; set; }
        public BusTypeRepository BusTypeRepository { get; set; }
        public AgencyRepository AgencyRepository { get; set; }
        public RoleRepository RoleRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public BusByDateRepository BusByDateRepository { get; set; }
        public BookingBusByDateRepository BookingBusByDateRepository { get; set; }
        public LockingTransferRepository LockingTransferRepository { get; set; }
        public ExpenseRepository ExpenseRepository { get; set; }
        public ExpenseServiceRepository ExpenseServiceRepository { get; set; }
        public CostTypeRepository CostTypeRepository { get; set; }
        public GuideRepository GuideRepository { get; set; }
        public TransferRequestByDateBLL()
        {
            RouteRepository = new RouteRepository();
            BusTypeRepository = new BusTypeRepository();
            AgencyRepository = new AgencyRepository();
            RoleRepository = new RoleRepository();
            BookingRepository = new BookingRepository();
            BusByDateRepository = new BusByDateRepository();
            BookingBusByDateRepository = new BookingBusByDateRepository();
            LockingTransferRepository = new LockingTransferRepository();
            ExpenseRepository = new ExpenseRepository();
            ExpenseServiceRepository = new ExpenseServiceRepository();
            CostTypeRepository = new CostTypeRepository();
            GuideRepository = new GuideRepository();
        }
        public void Dispose()
        {
            if (RouteRepository != null)
            {
                RouteRepository.Dispose();
                RouteRepository = null;
            }
            if (BusTypeRepository != null)
            {
                BusTypeRepository.Dispose();
                BusTypeRepository = null;
            }
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
            if (RoleRepository != null)
            {
                RoleRepository.Dispose();
                RoleRepository = null;
            }
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
            if (BusByDateRepository != null)
            {
                BusByDateRepository.Dispose();
                BusByDateRepository = null;
            }
            if (BookingBusByDateRepository != null)
            {
                BookingBusByDateRepository.Dispose();
                BookingBusByDateRepository = null;
            }
            if (LockingTransferRepository != null)
            {
                LockingTransferRepository.Dispose();
                LockingTransferRepository = null;
            }
            if (ExpenseRepository != null)
            {
                ExpenseRepository.Dispose();
                ExpenseRepository = null;
            }
            if (ExpenseServiceRepository != null)
            {
                ExpenseServiceRepository.Dispose();
                ExpenseServiceRepository = null;
            }
            if (CostTypeRepository != null)
            {
                CostTypeRepository.Dispose();
                CostTypeRepository = null;
            }
            if (GuideRepository != null)
            {
                GuideRepository.Dispose();
                GuideRepository = null;
            }
        }

        public IQueryOver<Route, Route> RouteGetAll()
        {
            return RouteRepository.RouteGetAll();
        }

        public IQueryOver<BusType, BusType> BusTypeGetAll()
        {
            return BusTypeRepository.BusTypeGetAll();
        }

        public BusType BusTypeGetById(int busTypeId)
        {
            return BusTypeRepository.BusTypeGetById(busTypeId);
        }

        public Route RouteGetById(int routeId)
        {
            return RouteRepository.RouteGetById(routeId);
        }

        public IQueryOver<Agency, Agency> AgencyGetAllByRole(Role role)
        {
            return AgencyRepository.AgencyGetAllByRole(role);
        }

        public Role RoleGetByName(string name)
        {
            return RoleRepository.RoleGetByName(name);
        }

        public IQueryOver<Booking, Booking> BookingGetAllByCriterionTransfer(BusType busType, Route route, string way, DateTime? date)
        {
            return BookingRepository.BookingGetAllByCriterionTransfer(busType, route, way, date);
        }
        public IQueryOver<Booking, Booking> BookingGetAllByCriterionTransfer(BusType busType, Route route, DateTime? date)
        {
            return BookingRepository.BookingGetAllByCriterionTransfer(busType, route, date);
        }
        public IQueryOver<BusByDate, BusByDate> BusByDateGetAllByCriterion(DateTime? date, BusType busType, Route route)
        {
            return BusByDateRepository.BusByDateGetAllByCriterion(date, busType, route);
        }

        public Route RouteBackGetByRouteTo(Route route)
        {
            return RouteRepository.RouteBackGetByRouteTo(route);
        }

        public IQueryOver<Route> RouteGetAllById(int routeId)
        {
            return RouteRepository.RouteGetAllById(routeId);
        }

        public IQueryOver<BusType, BusType> BusTypeGetAllById(int busTypeId)
        {
            return BusTypeRepository.BusTypeGetAllById(busTypeId);
        }

        public BusByDate BusByDateGetById(int busByDateId)
        {
            return BusByDateRepository.BusByDateGetById(busByDateId);
        }

        public void BusByDateSaveOrUpdate(BusByDate busByDate)
        {
            BusByDateRepository.SaveOrUpdate(busByDate);
        }

        public void BusByDateDelete(BusByDate busByDate)
        {
            BusByDateRepository.Delete(busByDate);
        }

        public IQueryOver<BusByDate, BusByDate> BusByDateGetAllByCriterion(DateTime? date, BusType busType, Route route, string way)
        {
            return BusByDateRepository.BusByDateGetAllByCriterion(date, busType, route, way);
        }

        public IQueryOver<BusByDate, BusByDate> BusByDateGetAllByCriterion(DateTime? date, BusType busType, Route route, string way, int group)
        {
            return BusByDateRepository.BusByDateGetAllByCriterion(date, busType, route, way, group);
        }

        public IQueryOver<BookingBusByDate, BookingBusByDate> BookingBusByDateGetAllByCriterion(Booking booking, BusByDate busByDate)
        {
            return BookingBusByDateRepository.BookingBusByDateGetAllByCriterion(booking, busByDate);
        }


        public IQueryOver<BookingBusByDate, BookingBusByDate> BookingBusByDateGetAllByCriterion(Booking booking)
        {
            return BookingBusByDateRepository.BookingBusByDateGetAllByCriterion(booking, null);
        }

        public IQueryOver<BookingBusByDate, BookingBusByDate> BookingBusByDateGetAllByCriterion(BusByDate busByDate)
        {
            return BookingBusByDateRepository.BookingBusByDateGetAllByCriterion(null, busByDate);
        }

        public void BookingBusByDateSaveOrUpdate(BookingBusByDate bookingBusByDate)
        {
            BookingBusByDateRepository.SaveOrUpdate(bookingBusByDate);
        }

        public Booking BookingGetById(int bookingId)
        {
            return BookingRepository.BookingGetById(bookingId);
        }

        public void BookingBusByDateDelete(BookingBusByDate bookingBusByDate)
        {
            BookingBusByDateRepository.Delete(bookingBusByDate);
        }

        public IQueryOver<BookingBusByDate, BookingBusByDate> BookingBusByDateGetAllByCriterion(Route route, BusType busType, int group)
        {
            return BookingBusByDateRepository.BookingBusByDateGetAllByCriterion(route, busType, group);
        }

        public Route RouteToGetByRouteBack(Route route)
        {
            return RouteRepository.RouteToGetByRouteBack(route);
        }


        public IQueryOver<LockingTransfer, LockingTransfer> LockingTransferGetAllByCriterion(DateTime? date)
        {
            return LockingTransferRepository.LockingTransferGetAllByCriterion(date);
        }

        public void LockingTransferSaveOrUpdate(LockingTransfer lockingTransfer)
        {
            LockingTransferRepository.SaveOrUpdate(lockingTransfer);
        }

        public void LockingTransferDelete(LockingTransfer lockingTransfer)
        {
            LockingTransferRepository.Delete(lockingTransfer);
        }

        public Agency AgencyGetById(int agencyId)
        {
            return AgencyRepository.AgencyGetById(agencyId);
        }

        public IQueryOver<Agency, Agency> Guide_AgencyGetAllGuideInDay(Role role, DateTime? date, Route route)
        {
            return AgencyRepository.Guide_AgencyGetAllGuideInDay(role, date, route);
        }

        public IQueryOver<Expense,Expense> ExpenseGetAllByCriterion(Agency guide, DateTime? date, Route route)
        {
            return ExpenseRepository.ExpenseGetAllByCriterion(guide, date, route);
        }

        public Expense ExpenseGetById(int expenseId)
        {
            return ExpenseRepository.ExpenseGetById(expenseId);
        }

        public ExpenseService ExpenseServiceGetByExpenseId(int expenseId)
        {
            return ExpenseServiceRepository.ExpenseServiceGetByExpenseId(expenseId);
        }

        public void ExpenseServiceSaveOrUpdate(ExpenseService expenseService)
        {
            ExpenseServiceRepository.SaveOrUpdate(expenseService);
        }

        public IQueryOver<CostType, CostType> CostTypeGetAll()
        {
            return CostTypeRepository.CostTypeGetAll();
        }

        public IQueryOver<Expense,Expense> ExpenseGetAllByCriterion(DateTime? date)
        {
            return ExpenseRepository.ExpenseGetAllByCriterion(date);
        }

        public void ExpenseSaveOrUpdate(Expense expense)
        {
            ExpenseRepository.SaveOrUpdate(expense);
        }

        public void BookingSaveOrUpdate(Booking booking)
        {
            BookingRepository.SaveOrUpdate(booking);
        }

        public Guide GuideGetById(int guideId)
        {
            return GuideRepository.GetById(guideId);
        }
    }
}