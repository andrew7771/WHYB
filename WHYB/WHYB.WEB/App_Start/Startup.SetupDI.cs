using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using WHYB.Infrastructure.AutofacModules;

namespace WHYB.WEB
{
    public partial class Startup
    {

        private void ConfigureDependencyInjection(IAppBuilder app)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WhybConnection"]
                .ConnectionString;
            StartupConfigurationModule module = new StartupConfigurationModule(connectionString);


            var builder = new ContainerBuilder();
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(executingAssembly);
			module.RegisterComponents(builder, app);
            builder.RegisterModule(new ServicesModule());

            var container = builder.Build();

            app.UseAutofacMiddleware(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMvc();
        }
    }
}