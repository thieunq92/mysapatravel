using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Admin;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails
{
    public partial class SailsModule
    {
        /// <summary>
        /// lấy booking room theo điều kiện
        /// </summary>
        /// <param name="startDate">từ ngày</param>
        /// <param name="enddate">tới ngày</param>
        /// <param name="cruiseId">tàu</param>
        /// <returns></returns>
        public IList<BookingRoom> IvsGetBookingRoom(DateTime startDate, DateTime enddate, int cruiseId)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<BookingRoom>();
            Booking booking = null;
            queryOver = queryOver.JoinAlias(x => x.Book, () => booking);
            queryOver.Where(r => booking.StartDate > startDate.AddDays(-1) && booking.EndDate < enddate.AddDays(1));
            queryOver.Where(r => booking.Cruise.Id == cruiseId);
            queryOver.Where(r => booking.Status == StatusType.Approved);
            queryOver.Where(r => r.Room != null);
            return queryOver.List<BookingRoom>();
        }
        /// <summary>
        /// lấy list phiếu xuất theo điều kiện
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="cruiseId"></param>
        /// <returns></returns>
        public IList<IvExport> IvsGetExport(DateTime startDate, int cruiseId)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<IvExport>();
            queryOver.Where(r => r.ExportDate > startDate.AddDays(-1));
            //IvStorage storage = null;
            //queryOver = queryOver.JoinAlias(x => x.Storage, () => storage);
            queryOver.Where(r => r.Cruise != null && r.Cruise.Id == cruiseId);
            return queryOver.List<IvExport>();
        }
        /// <summary>
        /// lấy toàn bộ giá sản phẩm
        /// </summary>
        /// <param name="cruiseId"></param>
        /// <returns></returns>
        public IList<IvProductPrice> IvGetProductPrices(int cruiseId)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<IvProductPrice>();
            IvStorage storage = null;
            queryOver = queryOver.JoinAlias(x => x.Storage, () => storage);
            queryOver.Where(r => storage.Cruise.Id == cruiseId);
            return queryOver.List<IvProductPrice>();
        }
        /// <summary>
        /// lấy ds sản phẩm trong kho theo tàu
        /// </summary>
        /// <param name="cruiseId"></param>
        /// <returns></returns>
        public IList<IvInStock> IvsGetProductInStock(int cruiseId)
        {
            var queryOver = _commonDao.OpenSession().QueryOver<IvInStock>();
            queryOver.Where(r => r.CruiseId == cruiseId);
            queryOver.Where(r => r.IsTool == false);
            return queryOver.List<IvInStock>();
        }
        /// <summary>
        /// lấy báo cáo phiếu nhập theo điều kiện
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<IvImportReportDate> GetIvImportReport(NameValueCollection query)
        {
            var sql = "";
            if (!string.IsNullOrWhiteSpace(query["year"]))
            {
                double time = Convert.ToDouble(query["year"]);
                DateTime timeConvert = DateTime.FromOADate(time);
                sql =
                    "select DATEADD(MONTH, DATEDIFF(MONTH, 0, importdate), 0) as ImportDate, sum(total) as total from [iv_Import] where year(importdate) = :Year group by DATEADD(MONTH, DATEDIFF(MONTH, 0, importdate), 0) order by importdate asc";
                var session = _commonDao.OpenSession();
                return session.CreateSQLQuery(sql)
                    .AddEntity(typeof(IvImportReportDate))
                    .SetInt32("Year", timeConvert.Year)
                    .List<IvImportReportDate>();
            }
            else if(!string.IsNullOrWhiteSpace(query["day"]))
            {
                double time = Convert.ToDouble(query["fromday"]);
                DateTime timeConvert = DateTime.FromOADate(time);

                double today = Convert.ToDouble(query["today"]);
                DateTime timetodayConvert = DateTime.FromOADate(today);

                sql =
                    "select CAST(importdate AS DATE) as importdate, sum(total) as total from [iv_Import] where importdate >= :fromDay and  importdate <= :toDay group by CAST(importdate AS DATE)  order by importdate asc";
                var session = _commonDao.OpenSession();
                return session.CreateSQLQuery(sql)
                    .AddEntity(typeof(IvImportReportDate))
                    .SetDateTime("fromDay", timeConvert)
                    .SetDateTime("toDay", timetodayConvert)
                    .List<IvImportReportDate>();
            }
            else 
            {
                DateTime timeConvert = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(query["month"]))
                {
                    double time = Convert.ToDouble(query["month"]);
                    timeConvert = DateTime.FromOADate(time);
                }

                sql =
                    "select CAST(importdate AS DATE) as importdate, sum(total) as total from [iv_Import] where year(importdate) = :Year and  month(importdate) = :Month group by CAST(importdate AS DATE) order by importdate asc";
                var session = _commonDao.OpenSession();
                return session.CreateSQLQuery(sql)
                    .AddEntity(typeof(IvImportReportDate))
                    .SetInt32("Year", timeConvert.Year)
                    .SetInt32("Month", timeConvert.Month)
                    .List<IvImportReportDate>();
            }
        }
        /// <summary>
        /// lấy báo cáo phiếu xuất
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<IvExportReportDate> GetIvExportReport(NameValueCollection query)
        {
            var sql = "";
            if (!string.IsNullOrWhiteSpace(query["year"]))
            {
                double time = Convert.ToDouble(query["year"]);
                DateTime timeConvert = DateTime.FromOADate(time);
                sql =
                    "select DATEADD(MONTH, DATEDIFF(MONTH, 0, ExportDate), 0) as ExportDate, sum(total) as total, sum(Pay) as pay, " +
                    " sum(TotalCustomer) as TotalCustomer " +
                    "from [iv_Export] where year(ExportDate) = :Year ";
                if (!string.IsNullOrWhiteSpace(query["cruiseId"]))
                {
                    sql += " and CruiseId  = :cruisesId ";
                }
                if (!string.IsNullOrEmpty(query["debt"]))
                {
                    if (query["debt"] == "0") sql += " and IsDebt  != 1 ";
                    else if (query["debt"] == "1") sql += " and IsDebt  = 1 ";
                }
                sql += " group by DATEADD(MONTH, DATEDIFF(MONTH, 0, ExportDate), 0) order by ExportDate asc";
                var session = _commonDao.OpenSession();
                var sqlQuery = session.CreateSQLQuery(sql)
                    .AddEntity(typeof(IvExportReportDate))
                    .SetInt32("Year", timeConvert.Year);
                if (!string.IsNullOrWhiteSpace(query["cruiseId"]))
                {
                    sqlQuery.SetInt32("cruisesId", Convert.ToInt32(query["cruiseId"]));
                }
                return sqlQuery.List<IvExportReportDate>();
            }
            else if (!string.IsNullOrWhiteSpace(query["month"]))
            {
                double time = Convert.ToDouble(query["month"]);
                DateTime timeConvert = DateTime.FromOADate(time);

                sql =
                    "select CAST(ExportDate AS DATE) as ExportDate, sum(total) as total, sum(Pay) as pay, " +
                    " sum(TotalCustomer) as TotalCustomer " +
                    "from [iv_Export] where year(ExportDate) = :Year and  month(ExportDate) = :Month ";
                if (!string.IsNullOrWhiteSpace(query["cruiseId"]))
                {
                    sql += " and CruiseId  = :cruisesId ";
                }
                if (!string.IsNullOrEmpty(query["debt"]))
                {
                    if (query["debt"] == "0") sql += " and IsDebt  != 1 ";
                    else if (query["debt"] == "1") sql += " and IsDebt  = 1 ";
                }
                sql += " group by CAST(ExportDate AS DATE) order by ExportDate asc";
                var session = _commonDao.OpenSession();
                var sqlQuery = session.CreateSQLQuery(sql)
                    .AddEntity(typeof(IvExportReportDate))
                    .SetInt32("Year", timeConvert.Year)
                    .SetInt32("Month", timeConvert.Month);
                if (!string.IsNullOrWhiteSpace(query["cruiseId"]))
                {
                    sqlQuery.SetInt32("cruisesId", Convert.ToInt32(query["cruiseId"]));
                }
                return sqlQuery.List<IvExportReportDate>();
            }
            else
            {
                DateTime timeConvert = DateTime.ParseExact(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime timetodayConvert = DateTime.ParseExact(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(query["fromday"]))
                {
                    double time = Convert.ToDouble(query["fromday"]);
                    timeConvert = DateTime.FromOADate(time);
                }
                if (!string.IsNullOrWhiteSpace(query["today"]))
                {
                    double today = Convert.ToDouble(query["today"]);
                    timetodayConvert = DateTime.FromOADate(today);
                }

                sql =
                    "select CAST(ExportDate AS DATE) as ExportDate, sum(total) as total, sum(Pay) as pay, " +
                    " sum(TotalCustomer) as TotalCustomer " +
                    "from [iv_Export] where ExportDate >= :fromDay and  ExportDate <= :toDay ";
                if (!string.IsNullOrWhiteSpace(query["cruiseId"]))
                {
                    sql += " and CruiseId  = :cruisesId ";
                }
                if (!string.IsNullOrEmpty(query["debt"]))
                {
                    if (query["debt"] == "0") sql += " and IsDebt  != 1 ";
                    else if (query["debt"] == "1") sql += " and IsDebt  = 1 ";
                }
                sql += " group by CAST(ExportDate AS DATE)  order by ExportDate asc";
                var session = _commonDao.OpenSession();
                var sqlQuery = session.CreateSQLQuery(sql)
                    .AddEntity(typeof(IvExportReportDate))
                    .SetDateTime("fromDay", timeConvert)
                    .SetDateTime("toDay", timetodayConvert);
                if (!string.IsNullOrWhiteSpace(query["cruiseId"]))
                {
                    sqlQuery.SetInt32("cruisesId", Convert.ToInt32(query["cruiseId"]));
                }
                return sqlQuery.List<IvExportReportDate>();
            }
        }
        /// <summary>
        /// lấy báo cáo các sản phẩm nhập theo thời gian
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<IvExportProductReportDate> GetExportProductReportDates(NameValueCollection query)
        {
            //if (!string.IsNullOrWhiteSpace(query["cruiseId"]))
            //{
                var queryOver = _commonDao.OpenSession().QueryOver<IvExportProductReportDate>();


                if (!string.IsNullOrWhiteSpace(query["cruiseId"]))
                {
                    queryOver.Where(r => r.CruiseId == Convert.ToInt32(query["cruiseId"]));
                }
                if (!string.IsNullOrWhiteSpace(query["debt"]))
                {
                    if (query["debt"] == "0") queryOver.Where(r => r.IsDebt != true);
                    else if (query["debt"] == "1") queryOver.Where(r => r.IsDebt == true);
                }
                DateTime timeConvert = DateTime.ParseExact(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime timetodayConvert = DateTime.ParseExact(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(query["fromday"]))
                {
                    double time = Convert.ToDouble(query["fromday"]);
                    timeConvert = DateTime.FromOADate(time);

                }
                queryOver.Where(r => r.ExportDate >= timeConvert);
                if (!string.IsNullOrWhiteSpace(query["today"]))
                {
                    double today = Convert.ToDouble(query["today"]);
                    timetodayConvert = DateTime.FromOADate(today);
                }
                queryOver.Where(r => r.ExportDate <= timetodayConvert);

                return queryOver.OrderBy(e => e.ExportDate).Asc.List<IvExportProductReportDate>();
            //}
            //return null;
        }
        /// <summary>
        /// lấy toàn bộ sản phẩm
        /// </summary>
        /// <returns></returns>
        public IList GetAllProduct()
        {
            return _commonDao.GetAll(typeof(IvProduct));
        }
    }
}