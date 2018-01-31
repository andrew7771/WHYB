using  Autofac;
using WHYB.BLL.Interfaces;
using WHYB.BLL.Services;
using WHYB.DAL.Repositories;

namespace WHYB.Infrastructure.AutofacModules
{
    public class ServiceCreatorModule : Module
    {
        //private readonly IUserService _userService;

        public ServiceCreatorModule()
        {
            
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServicesCreator>()
                .As<IServicesCreator>();
                //.WithParameter("userService", _userService);
            base.Load(builder);
        }
    }
}