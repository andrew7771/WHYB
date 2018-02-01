using Autofac;
using WHYB.BLL.Interfaces;
using WHYB.BLL.Services;


namespace WHYB.Infrastructure.AutofacModules
{
    public class ServicesModule : Module{

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerRequest();
            
            base.Load(builder);
        }
    }
}