using System;
using System.Configuration;
using System.Security;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Owin.Security.Infrastructure;

namespace Altai.Api.Http
{
    public class Auth0AuthenticationTokenProvider : AuthenticationTokenProvider
    {
        public override async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var domain = "https://" + ConfigurationManager.AppSettings["Auth0.Domain"];
            var clientId = context.OwinContext.Get<string>("oauth:clientId");
            var clientSecret = context.OwinContext.Get<string>("oauth:clientSecret");
            context.OwinContext.Set<string>("oauth:clientId", null);
            context.OwinContext.Set<string>("oauth:clientSecret", null);

            try
            {
                var client = new AuthenticationApiClient(new Uri(domain));
                var response = await client.GetTokenAsync(new ClientCredentialsTokenRequest
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    Audience = ConfigurationManager.AppSettings["Auth0.Audience"]
                });

                context.SetToken(response.AccessToken);
            }
            catch
            {
                throw new SecurityException("invalid request");
            }
        }
    }
}