using Hangfire;
using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using Portal.Modules.Orientalsails.Service.Share;
using Portal.Modules.OrientalSails.HangFire.BusinessLogic;
using Portal.Modules.OrientalSails.HangFire.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Portal.Modules.OrientalSails.HangFire.ScheduleJob
{
    public class AgencyContactSendBirthdayEmailJob
    {
        private static readonly ILog log = LogManager.GetLogger("MyLogger");
        public AgencyContactSendBirthdayEmailJobBLL agencyContactSendBirthdayEmailJobBLL = null;
        public EmailService emailService = null;
        public AgencyContactSendBirthdayEmailJobBLL AgencyContactSendBirthdayEmailJobBLL
        {
            get
            {
                if (agencyContactSendBirthdayEmailJobBLL == null)
                    agencyContactSendBirthdayEmailJobBLL = new AgencyContactSendBirthdayEmailJobBLL();
                return agencyContactSendBirthdayEmailJobBLL;
            }
        }

        public EmailService EmailService
        {
            get
            {
                if (emailService == null)
                    emailService = new EmailService();
                return emailService;
            }
        }

        public void DoJob()
        {
            var listAgencyContactBirthday = AgencyContactSendBirthdayEmailJobBLL.AgencyContactGetByBirthday();
            foreach (AgencyContact agencyContact in listAgencyContactBirthday)
            {
                string content = string.Empty;
                using (StreamReader reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules/Sails/Admin/EmailTemplate/HappyBirthday.txt")))
                {
                    content = reader.ReadToEnd();
                }
                content = content.Replace("{tenkhach}", agencyContact.Name);
                MailMessage message = new MailMessage();
                message.From = new MailAddress("no-reply@orientalsails.com", "Orientalsails Team");
                try
                {
                    message.To.Add(agencyContact.Email);
                }
                catch
                {
                    message.To.Add("it2@atravelmate.com");
                    message.Body = "Xem lại địa chỉ email của AgencyContact : " + agencyContact.Name;
                    EmailService.SendMessage(message);
                    Dispose();
                    return;
                }
                message.Subject = "Happy Birthday To " + agencyContact.Name;
                message.Body = content;
                EmailService.SendMessage(message);
            }
            Dispose();
        }

        public void Dispose()
        {
            if (agencyContactSendBirthdayEmailJobBLL != null)
            {
                agencyContactSendBirthdayEmailJobBLL.Dispose();
                agencyContactSendBirthdayEmailJobBLL = null;
            }
        }
    }
}