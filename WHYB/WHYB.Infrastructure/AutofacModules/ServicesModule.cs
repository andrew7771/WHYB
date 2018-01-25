using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.BLL.Interfaces;
using WHYB.BLL.Services;
using WHYB.DAL.Context;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;
using WHYB.DAL.Interfaces;
using WHYB.DAL.Repositories;

namespace WHYB.Infrastructure.AutofacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>();

            builder.RegisterType<ServiceCreator>()
                .As<IServiceCreator>();

            base.Load(builder);
        }
    }
}