using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public DateTime Time { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string Params { get; set; }
        public string Note { get; set; }
        public string ObjectType { get; set; }
        public int ObjectId { get; set; }
        public DateTime DateMeeting { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Type { get; set; }
        public bool NeedManagerAttention { get; set; }
        public string Attachment { get; set; }
        public string AttachmentContentType { get; set; }
        public string Problems { get; set; }
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactPosition { get; set; }
        public int CruiseId { get; set; }
    }
}