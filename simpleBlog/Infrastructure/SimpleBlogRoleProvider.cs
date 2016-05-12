using authcontroller.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authcontroller.Infrastructure
{
    public class SimpleBlogRoleProvider:System.Web.Security.RoleProvider
    {
        //public override string[] GetRolesForUser(string username)
        //{
        //    return new[] {"admin"};
        //}
        
        //public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        //{
        //    throw new NotImplementedException();
        //}

        //public override string ApplicationName
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public override void CreateRole(string roleName)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        //{
        //    throw new NotImplementedException();
        //}

        //public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        //{
        //    throw new NotImplementedException();
        //}

        //public override string[] GetAllRoles()
        //{
        //    throw new NotImplementedException();
        //}

       

        //public override string[] GetUsersInRole(string roleName)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool IsUserInRole(string username, string roleName)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool RoleExists(string roleName)
        //{
        //    throw new NotImplementedException();
        //}
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
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

        public override string[] GetRolesForUser(string username)
        {
            return AuthController.User.Roles.Select(p => p.Name).ToArray();
          
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
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