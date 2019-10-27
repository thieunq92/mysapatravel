using CMS.Core.Domain;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class DashBoardBLL
    {
        public BookingRepository BookingRepository { get; set; }
        public CruiseRepository CruiseRepository { get; set; }
        public RoomClassRepository RoomClassRepository { get; set; }
        public RoomTypeRepository RoomTypeRepository { get; set; }
        public SailsModule SailsModule { get; set; }
        public ActivityRepository ActivityRepository { get; set; }
        public AgencyRepository AgencyRepository { get; set; }
        public AgencyContactRepository AgencyContactRepository { get; set; }
        public CustomerRepository CustomerRepository { get; set; }
        public GoldenDayRepository GoldenDayRepository { get; set; }
        public DashBoardBLL()
        {
            BookingRepository = new BookingRepository();
            CruiseRepository = new CruiseRepository();
            RoomClassRepository = new RoomClassRepository();
            RoomTypeRepository = new RoomTypeRepository();
            SailsModule = SailsModule.GetInstance();
            ActivityRepository = new ActivityRepository();
            AgencyRepository = new AgencyRepository();
            AgencyContactRepository = new AgencyContactRepository();
            CustomerRepository = new CustomerRepository();
            GoldenDayRepository = new GoldenDayRepository();
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
            if (RoomClassRepository != null)
            {
                RoomClassRepository.Dispose();
                RoomClassRepository = null;
            }
            if (RoomTypeRepository != null)
            {
                RoomTypeRepository.Dispose();
                RoomTypeRepository = null;
            }
            if (ActivityRepository != null)
            {
                ActivityRepository.Dispose();
                ActivityRepository = null;
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
            if (CustomerRepository != null)
            {
                CustomerRepository.Dispose();
                CustomerRepository = null;
            }
            if (GoldenDayRepository != null)
            {
                GoldenDayRepository.Dispose();
                GoldenDayRepository = null;
            }
        }

        public IEnumerable<Booking> BookingGetAllTodayBookings(User user)
        {
            return BookingRepository.BookigBookingGetAllTodayBookings(user);
        }

        public IEnumerable<Cruise> CruiseGetAll()
        {
            return CruiseRepository.CruiseGetAll();
        }

        public int GetNumberOfRoomAvaiable(Cruise cruise, DateTime date)
        {
            var roomClasses = RoomClassRepository.RoomClassGetAll();
            var roomTypes = RoomTypeRepository.RoomTypeGetAll();
            var numberOfRoomAvaiable = 0;
            foreach (RoomClass roomClass in roomClasses)
            {
                foreach (RoomTypex roomType in roomTypes)
                {
                    var numberOfRoomAvailableByRoomClassRoomType = SailsModule.RoomCount(roomClass, roomType, cruise, date, 2, 0);
                    if (numberOfRoomAvailableByRoomClassRoomType > 0)
                    {
                        numberOfRoomAvaiable += numberOfRoomAvailableByRoomClassRoomType;
                    }
                }
            }
            return numberOfRoomAvaiable;
        }

        public IEnumerable<Booking> BookingGetAllNewBookings(User user)
        {
            return BookingRepository.BookingGetAllNewBookings(user);
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetings(User user)
        {
            return ActivityRepository.ActivityGetAllRecentMeetings(user);
        }

        public Agency AgencyGetById(int agencyId)
        {
            return AgencyRepository.AgencyGetById(agencyId);
        }

        public AgencyContact AgencyContactGetById(int agencyContactId)
        {
            return AgencyContactRepository.AgencyContactGetById(agencyContactId);
        }

        public IEnumerable<Booking> BookingGetAllBookingsInMonth(User user)
        {
            return BookingRepository.BookingGetAllBookingsInMonth(user);
        }

        public IEnumerable<Activity> ActivityGetAllActivityInMonth(int month, int year, User user)
        {
            return ActivityRepository.ActivityGetAllActivityInMonth(month, year, user);
        }

        public object AgencyGetAllAgenciesSendNoBookingsLast3Month(User user)
        {
            return AgencyRepository.AgencyGetAllAgenciesSendNoBookingsLast3Month(user);
        }

        public object AgencyGetAllAgenciesNotVisitedInLast2Month(User user)
        {
            return AgencyRepository.AgencyGetAllAgenciesNotVisitedInLast2Month(user);
        }

        public IEnumerable<AgencyContact> AgencyContactGetByAgencyId(int agencyId)
        {
            return AgencyContactRepository.AgencyContactGetAllByAgency(agencyId);
        }

        public void ActivitySaveOrUpdate(Activity activity)
        {
            ActivityRepository.SaveOrUpdate(activity);
        }

        public int BookingGetNumberOfBookingsInMonth(int month, int year, User user)
        {
            return BookingRepository.BookingGetNumberOfBookingsInMonth(month, year, user);
        }

        public int CustomerGetNumberOfCustomersInMonth(int month, int year, User user)
        {
            return CustomerRepository.CustomerGetNumberOfCustomersInMonth(month, year, user);
        }

        public double BookingGetTotalRevenueInMonth(int month, int year, User user)
        {
            return BookingRepository.BookingGetTotalRevenueInMonth(month, year, user);
        }

        public object AgencyGetTop10(User user)
        {
            return AgencyRepository.AgencyGetTop10(user);
        }

        public IEnumerable<RoomsAvaiableDTO> CruiseGetRoomsAvaiableInDateRange(DateTime from, DateTime to)
        {
            return CruiseRepository.CruiseGetRoomsAvaiableInDateRange(from, to);
        }

        public IEnumerable<GoldenDay> GoldenDayGetAllByDateRange(DateTime from, DateTime to)
        {
            return GoldenDayRepository.GoldenDayGetAllByDateRange(from, to);
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetingsInDateRange(User sales, DateTime from, DateTime to)
        {
            return ActivityRepository.ActivityGetAllRecentMeetingsInDateRange(sales, from, to);
        }

        public Activity ActivityGetById(int activityId)
        {
            return ActivityRepository.GetById(activityId);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.GetById(cruiseId);
        }
    }
}