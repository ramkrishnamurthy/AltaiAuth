using Altai.Api.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Altai.Api.AuthClientsConfig
{
    public class ApprovedClientsConfig
    {
        private static volatile ICollection<ApprovedClient> _clients = new List<ApprovedClient>();

        public ApprovedClientsConfig()
        {
            var isJwtLocalClient = bool.Parse(ConfigurationManager.AppSettings["Auth.Local.IsEnabled"] ?? "false");
            var isAuth0Client = bool.Parse(ConfigurationManager.AppSettings["Auth.Auth0.IsEnabled"] ?? "false");
            if (!isJwtLocalClient && !isAuth0Client)
                return;

            var configFile = isJwtLocalClient ? ConfigurationManager.AppSettings["Auth.ApprovedClients.Local.FilePath"] : ConfigurationManager.AppSettings["Auth.ApprovedClients.Auth0.FilePath"];
            if (!_clients.Any() && !string.IsNullOrWhiteSpace(configFile) && File.Exists(configFile))
            {
                _clients = JArray.Parse(File.ReadAllText(configFile)).ToObject<List<ApprovedClient>>()
                    .Where(x => !string.IsNullOrWhiteSpace(x.client_id) && !string.IsNullOrWhiteSpace(x.ui_client_secret)).ToList();
                if (isAuth0Client)
                    _clients = _clients.Where(x => !string.IsNullOrWhiteSpace(x.auth0_client_secret)).ToList();
            }
        }

        public ApprovedClient Get(string clientId, string clientSecret)
        {
            return _clients.FirstOrDefault(x => x.client_id == clientId && x.ui_client_secret == clientSecret);
        }

        public bool TryGet(string clientId, string clientSecret, out ApprovedClient client)
        {
            client = _clients.FirstOrDefault(x => x.client_id == clientId && x.ui_client_secret == clientSecret);
            return client != null;
        }
    }
}