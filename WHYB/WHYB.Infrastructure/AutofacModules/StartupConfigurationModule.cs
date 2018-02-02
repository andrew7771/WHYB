using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using WHYB.DAL.Context;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;
using WHYB.DAL.Interfaces;
using WHYB.DAL.Repositories;

namespace WHYB.Infrastructure.AutofacModules
{
    public class StartupConfigurationModule
    {
        private readonly string _connectionString;

        public StartupConfigurationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RegisterComponents(ContainerBuilder builder, IAppBuilder app)
        {
            builder.RegisterType<WhybDbContext>().As<DbContext>().WithParameter("connectionString", _connectionString).InstancePerRequest();

            builder.RegisterType<ApplicationSignInManager>().As<SignInManager<ApplicationUser, string>>().InstancePerRequest();

            //builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>().InstancePerRequest();

            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.Register<IAuthenticationManager>((c, p) => c.Resolve<IOwinContext>().Authentication).InstancePerRequest();


            var dataProtectionProvider = app.GetDataProtectionProvider();
            builder.Register<UserManager<ApplicationUser>>((c, p) => BuildUserManager(c, p, dataProtectionProvider));

            builder.RegisterType<IdentityUnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
        }

        

        private UserManager<ApplicationUser> BuildUserManager(IComponentContext context, IEnumerable<Parameter> parameters, IDataProtectionProvider dataProtectionProvider)
        {
            var manager = new ApplicationUserManager(context.Resolve<IUserStore<ApplicationUser>>());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
               // RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                //RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}


/* builder.RegisterType<ApplicationSignInManager>()
               .As<SignInManager<ApplicationUser, string>>()
               .InstancePerRequest();

           builder.RegisterType<UserStore<ApplicationUser>>()
               .As<IUserStore<ApplicationUser>>()
               .InstancePerRequest();

           builder.RegisterType<ApplicationRoleManager>()
               .As<RoleManager<ApplicationRole>>()
               .WithParameter("store", new RoleStore<ApplicationRole>(new IdentityDbContext<ApplicationUser>()))
               .InstancePerRequest();
           builder.RegisterType<ApplicationUserManager>()
               .As<UserManager<ApplicationUser>>()
               .WithParameter("store", new UserStore<ApplicationUser>(new IdentityDbContext<ApplicationUser>()))
               .InstancePerRequest();
               */
