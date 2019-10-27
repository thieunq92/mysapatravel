using System;
using CMS.ServerControls.FileUpload;
using CMS.Web.Util;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SailsTripEdit : SailsAdminBase
    {
        #region -- Private Member --

        private readonly ILog _logger = LogManager.GetLogger(typeof (SailsTripEdit));
        private SailsTrip _trip;

        private int TripId
        {
            get
            {
                int id;
                if (Request.QueryString["TripId"] != null && Int32.TryParse(Request.QueryString["TripId"], out id))
                {
                    return id;
                }
                return -1;
            }
        }

        #endregion

        #region -- Page Event --

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Title = Resources.titleSailsTripEdit;

                #region -- Ajax Image --

                #endregion

                if (!IsPostBack)
                {
                    LoadInfo();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in SailsTripEdit", ex);
                ShowError(ex.Message);
            }
        }

        #endregion

        #region -- Private Method --

        private void LoadInfo()
        {
            if (TripId > 0)
            {
                _trip = Module.TripGetById(TripId);
                textBoxName.Text = _trip.Name;
                textBoxNumberOfDay.Text = _trip.NumberOfDay.ToString();
                txtDescription.Text = _trip.Description;
                txtTripCode.Text = _trip.TripCode;
                ddlHalfDay.SelectedValue = _trip.HalfDay.ToString();
                ddlNumberOfOptions.SelectedValue = _trip.NumberOfOptions.ToString();
            }
        }

        #endregion

        #region -- Control Event --

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    if (TripId > 0)
                    {
                        _trip = Module.TripGetById(TripId);
                    }
                    else
                    {
                        _trip = new SailsTrip();
                        _trip.CreatedBy = UserIdentity;
                    }
                    _trip.ModifiedBy = UserIdentity;
                    _trip.Name = textBoxName.Text;
                    int numOdays;
                    if (!string.IsNullOrEmpty(textBoxNumberOfDay.Text) &&
                        Int32.TryParse(textBoxNumberOfDay.Text, out numOdays))
                    {
                        _trip.NumberOfDay = numOdays;
                    }
                    else
                    {
                        _trip.NumberOfDay = 0;
                    }
                    _trip.NumberOfOptions = Convert.ToInt32(ddlNumberOfOptions.SelectedValue);
                    _trip.Description = txtDescription.Text;
                    _trip.TripCode = txtTripCode.Text;
                    _trip.HalfDay = Convert.ToInt32(ddlHalfDay.SelectedValue);
                    if (TripId > 0)
                    {
                        Module.Update(_trip);
                    }
                    else
                    {
                        Module.Save(_trip);
                    }
                    PageRedirect(string.Format("SailsTripList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when buttonSave_Click in SailsTripEdit", ex);
                ShowError(ex.Message);
            }
        }

        protected void buttonCancel_Clicl(object sender, EventArgs e)
        {
            PageRedirect(string.Format("SailsTripList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
        }

        #endregion
    }
}