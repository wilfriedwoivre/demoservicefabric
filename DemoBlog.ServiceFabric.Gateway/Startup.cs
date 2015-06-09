using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CalculatorHistory;
using DemoBlog.ServiceFabric.Calculator;
using DemoBlog.ServiceFabric.CalculatorHistory;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;

namespace DemoBlog.ServiceFabric.Gateway
{
    public class Startup : IOwinAppBuilder
    {
        private readonly HttpConfiguration httpConfiguration;
        private IContainer container;

        public Startup()
        {
            httpConfiguration = new HttpConfiguration();
            BuildContainer();
        }

        public void BuildContainer()
        {
            container = ConfigureContainer();
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureWebApi();

            app.UseStaticFiles();
            app.UseErrorPage();
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfiguration);
            app.UseWebApi(httpConfiguration);
            app.UseCors(CorsOptions.AllowAll);
        }


        private IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            RegisterContainer(builder);
            builder.RegisterApiControllers(GetType().Assembly);
            return builder.Build();
        }

        public void RegisterContainer(ContainerBuilder builder)
        {
            builder.Register(c => CalculatorClient.GetClient()).As<CalculatorClient>();
            builder.Register(c => CalculatorHistoricClient.GetClient()).As<ICalculatorHistory>();
        }

        private void ConfigureWebApi()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                Converters = { new StringEnumConverter() }
            };

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings = JsonConvert.DefaultSettings();
            httpConfiguration.EnsureInitialized();
        }
    }
}
