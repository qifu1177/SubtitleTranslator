
using Newtonsoft.Json;

namespace App.NetWork.Models.Requests
{
    public class GoogleAudioDto
    {
        [JsonProperty("config")]
        public GoogleAudioConfig Config { get; set; }
        [JsonProperty("audio")]
        public Audio Audio { get; set; }
    }

    public class GoogleAudioConfig
    {
        [JsonProperty("encoding")]
        public string Encoding { get; set; }
        [JsonProperty("sampleRateHertz")]
        public int SampleRateHertz { get; set; }
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }
    }

    public class Audio
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }

}