using System;
using System.Web.UI.WebControls;
using log4net;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class RoomEdit : SailsAdminBase
    {
        #region -- Private Member --

        private readonly ILog _logger = LogManager.GetLogger(typeof(RoomEdit));
        private Room _room;

        private int RoomId
        {
            get
            {
                int id;
                if (Request.QueryString["RoomID"] != null && Int32.TryParse(Request.QueryString["RoomID"], out id))
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
                Title = Resources.titleRoomEdit;
                if (!IsPostBack)
                {
                    ddlRoomTypex.DataSource = Module.RoomTypexGetAll();
                    ddlRoomTypex.DataValueField = "Id";
                    ddlRoomTypex.DataTextField = "Name";
                    ddlRoomTypex.DataBind();

                    ddlRoomClass.DataSource = Module.RoomClassGetAll();
                    ddlRoomClass.DataValueField = "Id";
                    ddlRoomClass.DataTextField = "Name";
                    ddlRoomClass.DataBind();

                    ddlCruises.DataSource = Module.CruiseGetAll();
                    ddlCruises.DataTextField = "Name";
                    ddlCruises.DataValueField = "Id";
                    ddlCruises.DataBind();

                    LoadInfo();
                    ddlCruises_OnSelectedIndexChanged(null, null);

                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when Page_Load in RoomEdit", ex);
                ShowError(ex.Message);
            }
        }

        #endregion

        #region -- Private Method --

        public void LoadInfo()
        {
            if (RoomId > 0)
            {
                _room = Module.RoomGetById(RoomId);
                textBoxName.Text = _room.Name;
                ddlRoomTypex.SelectedValue = _room.RoomType.Id.ToString();
                ddlRoomClass.SelectedValue = _room.RoomClass.Id.ToString();
                ddlRoomClass.SelectedValue = _room.RoomClass.Id.ToString();
                if (_room.Floor > 0) ddlFloor.SelectedValue = _room.Floor.ToString();
                try
                {
                    if (_room.Cruise != null)
                    {
                        ddlCruises.SelectedValue = _room.Cruise.Id.ToString();
                    }
                }
                catch
                {
                    _room.Cruise = null;
                }
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
                    if (RoomId > 0)
                    {
                        _room = Module.RoomGetById(RoomId);
                    }
                    else
                    {
                        _room = new Room();
                        _room.Order = Module.RoomCount();
                    }
                    _room.Name = textBoxName.Text;
                    _room.RoomType = Module.RoomTypexGetById(Convert.ToInt32(ddlRoomTypex.SelectedValue));
                    _room.RoomClass = Module.RoomClassGetById(Convert.ToInt32(ddlRoomClass.SelectedValue));
                    if (!string.IsNullOrWhiteSpace(ddlFloor.SelectedValue)) _room.Floor = Convert.ToInt32(ddlFloor.SelectedValue);

                    if (ddlCruises.Items.Count > 0)
                    {
                        _room.Cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                    }
                    if (RoomId > 0)
                    {
                        Module.Update(_room);
                    }
                    else
                    {
                        Module.Save(_room);
                    }
                    PageRedirect(string.Format("RoomList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error when buttonSave_Click in RoomEdit", ex);
                ShowError(ex.Message);
            }
        }

        protected void buttonCancel_Clicl(object sender, EventArgs e)
        {
            PageRedirect(string.Format("RoomList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
        }

        #endregion

        protected void ddlCruises_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ddlCruises.SelectedValue))
            {
                var cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                if (cruise != null && cruise.NumberOfFloors > 0)
                {
                    for (int i = 1; i <= cruise.NumberOfFloors; i++)
                    {
                        ddlFloor.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                }
            }
            ddlFloor.Items.Insert(0, new ListItem("-- select floor --", ""));
        }
    }
}