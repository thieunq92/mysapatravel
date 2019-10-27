using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Modules.Orientalsails.Service.Share
{
    public class EmailService
    {
        public void SendMessage(MailMessage message)
        {
            //workaround It make server trust any certificate
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            SmtpClient smtpClient = new SmtpClient();
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
        }
    }
}
