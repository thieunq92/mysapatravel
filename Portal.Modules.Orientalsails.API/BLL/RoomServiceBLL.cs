using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.Orientalsails.API.BLL
{
    public class RoomServiceBLL 
    {
        public RoomClassRepository RoomClassRepository { get; set; }
        public RoomTypeRepository RoomTypeRepository { get; set;}
        public CruiseRepository CruiseRepository { get; set; }
        public TripRepository TripRepository { get; set; }
        public RoomServiceBLL() {
            RoomClassRepository = new RoomClassRepository();
            RoomTypeRepository = new RoomTypeRepository();
            CruiseRepository = new CruiseRepository();
            TripRepository = new TripRepository();
        }

        public RoomClass RoomClassGetById(int roomClassId)
        {
            return RoomClassRepository.RoomClassById(roomClassId);
        }

        public RoomTypex RoomTypeGetById(int roomTypeId)
        {
            return RoomTypeRepository.RoomTypeGetById(roomTypeId);
        }

        public Cruise CruiseGetById(int cruiseId)
        {
            return CruiseRepository.CruiseGetById(cruiseId);
        }

        public SailsTrip TripGetById(int tripId)
        {
            return TripRepository.TripGetById(tripId);
        }
    }
}