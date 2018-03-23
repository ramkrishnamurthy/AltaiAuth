using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using Altai.Api.Http;
using Altai.Api.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Altai.Api
{
    public static class AuthConfig
    {
        public static void ConfigureJwtAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["Jwt.Issuer"];
            var secret = ConfigurationManager.AppSettings["Jwt.Secret"];
            var expMin = int.Parse(ConfigurationManager.AppSettings["Jwt.AccessTokenExpireMinutes"]);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(expMin),
                Provider = new JwtAuthorizationServerProvider(),
                AccessTokenFormat = new JwtOAuthFormat(issuer)
            });

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider(issuer, secret)
                }
            });
        }

        public static void ConfigureAuth0(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["Auth0.Issuer"];
            var audience = ConfigurationManager.AppSettings["Auth0.Audience"];
            var certificatePath = ConfigurationManager.AppSettings["Auth0.CertificatePath"];

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                Provider = new Auth0AuthorizationServerProvider(),
                AccessTokenProvider = new Auth0AuthenticationTokenProvider()
            });
            
            var cert = new X509Certificate2(certificatePath);
            var securityKey = new X509SecurityKey(cert);
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                    {
                        new X509CertificateSecurityKeyProvider(issuer, cert)
                    },
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKeyResolver = (arbitrarily, declaring, these, parameters) => new List<SecurityKey> { securityKey },
                        ValidAudience = audience,
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        IssuerSigningKey = securityKey
                    }
                }
            );
        }
    }
}