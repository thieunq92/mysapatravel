using CMS.Core.Domain;
using NHibernate;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class ActivityRepository : RepositoryBase<Activity>
    {
        public ActivityRepository() : base() { }
        public ActivityRepository(ISession session)
            : base(session)
        {
        }
        public IList<Activity> MeetingGetAllBy(int userId, DateTime? from, DateTime? to)
        {
            var query = _session.QueryOver<Activity>()
                .Where(x => x.User.Id == userId);
            if (from != null)
            {
                query = query.Where(x => x.UpdateTime >= from);
            }

            if (to != null)
            {
                query = query.Where(x => x.UpdateTime <= to);
            }

            return query.Future().ToList();
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetings(User user)
        {
            var query = _session.QueryOver<Activity>();
            if (user != null)
            {
                query = query.Where(x => x.User == user);
            }
            return query.OrderBy(x => x.UpdateTime).Desc.Take(10).Future().ToList();
        }

        public IEnumerable<Activity> ActivityGetAllActivityInMonth(int month, int year, User user)
        {
            var firstDateOfMonth = new DateTime(year, month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1).Add(new TimeSpan(23, 59, 59));
            var query = _session.QueryOver<Activity>();
            query = query.Where(x => x.DateMeeting >= firstDateOfMonth && x.DateMeeting <= lastDateOfMonth)
                .Where(x => x.User == user);
            return query.Future().ToList();
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetings()
        {
            return ActivityGetAllRecentMeetings(null);
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetings(int salesId)
        {
            var query = _session.QueryOver<Activity>();
            User userAlias = null;
            query = query.JoinAlias(a => a.User, () => userAlias);
            if (salesId != 0)
            {
                query = query.Where(x => x.User.Id == salesId);
            }
            return query.Future().ToList();
        }
        public IEnumerable<Activity> ActivityGetAllRecentMeetingsInDateRange(int salesId, DateTime from, DateTime to)
        {
            var query = _session.QueryOver<Activity>();
            User userAlias = null;
            query = query.JoinAlias(a => a.User, () => userAlias);
            if (salesId != 0)
            {
                query = query.Where(x => x.User.Id == salesId);
            }
            query = query.Where(a => a.DateMeeting >= from && a.DateMeeting <= to);
            return query.Future().ToList();
        }

        public IEnumerable<Activity> ActivityGetAllRecentMeetingsInDateRange(User sales, DateTime from, DateTime to)
        {
            var query = _session.QueryOver<Activity>();
            if (sales != null)
            {
                query = query.Where(x => x.User == sales);
            }
            query = query.Where(a => a.DateMeeting >= from && a.DateMeeting <= to);
            return query.Future().ToList();
        }
    }
}