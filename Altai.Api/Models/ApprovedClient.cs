namespace Altai.Api.Models
{
    public class ApprovedClient
    {
        public string client_id { get; set; }
        public string ui_client_secret { get; set; }
        public string auth0_client_secret { get; set; }
        public string client_description { get; set; }
    }
}