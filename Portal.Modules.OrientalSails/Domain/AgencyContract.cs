using System;

namespace Portal.Modules.OrientalSails.Domain
{
    //Lớp model cho bảng os_AgencyContract
    public class AgencyContract
    {    
        public virtual int Id { get; set; }
        public virtual string ContractName { get; set; }
        [Obsolete]
        public virtual byte[] ContractFile { get; set; }
        public virtual DateTime? ExpiredDate { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual string FileName { get; set; } //Tên file hợp đồng 
        public virtual string FilePath { get; set; } //Đường dẫn đến file hợp đồng được sales upload
        public virtual DateTime? CreateDate { set; get; }
        [Obsolete]
        public virtual Boolean Received { set; get; }
        [Obsolete]
        public virtual int ContractTemplate { get; set; }
        [Obsolete]
        public virtual DateTime? ContractValidFromDate { get; set; }
        [Obsolete]
        public virtual DateTime? ContractValidToDate { get; set; }
        [Obsolete]
        public virtual int QuotationTemplate { get; set; }
        [Obsolete]
        public virtual DateTime? QuotationValidFromDate { get; set; }
        [Obsolete]
        public virtual DateTime? QuotationValidToDate { get; set; }
        [Obsolete]
        public virtual int Status { get; set; }
        [Obsolete]
        public virtual string ContractTemplatePath { get; set; }
        [Obsolete]
        public virtual string QuotationTemplatePath { get; set; }
        [Obsolete]
        public virtual string ContractTemplateName { get; set; }
        [Obsolete]
        public virtual string QuotationTemplateName { get; set; }
        [Obsolete]
        public virtual Contracts Contract { get; set; }
        [Obsolete]
        public virtual QQuotation QQuotation { get; set; }
        public virtual QCruiseGroup GroupCruise { get; set; }
        public virtual string AgentLevelCode { get; set; }
    }
}
