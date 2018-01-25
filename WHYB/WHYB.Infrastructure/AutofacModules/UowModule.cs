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
    public class UofModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityUnitOfWork>()
                .As<IUnitOfWork>();

            base.Load(builder);
        }
    }
}