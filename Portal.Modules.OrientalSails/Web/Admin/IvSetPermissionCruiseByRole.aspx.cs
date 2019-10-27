using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// phân quyền tàu
    /// </summary>
    public partial class IvSetPermissionCruiseByRole : SailsAdminBasePage
    {
        private string _currentGroup = string.Empty;
        private Role _role;
        private User _user;

        private IList<IvRoleCruise> _permissions;

        private IList<IvRoleCruise> _fixedPermission;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserIdentity.HasPermission(AccessLevel.Administrator))
            {
                ShowError("You must be administrator to use this function");
            }

            if (Request.QueryString["roleid"] != null)
            {
                _role = Module.RoleGetById(Convert.ToInt32(Request.QueryString["roleid"]));
                litTitle.Text = string.Format("PERMISSION CRUISE FOR ROLE {0}", _role.Name);
            }
            else if (Request.QueryString["userid"] != null)
            {
                _user = Module.UserGetById(Convert.ToInt32(Request.QueryString["userid"]));
                litTitle.Text = string.Format("PERMISSION CRUISE FOR USER {0}", _user.UserName);
            }
            else
            {
                ShowError("Bad request");
                return;
            }

            if (_role != null)
            {
                _permissions = Module.CruisePermissionsGetByRole(_role);
            }
            else
            {
                _permissions = Module.CruisePermissionsGetByUser(_user);
                _fixedPermission = Module.CruisePermissionsGetByUserRole(_user);
            }

            if (!IsPostBack)
            {
                rptCruise.DataSource = Module.CruiseGetAll2();
                rptCruise.DataBind();
            }
        }

        protected void rptCruise_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                var cruise = (Cruise)e.Item.DataItem;
                CheckBox chkPermission = (CheckBox)e.Item.FindControl("chkPermission");
                HiddenField hidCruiseRoleId = (HiddenField)e.Item.FindControl("hidCruiseRoleId");
                var permission = _permissions.FirstOrDefault(p => p.Cruise.Id == cruise.Id);
                if (permission != null)
                {
                    chkPermission.Checked = true;
                    hidCruiseRoleId.Value = permission.Id.ToString();
                }
                else
                {
                    chkPermission.Checked = false;
                }

                if (_fixedPermission != null)
                {
                    permission = _fixedPermission.FirstOrDefault(p => p.Cruise.Id == cruise.Id);
                    if (permission != null)
                    {
                        chkPermission.Checked = true;
                        chkPermission.Enabled = false;
                        hidCruiseRoleId.Value = permission.Id.ToString();

                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptCruise.Items)
            {
                CheckBox chkPermission = (CheckBox)item.FindControl("chkPermission");
                HiddenField hidCruiseRoleId = (HiddenField)item.FindControl("hidCruiseRoleId");
                HiddenField hidCruiseId = (HiddenField)item.FindControl("hidCruiseId");
                if (_role != null)
                {
                    if (hidCruiseRoleId != null && !string.IsNullOrWhiteSpace(hidCruiseRoleId.Value) && !chkPermission.Checked) // Nếu có quyền và không có check
                    {
                        var permission = Module.GetById<IvRoleCruise>(Convert.ToInt32(hidCruiseRoleId.Value));
                        if (permission != null)
                        {
                            Module.Delete(permission);
                        }
                    }
                    else if (hidCruiseRoleId != null && string.IsNullOrWhiteSpace(hidCruiseRoleId.Value) && chkPermission.Checked)
                    {
                        var roleCruise = new IvRoleCruise();
                        roleCruise.Role = _role;
                        roleCruise.Cruise = Module.GetById<Cruise>(Convert.ToInt32(hidCruiseId.Value));
                        Module.SaveOrUpdate(roleCruise);
                    }
                }
                else
                {
                    if (chkPermission.Enabled)// Phải enable, tức là quyền theo user chứ không phải theo role
                    {
                        if (hidCruiseRoleId != null && !string.IsNullOrWhiteSpace(hidCruiseRoleId.Value) && !chkPermission.Checked)
                        // Nếu có quyền và không có check
                        {
                            var permission = Module.GetById<IvRoleCruise>(Convert.ToInt32(hidCruiseRoleId.Value));
                            //if (permission != null)
                            {
                                Module.Delete(permission);
                            }
                        }
                        else if (hidCruiseRoleId != null && string.IsNullOrWhiteSpace(hidCruiseRoleId.Value) && chkPermission.Checked)
                        {
                            var roleCruise = new IvRoleCruise();
                            roleCruise.User = _user;
                            roleCruise.Cruise = Module.GetById<Cruise>(Convert.ToInt32(hidCruiseId.Value));
                            Module.SaveOrUpdate(roleCruise);
                        }
                    }
                }
            }
            PageRedirect(string.Format("SetPermission.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id));
        }
    }
}