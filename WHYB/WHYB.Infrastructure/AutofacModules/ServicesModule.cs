using Autofac;
using WHYB.BLL.Interfaces;
using WHYB.BLL.Services;
using WHYB.DAL.Repositories;


namespace WHYB.Infrastructure.AutofacModules
{
    public class ServicesModule : Module
    {
        private readonly string _connectionString;

        public ServicesModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .WithParameter("uow", new IdentityUnitOfWork(_connectionString));

            builder.RegisterType<ServiceCreator>()
                .As<IServiceCreator>()
                .WithParameter("userService", new UserService(new IdentityUnitOfWork(_connectionString)));

            base.Load(builder);
        }
    }
}