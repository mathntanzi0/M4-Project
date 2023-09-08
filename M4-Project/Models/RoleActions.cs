using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M4_Project.Models
{
    public class RoleActions
    {
        internal void AddRole(String roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);
            roleMgr.Create(new IdentityRole { Name = roleName });
        }

        internal void AddUsertoRole(string roleName, string userName, string pass)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var user = new ApplicationUser();
            user.Email = userName;
            user.UserName = userName;
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userMgr.Create(user, pass);
            userMgr.AddToRole(userMgr.FindByEmail(userName).Id, roleName);
        }

    }
}