using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace SportsStore.Infrastructure.Identity {
    public class StoreIdentityDbInitializer :
        CreateDatabaseIfNotExists<StoreIdentityDbContext> {

        protected override void Seed(StoreIdentityDbContext context) {

            StoreUserManager userMgr =
                new StoreUserManager(new UserStore<StoreUser>(context));
            StoreRoleManager roleMgr =
                new StoreRoleManager(new RoleStore<StoreRole>(context));

            string roleName = "Administrators";
            string userName = "Admin";
            string password = "secret";
            string email = "admin@example.com";

            if (!roleMgr.RoleExists(roleName)) {
                roleMgr.Create(new StoreRole(roleName));
            }

            StoreUser user = userMgr.FindByName(userName);
            if (user == null) {
                userMgr.Create(new StoreUser {
                    UserName = userName, Email = email
                }, password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName)) {
                userMgr.AddToRole(user.Id, roleName);
            }

            base.Seed(context);
        }
    }
}
