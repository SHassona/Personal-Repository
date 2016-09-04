using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WebApi2Book.Web.Api;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AutofacWebCommon), "Start")]
namespace WebApi2Book.Web.Api
{
    public class AutofacWebCommon
    {
        public static void Start()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            RegisterServices(builder);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder containerBuilder)
        {
            var containerConfigurator = new AutofacConfigurator();
            containerConfigurator.Configure(containerBuilder);
        }
    }
}