using Newtonsoft.Json;

namespace App.NetWork.Models.Requests
{
    public class GoogleDto
    {
        [JsonProperty("q")]
        public string Text { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("target")]
        public string Target { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
