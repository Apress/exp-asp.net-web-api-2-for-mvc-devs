using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Dispatch.Infrastructure {
    public class StaticUserManager {
        private static Dictionary<string, string[]> roles;

        static StaticUserManager() {
            roles = new Dictionary<string, string[]>();
            roles.Add("admin", new string[] { "admins", "users" });
            roles.Add("bob", new string[] { "users" });
        }

        public static IPrincipal AuthenticateUser(string user, string pass) {
            if (roles.ContainsKey(user) && pass == "secret") {
                return new StaticUser(user, roles[user]);
            }
            return null;
        }
    }

    public class StaticUser : IIdentity, IPrincipal {
        private string[] roles;

        public StaticUser(string name, string[] rolesList) {
            Name = name;
            AuthenticationType = "Basic";
            IsAuthenticated = true;
            roles = rolesList;
        }

        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string Name { get; private set; }

        public IIdentity Identity {
            get { return this; }
        }

        public bool IsInRole(string role) {
            return roles.Any(x => x == role);
        }
    }
}