using System.Security.Claims;
using System.Threading.Tasks;
using Altai.Api.AuthClientsConfig;
using Microsoft.Owin.Security.OAuth;

namespace Altai.Api.Http
{
    public class Auth0AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            context.Validated(new ClaimsIdentity());
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            context.TryGetFormCredentials(out clientId, out clientSecret);
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            {
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var clientsConfig = new ApprovedClientsConfig();
            var client = clientsConfig.Get(clientId, clientSecret);
            if (client != null)
            {
                context.OwinContext.Set("oauth:clientId", clientId);
                context.OwinContext.Set("oauth:clientSecret", client.auth0_client_secret);
                context.Validated(clientId);
                return Task.FromResult<object>(null);
            }
            else
            {
                context.Rejected();
                return Task.FromResult<object>(null);
            }
        }
    }
}