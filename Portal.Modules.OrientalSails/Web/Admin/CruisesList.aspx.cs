using System;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class CruisesList : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDataSource();
            }
        }

        private void GetDataSource()
        {
            rptCruises.DataSource = Module.CruiseGetAll();
            rptCruises.DataBind();
        }

        protected void rptCruises_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                Cruise cruise = (Cruise)e.Item.DataItem;
                HyperLink hplCruise = e.Item.FindControl("hplCruise") as HyperLink;
                if (hplCruise != null)
                {
                    hplCruise.Text = cruise.Name;
                    hplCruise.NavigateUrl = string.Format("CruisesEdit.aspx?NodeId={0}&SectionId={1}&cruiseid={2}",
                                                          Node.Id, Section.Id, cruise.Id);
                }

                HyperLink hplRoomClasses = e.Item.FindControl("hplRoomClasses") as HyperLink;
                if (hplRoomClasses != null)
                {
                    hplRoomClasses.NavigateUrl =
                        string.Format("RoomClassEdit.aspx?NodeId={0}&SectionId={1}&cruiseid={2}", Node.Id, Section.Id,
                                      cruise.Id);
                }

                HyperLink hplRooms = e.Item.FindControl("hplRooms") as HyperLink;
                if (hplRooms != null)
                {
                    hplRooms.NavigateUrl = string.Format("RoomList.aspx?NodeId={0}&SectionId={1}&cruiseid={2}", Node.Id,
                                                         Section.Id, cruise.Id);
                }
                var ddlGroup = e.Item.FindControl("ddlGroup") as DropDownList;
                if (ddlGroup != null)
                {
                    ddlGroup.DataSource = Module.GetCruiseGroup();
                    ddlGroup.DataTextField = "Name";
                    ddlGroup.DataValueField = "Id";
                    ddlGroup.DataBind();
                    if (cruise.Group != null)
                    {
                        ddlGroup.SelectedValue = cruise.Group.Id.ToString();
                    }
                }
            }
        }

        protected void OnClick(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptCruises.Items)
            {
                var ddlGroup = item.FindControl("ddlGroup") as DropDownList;
                if (ddlGroup != null)
                {
                    var cid = item.FindControl("cid") as HiddenField;
                    if (cid != null)
                    {
                        var cruise = Module.GetById<Cruise>(Convert.ToInt32(cid.Value));
                        cruise.Group = Module.GetById<QCruiseGroup>(Convert.ToInt32(ddlGroup.SelectedValue));
                        Module.SaveOrUpdate(cruise);
                    }
                }
            }
        }
    }
}
