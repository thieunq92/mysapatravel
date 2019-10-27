using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Web.Components;
using CMS.Web.Util;
using Portal.Modules.Orientalsails.API.BLL;
using Portal.Modules.OrientalSails;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace Portal.Modules.Orientalsails.API.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RoomService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RoomService.svc or RoomService.svc.cs at the Solution Explorer and start debugging.
    public class RoomService : IRoomService
    {
        private RoomServiceBLL roomServiceBLL;
        public RoomServiceBLL RoomServiceBLL
        {
            get
            {
                if (roomServiceBLL == null)
                    roomServiceBLL = new RoomServiceBLL();
                return roomServiceBLL;
            }
        }
        public string RoomGetAvaiable(int roomClassId, int roomTypeId, int cruiseId, string startDate, int tripId)
        {
            CoreRepository CoreRepository = HttpContext.Current.Items["CoreRepository"] as CoreRepository;
            int nodeId = 1;
            Node node = (Node)CoreRepository.GetObjectById(typeof(Node), nodeId);
            int sectionId = 15;
            Section section = (Section)CoreRepository.GetObjectById(typeof(Section), sectionId);
            SailsModule module = (SailsModule)ContainerAccessorUtil.GetContainer().Resolve<ModuleLoader>().GetModuleFromSection(section);
            var roomClass = RoomServiceBLL.RoomClassGetById(roomClassId);
            var roomType = RoomServiceBLL.RoomTypeGetById(roomTypeId);
            var cruise = RoomServiceBLL.CruiseGetById(cruiseId);
            var trip = RoomServiceBLL.TripGetById(tripId);
            if(startDate == null)
            {
                return "Start date is required!";
            }
            var startDateDT = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return module.RoomCount(roomClass, roomType, cruise, startDateDT, trip.NumberOfDay, trip.HalfDay).ToString();
        }
    }
}
