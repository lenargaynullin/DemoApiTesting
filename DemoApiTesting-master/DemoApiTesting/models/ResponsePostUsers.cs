using Newtonsoft.Json;

namespace DemoApiTesting
{
    public class ResponsePostUsers
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }
    }




}