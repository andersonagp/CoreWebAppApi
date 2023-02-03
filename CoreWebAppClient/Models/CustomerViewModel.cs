using Newtonsoft.Json;

namespace CoreWebAppClient.Models
{
     public class CustomerViewModel
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("address")]
        public string Address;
    }
}