using System;
using System.Collections;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    //Lớp model cho bảng os_Activity giữ thông tin Meeting và Problem report
    public class Activity
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        [Obsolete]
        public virtual DateTime Time { get; set; }
        [Obsolete]
        public virtual string Action { get; set; }
        public virtual string Url { get; set; }
        public virtual string Params { get; set; } //Id của AgencyContact
        [Obsolete]
        public virtual ImportantLevel Level { get; set; }
        public virtual string Note { get; set; } //Nội dung meeting
        [Obsolete]
        public virtual string ObjectType { get; set; }
        public virtual int ObjectId { get; set; }//Id của Agency
        public virtual DateTime DateMeeting { get; set; }//Ngày sales gặp đối tác
        public virtual DateTime? UpdateTime { get; set; }//Ngày tạo và thay đổi meeting
        public virtual string Type { get; set; }//Kiểu meeting hay problem report
        public virtual bool NeedManagerAttention { get; set; }//Chọn need manager attention sẽ được lưu ở đây
        public virtual string Attachment { get; set; }//File đính kèm trong meeting
        public virtual string AttachmentContentType { get; set; }//Content type của file đính kèm
        public virtual string Problems { get; set; }//Chọn các problems sẽ được lưu ở đây
        public virtual Cruise Cruise { get; set; }//Thông tin cruise được chọn trong problem report
    }

    [Obsolete]
    public enum ImportantLevel
    {
        Info,
        Regular,
        Important
    }

}
