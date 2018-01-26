using Autofac;
using WHYB.DAL.Interfaces;
using WHYB.DAL.Repositories;

namespace WHYB.Infrastructure.AutofacModules
{
    public class UowModule : Module
    {
        private readonly string _connectionString;

        public UowModule(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityUnitOfWork>()
                .As<IUnitOfWork>()
                .WithParameter("connectionString", _connectionString);

            base.Load(builder);
        }
    }
}