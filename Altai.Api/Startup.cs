using System.Configuration;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Altai.Api.Startup))]

namespace Altai.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            CorsConfig.Configure(app);

            var auth0IsEnabled = bool.Parse(ConfigurationManager.AppSettings["Auth.Auth0.IsEnabled"] ?? "false");
            var localAuthIsEnabled = bool.Parse(ConfigurationManager.AppSettings["Auth.Local.IsEnabled"] ?? "false");
            if (auth0IsEnabled && localAuthIsEnabled)
                throw new System.Exception("Server doesn't support multiple authorization types");
            if (auth0IsEnabled)
                AuthConfig.ConfigureAuth0(app);
            if (localAuthIsEnabled)
                AuthConfig.ConfigureJwtAuth(app);

            app.UseWebApi(config);
        }
    }
}
