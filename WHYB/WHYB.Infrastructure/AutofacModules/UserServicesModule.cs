using Autofac;
using WHYB.BLL.Interfaces;
using WHYB.BLL.Services;
using WHYB.DAL.Interfaces;
using WHYB.DAL.Repositories;


namespace WHYB.Infrastructure.AutofacModules
{
    public class UserServicesModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerRequest();
            
            base.Load(builder);
        }
    }
}