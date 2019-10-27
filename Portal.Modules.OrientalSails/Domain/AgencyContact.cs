using System;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.Domain
{
    //Lớp model cho bảng os_AgencyContact
    //AgencyContact là những thông tin liên quan đến người liên hệ của agency
    public class AgencyContact
    {
        public AgencyContact()
        {
            Id = -1;
            Enabled = true;
        }
        public virtual int Id { get; set; }
        public virtual bool IsBooker { get; set; }
        public virtual string Name { get; set; }
        public virtual string Position { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual bool Enabled { get; set; }//Contact còn hoạt động hay không
        public virtual Agency Agency { get; set; }//Thuộc agency nào

        public virtual int AgencyId
        {
            get
            {
                if (Agency != null)
                {
                    return Agency.Id;
                }
                return -1;
            }
        }

        public virtual DateTime? Birthday { set; get; }
        public virtual String Note { set; get; }//Thông tin thêm về contact
        public virtual IList<Series> ListSeries { get; set; }//Contact có những series booking nào
    }
}
