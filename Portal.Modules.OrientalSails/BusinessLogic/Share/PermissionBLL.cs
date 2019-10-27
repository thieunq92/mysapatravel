using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic.Share
{
    public class PermissionBLL
    {
        public UserRepository UserRepository { get; set; }

        public RoleRepository RoleRepository { get; set; }

        public PermissionRepository PermissionRepository { get; set; }

        public SpecialPermissionRepository SpecialPermissionRepository { get; set; }


        public PermissionBLL()
        {
            UserRepository = new UserRepository();
            RoleRepository = new RoleRepository();
            PermissionRepository = new PermissionRepository();
            SpecialPermissionRepository = new SpecialPermissionRepository();
        }

        public void Dispose()
        {
            if (UserRepository != null)
            {
                UserRepository.Dispose();
                UserRepository = null;
            }

            if (RoleRepository != null)
            {
                RoleRepository.Dispose();
                RoleRepository = null;
            }

            if (PermissionRepository != null)
            {
                PermissionRepository.Dispose();
                PermissionRepository = null;
            }

            if (SpecialPermissionRepository != null)
            {
                SpecialPermissionRepository.Dispose();
                SpecialPermissionRepository = null;
            }
        }

        public bool UserCheckRole(int userId, int roleId)
        {
            var user = UserRepository.UserGetById(userId);
            var role = RoleRepository.RoleGetById(roleId);

            if (user == null || role == null)
                return false;

            foreach (Role userRole in user.Roles)
            {
                if (role.Id == userRole.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UserCheckPermission(int userId, int permissionId)
        {
            var user = UserRepository.UserGetById(userId);
            var permission = PermissionRepository.PermissionGetById(permissionId);
            SpecialPermission specialPermission = null;
            if (permission != null)
            {
                specialPermission = SpecialPermissionRepository.SpecialPermissionGetByUserIdAndPermissionName(user.Id, permission.Name);
            }
            if (user != null && UserCheckRole(user.Id, (int)Roles.Administrator))
                return true;
            if (user == null || permission == null)
                return false;
            if (specialPermission != null)
                return true;

            return false;
        }

        public bool UserCheckPermission(User user, PermissionEnum permission)
        {
            var userId = 0;
            if(user != null) userId = user.Id;
            var permissionId = (int)permission;
            return UserCheckPermission(userId, permissionId);
        }
    }
}