using System.Security.Claims;
using System.Threading.Tasks;
using Altai.Api.AuthClientsConfig;
using Microsoft.Owin.Security.OAuth;

namespace Altai.Api.Http
{
    public class JwtAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var identity = GetClaimsIdentity(context);
            context.Validated(identity);
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            context.TryGetFormCredentials(out clientId, out clientSecret);

            if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
            {
                var clientsConfig = new ApprovedClientsConfig();
                var client = clientsConfig.Get(clientId, clientSecret);
                if (client != null)
                {
                    context.Validated(clientId);
                    return Task.FromResult<object>(null);
                }
                else
                {
                    context.Rejected();
                    return Task.FromResult<object>(null);
                }
            }
            else
            {
                context.Rejected();
                return Task.FromResult<object>(null);
            }
        }

        private static ClaimsIdentity GetClaimsIdentity(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.ClientId));
            identity.AddClaim(new Claim("sub", context.ClientId));
            return identity;
        }
    }
}