using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using WHYB.Infrastructure.AutofacModules;

namespace WHYB.WEB.Infrastructure.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["WhybConnection"]
                .ConnectionString;

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new DalModule(connectionString));
            builder.RegisterModule(new ServicesModule(connectionString));
            builder.RegisterModule(new UowModule(connectionString));

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}