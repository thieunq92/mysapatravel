using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.Admin.Controls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// Phân quyền tàu
    /// </summary>
    public partial class IvRoleCruiseList : SailsAdminBase
    {
        protected Cruise CurrentCruise { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = @"Role";
            if (Request.QueryString["cruiseid"] != null)
            {
                CurrentCruise = Module.CruiseGetById(Convert.ToInt32(Request.QueryString["cruiseid"]));
                txtCruiseName.Text = CurrentCruise.Name;
            }
            if (!IsPostBack)
            {
                rptCruises.DataSource = Module.CruiseGetAll();
                rptCruises.DataBind();
                GetBarUser();
            }
        }

        private void GetBarUser()
        {
            if (Request.QueryString["cruiseid"] != null)
            {
                var listRoleBar = Module.IvRoleCruiseGetByCruiseId(Convert.ToInt32(Request.QueryString["cruiseid"]));
                rptUsers.DataSource = listRoleBar;
                rptUsers.DataBind();
            }
        }

        protected void rptCruises_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                Cruise cruise = (Cruise)e.Item.DataItem;
                HyperLink hplCruise = e.Item.FindControl("hplCruise") as HyperLink;
                if (hplCruise != null)
                {
                    hplCruise.Text = cruise.Name;
                    hplCruise.NavigateUrl = string.Format("IvRoleCruiseList.aspx?NodeId={0}&SectionId={1}&cruiseid={2}",
                        Node.Id, Section.Id, cruise.Id);
                }
            }
        }
        protected void userSelector_UserSelectedChanged(object sender, UserSelectedEventArgs e)
        {
            btnAssignUser_Click(sender, e);
        }

        protected void btnAssignUser_Click(object sender, EventArgs e)
        {
            UserSelector selector = (UserSelector)userSelector;
            if (selector.SelectedUserId > 0)
            {
                if (CurrentCruise != null)
                {
                    User user = Module.UserGetById(selector.SelectedUserId);
                    var listRoleBar = Module.IvRoleCruiseGetByCruiseId(Convert.ToInt32(Request.QueryString["cruiseid"]));
                    var check = false;
                    foreach (IvRoleCruise roleBarCruise in listRoleBar)
                    {
                        if (roleBarCruise.User.Id == user.Id)
                        {
                            check = true;
                            break;
                        }
                    }
                    if (check)
                    {
                        lblMsg.Text  = ("User này đã được phân vào " + CurrentCruise.Name);
                    }
                    else
                    {
                        var roleBar = new IvRoleCruise
                        {
                            User = user,
                            Cruise = CurrentCruise
                        };
                        Module.SaveOrUpdate(roleBar);
                        GetBarUser();
                    }
                }
                else lblMsg.Text = ("Phải chọn tàu");
            }
            else
            {
                lblMsg.Text = ("Phải chọn user trước");
            }
        }

        protected void rptUsers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is IvRoleCruise)
            {
                var roleBar = (IvRoleCruise)e.Item.DataItem;
                User user = roleBar.User;
                Literal litFullname = e.Item.FindControl("litFullname") as Literal;
                if (litFullname != null)
                {
                    litFullname.Text = user.FullName;
                }

                HyperLink litUsername = e.Item.FindControl("litUsername") as HyperLink;
                if (litUsername != null)
                {
                    litUsername.Text = user.UserName;
                    litUsername.NavigateUrl = string.Format("User.aspx?NodeId={0}&SectionId={1}&userid={2}", Node.Id,
                        Section.Id, user.Id);
                }

                Literal litLastSignin = e.Item.FindControl("litLastSignin") as Literal;
                if (litLastSignin != null)
                {
                    if (user.LastLogin.HasValue)
                    {
                        litLastSignin.Text = user.LastLogin.Value.ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        litLastSignin.Text = @"Never";
                    }
                }

                Literal litLastIP = e.Item.FindControl("litLastIP") as Literal;
                if (litLastIP != null)
                {
                    litLastIP.Text = user.LastIp;
                }

                HyperLink hplPermission = e.Item.FindControl("hplPermission") as HyperLink;
                if (hplPermission != null)
                {
                    if (user.HasPermission(AccessLevel.Administrator))
                    {
                        hplPermission.Text = @"Administrator";
                    }
                    else
                    {
                        hplPermission.Text = @"Phân quyền";
                        hplPermission.ForeColor = Color.Red;
                        hplPermission.NavigateUrl = string.Format(
                            "Permissions.aspx?NodeId={0}&SectionId={1}&userid={2}", Node.Id, Section.Id, user.Id);
                    }
                }
            }
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var userBarCruise = Module.GetById<IvRoleCruise>(Convert.ToInt32(e.CommandArgument));
            switch (e.CommandName)
            {
                case "delete":
                    Module.Delete(userBarCruise);
                    GetBarUser();
                    break;
            }
        }
    }
}