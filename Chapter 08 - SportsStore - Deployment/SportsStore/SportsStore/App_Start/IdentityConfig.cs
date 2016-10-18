using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using SportsStore.Infrastructure.Identity;
using System;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(SportsStore.IdentityConfig))]

namespace SportsStore {
    public class IdentityConfig {
        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext<StoreIdentityDbContext>(
                StoreIdentityDbContext.Create);
            app.CreatePerOwinContext<StoreUserManager>(StoreUserManager.Create);
            app.CreatePerOwinContext<StoreRoleManager>(StoreRoleManager.Create);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions {
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            //});        

            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions {
                Provider = new StoreAuthProvider(),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Authenticate")
            });
        }
    }
}
