using CMS.Web;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Storage;
using Microsoft.Owin;
using Owin;
using Portal.Modules.OrientalSails.HangFire.ScheduleJob;
using System;
using System.IO;
using System.Web;
using System.Xml;

[assembly: OwinStartup(typeof(Startup))]
namespace CMS.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(GetConnectionString());
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(() => new AgencyContactSendBirthdayEmailJob().DoJob(), Cron.Daily);

            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }

        public static string GetConnectionString()
        {
            var pathFileConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/hibernate.cfg.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(pathFileConfig);
            var root = doc.DocumentElement;
            var propertiesNode = root.FirstChild.ChildNodes;
            foreach (XmlNode propertyNode in propertiesNode)
            {
                if (propertyNode.Attributes["name"].Value == "connection.connection_string")
                    return propertyNode.InnerText;
            }
            return "";
        }
    }

    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());
            return owinContext.Authentication.User.Identity.IsAuthenticated;
        }
    }
}