using CMS.Core.Domain;
using Newtonsoft.Json;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for ViewActivitiesWebMethod
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class ViewActivitiesWebMethod : System.Web.Services.WebService
    {

        private ViewActivitiesBLL viewActivitiesBLL;
        public ViewActivitiesBLL ViewActivitiesBLL
        {
            get
            {
                if (viewActivitiesBLL == null)
                {
                    viewActivitiesBLL = new ViewActivitiesBLL();
                }
                return viewActivitiesBLL;
            }
        }

        public new void Dispose()
        {
            if (viewActivitiesBLL != null)
            {
                viewActivitiesBLL.Dispose();
                viewActivitiesBLL = null;
            }
        }

        [WebMethod]
        public string UserGetById(string ui)
        {
            int userId = -1;
            try
            {
                userId = Int32.Parse(ui);
            }
            catch { }

            var user = ViewActivitiesBLL.UserGetById(userId);
            var userDTO = new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            Dispose();
            return JsonConvert.SerializeObject(userDTO);
        }

        [WebMethod]
        public string MeetingGetAllBy(string ui, string f, string t)
        {
            int userId = -1;
            try
            {
                userId = Int32.Parse(ui);
            }
            catch { }

            DateTime? from = null;
            try
            {
                from = DateTime.ParseExact(f, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            DateTime? to = null;
            try
            {
                to = DateTime.ParseExact(t, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var listMeeting = ViewActivitiesBLL.MeetingGetAllBy(userId, from, to);
            var listMeetingDTO = new List<MeetingDTO>();
            foreach (var meeting in listMeeting)
            {
                var updateTime = "";
                try
                {
                    updateTime = meeting.UpdateTime.Value.ToString("dd/MM/yyyy");
                }
                catch { }

                var dateMeeting = "";
                try
                {
                    dateMeeting = meeting.DateMeeting.ToString("dd/MM/yyyy");
                }
                catch { }

                var salesName = "";
                try
                {
                    salesName = meeting.User.FullName.ToString();
                }
                catch { }

                var agencyContact = ViewActivitiesBLL.AgencyContactGetById(meeting.ObjectId);

                var agencyName = "";
                try
                {
                    agencyName = agencyContact.Agency.Name;
                }
                catch { }

                var meetingDTO = new MeetingDTO()
                {
                    UpdateTime = updateTime,
                    DateMeeting = dateMeeting,
                    SalesName = salesName,
                    MeetWith = agencyContact.Name,
                    Position = agencyContact.Position,
                    BelongToAgency = agencyName,
                    Note = meeting.Note,
                };
                listMeetingDTO.Add(meetingDTO);
            }
            Dispose();
            return JsonConvert.SerializeObject(listMeetingDTO);
        }

        [WebMethod]
        public string BookingGetAllBy(string ui, string f, string t)
        {
            int userId = -1;
            try
            {
                userId = Int32.Parse(ui);
            }
            catch { }

            DateTime? from = null;
            try
            {
                from = DateTime.ParseExact(f, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            DateTime? to = null;
            try
            {
                to = DateTime.ParseExact(t, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var listBooking = ViewActivitiesBLL.BookingGetAllBy(userId, from, to);
            var listBookingDTO = new List<BookingDTO>();

            foreach (var booking in listBooking)
            {
                var lastEdit = "";
                try
                {
                    lastEdit = booking.ModifiedBy.FullName;
                }
                catch { }

                var bookingDTO = new BookingDTO()
                {
                    Id = booking.Id,
                    TripCode = booking.Trip.TripCode,
                    CruiseName = booking.Cruise.Name,
                    NoOfPax = booking.Pax,
                    CustomerName = booking.CustomerName,
                    AgencyName = booking.Agency.Name,
                    TACode = booking.AgencyCode,
                    Status = booking.Status.ToString(),
                    ModifiedBy = lastEdit,
                    StartDate = booking.StartDate.ToString("dd/MM/yyyy")
                };
                listBookingDTO.Add(bookingDTO);
            }
            Dispose();
            return JsonConvert.SerializeObject(listBookingDTO);
        }

        [WebMethod]
        public string SeriesGetAllBy(string ui, string f, string t)
        {
            int userId = -1;
            try
            {
                userId = Int32.Parse(ui);
            }
            catch { }

            DateTime? from = null;
            try
            {
                from = DateTime.ParseExact(f, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            DateTime? to = null;
            try
            {
                to = DateTime.ParseExact(t, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var listSeries = ViewActivitiesBLL.SeriesGetAllBy(userId, from, to);
            var listSeriesDTO = new List<SeriesDTO>();
            foreach (var series in listSeries)
            {
                var salesInChargeName = "";
                try
                {
                    salesInChargeName = series.Agency.Sale.FullName;
                }
                catch { }
                var seriesDTO = new SeriesDTO()
                {
                    AgencyName = series.Agency.Name,
                    SeriesCode = series.SeriesCode,
                    BookerName = series.Booker.Name,
                    SalesInChargeName = salesInChargeName,
                    CutoffDate = series.CutoffDate,
                    NoOfDays = series.NoOfDays == 1 ? "2 days 1 night" : "3 days 2 nights",
                    NoOfBooking = series.ListBooking.Count.ToString(),
                    Status = SeriesGetStatus(series.Id),
                };
                listSeriesDTO.Add(seriesDTO);
            }

            return JsonConvert.SerializeObject(listSeriesDTO);
        }

        public string SeriesGetStatus(int seriesId)
        {
            var series = ViewActivitiesBLL.SeriesGetById(seriesId);
            var result = "";
            var listBookingGroupByStatus = series.ListBooking.GroupBy(x => x.Status);
            foreach (var bookingGroupByStatus in listBookingGroupByStatus)
            {
                result = result + bookingGroupByStatus.Count() + " " + bookingGroupByStatus.Key.ToString() + "</br>";
            }
            return result;
        }

        [WebMethod]
        public string AgencyGetAllBy(string ui, string f, string t)
        {
            int userId = -1;
            try
            {
                userId = Int32.Parse(ui);
            }
            catch { }

            DateTime? from = null;
            try
            {
                from = DateTime.ParseExact(f, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            DateTime? to = null;
            try
            {
                to = DateTime.ParseExact(t, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var listAgency = ViewActivitiesBLL.AgencyGetAllBy(userId, from, to);
            var listAgencyDTO = new List<AgencyDTO>();

            foreach (var agency in listAgency)
            {
                var agencyDTO = new AgencyDTO()
                {
                    Id = agency.Id,
                    Name = agency.Name,
                    Phone = agency.Phone,
                    Fax = agency.Fax,
                    Email = agency.Email,
                    RoleName = agency.Role.Name,
                };
                listAgencyDTO.Add(agencyDTO);
            }
            return JsonConvert.SerializeObject(listAgencyDTO);
        }
    }
}
