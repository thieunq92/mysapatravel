using CMS.Core.Domain;
using Newtonsoft.Json;
using OfficeOpenXml;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using MoreLinq;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for TransferRequestByDateWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class TransferRequestByDateWebService : System.Web.Services.WebService
    {
        private TransferRequestByDateBLL transferRequestByDateBLL;
        private UserBLL userBLL;
        public TransferRequestByDateBLL TransferRequestByDateBLL
        {
            get
            {
                if (transferRequestByDateBLL == null)
                {
                    transferRequestByDateBLL = new TransferRequestByDateBLL();
                }
                return transferRequestByDateBLL;
            }
        }

        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                {
                    userBLL = new UserBLL();
                }
                return userBLL;
            }
        }
        public User CurrentUser
        {
            get
            {
                return UserBLL.UserGetCurrent();
            }
        }
        [WebMethod]
        public string TransferRequestDTOGetByCriterion(string d, string bti, string ri)
        {
            DateTime? date = null;
            try
            {
                date = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var busTypeId = -1;
            try
            {
                busTypeId = Int32.Parse(bti);
            }
            catch { }
            var listBusType = TransferRequestByDateBLL.BusTypeGetAllById(busTypeId).Future().ToList();
            var routeId = -1;
            try
            {
                routeId = Int32.Parse(ri);
            }
            catch { }
            var listRoute = TransferRequestByDateBLL.RouteGetAllById(routeId).Future().ToList();
            var listRouteDTO = new List<RouteDTO>();
            foreach (var route in listRoute)
            {
                var listBusTypeDTO = new List<BusTypeDTO>();
                var routeDTO = new RouteDTO()
                {
                    Id = route.Id,
                    Name = route.Name,
                    Way = route.Way,
                };
                foreach (var busType in listBusType)
                {
                    var busTypeDTO = new BusTypeDTO()
                    {
                        Id = busType.Id,
                        Name = busType.Name,
                        HaveBookingNoGroup = BusTypeCheckBookingHaveNoGroup(busType, route, date),
                        HaveNoBooking = false,
                    };
                    var listBusByDateDTO = new List<BusByDateDTO>();
                    var listBusByDate = TransferRequestByDateBLL.BusByDateGetAllByCriterion(date, busType, route, route.Way)
                        .OrderBy(y => y.Group).Asc.Future().ToList();
                    listBusByDate.ForEach(busByDate =>
                    {
                        var listBookingBusByDate = TransferRequestByDateBLL.BookingBusByDateGetAllByCriterion(busByDate).Future().ToList();
                        var listBooking = listBookingBusByDate.Select(x => x.Booking).Distinct().ToList();
                        int? supplierId = null;
                        if (busByDate.Supplier != null) supplierId = busByDate.Supplier.Id;
                        var busByDateDTO = new BusByDateDTO()
                        {
                            Id = busByDate.Id,
                            Group = busByDate.Group,
                            Driver_Name = busByDate.Driver_Name,
                            Driver_Phone = busByDate.Driver_Phone,
                            Adult = listBooking.Select(x => x.Adult).Sum(),
                            Child = listBooking.Select(x => x.Child).Sum(),
                            Baby = listBooking.Select(x => x.Baby).Sum(),
                            SupplierId = supplierId,
                            RouteName = busByDate.Route != null ? busByDate.Route.Name : "",
                            BusTypeName = busByDate.BusType != null ? busByDate.BusType.Name : "",
                        };
                        foreach (var busByDateGuide in busByDate.BusByDatesGuides)
                        {
                            var guide = busByDateGuide.Guide;
                            GuideDTO guideDTO = null;
                            if (guide != null && guide.Id > 0)
                            {
                                guideDTO = new GuideDTO()
                                {
                                    Id = guide.Id,
                                    Name = guide.Name,
                                    Phone = guide.Phone,
                                };
                            }
                            var busByDateGuideDTO = new BusByDateGuideDTO()
                            {
                                BusByDateDTO = busByDateDTO,
                                GuideDTO = guideDTO
                            };
                            busByDateDTO.BusByDatesGuidesDTO.Add(busByDateGuideDTO);
                        }
                        listBusByDateDTO.Add(busByDateDTO);
                    });
                    busTypeDTO.ListBusByDateDTO = listBusByDateDTO;
                    listBusTypeDTO.Add(busTypeDTO);
                }
                routeDTO.ListBusTypeDTO = listBusTypeDTO;
                listRouteDTO.Add(routeDTO);
            }
            var transferRequestDTO = new TransferRequestDTO();
            transferRequestDTO.ListRouteDTO = listRouteDTO;
            Dispose();
            return JsonConvert.SerializeObject(transferRequestDTO);
        }
        public bool BusTypeCheckBookingHaveNoGroup(BusType busType, Route route, DateTime? date)
        {
            var haveBookingNoGroup = false;
            var listBooking = TransferRequestByDateBLL.BookingGetAllByCriterionTransfer(busType, route, date)
                      .Future().ToList();
            listBooking.ForEach(booking =>
            {
                var listBookingBusByDate = TransferRequestByDateBLL.BookingBusByDateGetAllByCriterion(booking)
                    .Future().ToList().Where(x => x.BusByDate.Route.Id == route.Id).ToList();
                if (listBookingBusByDate.Count == 0)
                {
                    haveBookingNoGroup = true;
                    return;
                }
            });
            return haveBookingNoGroup;
        }
        public bool BusTypeCheckHaveNoBooking(BusType busType, Route route, DateTime? date)
        {
            var haveNoBooking = false;
            var listBooking = TransferRequestByDateBLL.BookingGetAllByCriterionTransfer(busType, route, date)
                      .Future().ToList();
            if (listBooking.Count == 0)
            {
                haveNoBooking = true;
            }
            return haveNoBooking;
        }
        [WebMethod]
        public void TransferRequestDTOSaveOrUpdate(TransferRequestDTO tr, string d)
        {
            var listRouteDTO = tr.ListRouteDTO;
            DateTime? date = null;
            try
            {
                date = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            foreach (var routeDTO in listRouteDTO)
            {
                var route = TransferRequestByDateBLL.RouteGetById(routeDTO.Id);
                foreach (var busTypeDTO in routeDTO.ListBusTypeDTO)
                {
                    var busType = TransferRequestByDateBLL.BusTypeGetById(busTypeDTO.Id);
                    foreach (var busByDateDTO in busTypeDTO.ListBusByDateDTO)
                    {
                        //Tạo hoặc update Busbydate
                        var busByDate = TransferRequestByDateBLL.BusByDateGetById(busByDateDTO.Id);//Tìm Busbydate theo id của BusbydateDTO
                        if (busByDate == null)
                            busByDate = new BusByDate();//Nếu không có tạo mới
                        //--//
                        //Update thông tin vào Busbydate vừa lấy được
                        busByDate.Group = busByDateDTO.Group;
                        busByDate.BusType = busType;
                        busByDate.Route = route;
                        busByDate.Date = date;
                        var supplierId = busByDateDTO.SupplierId ?? 0;
                        var supplier = TransferRequestByDateBLL.AgencyGetById(supplierId);
                        if (supplier != null && supplier.Id <= 0) supplier = null;
                        busByDate.Supplier = supplier;
                        busByDate.Driver_Name = busByDateDTO.Driver_Name;
                        busByDate.Driver_Phone = busByDateDTO.Driver_Phone;
                        //Lấy thông tin guide gắn vào bus
                        busByDate.BusByDatesGuides.Clear();
                        foreach (var busByDateGuideDTO in busByDateDTO.BusByDatesGuidesDTO)
                        {
                            var guideId = 0;
                            if (busByDateGuideDTO.GuideDTO != null)
                            {
                                guideId = busByDateGuideDTO.GuideDTO.Id ?? 0;
                            }
                            Guide guide = null;
                            guide = transferRequestByDateBLL.GuideGetById(guideId);
                            if (guide != null && guide.Id <= 0) guide = null;
                            var busByDateGuide = new BusByDateGuide()
                            {
                                Guide = guide,
                                BusByDate = busByDate,
                            };
                            busByDate.BusByDatesGuides.Add(busByDateGuide);
                        }
                        TransferRequestByDateBLL.BusByDateSaveOrUpdate(busByDate);
                        //--//
                        //Xóa Busbydate nếu đã được lưu nhưng bị đánh dấu deleted
                        if (busByDateDTO.Deleted)
                        {
                            if (busByDate.Id > 0)
                            {
                                TransferRequestByDateBLL.BusByDateDelete(busByDate);
                                continue;
                            }
                        }
                        //--//
                        BusByDateCloneForRouteBackNextDay(busByDate);
                    }
                }
            }
            Dispose();
        }
        private void BusByDateCloneForRouteBackNextDay(BusByDate busByDate)
        {
            if (busByDate == null)
            {
                return;
            }
            var route = busByDate.Route;
            if (route.Way != "To")
            {
                return;
            }
            var routeBack = TransferRequestByDateBLL.RouteBackGetByRouteTo(busByDate.Route);
            var date = busByDate.Date;
            var tomorrow = date.HasValue ? date.Value.AddDays(1) : date;
            //Tạo bus cho chiều về vào ngày mai nếu được tạo 
            BusByDate clonedBusByDateRouteBack = null;
            if (busByDate.Cloned)
            {
                clonedBusByDateRouteBack = busByDate.BusByDateRouteBackRef;
            }
            else
            {
                clonedBusByDateRouteBack = new BusByDate();
            }
            if (clonedBusByDateRouteBack == null)
                return;
            clonedBusByDateRouteBack.BusType = busByDate.BusType;
            clonedBusByDateRouteBack.Date = tomorrow;
            clonedBusByDateRouteBack.Group = busByDate.Group;
            clonedBusByDateRouteBack.Guide = busByDate.Guide;
            clonedBusByDateRouteBack.Route = routeBack;
            //Xóa hết các liên kết bookingbusbydate cũ của busByDateRouteBack 
            var listBookingBusByDate = TransferRequestByDateBLL
                   .BookingBusByDateGetAllByCriterion(clonedBusByDateRouteBack).Future().ToList();
            listBookingBusByDate.ForEach(x =>
            {
                TransferRequestByDateBLL.BookingBusByDateDelete(x);
            });
            //--
            TransferRequestByDateBLL.BusByDateSaveOrUpdate(clonedBusByDateRouteBack);
            busByDate.BusByDateRouteBackRef = clonedBusByDateRouteBack;
            busByDate.Cloned = true;
            TransferRequestByDateBLL.BusByDateSaveOrUpdate(busByDate);
            //Tìm các booking có chiều về vào ngày mai của bus hôm nay
            var listBookingNeedTransferBackOnTomorrow = busByDate.ListBookingBusByDate
                .Select(x => x.Booking).Where(x => x.Transfer_DateBack == tomorrow).ToList();
            listBookingNeedTransferBackOnTomorrow.ForEach(booking =>
            {
                //Gắn booking chiều về vào bus chiều về
                TransferRequestByDateBLL.BookingBusByDateSaveOrUpdate(new BookingBusByDate()
                {
                    Booking = booking,
                    BusByDate = clonedBusByDateRouteBack,
                });
                //--
            });
            //--
        }
        public new void Dispose()
        {
            if (transferRequestByDateBLL != null)
            {
                transferRequestByDateBLL.Dispose();
                transferRequestByDateBLL = null;
            }
            if (userBLL != null)
            {
                userBLL.Dispose();
                userBLL = null;
            }
        }
        [WebMethod]
        public string Supplier_AgencyGetAllByRole()
        {
            var role = TransferRequestByDateBLL.RoleGetByName("Suppliers");
            var listSupplier = TransferRequestByDateBLL.AgencyGetAllByRole(role).Future().ToList();
            var listSupplierDTO = new List<AgencyDTO>();
            listSupplier.ForEach(supplier =>
            {
                var supplierDTO = new AgencyDTO()
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                };
                listSupplierDTO.Add(supplierDTO);
            });
            Dispose();
            return JsonConvert.SerializeObject(listSupplierDTO);
        }
        [WebMethod]
        public string Guide_AgencyGetAllByRole(string d, string ri, string w)
        {
            DateTime? date = null;
            try
            {
                date = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var routeId = -1;
            try
            {
                routeId = Int32.Parse(ri);
            }
            catch { }
            var way = w;
            var route = TransferRequestByDateBLL.RouteGetById(routeId);
            var role = TransferRequestByDateBLL.RoleGetByName("Guides");
            var listGuide = TransferRequestByDateBLL.AgencyGetAllByRole(role).Future().ToList();
            var listGuideInDay = TransferRequestByDateBLL.Guide_AgencyGetAllGuideInDay(role, date, route).Future().ToList();
            var listGuideDTO = new List<AgencyDTO>();
            listGuide.ForEach(guide =>
            {
                var guideInDay = listGuideInDay.Where(x => x.Id == guide.Id).FirstOrDefault();
                Expense guideExpenseInDay = null;
                if (guideInDay != null)
                    guideExpenseInDay = TransferRequestByDateBLL.ExpenseGetAllByCriterion(guide, date, route)
                        .Future().SingleOrDefault();
                var guideDTO = new AgencyDTO()
                {
                    Id = guide.Id,
                    Name = guideExpenseInDay != null ? guideExpenseInDay.Guide.Name + " - " + guideExpenseInDay.Cruise.Code : guide.Name,
                    Group = guideInDay != null ? "Guide in day" : null,
                };
                listGuideDTO.Add(guideDTO);
            });
            if (way == "Back")
            {
                var listGuideDayBefore = TransferRequestByDateBLL.Guide_AgencyGetAllGuideInDay(role, date.Value.AddDays(-1), route).Future().ToList();
                listGuideDayBefore.ForEach(guideDayBefore =>
                {
                    var guideDTO = new AgencyDTO()
                    {
                        Id = guideDayBefore.Id,
                        Name = guideDayBefore.Name,
                        Group = guideDayBefore != null ? "Guide day before" : null,
                    };
                    listGuideDTO.Add(guideDTO);
                });
            }
            Dispose();
            return JsonConvert.SerializeObject(listGuideDTO);
        }
        [WebMethod]
        public string GuidePhone_AgencyGetById(int? gi)
        {
            int guideId = 0;
            if (gi.HasValue) guideId = gi.Value;
            var guide = TransferRequestByDateBLL.AgencyGetById(guideId);
            if (guide == null || guide.Id <= 0)
            {
                return null;
            }
            var guidePhone = NumberUtil.FormatPhoneNumber(guide.Phone);
            Dispose();
            return JsonConvert.SerializeObject(guidePhone);
        }
        [WebMethod]
        public string GuideName_AgencyGetById(int? gi)
        {
            int guideId = 0;
            if (gi.HasValue) guideId = gi.Value;
            var guide = TransferRequestByDateBLL.GuideGetById(guideId);
            if (guide == null || guide.Id <= 0)
            {
                return null;
            }
            var guideName = guide.Name;
            Dispose();
            return JsonConvert.SerializeObject(guideName);
        }
        [WebMethod]
        public void BusByDateExport(string bbdi)
        {
            var busByDateId = -1;
            try
            {
                busByDateId = Int32.Parse(bbdi);
            }
            catch { }
            var busByDate = TransferRequestByDateBLL.BusByDateGetById(busByDateId);

            if (busByDate == null || busByDate.Id <= 0)
            {
                return;
            }
            using (var memoryStream = new MemoryStream())
            {
                using (var excelPackage = new ExcelPackage(new FileInfo(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/TourCommandAndWelcomeBoard.xlsx"))))
                {
                    ExportTour(excelPackage, busByDate);
                    ExportWelcomeBoard(excelPackage, busByDate);
                    excelPackage.Workbook.Worksheets.Delete("Welcome Board");
                    excelPackage.Workbook.Worksheets.Delete("Tour Command");
                    excelPackage.Workbook.View.ActiveTab = 0;
                    excelPackage.SaveAs(memoryStream);
                }
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            Dispose();
        }
        [WebMethod]
        public void BusByDateExportAll(string ri, string bti, string d)
        {
            var routeId = -1;
            try
            {
                routeId = Int32.Parse(ri);
            }
            catch { }
            var route = TransferRequestByDateBLL.RouteGetById(routeId);
            var busTypeId = -1;
            try
            {
                busTypeId = Int32.Parse(bti);
            }
            catch { }
            var busType = TransferRequestByDateBLL.BusTypeGetById(busTypeId);
            DateTime? date = null;
            try
            {
                date = DateTime.ParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var listBusByDate = TransferRequestByDateBLL.BusByDateGetAllByCriterion(date, busType, route, "All")
                .Future().ToList().OrderBy(x => x.Route.Id).ThenBy(x => x.Id);
            using (var memoryStream = new MemoryStream())
            {
                using (var excelPackage = new ExcelPackage(new FileInfo(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/TourCommandAndWelcomeBoard.xlsx"))))
                {
                    foreach (var busByDate in listBusByDate)
                    {
                        ExportTour(excelPackage, busByDate);
                        ExportWelcomeBoard(excelPackage, busByDate);
                    }
                    excelPackage.Workbook.Worksheets.Delete("Welcome Board");
                    excelPackage.Workbook.Worksheets.Delete("Tour Command");
                    excelPackage.SaveAs(memoryStream);
                }
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            Dispose();
        }

        public void ExportWelcomeBoard(ExcelPackage excelPackage, BusByDate busByDate)
        {
            var listBookingBusByDate = TransferRequestByDateBLL.BookingBusByDateGetAllByCriterion(busByDate).Future().ToList();
            var listBooking = listBookingBusByDate.Select(x => x.Booking).ToList();
            var shortRouteName = StringUtil.GetFirstLetter(busByDate.Route.Name);
            var sheet = excelPackage.Workbook.Worksheets.Copy("Welcome Board", "WB" + "-"
                  + shortRouteName.Replace(" ", "").Replace("-", "_") + "-" + busByDate.BusType.Name
                  + "-G" + busByDate.Group);
            var totalRow = 6;
            var totalCol = 11;
            for (int i = 0; i < listBooking.Count; i++)
            {
                var booking = listBooking[i];
                var listCustomer = booking.Customers;
                sheet.Cells[1, 1, totalRow, totalCol].Copy(sheet.Cells[(i + 1) * totalRow + 1, 1]);
                var currentRow = (i + 2) * totalRow;
                //chỉnh lại chiều cao của các row vừa mới copy
                sheet.Row(currentRow - 5).Height = 119.25;
                sheet.Row(currentRow - 4).Height = 42;
                sheet.Row(currentRow - 3).Height = 300.75;
                sheet.Row(currentRow - 2).Height = 30.75;
                sheet.Row(currentRow - 1).Height = 45.75;
                sheet.Row(currentRow - 0).Height = 42;
                var name = booking.CustomerNameFull.Replace("<br/>", "\r\n").ToUpper();
                sheet.Cells[currentRow - 3, 1].Value = name;
                sheet.Cells[currentRow - 1, 2].Value = busByDate.Driver_Name + " - " + NumberUtil.FormatPhoneNumber(busByDate.Driver_Phone);
                foreach (var busByDateGuide in busByDate.BusByDatesGuides)
                {
                    sheet.Cells[currentRow - 0, 2].Value += (busByDateGuide.Guide != null ? busByDateGuide.Guide.Name : "") + " - "
                    + (busByDateGuide.Guide != null ? NumberUtil.FormatPhoneNumber(busByDateGuide.Guide.Phone) : "") + "\r\n";
                }
                sheet.Cells[currentRow - 1, 5].Value = booking.PickupAddress;
                sheet.Cells[currentRow - 1, 10].Value = CurrentUser.FullName;
            }
            sheet.DeleteRow(1, totalRow);
        }
        public void ExportTour(ExcelPackage excelPackage, BusByDate busByDate)
        {
            var listBookingBusByDate = TransferRequestByDateBLL.BookingBusByDateGetAllByCriterion(busByDate).Future().ToList();
            var listBooking = listBookingBusByDate.Select(x => x.Booking).ToList();
            var listCustomer = listBooking.SelectMany(x => x.Customers).ToList();
            var shortRouteName = StringUtil.GetFirstLetter(busByDate.Route.Name);
            var sheet = excelPackage.Workbook.Worksheets.Copy("Tour Command", "TC" + "-"
                + shortRouteName.Replace(" ", "").Replace("-", "_") + "-" + busByDate.BusType.Name + "-G" + busByDate.Group);
            sheet.Cells["I1"].Value = "Group " + busByDate.Group;
            sheet.Cells["E1"].Value = (busByDate.Date.HasValue ? busByDate.Date.Value.ToLongDateString() : "");
            //Điền guide vào lệnh điều tour
            var startRow = 3;
            var currentRow = startRow;
            var templateGuideRow = currentRow;
            var listGuide = busByDate.BusByDatesGuides.Where(x => x.Guide != null).Select(x => x.Guide).ToList();
            sheet.InsertRow(currentRow, listGuide.Count - 1, templateGuideRow);
            FillGuide(listGuide, sheet, ref currentRow);
            //--
            //Điền driver vào lệnh điều tour
            var templateDriverRow = currentRow;
            var listBusByDate = new List<BusByDate>();
            listBusByDate.Add(busByDate);
            sheet.InsertRow(currentRow, listBusByDate.Count - 1, templateDriverRow);
            FillDriver(listBusByDate, sheet, ref currentRow);
            //--
            //Điền opt vào lệnh điều tour
            var listGuideExpense = new List<Expense>();
            foreach (var guide in listGuide)
            {
                var guideExpense = TransferRequestByDateBLL.ExpenseGetAllByCriterion(guide, busByDate.Date, busByDate.Route)
                .Future().ToList().FirstOrDefault();
                if (guideExpense != null) listGuideExpense.Add(guideExpense);
            }
            var templateOptRow = currentRow;
            //Lấy danh sách opt
            var listOpt = listGuideExpense.Select(x => x.Operator);
            var listOpt_Distinct = listOpt.Distinct().DefaultIfEmpty(new User()).ToList();//Lấy danh sách Opt không lặp
            sheet.InsertRow(currentRow, listOpt_Distinct.Count - 1, templateOptRow);
            FillOpt(listOpt_Distinct, sheet, ref currentRow);
            //--
            //Export booking trong ngày
            currentRow = currentRow + 2;//Chuyển current row đến templaterow booking
            int templateRow = currentRow;
            int totalRow = templateRow + listBooking.Count();
            int index = 1;
            currentRow++;//Chuyển current row đến trước template row để bắt đầu coppyrow
            sheet.InsertRow(currentRow, listBooking.Count, templateRow);
            for (int i = 0; i < listBooking.Count; i++)
            {
                var booking = listBooking[i] as Booking;
                if (booking != null)
                {
                    var name = booking.CustomerNameFull.Replace("<br/>", "\r\n").ToUpper();
                    sheet.Cells[currentRow, 1].Value = index;
                    sheet.Cells[currentRow, 2, currentRow, 3].Merge = true;
                    sheet.Cells[currentRow, 2].Value = name;
                    sheet.Cells[currentRow, 4].Value = booking.Adult;
                    sheet.Cells[currentRow, 5].Value = booking.Child;
                    sheet.Cells[currentRow, 6].Value = booking.Baby;
                    sheet.Cells[currentRow, 7].Value = booking.Trip.TripCode;
                    sheet.Cells[currentRow, 8].Value = booking.PickupAddress;
                    sheet.Cells[currentRow, 9].Value = booking.SpecialRequest;
                    sheet.Cells[currentRow, 11].Value = "OS" + booking.Id;
                    sheet.Cells[currentRow, 26].Value = name;//Work around cho cột merged name không hiển thị hết khi nội dung quá dài
                    currentRow++;
                    index++;
                }
            }
            sheet.DeleteRow(templateRow);
            sheet.Cells[totalRow, 4].Value = listBooking.Sum(x => x.Adult);
            sheet.Cells[totalRow, 5].Value = listBooking.Sum(x => x.Child);
            sheet.Cells[totalRow, 6].Value = listBooking.Sum(x => x.Baby);
            sheet.Cells["I3"].Value = "Total number of pax: " + (listBooking.Sum(x => x.Adult) + listBooking.Sum(x => x.Child));
            //--
        }
        private void FillGuide(IList<Guide> listGuide, ExcelWorksheet sheet, ref int currentRow)
        {
            for (var i = 0; i < listGuide.Count; i++)
            {
                //Nếu là Guide thứ 2 trở đi xóa chữ "Guide:"
                if (i == 0) { sheet.Cells[currentRow, 2].Value = "Guide:"; }
                else { sheet.Cells[currentRow, 2].Value = ""; }
                //--
                sheet.Cells[currentRow, 3].Value = listGuide[i].Name;
                sheet.Cells[currentRow, 7].Value = "Mob:";
                sheet.Cells[currentRow, 8].Value = NumberUtil.FormatPhoneNumber(listGuide[i].Phone);
                ++currentRow;
            }
        }
        private void FillOpt(List<User> listOpt, ExcelWorksheet sheet, ref int currentRow)
        {
            for (var i = 0; i < listOpt.Count; i++)
            {
                //Nếu là Opt thứ 2 trở đi xóa chữ "Opt:"
                if (i == 0) { sheet.Cells[currentRow, 2].Value = "Opt:"; }
                else { sheet.Cells[currentRow, 2].Value = ""; }
                //--
                sheet.Cells[currentRow, 3].Value = listOpt[i].FullName;
                sheet.Cells[currentRow, 7].Value = "Mob:";
                sheet.Cells[currentRow, 8].Value = NumberUtil.FormatPhoneNumber(listOpt[i].Phone);
                ++currentRow;
            }
        }
        private void FillDriver(IList<BusByDate> listBusByDate, ExcelWorksheet sheet, ref int currentRow)
        {
            for (var i = 0; i < listBusByDate.Count; i++)
            {
                //Nếu là Driver thứ 2 trở đi xóa chữ "Driver:"
                if (i == 0) { sheet.Cells[currentRow, 2].Value = "Driver:"; }
                else { sheet.Cells[currentRow, 2].Value = ""; }
                //--
                sheet.Cells[currentRow, 3].Value = listBusByDate[i].Driver_Name;
                sheet.Cells[currentRow, 7].Value = "Mob:";
                sheet.Cells[currentRow, 8].Value = NumberUtil.FormatPhoneNumber(listBusByDate[i].Driver_Phone);
                ++currentRow;
            }
        }
    }
}
