using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic.Share
{
    public class UserBLL
    {
        public UserRepository UserRepository { get; set; }

        public UserBLL()
        {
            UserRepository = new UserRepository();
        }

        public void Dispose()
        {
            if (UserRepository != null)
            {
                UserRepository.Dispose();
                UserRepository = null;
            }
        }

        public User UserGetCurrent()
        {
            return UserRepository.UserGetById(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
        }


        public string UserCurrentGetName()
        {
            return UserGetCurrent().FullName;
        }

        public object UserGetByRole(int roleId)
        {
            return UserRepository.UserGetByRole(roleId);
        }
    }
}