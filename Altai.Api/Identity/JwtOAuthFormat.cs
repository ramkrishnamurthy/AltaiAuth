using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Altai.Api.Identity
{
    public class JwtOAuthFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private static readonly byte[] Secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["Jwt.Secret"]);
        private readonly string _issuer;

        public JwtOAuthFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var symKey = new SymmetricSecurityKey(Secret);
            var signingKey = new SigningCredentials(symKey, "HS512");
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var tokenManager = new JwtSecurityToken(_issuer, "Any", data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenManager);
            return token;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}