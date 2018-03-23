using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.Owin.Cors;
using Owin;

namespace Altai.Api
{
    public static class CorsConfig
    {
        public static void Configure(IAppBuilder app)
        {
            var policy = new CorsPolicy()
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                SupportsCredentials = true
            };

            var origins = ConfigurationManager.AppSettings["allowOrigins"]
                .Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            foreach (var origin in origins)
                policy.Origins.Add(origin);

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });
        }
    }
}