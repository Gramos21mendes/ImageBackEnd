using Autofac;
using Autofac.Integration.WebApi;
using Repository;
using Repository.DataContexts;
using Repository.Interfaces;
using Service;
using Service.Interfaces;
using Shared;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace ImagesApi.App_Start
{
    public static class AutofacConfig
    {
        private static IContainer container;
        private static ContainerBuilder builder;
        public static void Register()
        {
            builder = new ContainerBuilder();

            ConfigureConnectionSettings();

            builder.RegisterInstance(new DataContext());
            builder.RegisterType<ImageService>().As<IImageService>();
            builder.RegisterType<ImageRepository>().As<IImageRepository>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void ConfigureConnectionSettings()
        {
            Settings.ConnectionString = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
        }

    }
}