using CMS.Core.Domain;
using NHibernate;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class BookingReportBLL
    {
        public BookingRepository BookingRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }
        public ExpenseRepository ExpenseRepository { get; set; }
        public AgencyRepository AgencyRepository { get; set; }
        public UserRepository UserRepository { get; set; }
        public ExpenseHistoryRepository ExpenseHistoryRepository { get; set; }
        public CostTypeRepository CostTypeRepository { get; set; }
        public ExpenseServiceRepository ExpenseServiceRepository { get; set; }
        public LockingExpenseRepository LockingExpenseRepository { get; set; }
        public BookingHistoryRepository BookingHistoryRepository { get; set; }
        public BookingRoomRepository BookingRoomRepository { get; set; }
        public CustomerRepository CustomerRepository { get; set; }
        public AgencyNotesRepository AgencyNotesRepository { get; set; }
        public BookingReportBLL()
        {
            BookingRepository = new BookingRepository();
            CruiseRepository = new CruiseRepository();
            ExpenseRepository = new ExpenseRepository();
            AgencyRepository = new AgencyRepository();
            UserRepository = new UserRepository();
            ExpenseHistoryRepository = new ExpenseHistoryRepository();
            CostTypeRepository = new CostTypeRepository();
            ExpenseServiceRepository = new ExpenseServiceRepository();
            LockingExpenseRepository = new LockingExpenseRepository();
            BookingHistoryRepository = new BookingHistoryRepository();
            BookingRoomRepository = new BookingRoomRepository();
            CustomerRepository = new CustomerRepository();
            AgencyNotesRepository = new AgencyNotesRepository();
        }
        public void Dispose()
        {
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
            if (CruiseRepository != null)
            {
                CruiseRepository.Dispose();
                CruiseRepository = null;
            }
            if (ExpenseRepository != null)
            {
                ExpenseRepository.Dispose();
                ExpenseRepository = null;
            }
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
            if (UserRepository != null)
            {
                UserRepository.Dispose();
                UserRepository = null;
            }
            if (ExpenseHistoryRepository != null)
            {
                ExpenseHistoryRepository.Dispose();
                ExpenseHistoryRepository = null;
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
            if (LockingExpenseRepository != null)
            {
                LockingExpenseRepository.Dispose();
                LockingExpenseRepository = null;
            }
            if (BookingHistoryRepository != null)
            {
                BookingHistoryRepository.Dispose();
                BookingHistoryRepository = null;
            }
            if (BookingRoomRepository != null)
            {
                BookingRoomRepository.Dispose();
                BookingRoomRepository = null;
            }
            if (CustomerRepository != null)
            {
                CustomerRepository.Dispose();
                CustomerRepository = null;
            }
            if (AgencyNotesRepository != null)
            {
                AgencyNotesRepository.Dispose();
                AgencyNotesRepository = null;
            }
        }
        public IList<Booking> BookingReportBLL_BookingSearchBy(DateTime startDate, int cruiseId, int bookingStatus)
        {
            return BookingRepository.BookingReportBLL_BookingSearchBy(startDate, cruiseId, bookingStatus);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.CruiseGetById(cruiseId);
        }

        public IList<Booking> BookingGetAllBy(DateTime? startDate, int bookingStatus, bool isLimousine)
        {
            return BookingRepository.BookingGetAllBy(startDate, bookingStatus, isLimousine);
        }

        public void ExpenseSaveOrUpdate(Expense expense)
        {
            ExpenseRepository.SaveOrUpdate(expense);
        }

        public Agency AgencyGetById(int agencyId)
        {
            return AgencyRepository.AgencyGetById(agencyId);
        }
        public User UserGetById(int userId)
        {
            return UserRepository.UserGetById(userId);
        }
        public IQueryOver<Expense, Expense> ExpenseGetAllByCriterion(int cruiseId, DateTime? date)
        {
            return ExpenseRepository.ExpenseGetAllByCriterion(cruiseId, date);
        }

        public Expense ExpenseGetById(int expenseId)
        {
            return ExpenseRepository.ExpenseGetById(expenseId);
        }

        public void ExpenseDelete(Expense expense)
        {
            ExpenseRepository.Delete(expense);
        }

        public void ExpenseHistorySaveOrUpdate(ExpenseHistory expenseHistory)
        {
            ExpenseHistoryRepository.SaveOrUpdate(expenseHistory);
        }

        public IEnumerable<Cruise> CruiseGetAll()
        {
            return CruiseRepository.CruiseGetAll();
        }

        public void ExpenseHistoryDelete(ExpenseHistory expenseHistory)
        {
            ExpenseHistoryRepository.Delete(expenseHistory);
        }

        public IQueryOver<CostType, CostType> CostTypeGetAll()
        {
            return CostTypeRepository.CostTypeGetAll();
        }
        public void ExpenseServiceSaveOrUpdate(ExpenseService expenseService)
        {
            ExpenseServiceRepository.SaveOrUpdate(expenseService);
        }
        public IQueryOver<ExpenseService, ExpenseService> ExpenseServiceGetAllByCriterion(int expenseIdRef)
        {
            return ExpenseServiceRepository.ExpenseServiceGetAllByCriterion(expenseIdRef);
        }

        public void LockingExpenseSaveOrUpdate(LockingExpense lockingExpense)
        {
            LockingExpenseRepository.SaveOrUpdate(lockingExpense);
        }

        public IQueryOver<LockingExpense> LockingExpenseGetAllByCriterion(DateTime? date)
        {
            return LockingExpenseRepository.LockingExpenseGetAllByCriterion(date);
        }

        public void LockingExpenseDelete(LockingExpense lockingExpense)
        {
            LockingExpenseRepository.Delete(lockingExpense);
        }

        public Agency Guide_AgencyGetById(int guideId)
        {
            return AgencyRepository.AgencyGetById(guideId);
        }

        public IQueryOver<Booking, Booking> BookingGetByCriterion(DateTime? date, Cruise cruise)
        {
            return BookingRepository.BookingGetByCriterion(date, cruise);
        }

        public IEnumerable<Booking> BookingGetAllByCriterion(User user,DateTime? date, Cruise cruise, IEnumerable<StatusType> listStatus)
        {
            return BookingRepository.BookingGetAllByCriterion(user,date, cruise, listStatus);
        }

        public IEnumerable<BookingHistory> BookingHistoryGetAllByBooking(Booking booking)
        {
            return BookingHistoryRepository.BookingHistoryGetByBooking(booking);
        }

        public IEnumerable<Booking> ShadowBookingGetByDate(DateTime Date)
        {
            return BookingRepository.ShadowBookingGetByDate(Date);
        }
        public IEnumerable<BookingRoom> BookingRoomGetAllByBooking(Booking booking)
        {
            return BookingRoomRepository.BookingRoomGetAllByBooking(booking);
        }

        public IEnumerable<BookingRoom> BookingRoomGetAllByCriterion(Cruise cruise, DateTime date)
        {
            return BookingRoomRepository.BookingRoomGetAllByCriterion(cruise, date);
        }

        public int BookingRoomGetRowCountByCriterion(Cruise cruise, DateTime? date)
        {
            return BookingRoomRepository.BookingRoomGetRowCountByCriterion(cruise, date);
        }

        public int CustomerGetRowCountByCriterion(Cruise cruise, DateTime date)
        {
            return CustomerRepository.CustomerGetRowCountByCriterion(cruise, date);
        }

        public IEnumerable<Booking> BookingGetAllByByCriterion(User user,DateTime date, Cruise cruise, List<StatusType> listStatusType)
        {
            return BookingRepository.BookingGetAllByCriterion(user,date, cruise, listStatusType);
        }

        public IEnumerable<Booking> BookingGetAllStartInDate(DateTime date, Cruise cruise)
        {
            return BookingRepository.BookingGetAllStartInDate(date, cruise);
        }

        public IEnumerable<AgencyNotes> AgencyNotesGetAllByAgencyAndRole(Agency agency, Role role)
        {
            return AgencyNotesRepository.AgencyNotesGetAllByAgencyAndRole(agency, role);
        }

        public IEnumerable<AgencyNotes> AgencyNotesGetAllByAgency(Agency agency)
        {
            return AgencyNotesRepository.AgencyNotesGetAllByAgency(agency);
        }
    }
}