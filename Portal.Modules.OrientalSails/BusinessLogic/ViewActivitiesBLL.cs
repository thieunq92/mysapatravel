using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class ViewActivitiesBLL
    {
        public UserRepository UserRepository { get; set; }
        public ActivityRepository ActivityRepository { get; set; }
        public AgencyContactRepository AgencyContactRepository { get; set; }
        public BookingRepository BookingRepository { get; set; }
        public SeriesRepository SeriesRepository { get; set; }
        public AgencyRepository AgencyRepository { get; set; }

        public ViewActivitiesBLL()
        {
            UserRepository = new UserRepository();
            ActivityRepository = new ActivityRepository();
            AgencyContactRepository = new AgencyContactRepository();
            BookingRepository = new BookingRepository();
            SeriesRepository = new SeriesRepository();
            AgencyRepository = new AgencyRepository();
        }

        public void Dispose()
        {
            if (UserRepository != null)
            {
                UserRepository.Dispose();
                UserRepository = null;
            }

            if (ActivityRepository != null)
            {
                ActivityRepository.Dispose();
                ActivityRepository = null;
            }

            if (AgencyContactRepository != null)
            {
                AgencyContactRepository.Dispose();
                AgencyContactRepository = null;
            }

            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }

            if (SeriesRepository != null)
            {
                SeriesRepository.Dispose();
                SeriesRepository = null;
            }

            if (AgencyRepository != null)
            {
                AgencyRepository.Dispose();
                AgencyRepository = null;
            }
        }

        public User UserGetById(int userId)
        {
            return UserRepository.UserGetById(userId);
        }

        public IList<Activity> MeetingGetAllBy(int userId, DateTime? from, DateTime? to)
        {
            return ActivityRepository.MeetingGetAllBy(userId, from, to);
        }

        public AgencyContact AgencyContactGetById(int agencyContactId)
        {
            return AgencyContactRepository.AgencyContactGetById(agencyContactId);
        }

        public IList<Booking> BookingGetAllBy(int userId, DateTime? from, DateTime? to)
        {
            return BookingRepository.ViewActivitiesBLL_BookingGetAllBy(userId, from, to);
        }

        public IList<Series> SeriesGetAllBy(int userId, DateTime? from, DateTime? to)
        {
            return SeriesRepository.ViewActivitiesBLL_SeriesGetAllBy(userId, from, to);
        }

        public Series SeriesGetById(int seriesId)
        {
            return SeriesRepository.SeriesGetById(seriesId);
        }

        public IList<Agency> AgencyGetAllBy(int userId, DateTime? from, DateTime? to)
        {
            return AgencyRepository.ViewActivitiesBLL_AgencyGetAllBy(userId, from, to);
        }
    }
}