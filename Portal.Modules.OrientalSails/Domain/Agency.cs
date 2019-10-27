using System;
using System.Collections;
using CMS.Core.Domain;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.Domain
{
    //Lớp model cho bảng os_Agency giữ thông tin của Agency
    public class Agency
    {
        private IList users;
        private IList bookings;
        protected IList history;
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Address { get; set; }
        [Obsolete]
        public virtual string Contract { get; set; }
        public virtual string Description { get; set; }
        [Obsolete]
        public virtual int ContractStatus { get; set; }
        public virtual string TaxCode { get; set; }
        public virtual string Email { get; set; }
        public virtual string Fax { get; set; }
        public virtual Role Role { get; set; } //Loại agency là đối tác, nhà xe hay hướng dẫn
        public virtual string TradingName { get; set; } //Tên giao dịch trong hợp đồng 
        public virtual string Representative { get; set; }//Tên người đại diện của agency trong hợp đồng
        public virtual string RepresentativePosition { get; set; }//chức vụ của người đại diện trong hợp đồng
        public virtual string Contact { get; set; }//người liên hệ của agency
        public virtual string ContactAddress { get; set; }//Địa chỉ của người liên hệ 
        public virtual string ContactPosition { get; set; }//Chức vụ của người liên hẹ
        public virtual string ContactEmail { get; set; }//Email của người liên hệ
        public virtual string Website { get; set; }
        [Obsolete]
        public virtual string AgencyType { get; set; }
        [Obsolete]
        public virtual IList Users
        {
            get
            {
                if (users == null)
                {
                    users = new ArrayList();
                }
                return users;
            }
            set { users = value; }
        }

        //Lịch sử chỉnh sửa salesincharge
        public virtual IList History
        {
            get
            {
                if (history == null)
                {
                    history = new ArrayList();
                }
                return history;
            }
            set { history = value; }
        }
        //--
        [Obsolete]
        public virtual IList Bookings
        {
            get
            {
                if (bookings == null)
                {
                    bookings = new ArrayList();
                }
                return bookings;
            }
            set { bookings = value; }
        }

        public virtual User Sale { get; set; } //Sales in charge của agency
        [Obsolete]
        public virtual string Accountant { get; set; }
        [Obsolete]
        public virtual PaymentPeriod PaymentPeriod { get; set; }
        [Obsolete]
        public virtual DateTime LastBooking { get; set; }
        public virtual DateTime? CreatedDate { get; set; } //Ngày tạo agency
        public virtual DateTime? ModifiedDate { get; set; }//Ngày chỉnh sửa agency
        public virtual User CreatedBy { get; set; }//Người tạo agency
        public virtual User ModifiedBy { get; set; }//Người chỉnh sửa agency
        public virtual bool Deleted { get; set; }//Đánh dấu agency đã được xóa hay chưa 
        public virtual DateTime? SaleStart { get; set; }//Ngày sales bắt đầu nhận agency
        public virtual AgencyLocation Location { get; set; }//Agency ở vùng nào
        public virtual ICollection<Series> ListSeries { get; set; }//Danh sách các series booking liên quan đến agency 
        public virtual ICollection<Expense> ListExpense { get; set; }//Danh sách các chi phí liên quan đến agency
        public virtual ICollection<BusByDate> BusByDates { get; set; }//Danh sách các bus liên quan đến agency
        public virtual string ContractAddress { get; set; }//Địa chỉ của agency trên hợp đồng
        public virtual string ContractPhone { get; set; }//Só điện thoại của agency trên hợp đồng
        public virtual string ContractTaxCode { get; set; }//Mã số thuế của agency trên hợp đồng
        public virtual QAgentLevel QAgentlevel { get; set; }

        public Agency()
        {
            ListSeries = new List<Series>();
            ListExpense = new List<Expense>();
            BusByDates = new List<BusByDate>();
        }
    }

    [Obsolete]
    public enum PaymentPeriod
    {
        None,
        Monthly,
        MonthlyCK
    }

    [Obsolete]
    public class PaymentPeriodClass : NHibernate.Type.EnumStringType
    {
        public PaymentPeriodClass()
            : base(typeof(PaymentPeriod), 20)
        {

        }
    }
}
