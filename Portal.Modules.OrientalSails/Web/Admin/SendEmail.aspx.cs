using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SendEmailPage : SailsAdminBasePage
    {
        //private const string NO_EMAIL = "Unable to obtain email address";
        private const string APPROVED_SUBJECT = "Approved for your booking in {0:dd/MM/yyyy}";
        private const string REJECTED_SUBJECT = "Booking in {0:dd/MM/yyyy} rejected";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Booking booking = Module.BookingGetById(Convert.ToInt32(Request.QueryString["BookingId"]));
                if (!string.IsNullOrEmpty(booking.Email))
                {
                    lblEmailTo.Text = booking.Email;
                }
                else if (booking.Booker != null && !string.IsNullOrEmpty(booking.Booker.Email))
                {
                    lblEmailTo.Text = booking.Booker.Email;
                }
                else if (booking.Agency != null && !string.IsNullOrEmpty(booking.Agency.Email))
                {
                    lblEmailTo.Text = booking.Agency.Email;
                }
                else
                {
                    lblEmailTo.Text = booking.CreatedBy.Email;
                }

                string[] data = new string[15];
                if (booking.Agency != null)
                {
                    data[0] = booking.Agency.Name;
                }
                else
                {
                    data[0] = SailsModule.NOAGENCY;
                }
                data[1] = booking.AgencyCode;
                data[2] = string.Format("MYS{0:00000}", booking.Id);
                data[3] = booking.Trip.Name;
                data[4] = booking.StartDate.ToString("dd/MM/yyyy");
                data[5] = booking.Pax.ToString();
                data[6] = booking.CustomerName;
                data[7] = booking.PickupAddress;
                data[8] = booking.SpecialRequest;
                data[9] = booking.Total.ToString("#,0.##");
                data[10] = UserIdentity.FullName;
                data[11] = UserIdentity.Email;
                data[12] = UserIdentity.Website;


                StatusType status = (StatusType)Convert.ToInt32(Request.QueryString["status"]);
                if (Request.QueryString["status"] == null)
                {
                    status = booking.Status;
                }
                switch (status)
                {
                    case StatusType.Approved:
                        StreamReader appReader = new StreamReader(Server.MapPath("/Modules/Sails/Admin/EmailTemplate/Approved.txt"));
                        string appFormat = appReader.ReadToEnd();
                        txtSubject.Text = string.Format(APPROVED_SUBJECT, booking.StartDate);
                        fckContent.Value = string.Format(appFormat, data);
                        break;
                    //case StatusType.Rejected:
                    //    StreamReader rejReader = new StreamReader(Server.MapPath("/Modules/Sails/Admin/EmailTemplate/rejected.txt"));
                    //    string rejFormat = rejReader.ReadToEnd();
                    //    txtSubject.Text = string.Format(REJECTED_SUBJECT, booking.StartDate);
                    //    fckContent.Value =
                    //        string.Format(rejFormat, data);
                    //    break;
                    case StatusType.Cancelled:
                        StreamReader canReader = new StreamReader(Server.MapPath("/Modules/Sails/Admin/EmailTemplate/cancelled.txt"));
                        string canFormat = canReader.ReadToEnd();
                        txtSubject.Text = string.Format(REJECTED_SUBJECT, booking.StartDate);
                        data[10] = booking.CancelPay.ToString();
                        fckContent.Value =
                            string.Format(canFormat, data);
                        break;
                    default:
                        break;
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Đăng nhập            
            SmtpClient smtpClient = new SmtpClient("mail.orientalsails.com");
            smtpClient.Credentials = new NetworkCredential("sales.c2@orientalsails.com", "os#@!123");
            //smtpClient.EnableSsl = true;

            // Địa chỉ email người gửi
            //MailAddress fromAddress = new MailAddress(UserIdentity.Email);
            MailAddress fromAddress = new MailAddress("sales@orientalsails.com");

            MailMessage message = new MailMessage();
            message.From = fromAddress;
            message.To.Add(lblEmailTo.Text);
            message.Subject = txtSubject.Text;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = fckContent.Value;
            message.CC.Add(UserIdentity.Email);

            smtpClient.Send(message);
            ClientScript.RegisterClientScriptBlock(typeof(SendEmail), "closure", "window.close()", true);
        }
    }
}
