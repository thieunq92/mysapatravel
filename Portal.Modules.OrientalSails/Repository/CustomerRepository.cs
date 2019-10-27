using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using NHibernate.Transform;
using Portal.Modules.OrientalSails.Web.Util;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository() : base() { }

        public CustomerRepository(ISession session) : base(session) { }

        public string CustomerGetNameByBookingId(int bookingId)
        {
            BookingRoom bookingRoomAlias = null;
            var customer = _session.QueryOver<Customer>()
                .JoinAlias(x => x.BookingRooms, () => bookingRoomAlias)
                .JoinQueryOver(() => bookingRoomAlias.Book)
                .Where(x => x.Id == bookingId)
                .Take(1).FutureValue<Customer>().Value;

            if (customer == null)
                return "";

            return customer.Fullname;
        }

        public int CustomerCountPaxByBookingId(int bookingId)
        {
            BookingRoom bookingRoomAlias = null;
            var customerCounting = _session.QueryOver<Customer>()
                .Fetch(x => x.BookingRooms).Eager
                .JoinAlias(x => x.BookingRooms, () => bookingRoomAlias)
                .Fetch(x => bookingRoomAlias.Book).Eager
                .JoinQueryOver(() => bookingRoomAlias.Book)
                .Where(x => x.Id == bookingId)
                .Select(Projections.RowCount())
                .FutureValue<int>().Value;

            return customerCounting;
        }

        public int CustomerCountAdult(int bookingId)
        {
            BookingRoom bookingRoomAlias = null;
            var customerAdultCounting = _session.QueryOver<Customer>()
                .Where(x => x.IsChild == false)
                .Fetch(x => x.BookingRooms).Eager
                .JoinAlias(x => x.BookingRooms, () => bookingRoomAlias)
                .Fetch(x => bookingRoomAlias.Book).Eager
                .JoinQueryOver(() => bookingRoomAlias.Book)
                .Where(x => x.Id == bookingId)
                .Select(Projections.RowCount())
                .FutureValue<int>().Value;

            return customerAdultCounting;
        }

        public void CustomerSaveOrUpdate(Customer customer)
        {
            SaveOrUpdate(customer);
        }

        public int CustomerGetRowCountByCriterion(Cruise cruise, DateTime? date)
        {
            var query = _session.QueryOver<Customer>();
            query = query.Where(x => x.Type == CustomerType.Adult || x.Type == CustomerType.Children);
            BookingRoom bookingRoomAlias = null;
            query = query.JoinAlias(x => x.BookingRooms, () => bookingRoomAlias);
            Booking bookingAlias = null;
            query = query.JoinAlias(() => bookingRoomAlias.Book, () => bookingAlias);
            if (cruise != null)
            {
                query = query.Where(() => bookingAlias.Cruise == cruise);
            }
            if (date != null)
            {
                query = query.Where(() => (bookingAlias.EndDate > date && bookingAlias.StartDate > date.Value.AddDays(-1) && bookingAlias.StartDate < date.Value.AddDays(1)) || (bookingAlias.StartDate < date && bookingAlias.EndDate > date));
            }
            query = query.Where(() => bookingAlias.Deleted == false);
            query = query.Where(() => bookingAlias.Status != StatusType.Cancelled && bookingAlias.Status != StatusType.CutOff);
            query = query.Select(Projections.RowCount());
            return query.FutureValue<int>().Value;
        }

        public int CustomerGetNumberOfCustomersInMonth(int month, int year, User user)
        {
            var firstDateOfMonth = new DateTime(year, month, 1);
            var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);
            var query = _session.QueryOver<Customer>();
            query = query.Where(x => x.Type == CustomerType.Adult || x.Type == CustomerType.Children);
            BookingRoom bookingRoomAlias = null;
            query = query.JoinAlias(x => x.BookingRooms, () => bookingRoomAlias);
            Booking bookingAlias = null;
            query = query.JoinAlias(() => bookingRoomAlias.Book, () => bookingAlias);
            BookingSale bookingSalesAlias = null;
            query = query.JoinAlias(() => bookingAlias.BookingSale, () => bookingSalesAlias);
            if (user != null)
            {
                query = query.Where(() => bookingSalesAlias.Sale == user);
            }
            query = query.Where(() => bookingAlias.Deleted == false);
            query = query.Where(() => bookingAlias.StartDate >= firstDateOfMonth && bookingAlias.StartDate <= lastDateOfMonth);
            query = query.Where(() => bookingAlias.Status == StatusType.Approved);
            query = query.Select(Projections.RowCount());
            return query.FutureValue<int>().Value;
        }
    }
}