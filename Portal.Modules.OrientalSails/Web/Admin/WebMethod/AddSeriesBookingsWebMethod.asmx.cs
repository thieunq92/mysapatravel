using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Web.Components;
using CMS.Web.Util;
using Newtonsoft.Json;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Utils;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AddSeriesBookingsWebMethod : System.Web.Services.WebService
    {
        private AddSeriesBookingsBLL addSeriesBookingsBLL;

        public AddSeriesBookingsBLL AddSeriesBookingsBLL
        {
            get
            {
                if (addSeriesBookingsBLL == null)
                    addSeriesBookingsBLL = new AddSeriesBookingsBLL();
                return addSeriesBookingsBLL;
            }
        }

        private UserBLL userBLL;
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                    userBLL = new UserBLL();
                return userBLL;
            }
        }

        private PermissionBLL permissionBLL;
        public PermissionBLL PermissionBLL
        {
            get
            {
                if (permissionBLL == null)
                {
                    permissionBLL = new PermissionBLL();
                }
                return permissionBLL;
            }
        }

        public new void Dispose()
        {
            if (addSeriesBookingsBLL != null)
            {
                addSeriesBookingsBLL.Dispose();
                addSeriesBookingsBLL = null;
            }

            if (userBLL != null)
            {
                userBLL.Dispose();
                userBLL = null;
            }

            if (permissionBLL != null)
            {
                permissionBLL.Dispose();
                permissionBLL = null;
            }
        }

        [WebMethod]
        public string AgencyContactGetAllByAgency(string ai)
        {
            var agencyId = -1;
            try
            {
                agencyId = Convert.ToInt32(ai);
            }
            catch { }

            var listAgencyContactDTO = new List<AgencyContactDTO>();
            var listAgencyContact = AddSeriesBookingsBLL.AgencyContactGetAllByAgency(agencyId);
            foreach (var agencyContact in listAgencyContact)
            {
                var agencyContactDTO = new AgencyContactDTO()
                {
                    Id = agencyContact.Id,
                    Name = agencyContact.Name,
                };
                listAgencyContactDTO.Add(agencyContactDTO);
            }
            Dispose();
            return JsonConvert.SerializeObject(listAgencyContactDTO);
        }

        [WebMethod]

        public string CreateSeries(string ai, string bi, string sc, string cd, string nod, string sr)
        {
            var agencyId = -1;
            try
            {
                agencyId = Convert.ToInt32(ai);
            }
            catch { }

            var agency = AddSeriesBookingsBLL.AgencyGetById(agencyId);

            var bookerId = -1;
            try
            {
                bookerId = Convert.ToInt32(bi);
            }
            catch { }

            var booker = AddSeriesBookingsBLL.AgencyContactGetById(bookerId);

            var seriesCode = sc;

            int cutoffDate = -1;
            try
            {
                cutoffDate = Convert.ToInt32(cd);
            }
            catch { }


            var noOfDays = -1;
            try
            {
                noOfDays = Convert.ToInt32(nod);
            }
            catch { }

            var specialRequest = sr;

            var series = new Series()
            {
                CutoffDate = cutoffDate,
                NoOfDays = noOfDays,
                CreatedDate = DateTime.Now,
                CreatedBy = UserBLL.UserGetCurrent(),
                Booker = booker,
                Agency = agency,
                SpecialRequest = specialRequest,
            };

            AddSeriesBookingsBLL.SeriesSaveOrUpdate(series);
            series.SeriesCode = String.IsNullOrEmpty(seriesCode) ? "OSS" + series.Id : seriesCode;
            AddSeriesBookingsBLL.SeriesSaveOrUpdate(series);
            var createdBy = "";
            try
            {
                createdBy = series.CreatedBy.FullName;
            }
            catch { }

            var createdDate = "";
            try
            {
                createdDate = series.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
            }
            catch { }

            var seriesDTO = new SeriesDTO()
            {
                Id = series.Id,
                SeriesCode = series.SeriesCode,
                CutoffDate = series.CutoffDate,
                NoOfDays = series.NoOfDays.ToString(),
                NoOfDaysTrip = series.NoOfDays == 2 ? "2 days 1 night" : "3 days 2 nights",
                CreatedBy = createdBy,
                CreatedDate = createdDate,
                SpecialRequest = specialRequest,
            };

            if (series.Booker != null)
            {
                seriesDTO.Booker = new SeriesDTO.BookerDTO()
                {
                    Id = series.Booker.Id,
                    Name = series.Booker.Name,
                };
            }

            if (series.Agency != null)
            {
                seriesDTO.Agency = new SeriesDTO.AgencyDTO()
                {
                    Id = series.Agency.Id,
                    Name = series.Agency.Name
                };
            }

            Dispose();
            return JsonConvert.SerializeObject(seriesDTO);
        }

        [WebMethod]
        public string UpdateSeries(string si, string ai, string bi, string sc, string cd, string nod, string sr)
        {
            var seriesId = -1;
            try
            {
                seriesId = Convert.ToInt32(si);
            }
            catch { }

            var series = AddSeriesBookingsBLL.SeriesGetById(seriesId);

            var agencyId = -1;
            try
            {
                agencyId = Convert.ToInt32(ai);
            }
            catch { }

            var agency = AddSeriesBookingsBLL.AgencyGetById(agencyId);

            var bookerId = -1;
            try
            {
                bookerId = Convert.ToInt32(bi);
            }
            catch { }

            var booker = AddSeriesBookingsBLL.AgencyContactGetById(bookerId);

            var seriesCode = sc;

            int cutoffDate = -1;
            try
            {
                cutoffDate = Convert.ToInt32(cd);
            }
            catch { }


            var noOfDays = -1;
            try
            {
                noOfDays = Convert.ToInt32(nod);
            }
            catch { }

            var specialRequest = sr;

            series.CutoffDate = cutoffDate;
            series.NoOfDays = noOfDays;
            series.Booker = booker;
            series.Agency = agency;
            series.SpecialRequest = specialRequest;

            AddSeriesBookingsBLL.SeriesSaveOrUpdate(series);
            series.SeriesCode = String.IsNullOrEmpty(seriesCode) ? "OSS" + series.Id : seriesCode;
            AddSeriesBookingsBLL.SeriesSaveOrUpdate(series);

            var createdBy = "";
            try
            {
                createdBy = series.CreatedBy.FullName;
            }
            catch { }

            var createdDate = "";
            try
            {
                createdDate = series.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
            }
            catch { }

            var seriesDTO = new SeriesDTO()
            {
                Id = series.Id,
                SeriesCode = series.SeriesCode,
                CutoffDate = series.CutoffDate,
                NoOfDays = series.NoOfDays.ToString(),
                NoOfDaysTrip = series.NoOfDays == 2 ? "2 days 1 night" : "3 days 2 nights",
                CreatedBy = createdBy,
                CreatedDate = createdDate,
                SpecialRequest = specialRequest,
            };

            if (series.Booker != null)
            {
                seriesDTO.Booker = new SeriesDTO.BookerDTO()
                {
                    Id = series.Booker.Id,
                    Name = series.Booker.Name,
                };
            }

            if (series.Agency != null)
            {
                seriesDTO.Agency = new SeriesDTO.AgencyDTO()
                {
                    Id = series.Agency.Id,
                    Name = series.Agency.Name
                };
            }

            Dispose();
            return JsonConvert.SerializeObject(seriesDTO);
        }

        [WebMethod]
        public string TripGetAllByNoOfDays(string nod)
        {

            var noOfDays = -1;
            try
            {
                noOfDays = Convert.ToInt32(nod);
            }
            catch { }

            var listTrip = AddSeriesBookingsBLL.TripGetAllByNoOfDays(noOfDays);
            var listTripDTO = new List<TripDTO>();
            foreach (var trip in listTrip)
            {
                var tripDTO = new TripDTO()
                {
                    Id = trip.Id,
                    Name = trip.Name
                };
                listTripDTO.Add(tripDTO);
            }
            Dispose();
            return JsonConvert.SerializeObject(listTripDTO);
        }

        [WebMethod]
        public string CheckRoom(string sd, string ti, string SectionId, string NodeId)
        {
            CoreRepository CoreRepository = HttpContext.Current.Items["CoreRepository"] as CoreRepository;
            int nodeId = Int32.Parse(NodeId);
            Node node = (Node)CoreRepository.GetObjectById(typeof(Node), nodeId);
            int sectionId = Int32.Parse(SectionId);
            Section section = (Section)CoreRepository.GetObjectById(typeof(Section), sectionId);
            SailsModule module = (SailsModule)ContainerAccessorUtil.GetContainer().Resolve<ModuleLoader>().GetModuleFromSection(section);

            DateTime? startDate = null;
            try
            {
                startDate = DateTime.ParseExact(sd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var tripId = -1;
            try
            {
                tripId = Convert.ToInt32(ti);
            }
            catch { }

            var trip = AddSeriesBookingsBLL.TripGetById(tripId);
            var listRoomClass = AddSeriesBookingsBLL.RoomClassGetAll();
            var listRoomType = AddSeriesBookingsBLL.RoomTypeGetAll();
            var listCruise = AddSeriesBookingsBLL.CruiseGetAllByTrip(trip);

            var listCheckRoomResultDTO = new List<CheckRoomResultDTO>();
            foreach (var cruise in listCruise)
            {
                var checkRoomResultDTO = new CheckRoomResultDTO();
                checkRoomResultDTO.Cruise = new CheckRoomResultDTO.CruiseDTO()
                {
                    Id = cruise.Id,
                    Name = cruise.Name
                };

                int total = 0;
                string detail = "";
                foreach (var roomClass in listRoomClass)
                {
                    foreach (var roomType in listRoomType)
                    {

                        if (trip == null)
                            break;

                        if (!startDate.HasValue)
                            break;

                        int avail = module.RoomCount(roomClass, roomType, cruise, startDate.Value, trip.NumberOfDay, trip.HalfDay);

                        if (avail > 0)
                        {
                            total += avail;
                            detail += string.Format("{0} {2} {1} ", avail, roomType.Name, roomClass.Name);
                        }

                    }
                }
                checkRoomResultDTO.NoOfRoomAvaiable = total;
                checkRoomResultDTO.DetailRooms = detail;
                listCheckRoomResultDTO.Add(checkRoomResultDTO);
            }
            Dispose();
            return JsonConvert.SerializeObject(listCheckRoomResultDTO);
        }


        [WebMethod]
        public string GetAvaiableRoom(string ci, string sd, string ti, string SectionId, string NodeId)
        {
            CoreRepository CoreRepository = HttpContext.Current.Items["CoreRepository"] as CoreRepository;
            int nodeId = Int32.Parse(NodeId);
            Node node = (Node)CoreRepository.GetObjectById(typeof(Node), nodeId);
            int sectionId = Int32.Parse(SectionId);
            Section section = (Section)CoreRepository.GetObjectById(typeof(Section), sectionId);
            SailsModule module = (SailsModule)ContainerAccessorUtil.GetContainer().Resolve<ModuleLoader>().GetModuleFromSection(section);

            DateTime? startDate = null;
            try
            {
                startDate = DateTime.ParseExact(sd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var tripId = -1;
            try
            {
                tripId = Convert.ToInt32(ti);
            }
            catch { }
            var trip = AddSeriesBookingsBLL.TripGetById(tripId);

            var cruiseId = -1;
            try
            {
                cruiseId = Convert.ToInt32(ci);
            }
            catch { }
            var cruise = AddSeriesBookingsBLL.CruiseGetById(cruiseId);

            var listRoomClass = AddSeriesBookingsBLL.RoomClassGetAll();
            var listRoomType = AddSeriesBookingsBLL.RoomTypeGetAll();

            var listAvaiableRoomDTO = new List<AvaiableRoomDTO>();
            foreach (var roomClass in listRoomClass)
            {
                foreach (var roomType in listRoomType)
                {

                    if (trip == null)
                        break;

                    if (!startDate.HasValue)
                        break;

                    var roomCount = module.RoomCount(roomClass, roomType, cruise, startDate.Value, trip.NumberOfDay, true,
                          trip.HalfDay);


                    var avaiableRoomDTO = new AvaiableRoomDTO();
                    if (roomCount > -1)
                    {
                        avaiableRoomDTO.KindOfRoom = roomClass.Name + " " + roomType.Name;
                        avaiableRoomDTO.RoomClass = new AvaiableRoomDTO.RoomClassDTO()
                        {
                            Id = roomClass.Id,
                            Name = roomClass.Name
                        };

                        avaiableRoomDTO.RoomType = new AvaiableRoomDTO.RoomTypeDTO()
                        {
                            Id = roomType.Id,
                            Name = roomType.Name,
                        };

                        var NoOfAdult = roomCount;
                        avaiableRoomDTO.NoOfAdult = NoOfAdult;

                        var NoOfChild = roomCount;
                        avaiableRoomDTO.NoOfChild = NoOfChild;

                        var NoOfBaby = roomCount;
                        avaiableRoomDTO.NoOfBaby = NoOfBaby;

                        listAvaiableRoomDTO.Add(avaiableRoomDTO);
                    }
                }
            }
            Dispose();
            return JsonConvert.SerializeObject(listAvaiableRoomDTO);
        }

        [WebMethod]

        public string SaveBooking(IList<AvaiableRoomDTO> listAvaiableRoomDTO, string si, string sd, string ci, string ti, string tac, string bsr)
        {

            var seriesId = -1;
            try
            {
                seriesId = Convert.ToInt32(si);
            }
            catch { }

            var series = AddSeriesBookingsBLL.SeriesGetById(seriesId);

            DateTime? startDate = null;
            try
            {
                startDate = DateTime.ParseExact(sd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var tripId = -1;
            try
            {
                tripId = Convert.ToInt32(ti);
            }
            catch { }
            var trip = AddSeriesBookingsBLL.TripGetById(tripId);

            var cruiseId = -1;
            try
            {
                cruiseId = Convert.ToInt32(ci);
            }
            catch { }
            var cruise = AddSeriesBookingsBLL.CruiseGetById(cruiseId);

            var bookingSpecialRequest = bsr;
            var taCode = tac;

            var booking = new Booking()
            {
                Agency = series.Agency,
                Booker = series.Booker,
                StartDate = startDate.Value,
                Trip = trip,
                Cruise = cruise,
                CreatedBy = UserBLL.UserGetCurrent(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedBy = UserBLL.UserGetCurrent(),
                Sale = UserBLL.UserGetCurrent(),
                Status = Util.StatusType.Pending,
                Total = 0,
                Series = series,
                AgencyCode = taCode,
                PickupAddress = "N/A",
                SpecialRequest = bookingSpecialRequest,
                Deadline = startDate.Value.Subtract(new TimeSpan(series.CutoffDate, 0, 0, 0)),
            };
            booking.EndDate = booking.StartDate.AddDays(booking.Trip.NumberOfDay - 1);
            AddSeriesBookingsBLL.BookingSaveOrUpdate(booking);

            foreach (var avaiableRoomDTO in listAvaiableRoomDTO)
            {
                var selectedRoom = 0;
                try
                {
                    selectedRoom = Convert.ToInt32(avaiableRoomDTO.selectedRoom);
                }
                catch { }

                var selectedChild = 0;
                try
                {
                    selectedChild = Convert.ToInt32(avaiableRoomDTO.selectedChild);
                }
                catch { }

                var selectedBaby = 0;
                try
                {
                    selectedBaby = Convert.ToInt32(avaiableRoomDTO.selectedBaby);
                }
                catch { }

                var noOfBaby = selectedBaby;
                var noOfChild = selectedChild;
                for (int i = 0; i < selectedRoom; i++)
                {

                    var bookingRoom = new BookingRoom()
                    {
                        Book = booking,
                        Booked = 2,
                        HasChild = noOfBaby > 0 ? true : false,
                        HasBaby = noOfChild > 0 ? true : false,
                        RoomClass = AddSeriesBookingsBLL.RoomClassGetById(avaiableRoomDTO.RoomClass.Id),
                        RoomType = AddSeriesBookingsBLL.RoomTypeGetById(avaiableRoomDTO.RoomType.Id),
                    };
                    AddSeriesBookingsBLL.BookingRoomSaveOrUpdate(bookingRoom);
                    noOfChild--;
                    noOfBaby--;

                    for (int j = 0; j < bookingRoom.Booked; j++)
                    {
                        var customer = new Customer()
                        {
                            Type = Util.CustomerType.Adult,
                        };
                        customer.BookingRooms.Clear();
                        customer.BookingRooms.Add(bookingRoom);
                        AddSeriesBookingsBLL.CustomerSaveOrUpdate(customer);

                    }

                    if (bookingRoom.HasChild)
                    {
                        var customer = new Customer()
                        {
                            Type = Util.CustomerType.Children,
                        };
                        customer.BookingRooms.Add(bookingRoom);
                        AddSeriesBookingsBLL.CustomerSaveOrUpdate(customer);
                    }

                    if (bookingRoom.HasBaby)
                    {
                        var customer = new Customer()
                        {
                            Type = Util.CustomerType.Baby,
                        };
                        customer.BookingRooms.Add(bookingRoom);
                        AddSeriesBookingsBLL.CustomerSaveOrUpdate(customer);
                    }
                }
            }
            Dispose();
            booking = AddSeriesBookingsBLL.BookingGetById(booking.Id);

            var modifiedName = "";
            try
            {
                modifiedName = booking.ModifiedBy.FullName;
            }
            catch (Exception) { }

            var modifiedDate = "";
            try
            {
                modifiedDate = booking.ModifiedDate.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception) { }
            var bookingDTO = new BookingDTO()
            {
                Id = booking.Id,
                BookingId = "OS" + booking.Id.ToString(),
                StartDate = booking.StartDate.ToString("dd/MM/yyyy"),
                TripName = booking.Trip.Name,
                CruiseName = booking.Cruise.Name,
                NoOfRoom = booking.BookingRooms.Count,
                NoOfPax = booking.Pax,
                Pax = string.Format("Adults : {0}</br> Childs : {1}<br/> Baby : {2}", booking.Adult, booking.Child, booking.Baby),
                Room = booking.RoomName,
                TACode = booking.AgencyCode,
                SpecialRequest = booking.SpecialRequest,
                Status = booking.Status.ToString(),
                CutoffDate = booking.Series.CutoffDate + " day(s)",
                TripCode = booking.Trip.TripCode,
                ModifiedBy = modifiedName,
                ModifiedDate = modifiedDate,
            };
            Dispose();
            return JsonConvert.SerializeObject(bookingDTO);
        }

        [WebMethod]

        public string CheckHaveAddSeriesPermission()
        {
            var haveAddSeriesPermission = PermissionBLL.UserCheckPermission(UserBLL.UserGetCurrent().Id, (int)PermissionEnum.ADDSERIES);

            Dispose();
            return JsonConvert.SerializeObject(haveAddSeriesPermission);
        }

        [WebMethod]
        public string SeriesGetByid(string si)
        {
            int seriesId = -1;
            try
            {
                seriesId = Int32.Parse(si);
            }
            catch { }

            var series = AddSeriesBookingsBLL.SeriesGetById(seriesId);

            var createdBy = "";
            try
            {
                createdBy = series.CreatedBy.FullName;
            }
            catch { }

            var createdDate = "";
            try
            {
                createdDate = series.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
            }
            catch { }

            var seriesDTO = new SeriesDTO()
            {
                Id = series.Id,
                SeriesCode = series.SeriesCode,
                CutoffDate = series.CutoffDate,
                NoOfDays = series.NoOfDays.ToString(),
                NoOfDaysTrip = series.NoOfDays == 2 ? "2 days 1 night" : "3 days 2 nights",
                CreatedBy = createdBy,
                CreatedDate = createdDate,
                SpecialRequest = series.SpecialRequest,
            };

            if (series.Booker != null)
            {
                seriesDTO.Booker = new SeriesDTO.BookerDTO()
                {
                    Id = series.Booker.Id,
                    Name = series.Booker.Name,
                };
            }

            if (series.Agency != null)
            {
                seriesDTO.Agency = new SeriesDTO.AgencyDTO()
                {
                    Id = series.Agency.Id,
                    Name = series.Agency.Name
                };
            }

            Dispose();
            return JsonConvert.SerializeObject(seriesDTO);
        }

        [WebMethod]
        public string BookingGetBySeries(string si)
        {
            int seriesId = -1;
            try
            {
                seriesId = Int32.Parse(si);
            }
            catch { }

            var series = AddSeriesBookingsBLL.SeriesGetById(seriesId);

            var listBookingDTO = new List<BookingDTO>();
            foreach (var booking in series.ListBooking.OrderBy(x => x.Status).ThenByDescending(x => x.StartDate))
            {
                var modifiedName = "";
                try
                {
                    modifiedName = booking.ModifiedBy.FullName;
                }
                catch { }

                var modifiedDate = "";
                try
                {
                    modifiedDate = booking.ModifiedDate.ToString("dd/MM/yyyy HH:mm");
                }
                catch { }
                var bookingDTO = new BookingDTO
                {
                    Id = booking.Id,
                    BookingId = "OS" + booking.Id.ToString(),
                    StartDate = booking.StartDate.ToString("dd/MM/yyyy"),
                    TripName = booking.Trip.Name,
                    CruiseName = booking.Cruise.Name,
                    NoOfRoom = booking.BookingRooms.Count,
                    NoOfPax = booking.Pax,
                    Pax = string.Format("Adults : {0}</br> Childs : {1}<br/> Baby : {2}", booking.Adult, booking.Child, booking.Baby),
                    Room = booking.RoomName,
                    TACode = booking.AgencyCode,
                    SpecialRequest = booking.SpecialRequest,
                    Status = booking.Status.ToString(),
                    CutoffDate = booking.Series.CutoffDate + " day(s)",
                    TripCode = booking.Trip.TripCode,
                    ModifiedBy = modifiedName,
                    ModifiedDate = modifiedDate,
                };
                listBookingDTO.Add(bookingDTO);
            }

            Dispose();
            return JsonConvert.SerializeObject(listBookingDTO);
        }

        [WebMethod]
        public void BookingApproved(string bi)
        {
            var bookingId = -1;
            try
            {
                bookingId = Int32.Parse(bi);
            }
            catch { }

            var booking = AddSeriesBookingsBLL.BookingGetById(bookingId);
            booking.Status = StatusType.Approved;
            booking.ModifiedBy = UserBLL.UserGetCurrent();
            booking.ModifiedDate = DateTime.Now;
            AddSeriesBookingsBLL.BookingSaveOrUpdate(booking);
            BookingHistorySave(booking);
            Dispose();
        }

        [WebMethod]
        public void BookingCancel(string bi)
        {
            var bookingId = -1;
            try
            {
                bookingId = Int32.Parse(bi);
            }
            catch { }

            var booking = AddSeriesBookingsBLL.BookingGetById(bookingId);
            booking.Status = StatusType.Cancelled;
            booking.ModifiedBy = UserBLL.UserGetCurrent();
            booking.ModifiedDate = DateTime.Now;
            AddSeriesBookingsBLL.BookingSaveOrUpdate(booking);
            BookingHistorySave(booking);
            Dispose();
        }

        public void BookingHistorySave(Booking booking)
        {
            var bookingHistory = new BookingHistory()
            {
                Booking = booking,
                Date = DateTime.Now,
                User = UserBLL.UserGetCurrent(),
                CabinNumber = booking.BookingRooms.Count,
                CustomerNumber = booking.Pax,
                StartDate = booking.StartDate,
                Status = booking.Status,
                Trip = booking.Trip,
                Agency = booking.Agency,
                Total = booking.Total,
                TotalCurrency = booking.IsTotalUsd ? "USD" : "VND",
            };
            AddSeriesBookingsBLL.BookingHistorySaveOrUpdate(bookingHistory);
        }

        [WebMethod]
        public string CheckDuplicateSeriesCode(string txtSeriesCode, string si)
        {
            var seriesId = -1;
            try
            {
                seriesId = Int32.Parse(si);
            }
            catch { }

            var duplicate = AddSeriesBookingsBLL.CheckDuplicateSeriesCode(txtSeriesCode, seriesId);
            if (duplicate)
            {
                Dispose();
                return "Series code đã tồn tại. Yêu cầu nhập series code khác";
            }
            else
            {
                Dispose();
                return JsonConvert.SerializeObject(true);
            }
        }
    }
}
