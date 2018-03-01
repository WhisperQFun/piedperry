using hr_hackaton_mysql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace hr_hackaton_mysql.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (UserContext db = new UserContext())
            {
                // Получаем пользователя
                User user = db.Users.FirstOrDefault(u => u.email == username);
                if (user != null )
                {
                    Role userRole = db.Roles.Find(user.role_id);
                    // получаем роль
                    if(userRole!=null)
                    roles = new string[] { userRole.name };
                }
                return roles;
            }
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            using (UserContext db = new UserContext())
            {
                var outputResult = false;
                // Получаем пользователя
                User user = db.Users.FirstOrDefault(u => u.email == username);

                if (user != null)
                {
                    Role userRole = db.Roles.Find(user.role_id);
                    // получаем роль
                    if (userRole != null && userRole.name == roleName)
                        outputResult = true;
                }
                return outputResult;
            }
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}