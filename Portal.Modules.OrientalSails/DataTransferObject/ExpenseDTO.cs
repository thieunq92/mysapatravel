using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public string GuideName { get; set; }
        public string GuidePhone { get; set; }
        public int CruiseId { get; set; }
        public string Cost { get; set; }
        public string Date { get; set; }
        public int Operator_UserId { get; set; }
        public string Operator_FullName { get; set; }
        public string Operator_Phone { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LockStatus { get; set; }
    }
}