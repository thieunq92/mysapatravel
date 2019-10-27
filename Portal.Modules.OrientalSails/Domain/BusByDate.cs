using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BusByDate
    {
        public virtual int Id { get; set; }
        public virtual Agency Guide{ get; set; }
        public virtual string Driver_Name { get; set; }
        public virtual string Driver_Phone { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual BusType BusType { get; set; }
        public virtual Route Route { get; set; }
        public virtual int Group { get; set; }
        public virtual BusByDate BusByDateRouteBackRef { get; set; }
        public virtual bool Cloned { get; set; }
        public virtual Agency Supplier { get; set; }
        public virtual ICollection<Expense> ListExpense { get; set; }
        public virtual ICollection<BookingBusByDate> ListBookingBusByDate { get; set; }
        public virtual ICollection<BusByDateGuide> BusByDatesGuides { get; set; }
        public BusByDate()
        {
            ListExpense = new List<Expense>();
            ListBookingBusByDate = new List<BookingBusByDate>();
            BusByDatesGuides = new List<BusByDateGuide>();
        }
    }
}