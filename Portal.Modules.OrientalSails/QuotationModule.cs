using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails
{
    public partial class SailsModule
    {
        /// <summary>
        /// lấy loại đối tác
        /// </summary>
        /// <returns></returns>
        public IList<QAgentLevel> GetAgentLevel()
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QAgentLevel>();
            return queryOver.List<QAgentLevel>();
        }
        /// <summary>
        /// lấy các nhóm tàu cấu hình giá
        /// </summary>
        /// <returns></returns>
        public IList<QCruiseGroup> GetCruiseGroup()
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QCruiseGroup>();
            return queryOver.List<QCruiseGroup>();
        }
        public IList<QGroupRomPrice> GetGroupRoomPrice(int groupId, string agentLvCode, int trip, QQuotation quotation)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QGroupRomPrice>();
            queryOver.Where(x => x.GroupCruise.Id == groupId && x.AgentLevelCode == agentLvCode && x.Trip == trip);
            if (quotation != null && quotation.Id > 0)
            {
                queryOver.Where(x => x.QQuotation.Id == quotation.Id);
            }
            return queryOver.List<QGroupRomPrice>();
        }
        /// <summary>
        /// lấy kiểu phòng
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public IList<QRoomType> GetRoomType(QCruiseGroup group)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QRoomType>();
            queryOver.Where(p => p.Group == group);
            return queryOver.List<QRoomType>();
        }
        /// <summary>
        /// lấy loại phòng
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public IList<QRoomClass> GetRoomClass(QCruiseGroup group)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QRoomClass>();
            queryOver.Where(p => p.Group == group);
            return queryOver.List<QRoomClass>();
        }
        /// <summary>
        /// lấy cấu hình số khách theo tàu
        /// </summary>
        /// <param name="group">nhóm tàu</param>
        /// <returns></returns>
        public IList<QCruiseCharterRange> GetCruiseCharterRange(QCruiseGroup group)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QCruiseCharterRange>();
            queryOver.Where(p => p.Group == group);
            return queryOver.List<QCruiseCharterRange>();
        }
        /// <summary>
        /// lấy giá theo phòng
        /// </summary>
        /// <param name="quotation">bảng giá</param>
        /// <param name="group">nhóm tàu</param>
        /// <param name="roomClass">loại phòng</param>
        /// <param name="roomType">kiểu phòng</param>
        /// <param name="trip">số ngày trip</param>
        /// <returns></returns>
        public QRoomPrice GetRoomPrice(QQuotation quotation, QCruiseGroup group, QRoomClass roomClass, QRoomType roomType, int trip)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QRoomPrice>();
            queryOver.Where(p => p.QQuotation == quotation);
            queryOver.Where(p => p.Group == group);
            queryOver.Where(p => p.QRoomClass == roomClass);
            queryOver.Where(p => p.QRoomType == roomType);
            queryOver.Where(p => p.Trip == trip);
            return queryOver.SingleOrDefault();
        }
        /// <summary>
        /// lay bang gia theo số lượng khách charter
        /// </summary>
        /// <param name="quotation"></param>
        /// <param name="cruiseCharterRange">tàu</param>
        /// <param name="trip"></param>
        /// <returns></returns>
        public QCharterRangePrice GetCharterRangePrice(QQuotation quotation, QCruiseCharterRange cruiseCharterRange, int trip)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QCharterRangePrice>();
            queryOver.Where(p => p.QQuotation == quotation);
            queryOver.Where(p => p.Group == cruiseCharterRange.Group);
            queryOver.Where(p => p.Cruise == cruiseCharterRange.Cruise);
            queryOver.Where(p => p.QCruiseCharterRange == cruiseCharterRange);
            queryOver.Where(p => p.Trip == trip);
            return queryOver.SingleOrDefault();
        }
        /// <summary>
        /// lấy cấu hình giá
        /// </summary>
        /// <returns></returns>
        public IList<QQuotation> GetQQuotation(int pageSize, int pageIndex, out int total)
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            var queryOver = _commonDao.OpenSession().QueryOver<QQuotation>();
            queryOver.Where(p => p.Validto > DateTime.Now.AddDays(-1));
            queryOver.Where(p => p.Enable == true);
            total = queryOver.RowCount();

            if (pageSize > 0)
            {
                return queryOver.Skip(pageSize * pageIndex).Take(pageSize).List<QQuotation>();
            }
            else
            {
                return queryOver.List<QQuotation>();
            }
        }
        public IList<Cruise> CruiseGetAllByGroup(int groupId)
        {
            return _commonDao.OpenSession().QueryOver<Cruise>().Where(c => c.Group.Id == groupId).List();
        }
        public IList<QCharterPrice> GetCruiseCharterPrice(int groupId, Cruise cruise, string agentLvCode, int trip, QQuotation quotation)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<QCharterPrice>();
            queryOver.Where(x => x.GroupCruise.Id == groupId && x.Cruise == cruise && x.AgentLevelCode == agentLvCode && x.Trip == trip);
            if (quotation != null && quotation.Id > 0)
            {
                queryOver.Where(x => x.QQuotation.Id == quotation.Id);
            }
            return queryOver.List<QCharterPrice>();
        }
    }
}