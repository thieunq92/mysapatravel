using System;
using System.Text;
using System.Web.Services.Discovery;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Web.Util;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Aspose.Words;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using Portal.Modules.OrientalSails.BusinessLogic;
using Aspose.Words.Rendering;
using Microsoft.Office.Interop.Word;
using System.Linq;
using Portal.Modules.OrientalSails.Enums.Shared;
using C = Portal.Modules.OrientalSails.Enums.Contract;
using Q = Portal.Modules.OrientalSails.Enums.Quotation;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class AgencyView : SailsAdminBasePage
    {
        private bool _editPermission;
        private bool _viewBookingPermission;
        private bool _contactsPermission;
        private bool _recentActivitiesPermission;
        private bool _contractsPermission;
        private AgencyViewBLL agencyViewBLL;
        private PermissionBLL permissionBLL;
        private UserBLL userBLL;
        private User currentUser;
        public AgencyViewBLL AgencyViewBLL
        {
            get
            {
                if (agencyViewBLL == null)
                    agencyViewBLL = new AgencyViewBLL();
                return agencyViewBLL;
            }
        }
        public Agency Agency
        {
            get
            {
                Agency agency = null;
                try
                {
                    if (Request.QueryString["AgencyId"] != null)
                        agency = AgencyViewBLL.AgencyGetById(Convert.ToInt32(Request.QueryString["AgencyId"]));
                }
                catch (Exception) { }
                return agency;
            }
        }
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                {
                    userBLL = new UserBLL();
                }
                return userBLL;
            }
        }
        public User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    currentUser = UserBLL.UserGetCurrent();
                }
                return currentUser;
            }
        }
        public PermissionBLL PermissionBLL
        {
            get
            {
                if (permissionBLL == null)
                {
                    permissionBLL = new PermissionBLL();
                }
                return permissionBLL;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _editPermission = Module.PermissionCheck(Permission.ACTION_EDITAGENCY, UserIdentity);
            _viewBookingPermission = Module.PermissionCheck("VIEWBOOKINGBYAGENCY", UserIdentity);
            _contactsPermission = Module.PermissionCheck("CONTACTS", UserIdentity);
            _recentActivitiesPermission = Module.PermissionCheck("RECENTACTIVITIES", UserIdentity);
            _contractsPermission = Module.PermissionCheck("CONTRACTS", UserIdentity);

            if (!IsPostBack)
            {
                if (Request.QueryString["agencyid"] != null)
                {
                    var agency = Module.AgencyGetById(Convert.ToInt32(Request.QueryString["agencyid"]));

                    if (agency.Sale == UserIdentity)
                    {
                        _editPermission = true;
                    }
                    litName1.Text = agency.Name;
                    litName.Text = agency.Name;
                    litTradingName.Text = agency.TradingName;
                    litRepresentative.Text = agency.Representative;
                    litRepresentativePosition.Text = agency.RepresentativePosition;
                    litContact.Text = agency.Contact;
                    litContactAddress.Text = agency.ContactAddress;
                    litContactEmail.Text = agency.ContactEmail;
                    litContactPosition.Text = agency.ContactPosition;
                    if (agency.QAgentlevel != null) litAgentLevel.Text = agency.QAgentlevel.Name;
                    litWebsite.Text = !String.IsNullOrEmpty(agency.Website) ? "<a href = " + agency.Website + ">" + agency.Website + "</a>" : "";
                    if (agency.Role != null)
                        litRole.Text = agency.Role.Name;
                    else
                    {
                        litRole.Text = "Customize Role";
                    }
                    if (agency.Sale != null)
                    {
                        litSale.Text = agency.Sale.AllName;
                    }
                    else
                        litSale.Text = @"Unbound";
                    litTax.Text = agency.TaxCode;
                    if (agency.Location != null)
                        litLocation.Text = agency.Location.Name;
                    litAddress.Text = agency.Address;

                    if (!string.IsNullOrEmpty(agency.Email))
                    {
                        hplEmail.NavigateUrl = string.Format("mailto:{0}", agency.Email);
                        hplEmail.Text = agency.Email;
                    }
                    litPhone.Text = agency.Phone;

                    litPayment.Text = agency.PaymentPeriod.ToString();

                    litNote.Text = agency.Description;

                    var agencyId = Agency.Id;
                    rptContracts.DataSource = AgencyViewBLL.AgencyContractGetAllByAgency(agencyId);
                    rptContracts.DataBind();

                    rptContacts.DataSource = Module.ContactGetByAgency(agency, !_editPermission); // nếu không có quyền edit thì ko có quyền view
                    rptContacts.DataBind();

                    hplEditAgency.Visible = _editPermission;
                    hplEditAgency.NavigateUrl = string.Format("AgencyEdit.aspx?NodeId={0}&SectionId={1}&agencyid={2}", Node.Id,
                                                        Section.Id, agency.Id);

                    hplAddContact.Visible = _editPermission;
                    hplAddContact.NavigateUrl = "javascript:";
                    string url = string.Format("AgencyContactEdit.aspx?NodeId={0}&SectionId={1}&agencyid={2}",
                                                    Node.Id, Section.Id, agency.Id);
                    hplAddContact.Attributes.Add("onclick", CMS.ServerControls.Popup.OpenPopupScript(url, "Contact", 300, 400));

                    url = string.Format("AgencyContractEdit.aspx?NodeId={0}&SectionId={1}&agencyid={2}",
                                                    Node.Id, Section.Id, agency.Id);

                    hplBookingList.NavigateUrl = string.Format(
                        "BookingList.aspx?NodeId={0}&SectionId={1}&ai={2}", Node.Id, Section.Id, agency.Id);
                    hplReceivable.NavigateUrl =
                        string.Format("PaymentReport.aspx?NodeId={0}&SectionId={1}&ai={2}&from={3}&to={4}",
                                      Node.Id, Section.Id, agency.Id, DateTime.Today.AddMonths(-3).ToOADate(), DateTime.Today.ToOADate());

                    rptActivities.DataSource = Module.GetObject<Activity>(Expression.And(Expression.Eq("ObjectType", "MEETING"), Expression.Eq("Params", Convert.ToString(agency.Id))), 0, 0,
                                                                          Order.Desc("DateMeeting"));
                    rptActivities.DataBind();

                }
            }
            LoadContracts();
            RenderViewBookingByThisAgency();
            RenderContacts();
            RenderRecentActivities();
            RenderContracts();
            ddlContractTemplate.DataSource = AgencyViewBLL.ContractGetAll();
            ddlContractTemplate.DataTextField = "Name";
            ddlContractTemplate.DataValueField = "Id";
            ddlContractTemplate.DataBind();
            //ddlQuotationTemplate.DataSource = AgencyViewBLL.QuotationGetAll();
            //ddlQuotationTemplate.DataTextField = "Name";
            //ddlQuotationTemplate.DataValueField = "Id";
            //ddlQuotationTemplate.DataBind();
            ddlCruise.DataSource = AgencyViewBLL.CruiseGetAll();
            ddlCruise.DataTextField = "Name";
            ddlCruise.DataValueField = "Id";
            ddlCruise.DataBind();
            var agencyNotes = AgencyViewBLL.AgencyNotesGetAllByAgency(Agency);
            rptAgencyNotes.DataSource = agencyNotes.OrderBy(an => an.Role.Id);
            rptAgencyNotes.DataBind();
            var roles = AgencyViewBLL.RolesGetAll();
            ddlAgencyNotesRole.DataSource = roles.Where(r => r.IsUsedInAgencyNotes == true);
            ddlAgencyNotesRole.DataTextField = "Name";
            ddlAgencyNotesRole.DataValueField = "Id";
            ddlAgencyNotesRole.DataBind();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (agencyViewBLL != null)
            {
                agencyViewBLL.Dispose();
                agencyViewBLL = null;
            }
            if (userBLL != null)
            {
                userBLL.Dispose();
                userBLL = null;
            }
            if (permissionBLL != null)
            {
                permissionBLL.Dispose();
                permissionBLL = null;
            }
        }
        public void RenderViewBookingByThisAgency()
        {
            if (!_viewBookingPermission)
            {
                hplBookingList.CssClass = hplBookingList.CssClass + " " + "disable";
                hplBookingList.Attributes["href"] = "javascript:";
                var script = @"<script type = 'text/javascript'>";
                script = script +
                         @"$('#" + hplBookingList.ClientID + "').click(function(){$('#disableInform').dialog({resiable:false,modal:true,draggable:false})})";
                script = script + "</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "disableInform", script);
            }
        }
        public void RenderContacts()
        {
            if (!_contactsPermission)
            {
                plhContacts.Visible = false;
                lblContacts.Visible = true;
            }
        }
        public void RenderRecentActivities()
        {
            if (!_recentActivitiesPermission)
            {
                plhActivities.Visible = false;
                lblActivities.Visible = true;
            }
        }
        public void RenderContracts()
        {
            if (!_contractsPermission)
            {
                plhContracts.Visible = false;
                lblContracts.Visible = true;
            }
        }
        public void LoadContracts()
        {
            var agencyId = Agency.Id;
            rptContracts.DataSource = AgencyViewBLL.AgencyContractGetAllByAgency(agencyId);
            rptContracts.DataBind();
        }
        protected void rptContracts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is AgencyContract)
            {
                var contract = (AgencyContract)e.Item.DataItem;
                var hplDownload = (HyperLink)e.Item.FindControl("hplDownload");
                hplDownload.NavigateUrl = contract.FilePath;
                hplDownload.Text = contract.FileName;
            }
        }
        public void lbtQuotationTemplate_Click(object sender, EventArgs e)
        {
            var agencyContractId = -1;
            try
            {
                agencyContractId = Int32.Parse(((LinkButton)sender).CommandArgument);
            }
            catch { }
            var agencyContract = AgencyViewBLL.AgencyContractGetById(agencyContractId);
            ExportQuotationToWord(agencyContract);
        }
        public void lbtContractTemplate_Click(object sender, EventArgs e)
        {
            var agencyContractId = -1;
            try
            {
                agencyContractId = Int32.Parse(((LinkButton)sender).CommandArgument);
            }
            catch { }
            var agencyContract = AgencyViewBLL.AgencyContractGetById(agencyContractId);
            ExportContractToWord(agencyContract);
        }
        protected void rptContacts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is AgencyContact)
            {
                var contact = (AgencyContact)e.Item.DataItem;

                if (!contact.Enabled)
                {
                    e.Item.Visible = false;
                    return;
                }

                var ltrName = (Literal)e.Item.FindControl("ltrName");
                ltrName.Text = contact.Name;

                var hplName = (HyperLink)e.Item.FindControl("hplName");
                hplName.NavigateUrl = "javascript:";

                if (_editPermission)
                {
                    string url = string.Format("AgencyContactEdit.aspx?NodeId={0}&SectionId={1}&contactid={2}",
                                               Node.Id, Section.Id, contact.Id);
                    hplName.Attributes.Add("onclick", CMS.ServerControls.Popup.OpenPopupScript(url, "Contact", 300, 400));
                }

                var linkEmail = (HyperLink)e.Item.FindControl("hplEmail");
                linkEmail.Text = contact.Email;
                linkEmail.NavigateUrl = string.Format("mailto:{0}", contact.Email);

                ValueBinder.BindLiteral(e.Item, "litPosition", contact.Position);
                ValueBinder.BindLiteral(e.Item, "litPhone", contact.Phone);

                if (contact.IsBooker)
                {
                    ValueBinder.BindLiteral(e.Item, "litBooker", "Booker");
                }

                var lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
                lbtDelete.Visible = _editPermission;
                lbtDelete.CommandArgument = contact.Id.ToString();
            }
        }
        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            var btn = (IButtonControl)sender;
            var contact = Module.ContactGetById(Convert.ToInt32(btn.CommandArgument));

            contact.Enabled = false;
            Module.SaveOrUpdate(contact);

            PageRedirect(Request.RawUrl);
        }
        protected void rptActivities_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var activity = e.Item.DataItem as Activity;
            var name = e.Item.FindControl("ltrName") as Literal;
            var position = e.Item.FindControl("ltrPosition") as Literal;
            var dateMeeting = e.Item.FindControl("ltrDateMeeting") as Literal;
            if (activity != null)
            {
                if (dateMeeting != null) dateMeeting.Text = activity.DateMeeting.ToString("dd/MM/yyyy");
                if (name != null) name.Text = Module.GetObject<AgencyContact>(activity.ObjectId) != null ? Module.GetObject<AgencyContact>(activity.ObjectId).Name : "";
                if (position != null) position.Text = Module.GetObject<AgencyContact>(activity.ObjectId) != null ? Module.GetObject<AgencyContact>(activity.ObjectId).Position : "";

                var note = e.Item.FindControl("ltrNote") as Literal;
                var strBuilder = new StringBuilder();
                string[] noteWord = activity.Note.Split(new char[] { ' ' });
                bool isLessWords = false;
                for (int i = 0; i <= 50; i++)
                {
                    try
                    {
                        strBuilder.Append(noteWord[i] + " ");
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        isLessWords = true;
                        break;
                    }
                }
                if (!isLessWords) strBuilder.Append("...");
                if (note != null) note.Text = strBuilder.ToString();
                var ltrSale = (Literal)e.Item.FindControl("ltrSale");
                ltrSale.Text = activity.User.FullName;
            }
        }
        protected void lbtDeleteActivity_Click(object sender, EventArgs e)
        {
            var btn = (IButtonControl)sender;
            Activity activity = Module.GetObject<Activity>(Convert.ToInt32(btn.CommandArgument));
            Module.Delete(activity);
            PageRedirect(Request.RawUrl);
        }
        public void ExportContractToWord(AgencyContract agencyContract)
        {
            var doc = GetGeneratedContract(agencyContract);
            var m = new MemoryStream();
            doc.Save(m, SaveFormat.Doc);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/msword";
            Response.AppendHeader("content-disposition",
                                  "attachment; filename=" + string.Format("{0}.doc", agencyContract.ContractTemplateName));
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            m.Close();
            Response.End();
        }
        public void ExportQuotationToWord(AgencyContract agencyContract)
        {
            var doc = GetGeneratedQuotation(agencyContract);
            var m = new MemoryStream();
            doc.Save(m, SaveFormat.Doc);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/msword";
            Response.AppendHeader("content-disposition",
                                  "attachment; filename=" + string.Format("{0}.doc", agencyContract.QuotationTemplateName));
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            m.Close();
            Response.End();
        }
        public Aspose.Words.Document GetGeneratedContract(AgencyContract agencyContract)
        {
            var templatePath = agencyContract.ContractTemplatePath;
            DateTime? validFromDate = null;

            try
            {
                validFromDate = DateTime.ParseExact(txtContractValidFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception) { }

            var validFromDay = validFromDate != null ? validFromDate.Value.Day.ToString("#00") : "";
            var validFrommonth = validFromDate != null ? (validFromDate.Value.Month + 1).ToString("#00") : "";
            var validFromYear = validFromDate != null ? validFromDate.Value.Year.ToString() : "";

            var doc = new Aspose.Words.Document(Server.MapPath(templatePath));
            var agencyName = "";
            var agencyTradingName = "";
            var agencyRepresentative = "";
            var agencyRepresentativePosition = "";
            var agencyContact = "";
            var agencyContactEmail = "";
            var agencyContactAddress = "";
            var agencyContactPosition = "";
            var agencyAddress = "";
            var agencyPhone = "";
            var agencyFax = "";
            var agencyTaxCode = "";
            var agencyWebsite = "";

            try
            {
                agencyName = !String.IsNullOrEmpty(Agency.Name) ? Agency.Name : "";
            }
            catch { }

            try
            {
                agencyTradingName = !String.IsNullOrEmpty(Agency.TradingName) ? Agency.TradingName : "";
            }
            catch { }

            try
            {
                agencyRepresentative = !String.IsNullOrEmpty(Agency.Representative) ? Agency.Representative : "";
            }
            catch { }

            try
            {
                agencyRepresentativePosition = !String.IsNullOrEmpty(Agency.RepresentativePosition) ? Agency.RepresentativePosition : "";
            }
            catch { }

            try
            {
                agencyContact = !String.IsNullOrEmpty(Agency.Contact) ? Agency.Contact : "";
            }
            catch { }

            try
            {
                agencyContactAddress = !String.IsNullOrEmpty(Agency.ContactAddress) ? Agency.ContactAddress : "";
            }
            catch { }

            try
            {
                agencyContactPosition = !String.IsNullOrEmpty(Agency.ContactPosition) ? Agency.ContactPosition : "";
            }
            catch { }

            try
            {
                agencyContactEmail = !String.IsNullOrEmpty(Agency.ContactEmail) ? Agency.ContactEmail : "";
            }
            catch { }

            try
            {
                agencyAddress = !String.IsNullOrEmpty(Agency.Address) ? Agency.Address : "";
            }
            catch { }

            try
            {
                agencyPhone = !String.IsNullOrEmpty(Agency.Phone) ? Agency.Phone : "";
            }
            catch { }

            try
            {
                agencyFax = !String.IsNullOrEmpty(Agency.Fax) ? Agency.Fax : "";
            }
            catch { }

            try
            {
                agencyTaxCode = !String.IsNullOrEmpty(Agency.TaxCode) ? Agency.TaxCode : "";
            }
            catch { }

            DateTime? validToDate = null;
            try
            {
                validToDate = DateTime.ParseExact(txtContractValidToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }

            var textValidToDate = "";
            try
            {
                textValidToDate = validToDate.Value.ToString("dd/MM/yyyy");
            }
            catch { }

            try
            {
                agencyWebsite = !String.IsNullOrEmpty(Agency.Website) ? Agency.Website : "";
            }
            catch { }

            doc.Range.Replace(new Regex("(\\[ValidFromDay\\])"), validFromDay);
            doc.Range.Replace(new Regex("(\\[ValidFromMonth\\])"), validFrommonth);
            doc.Range.Replace(new Regex("(\\[ValidFromYear\\])"), validFromYear);
            doc.Range.Replace(new Regex("(\\[AgencyName\\])"), agencyName);
            doc.Range.Replace(new Regex("(\\[TradingName\\])"), agencyTradingName);
            doc.Range.Replace(new Regex("(\\[Representative\\])"), agencyRepresentative);
            doc.Range.Replace(new Regex("(\\[RepresentativePosition\\])"), agencyRepresentativePosition);
            doc.Range.Replace(new Regex("(\\[Contact\\])"), agencyContact);
            doc.Range.Replace(new Regex("(\\[ContactPosition\\])"), agencyContactPosition);
            doc.Range.Replace(new Regex("(\\[ContactAddress\\])"), agencyContactAddress);
            doc.Range.Replace(new Regex("(\\[ContactEmail\\])"), agencyContactEmail);
            doc.Range.Replace(new Regex("(\\[AgencyWebsite\\])"), agencyWebsite);
            doc.Range.Replace(new Regex("(\\[AgencyAddress\\])"), agencyAddress);
            doc.Range.Replace(new Regex("(\\[AgencyPhone\\])"), agencyPhone);
            doc.Range.Replace(new Regex("(\\[AgencyFax\\])"), agencyFax);
            doc.Range.Replace(new Regex("(\\[AgencyTaxCode\\])"), agencyTaxCode);
            doc.Range.Replace(new Regex("(\\[ValidToDate\\])"), textValidToDate);
            return doc;
        }
        public Aspose.Words.Document GetGeneratedQuotation(AgencyContract agencyContract)
        {
            Quotation quotation = null;
            if (agencyContract != null)
            {
                //quotation = agencyContract.Quotation;
            }
            else
            {
                var selectedQuotation = -1;
                try
                {
                    //selectedQuotation = Int32.Parse(ddlQuotationTemplate.SelectedValue);
                }
                catch { }
                quotation = AgencyViewBLL.QuotationGetById(selectedQuotation);
            }

            var doc = new Aspose.Words.Document(Server.MapPath("ExportTemplates/Quotation.doc"));

            var textValidFromDate = "";
            try
            {
                textValidFromDate = quotation.ValidFrom.Value.ToString("dd/MM/yyyy");
            }
            catch { }
            var textValidToDate = "";
            try
            {
                textValidToDate = quotation.ValidTo.Value.ToString("dd/MM/yyyy");
            }
            catch { }

            doc.Range.Replace(new Regex("(\\[ValidFromDate\\])"), textValidFromDate);
            doc.Range.Replace(new Regex("(\\[ValidToDate\\])"), textValidToDate);
            return doc;
        }
        protected void btnIssueQuotation_Click(object sender, EventArgs e)
        {
            var agencyContract = new AgencyContract();
            agencyContract.Agency = Agency;
            DateTime? quotationValidFromDate = null;
            try
            {
                //quotationValidFromDate = DateTime.ParseExact(txtQuotationValidFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            agencyContract.QuotationValidFromDate = quotationValidFromDate;
            DateTime? quotationValidToDate = null;
            try
            {
                //quotationValidToDate = DateTime.ParseExact(txtQuotationValidToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            agencyContract.QuotationValidToDate = quotationValidToDate;
            agencyContract.QuotationTemplateName = "Quotation_" + Agency.Name + "_" + quotationValidFromDate.Value.ToString("ddMMyyyy") + "_" + quotationValidToDate.Value.ToString("ddMMyyyy");
            agencyContract.QuotationTemplatePath = "/Modules/Sails/Admin/ExportTemplates/Quotation Lv1.doc";
            AgencyViewBLL.AgencyContractSaveOrUpdate(agencyContract);
            Response.Redirect(Request.RawUrl);
        }
        protected void btnIssueContract_Click(object sender, EventArgs e)
        {
            var agencyContract = new AgencyContract();
            agencyContract.Agency = Agency;
            DateTime? contractValidFromDate = null;
            try
            {
                contractValidFromDate = DateTime.ParseExact(txtContractValidFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            agencyContract.ContractValidFromDate = contractValidFromDate;
            DateTime? contractValidToDate = null;
            try
            {
                contractValidToDate = DateTime.ParseExact(txtContractValidToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            agencyContract.ContractValidToDate = contractValidToDate;
            agencyContract.ContractTemplateName = "Contract_" + Agency.Name + "_" + contractValidFromDate.Value.ToString("ddMMyyyy") + "_" + contractValidToDate.Value.ToString("ddMMyyyy");
            agencyContract.ContractTemplatePath = "/Modules/Sails/Admin/ExportTemplates/Contract Lv1.doc";
            AgencyViewBLL.AgencyContractSaveOrUpdate(agencyContract);
            Response.Redirect(Request.RawUrl);
        }
        protected void rptContracts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var agencyContractId = int.Parse(e.CommandArgument.ToString());
            var agencyContract = AgencyViewBLL.AgencyContractGetById(agencyContractId);
            if (e.CommandName == "DownloadContract")
            {
                ExportContractToWord(agencyContract);
            }
            if (e.CommandName == "DownloadQuotation")
            {
                ExportQuotationToWord(agencyContract);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var uploadPath = "";
            var contentType = "";
            if (fuAttachment.HasFile)
            {
                var fileName = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString() + Guid.NewGuid().ToString();
                var fileExtension = new FileInfo(fuAttachment.FileName).Extension;
                var fullFileName = fileName + fileExtension;
                uploadPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Upload/"), fullFileName);
                fuAttachment.SaveAs(uploadPath);
                contentType = fuAttachment.PostedFile.ContentType;
            }
            var agencyContactId = 0;
            try
            {
                agencyContactId = Int32.Parse(hidAgencyContactId.Value);
            }
            catch { }
            var agencyContact = AgencyViewBLL.AgencyContactGetById(agencyContactId);
            var dateMeeting = new DateTime();
            try
            {
                dateMeeting = DateTime.ParseExact(txtDateMeeting.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var problems = "";
            Cruise cruise = null;
            if (ddlType.SelectedValue == "Problem Report")
            {
                if (chkBus.Checked) problems += "Bus,";
                if (chkCabin.Checked) problems += "Cabin,";
                if (chkGuide.Checked) problems += "Guide,";
                if (chkFood.Checked) problems += "Food,";
                if (chkOthers.Checked) problems += "Others,";
                problems = problems.TrimEnd(new char[] { ',' });
                var cruiseId = 0;
                try
                {
                    cruiseId = Int32.Parse(ddlCruise.SelectedValue);
                }
                catch { }
                cruise = AgencyViewBLL.CruiseGetById(cruiseId);
            }

            var activityId = 0;
            try
            {
                activityId = Int32.Parse(hidActivityId.Value);
            }
            catch { }
            var activity = AgencyViewBLL.ActivityGetById(activityId);
            if (activity == null || activity.Id == 0)
            {
                activity = new Activity();
                activity.Params = agencyContact != null && agencyContact.Agency != null ? agencyContact.Agency.Id.ToString() : 0.ToString();
                activity.Url = "AgencyView.aspx?NodeId=1&SectionId=15&agencyid=" + agencyContact != null && agencyContact.Agency != null ? agencyContact.Agency.Id.ToString() : 0.ToString();
                activity.ObjectId = agencyContact != null ? agencyContact.Id : 0;
            }
            activity.UpdateTime = DateTime.Now;
            activity.Time = DateTime.Now;
            activity.DateMeeting = dateMeeting;
            activity.Note = txtNote.Text;
            activity.ObjectType = "MEETING";
            activity.User = CurrentUser;
            activity.Level = ImportantLevel.Important;
            activity.Type = ddlType.SelectedValue;
            activity.NeedManagerAttention = chkNeedManagerAttention.Checked;
            activity.Attachment = uploadPath == "" ? activity.Attachment : uploadPath;
            activity.AttachmentContentType = contentType == "" ? activity.AttachmentContentType : contentType;
            activity.Problems = problems;
            activity.Cruise = cruise;
            AgencyViewBLL.ActivitySaveOrUpdate(activity);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnAgencyNotesSave_Click(object sender, EventArgs e)
        {
            var roleId = 0;
            try
            {
                roleId = Int32.Parse(ddlAgencyNotesRole.SelectedValue);
            }
            catch { }
            var role = AgencyViewBLL.RoleGetById(roleId);
            var agencyNotesId = 0;
            try
            {
                agencyNotesId = Int32.Parse(hidAgencyNotesId.Value);
            }
            catch { }
            AgencyNotes agencyNotes = null;
            agencyNotes = AgencyViewBLL.AgencyNotesGetById(agencyNotesId);
            if (agencyNotes == null || agencyNotes.Id == 0) agencyNotes = new AgencyNotes();
            agencyNotes.Agency = Agency;
            agencyNotes.Role = role;
            agencyNotes.Note = txtAgencyNotesNote.Text;
            AgencyViewBLL.AgencyNotesSaveOrUpdate(agencyNotes);
            Response.Redirect(Request.RawUrl);
        }

        protected void rptAgencyNotes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var agencyNotesId = 0;
            if (e.CommandName == "Delete")
            {
                try
                {
                    agencyNotesId = Convert.ToInt32(e.CommandArgument);
                }
                catch { }
                var agencyNotes = AgencyViewBLL.AgencyNotesGetById(agencyNotesId);
                AgencyViewBLL.DeleteAgencyNotes(agencyNotes);
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}