using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Core.Service.Membership;
using CMS.Core.Util;
using CMS.Web.UI;
using CMS.Web.Util;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.ReportEngine;
using Portal.Modules.OrientalSails.Web.Admin;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.UI
{
    public class SailsAdminBasePage : KitModuleAdminBasePage
    {
        #region -- PRIVATE MEMBERS --
        protected IList _allRoomTypes;
        protected IList _allRoomClasses;
        #endregion

        #region -- PROPERTIES --
        public new SailsModule Module
        {
            get { return (SailsModule)base.Module; }
        }

        public IList AllRoomTypes
        {
            get
            {
                if (_allRoomTypes == null)
                {
                    _allRoomTypes = Module.RoomTypexGetAll();
                }
                return _allRoomTypes;
            }
        }

        public IList AllRoomClasses
        {
            get
            {
                if (_allRoomClasses == null)
                {
                    _allRoomClasses = Module.RoomClassGetAll();
                }
                return _allRoomClasses;
            }
        }

        #region -- Settings --
        public double DefaultTicket
        {
            get
            {
                if (ModuleSettings[SailsModule.TICKET] != null)
                {
                    return Convert.ToDouble(ModuleSettings[SailsModule.TICKET]);
                }
                return 0;
            }
        }

        public double DefaultMeal
        {
            get
            {
                if (ModuleSettings[SailsModule.MEAL] != null)
                {
                    return Convert.ToDouble(ModuleSettings[SailsModule.MEAL]);
                }
                return 0;
            }
        }

        public double DefaultKayaking
        {
            get
            {
                if (ModuleSettings[SailsModule.KAYAKING] != null)
                {
                    return Convert.ToDouble(ModuleSettings[SailsModule.KAYAKING]);
                }
                return 0;
            }
        }

        public double DefaultService
        {
            get
            {
                if (ModuleSettings[SailsModule.SERVICE] != null)
                {
                    return Convert.ToDouble(ModuleSettings[SailsModule.SERVICE]);
                }
                return 0;
            }
        }

        public double DefaultRent
        {
            get
            {
                if (ModuleSettings[SailsModule.RENT] != null)
                {
                    return Convert.ToDouble(ModuleSettings[SailsModule.RENT]);
                }
                return 0;
            }
        }

        public double ChildPrice
        {
            get
            {
                if (ModuleSettings["CHILD_PRICE"] != null)
                {
                    return Convert.ToDouble(ModuleSettings["CHILD_PRICE"]);
                }
                return 80;
            }
        }

        public bool UseCustomBookingId
        {
            get
            {
                if (ModuleSettings["CUSTOM_BK_ID"] != null)
                {
                    return Convert.ToBoolean(ModuleSettings["CUSTOM_BK_ID"]);
                }
                return false;
            }
        }

        public bool ShowCustomerName
        {
            get
            {
                if (ModuleSettings["SHOW_CUSTOMER"] != null)
                {
                    return Convert.ToBoolean(ModuleSettings["SHOW_CUSTOMER"]);
                }
                return true;
            }
        }

        public string BookingFormat
        {
            get
            {
                if (ModuleSettings["BOOKING_FORMAT"] != null)
                {
                    return ModuleSettings["BOOKING_FORMAT"].ToString();
                }
                return "{0:00000}";
            }
        }

        public double AgencySupplement
        {
            get
            {
                if (ModuleSettings["AgencySupplement"] != null)
                {
                    return Convert.ToDouble(ModuleSettings["AgencySupplement"]);
                }
                return 0;
            }
        }

        public double ChildCost
        {
            get
            {
                if (ModuleSettings["CHILD_COST"] != null)
                {
                    return Convert.ToDouble(ModuleSettings["CHILD_COST"]);
                }
                return 80;
            }
        }

        public bool CustomPriceAddBooking
        {
            get
            {
                if (ModuleSettings[SailsModule.ADD_BK_CUSTOMPRICE] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.ADD_BK_CUSTOMPRICE]);
                }
                return false;
            }
        }

        public bool CustomPriceForRoom
        {
            get
            {
                if (ModuleSettings[SailsModule.ROOM_CUSTOMPRICE] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.ROOM_CUSTOMPRICE]);
                }
                return false;
            }
        }

        public bool PartnershipManager
        {
            get
            {
                if (ModuleSettings[SailsModule.PARTNERSHIP] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.PARTNERSHIP]);
                }
                return false;
            }
        }

        public bool CheckAccountStatus
        {
            get
            {
                if (ModuleSettings[SailsModule.ACCOUNT_STATUS] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.ACCOUNT_STATUS]);
                }
                return false;
            }
        }

        public bool CheckCharter
        {
            get
            {
                if (ModuleSettings[SailsModule.CHECK_CHARTER] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.CHECK_CHARTER]);
                }
                return false;
            }
        }

        public bool ShowExpenseByDate
        {
            get
            {
                if (ModuleSettings[SailsModule.SHOW_EXPENSE_BY_DATE] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.SHOW_EXPENSE_BY_DATE]);
                }
                return false;
            }
        }

        public bool ShowBarRevenue
        {
            get
            {
                if (ModuleSettings[SailsModule.BAR_REVENUE] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.BAR_REVENUE]);
                }
                return false;
            }
        }

        public bool AllowNoAgency
        {
            get
            {
                if (ModuleSettings[SailsModule.NO_AGENCY_BK] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.NO_AGENCY_BK]);
                }
                return false;
            }
        }

        public bool DetailService
        {
            get
            {
                if (ModuleSettings[SailsModule.DETAIL_SERVICE] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.DETAIL_SERVICE]);
                }
                return false;
            }
        }

        public bool ShowOverallDailyExpense
        {
            get
            {
                if (ModuleSettings[SailsModule.OVERALL_EXPENSE] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.OVERALL_EXPENSE]);
                }
                return false;
            }
        }

        public bool ApprovedDefault
        {
            get
            {
                if (ModuleSettings[SailsModule.APPROVED_DEFAULT] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.APPROVED_DEFAULT]);
                }
                return false;
            }
        }

        public bool PuRequired
        {
            get
            {
                if (ModuleSettings[SailsModule.PUREQUIRED] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.PUREQUIRED]);
                }
                return false;
            }
        }

        public bool PeriodExpenseAvg
        {
            get
            {
                if (ModuleSettings[SailsModule.PERIOD_EXPENSE_AVG] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.PERIOD_EXPENSE_AVG]);
                }
                return true;
            }
        }

        public bool ApprovedLock
        {
            get
            {
                if (ModuleSettings[SailsModule.APPROVED_LOCK] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.APPROVED_LOCK]);
                }
                return true;
            }
        }

        public bool CustomerPrice
        {
            get
            {
                if (ModuleSettings[SailsModule.CUSTOMER_PRICE] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.CUSTOMER_PRICE]);
                }
                return true;
            }
        }

        public bool UseVNDExpense
        {
            get
            {
                if (ModuleSettings[SailsModule.USE_VND_EXPENSE] != null)
                {
                    return Convert.ToBoolean(ModuleSettings[SailsModule.USE_VND_EXPENSE]);
                }
                return true;
            }
        }
        #endregion
        #endregion

        protected override void OnPreInit(EventArgs e)
        {
            //LoadRes(null); ;
            //if (Page.MasterPageFile.Contains("SailsMaster.Master"))
            //{
            //    //Page.MasterPageFile = "OS.Master";
            //}
            base.OnPreInit(e);
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            HideError();
            if (Master is SailsMaster)
            {
                SailsMaster master = (SailsMaster)Master;


                if (Section != null && Node != null)
                {
                    string title = Node.Site.Name;
                    master.SetTitle(title);
                }

                if (UserIdentity != null)
                {
                    var allUser = Session["allUser"];
                    if (allUser == null)
                    {
                        Session["allUser"] = Module.GetAllUser();
                    }
                    bool needUpdate = false;
                    if (Session["lastupdate" + UserIdentity.Id] != null)
                    {
                        if (Convert.ToDateTime(Session["lastupdate" + UserIdentity.Id]) > DateTime.Now.AddMinutes(5))
                            needUpdate = true;
                    }
                    else
                    {
                        needUpdate = true;
                    }

                    if (needUpdate)
                    {
                        // Điều kiện bắt buộc: chưa xóa và đang pending
                        //ICriterion criterion = Expression.Eq(Booking.DELETED, false);
                        //criterion = Expression.And(criterion, Expression.Eq(Booking.STATUS, StatusType.Pending));
                        var criterion = Expression.And(Expression.Eq("Status", StatusType.Pending),
                                                       Expression.Ge("Deadline", DateTime.Now));
                        criterion = Expression.And(criterion, Expression.Eq("Deleted", false));

                        Session["pendingall"] = Module.CountBookingByCriterion(criterion);

                        criterion = Expression.And(criterion,
                                           Expression.Or(Expression.Eq("CreatedBy", UserIdentity),
                                                         Expression.Eq("agency.Sale", UserIdentity)));

                        // Update
                        Session["pending" + UserIdentity.Id] = Module.CountBookingByCriterion(criterion);

                        // Deadline today
                        criterion = Expression.And(criterion, Expression.Ge("Deadline", DateTime.Now));
                        criterion = Expression.And(criterion, Expression.Le("Deadline", DateTime.Now.AddHours(36)));

                        Session["todaypending" + UserIdentity.Id] = Module.CountBookingByCriterion(criterion);

                        Session["lastupdate" + UserIdentity.Id] = DateTime.Now;
                    }

                    var litMyPendings = master.FindControl("litMyPendings") as Literal;
                    if (litMyPendings != null)
                    {
                        litMyPendings.Text = Session["pending" + UserIdentity.Id].ToString();
                    }

                    var litTodayPending = master.FindControl("litTodayPending") as Literal;
                    if (litTodayPending != null)
                    {
                        litTodayPending.Text = Session["todaypending" + UserIdentity.Id].ToString();
                    }

                    if (UserIdentity.HasPermission(AccessLevel.Administrator))
                    {
                        var litAdministrator = master.FindControl("litAdministrator") as Literal;
                        if (litAdministrator != null)
                        {
                            litAdministrator.Text = string.Format("System total: {0}<br/>", Session["pendingall"]);
                        }
                    }
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            // Check quyền
            if (Page.Master is SailsMaster && Module != null)
            {
                SailsMaster master = (SailsMaster)Page.Master;
                master.CheckPermisson(Module, UserIdentity);
                master.BindDataToRptBirdthday(Module);
            }
        }

        public void ShowMessage(string message)
        {
            ShowSuccess(message);
            HtmlGenericControl divMessage = (HtmlGenericControl)Master.FindControl("divMessage");
            if (divMessage != null)
            {
                divMessage.Visible = true;
                Label labelMessage = Master.FindControl("labelMessage") as Label;
                if (labelMessage != null)
                {
                    labelMessage.Text = message;
                    if (divMessage.Attributes["class"] != null)
                    {
                        divMessage.Attributes["class"] = "module_message";
                    }
                    else
                    {
                        divMessage.Attributes.Add("class", "module_message");
                    }
                }
            }
        }

        public void ShowError(string message)
        {
            ShowErrors(message);
            HtmlGenericControl divMessage = (HtmlGenericControl)Master.FindControl("divMessage");
            if (divMessage != null)
            {
                divMessage.Visible = true;
                Label labelMessage = Master.FindControl("labelMessage") as Label;
                if (labelMessage != null)
                {
                    labelMessage.Text = message;
                    if (divMessage.Attributes["class"] != null)
                    {
                        divMessage.Attributes["class"] = "module_error";
                    }
                    else
                    {
                        divMessage.Attributes.Add("class", "module_error");
                    }
                }
            }
        }
        public void ShowWarning(string warning)
        {
            Session["WarningMessage"] = "<strong>Warning!</strong> " + warning + "<br/>" + Session["WarningMessage"];
        }

        public void ShowErrors(string error)
        {
            Session["ErrorMessage"] = "<strong>Error!</strong> " + error + "<br/>" + Session["ErrorMessage"];
        }

        public void ShowSuccess(string success)
        {
            Session["SuccessMessage"] = "<strong>Success!</strong> " + success + "<br/>" + Session["SuccessMessage"];
        }
        public void HideError()
        {
            if (Master == null)
            {
                return;
            }
            HtmlGenericControl divMessage = Master.FindControl("divMessage") as HtmlGenericControl;
            if (divMessage != null)
            {
                divMessage.Visible = false;
            }
        }

        private IReportEngine _reportEngine;
        public IReportEngine ReportEngine
        {
            get
            {
                if (_reportEngine == null)
                {
                    string engine;
                    if (string.IsNullOrEmpty(Config.GetConfiguration()["ReportEngine"]))
                    {
                        engine = "orientalsails";
                    }
                    else
                    {
                        engine = Config.GetConfiguration()["ReportEngine"];
                    }

                    switch (engine.ToLower())
                    {
                        case "emotion":
                            _reportEngine = new Emotion();
                            break;
                        case "orientalsails":
                            _reportEngine = new ReportEngine.OrientalSails();
                            break;
                        case "indochinajunk":
                            _reportEngine = new IndochinaJunk();
                            break;
                    }
                }
                return _reportEngine;
            }
        }

        private bool _tripBased;
        public bool TripBased
        {
            get
            {
                if (string.IsNullOrEmpty(Config.GetConfiguration()["BookingMode"]))
                {
                    return true;
                }
                return Config.GetConfiguration()["BookingMode"] == "TripBased";
            }
        }
    }
}