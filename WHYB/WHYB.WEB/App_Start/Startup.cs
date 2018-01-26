using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using WHYB.BLL.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using WHYB.BLL.Interfaces;

[assembly: OwinStartup(typeof(WHYB.App_Start.Startup))]
namespace WHYB.App_Start
{
    public class Startup
    {
        private IServiceCreator _serviceCreator = new ServiceCreator();

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        private IUserService CreateUserService()
        {
            return _serviceCreator.CreateUserService("WhybConnection");
        }
    }
}