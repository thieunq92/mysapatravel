using System;
using System.Collections;
using System.Collections.Generic;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BookingVoucher
    {
        public virtual int Id { get; set; }
        public virtual VoucherBatch Voucher { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual string Code { get; set; }
    }
}