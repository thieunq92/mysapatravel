using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Portal.Modules.Orientalsails.API.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRoomService" in both code and config file together.
    [ServiceContract]
    public interface IRoomService
    {
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string RoomGetAvaiable(int roomClassId, int roomTypeId, int cruiseId, string startDate, int tripId);
    }
}
