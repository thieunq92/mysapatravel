using NHibernate;
using Portal.Modules.OrientalSails.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository.Shared
{
    public class StoreProcedureRepository : RepositoryBase<object>
    {
        public StoreProcedureRepository() { }
        public StoreProcedureRepository(ISession session)
            : base(session)
        {

        }

        public IEnumerable<SalesMonthSummaryDTO> GetSalesMonthSummary(DateTime from, DateTime to)
        {
            var query = _session.CreateSQLQuery("exec dbo.sp_DashBoardManager_getSalesMonthSummary :from, :to");
            query.SetParameter("from", from);
            query.SetParameter("to", to);
            return query.List<object[]>().Select(o => new SalesMonthSummaryDTO
            {
                NumberOfBookings = (int)o[0]
                ,
                NumberOfPax2Days = (int)o[1]
                ,
                NumberOfPax3Days = (int)o[2]
                ,
                Revenue = (decimal)o[3]
                ,
                MeetingReports = (int)o[4]
                ,
                SalesId = (int)o[5]
                ,
                SalesUserName = (string)o[6]
                ,
            });
        }

        public IEnumerable<Top10AgenciesDTO> AgencyGetTop10(DateTime from, DateTime to)
        {
            var query = _session.CreateSQLQuery("exec dbo.sp_DashBoardManager_getTop10Agencies :from , :to");
            query.SetParameter("from", from);
            query.SetParameter("to", to);
            return query.List<object[]>().Select(o => new Top10AgenciesDTO
            {
                NumberOfPax = (int)o[0]
                ,
                AgencyId = (int)o[1]
                ,
                AgencyName = (string)o[2]
                ,
            });
        }

        public IEnumerable<AgencySendNoBookingDTO> GetAgenciesSendNoBookingLast3Month()
        {
            var query = _session.CreateSQLQuery("exec dbo.sp_DashBoardManager_getAgenciesSendNoBookingLast3Month");
            return query.List<object[]>().Select(o => new AgencySendNoBookingDTO
            {
                LastBookingDate = (DateTime?)o[0]
                ,
                AgencyId = (int)o[1]
                ,
                AgencyName = (string)o[2]
                ,
                LastMeetingDate = (DateTime?)o[3]
                ,
                MeetingDetails = (string)o[4]
            });
        }

        public IEnumerable<AgencyNotVisitedUpdatedDTO> GetAgenciesNotVisitedUpdatedLast2Month()
        {
            var query = _session.CreateSQLQuery("exec dbo.sp_DashBoardManager_getAgenciesNotVisitedOrUpdateLast2Month");
            return query.List<object[]>().Select(o => new AgencyNotVisitedUpdatedDTO
            {
                AgencyId = (int)o[0]
                ,
                AgencyName = (string)o[1]
                ,
                LastMeetingDate = (DateTime?)o[2]
                ,
                MeetingDetails = (string)o[3]
            });
        }

        public IEnumerable<AgencySendNoBookingDTO> GetAgenciesSendNoBookingLast3Month(int salesId)
        {
            var query = _session.CreateSQLQuery("exec dbo.sp_DashBoardManager_getAgenciesSendNoBookingLast3Month :salesId");
            query.SetParameter("salesId", salesId);
            return query.List<object[]>().Select(o => new AgencySendNoBookingDTO
            {
                LastBookingDate = (DateTime?)o[0]
                ,
                AgencyId = (int)o[1]
                ,
                AgencyName = (string)o[2]
                ,
                LastMeetingDate = (DateTime?)o[3]
                ,
                MeetingDetails = (string)o[4]
            });
        }

        public IEnumerable<AgencyNotVisitedUpdatedDTO> GetAgenciesNotVisitedUpdatedLast2Month(int salesId)
        {
            var query = _session.CreateSQLQuery("exec dbo.sp_DashBoardManager_getAgenciesNotVisitedOrUpdateLast2Month :salesId");
            query.SetParameter("salesId", salesId);
            return query.List<object[]>().Select(o => new AgencyNotVisitedUpdatedDTO
            {
                AgencyId = (int)o[0]
                ,
                AgencyName = (string)o[1]
                ,
                LastMeetingDate = (DateTime?)o[2]
                ,
                MeetingDetails = (string)o[3]
            });
        }
    }
}