using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Context;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;
using WHYB.DAL.Interfaces;
using WHYB.DAL.Repositories;

namespace WHYB.Infrastructure.AutofacModules
{
    public class DalModule : Module
    {
        private readonly string _connectionString;

        public DalModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationRoleManager>()
                .As<RoleManager<ApplicationRole>>();

            builder.RegisterType<ApplicationUserManager>()
                .As<UserManager<ApplicationUser>>();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>));

            builder.RegisterType<WhybDbContext>()
                .As<IdentityDbContext<ApplicationUser>>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerRequest();

            base.Load(builder);
        }
    }
}