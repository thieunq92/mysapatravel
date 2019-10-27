using System;
using System.Collections;
using System.Collections.Generic;
using CMS.Core.DataAccess;
using CMS.Core.Domain;
using NHibernate.Criterion;
namespace CMS.Core.Service.Membership
{
    /// <summary>
    /// Provides functionality for user management based on Cuyahoga's internal database.
    /// </summary>
    public class DefaultUserService : IUserService
    {
        private readonly ICommonDao _commonDao;
        private readonly IUserDao _userDao;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDao"></param>
        /// <param name="commonDao"></param>
        public DefaultUserService(IUserDao userDao, ICommonDao commonDao)
        {
            _userDao = userDao;
            _commonDao = commonDao;
        }

        public void SaveOrUpdateObject(object obj)
        {
            _commonDao.SaveOrUpdateObject(obj);
        }

        public object GetObjectById(Type type, int id)
        {
            return _commonDao.GetObjectById(type, id);
        }
        #region IUserService Members

        public IList FindUsersByUsername(string searchString)
        {
            return _userDao.FindUsersByUsername(searchString);
        }

        public User GetUserById(int userId)
        {
            return _commonDao.GetObjectById(typeof(User), userId, true) as User;
        }

        public User GetUserByUsernameAndEmail(string username, string email)
        {
            return _userDao.GetUserByUsernameAndEmail(username, email);
        }

        public string CreateUser(string username, string email, Site currentSite)
        {
            User user = new User();
            user.UserName = username;
            user.Email = email;
            user.IsActive = true;
            string newPassword = user.GeneratePassword();
            // Add the default role from the current site.
            user.Roles.Add(currentSite.DefaultRole);
            _commonDao.SaveOrUpdateObject(user);

            return newPassword;
        }

        public void UpdateUser(User user)
        {
            _commonDao.SaveOrUpdateObject(user);
        }

        public void DeleteUser(User user)
        {
            _commonDao.DeleteObject(user);
        }

        public string ResetPassword(string username, string email)
        {
            User user = _userDao.GetUserByUsernameAndEmail(username, email);
            if (user == null)
            {
                throw new NullReferenceException("No user found with the given username and email");
            }
            string newPassword = user.GeneratePassword();
            _userDao.SaveOrUpdateUser(user);
            return newPassword;
        }

        public IList GetAllRoles()
        {
            return _commonDao.GetAll(typeof(Role));
        }

        public Role GetRoleById(int roleId)
        {
            return (Role)_commonDao.GetObjectById(typeof(Role), roleId);
        }

        #endregion

        #region -- chat ----

        public User GetUserByConnectionId(string connectionId)
        {
            return this._commonDao.OpenSession().QueryOver<User>().Where(u => u.ConnectionId == connectionId).SingleOrDefault();
        }
        public virtual IList<User> GetAllUser()
        {
            return this._commonDao.OpenSession().QueryOver<User>().Where(u => u.IsActive).List();
        }

        public IList<ChatMessage> GetPrivateMessage(string fromid, string toid, int take)
        {
            var fromUser = GetUserById(Convert.ToInt32(fromid));
            var toUser = GetUserById(Convert.ToInt32(toid));
            return this._commonDao.OpenSession().QueryOver<ChatMessage>().Where(m => m.FromUser.Id == Convert.ToInt32(fromid) && m.ToUser.Id == Convert.ToInt32(toid) || (m.FromUser.Id == Convert.ToInt32(toid) && m.ToUser.Id == Convert.ToInt32(fromid)))
                .Where(m => m.ChatGroup == null).OrderBy(m => m.Id).Desc.Take(take).List();
        }
        public IList<ChatMessage> GetPrivateMessage(string fromid, string toid, int takeCounter, int skipCounter)
        {
            var fromUser = GetUserById(Convert.ToInt32(fromid));
            var toUser = GetUserById(Convert.ToInt32(toid));
            return this._commonDao.OpenSession().QueryOver<ChatMessage>().Where(m => m.FromUser == fromUser && m.ToUser == toUser || (m.FromUser == toUser && m.ToUser == fromUser))
                .Where(m => m.ChatGroup == null).OrderBy(m => m.Id).Desc.Take(takeCounter).Skip(skipCounter).List();
        }
        public IList<ChatGroup> GetAllGroup(int userId)
        {
            ChatGroupUser chatGroupUser = null;
            return this._commonDao.OpenSession().QueryOver<ChatGroup>().Left.JoinAlias(c => c.ChatGroupUsers, () => chatGroupUser)
                .Where(c => chatGroupUser.User.Id == userId).OrderBy(m => m.Id).Desc.List();
        }

        public IList<ChatMessage> GetGroupMessage(int userId, int groupId, int take)
        {
            var userGroup = _commonDao.OpenSession().QueryOver<ChatGroupUser>()
                .Where(u => u.User.Id == userId && u.ChatGroup.Id == groupId).SingleOrDefault();
            if (userGroup != null)
            {
                return this._commonDao.OpenSession().QueryOver<ChatMessage>().Where(m => m.ChatGroup.Id == Convert.ToInt32(groupId))
                    .OrderBy(m => m.Id).Desc.Take(take).List();
            }
            return new List<ChatMessage>();
        }

        public IList<ChatMessage> GetGroupMessage(int userId, int groupId, int takeCounter, int skipCounter)
        {
            var userGroup = _commonDao.OpenSession().QueryOver<ChatGroupUser>()
                .Where(u => u.User.Id == userId && u.ChatGroup.Id == groupId).SingleOrDefault();
            if (userGroup != null)
            {
                return this._commonDao.OpenSession().QueryOver<ChatMessage>()
                    .Where(m => m.ChatGroup.Id == Convert.ToInt32(groupId))
                    .OrderBy(m => m.Id).Desc.Take(takeCounter).Skip(skipCounter).List();
            }
            return new List<ChatMessage>();
        }

        public bool CheckExistUserInGroup(User user, int groupId)
        {
            var userGroup = _commonDao.OpenSession().QueryOver<ChatGroupUser>()
                .Where(u => u.User.Id == user.Id && u.ChatGroup.Id == groupId).SingleOrDefault();
            return userGroup != null;
        }

        public void OutGroup(int userId, int groupId)
        {
            var userGroup = _commonDao.OpenSession().QueryOver<ChatGroupUser>()
                .Where(u => u.User.Id == userId && u.ChatGroup.Id == groupId).SingleOrDefault();
            if (userGroup != null)
            {
                this._commonDao.DeleteObject(userGroup);
            }
        }

        public IList<ChatGroupUser> GetGroupUser(int groupId)
        {
            return this._commonDao.OpenSession().QueryOver<ChatGroupUser>().Where(c => c.ChatGroup.Id == groupId).OrderBy(m => m.Id).Desc.List();
        }

        public bool CheckUserExistGroup(int userId, int groupId)
        {
            var userGroup = _commonDao.OpenSession().QueryOver<ChatGroupUser>()
                .Where(u => u.User.Id == userId && u.ChatGroup.Id == groupId).SingleOrDefault();
            return userGroup != null && userGroup.Id > 0;
        }

        #endregion
    }
}