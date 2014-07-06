using Newtonsoft.Json;

namespace SharePointPTSample.Office365
{
    public class Me
    {
        [JsonProperty(PropertyName = "Alias")]
        public string Alias { get; set; }

        [JsonProperty(PropertyName = "DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
    }
}
