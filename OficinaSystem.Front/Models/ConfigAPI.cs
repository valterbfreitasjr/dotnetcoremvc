namespace OficinaSystem.Front.Models
{
    public class ConfigAPI
    {
        public string UrlAPI { get; set; }


        public ConfigAPI()
        {
            // LocalHost
            UrlAPI = "https://localhost:7270/api/";

        }
    }

}
