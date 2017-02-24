using ModeloApi.Recursos;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace ModeloApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new WebApiExceptionHandler());

            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            formatters.Remove(formatters.XmlFormatter);
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //settings.Formatting = Formatting.Indented;
            //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}