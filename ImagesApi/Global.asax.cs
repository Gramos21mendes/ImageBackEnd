using ImagesApi.App_Start;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ImagesApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {

            AutofacConfig.Register();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
