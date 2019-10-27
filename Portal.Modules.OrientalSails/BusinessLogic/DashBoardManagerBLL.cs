using CMS.Core.Domain;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using Portal.Modules.OrientalSails.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class DashBoardManagerBLL
    {
        public StoreProcedureRepository StoreProcedureRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public AgencyRepository AgencyRepository { get; set; }
        public AgencyContactRepository AgencyContactRepository { get; set; }
        public ActivityRepository ActivityRepository { get; set; }
        public UserRepository UserRepository { get; set; }
        public DashBoardManagerBLL()
        {
            StoreProcedureRepository = new StoreProcedureRepository();
            CruiseRepository = new CruiseRepository();
            BookingRepository = new BookingRepository();
            AgencyRepository = new AgencyRepository();
            AgencyContactRepository = new AgencyContactRepository();
            ActivityRepository = new ActivityRepository();
            UserRepository = new UserRepository();
        }
        public void Dispose()
        {
            if (StoreProcedureRepository != null)
            {
                StoreProcedureRepository.Dispose();
                StoreProcedureRepository = null;
            }
            if (CruiseRepository != null)
            {
                CruiseRepository.Dispose();
                CruiseRepository = null;
            }
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }
            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
            if (AgencyContactRepository != null)
            {
                AgencyContactRepository.Dispose();
                AgencyContactRepository = null;
            }
            if (ActivityRepository != null)
            {
                ActivityRepository.Dispose();
                ActivityRepository = null;
            }
            if (UserRepository != null)
            {
                UserRepository.Dispose();
                UserRepository = null;
            }
        }

        public IEnumerable<SalesMonthSummaryDTO> GetSalesMonthSummary(DateTime from, DateTime to)
        {
            return StoreProcedureRepository.GetSalesMonthSummary(from, to);
        }

        public IEnumerable<RoomsAvaiableDTO> CruiseGetRoomsAvaiableInDateRange(DateTime from, DateTime to)
        {
            return CruiseRepository.CruiseGetRoomsAvaiableInDateRange(from, to);
        }

        public IEnumerable<Cruise> CruiseGetAll()
        {
            return CruiseRepository.CruiseGetAll();
        }

        public IEnumerable<Top10AgenciesDTO> AgencyGetTop10(DateTime from, DateTime to)
        {
            return StoreProcedureRepository.AgencyGetTop10(from,to);
        }

        public IEnumerable<Booking> BookingGetAllNewBookings()
        {
            return BookingRepository.BookingGetAllNewBookings();
        }

        public IEnumerable<AgencySendNoBookingDTO> GetAgenciesSendNoBookingLast3Month()
        {
            return StoreProcedureRepository.GetAgenciesSendNoBookingLast3Month();
        }

        public Agency AgencyGetById(int agencyId)
        {
            return AgencyRepository.AgencyGetById(agencyId);
        }

        public AgencyContact AgencyContactGetById(int agencyContactId)
        {
            return AgencyContactRepository.AgencyContactGetById(agencyContactId);
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetings()
        {
            return ActivityRepository.ActivityGetAllRecentMeetings();
        }

        public IEnumerable<AgencyNotVisitedUpdatedDTO> GetAgenciesNotVistedUpdatedLast2Month()
        {
            return StoreProcedureRepository.GetAgenciesNotVisitedUpdatedLast2Month();
        }

        public IEnumerable<User> SalesGetAll()
        {
            return UserRepository.SalesGetAll();
        }

        public IEnumerable<AgencySendNoBookingDTO> GetAgenciesSendNoBookingLast3Month(int salesId)
        {
            return StoreProcedureRepository.GetAgenciesSendNoBookingLast3Month(salesId);
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetings(int salesId)
        {
           return ActivityRepository.ActivityGetAllRecentMeetings(salesId);
        }

        public IEnumerable<AgencyNotVisitedUpdatedDTO> GetAgenciesNotVistedUpdatedLast2Month(int salesId)
        {
            return StoreProcedureRepository.GetAgenciesNotVisitedUpdatedLast2Month(salesId);
        }

        public IEnumerable<Booking> BookingGetAllNewBookings(DateTime date)
        {
            return BookingRepository.BookingGetAllNewBookings(date);
        }

        public IEnumerable<Booking> BookingGetAllCancelledBookingOnDate(DateTime date)
        {
            return BookingRepository.BookingGetAllCancelledBookingOnDate(date);
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetingsInDateRange(int salesId, DateTime from, DateTime to)
        {
            return ActivityRepository.ActivityGetAllRecentMeetingsInDateRange(salesId, from, to);
        }
    }
}