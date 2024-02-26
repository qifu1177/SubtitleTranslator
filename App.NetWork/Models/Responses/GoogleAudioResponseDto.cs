
using Newtonsoft.Json;

namespace App.NetWork.Models.Responses
{
    public class GoogleAudioResponseDto
    {
        [JsonProperty("results")]
        public Result[] Results { get; set; }
        [JsonProperty("totalBilledTime")]
        public string TotalBilledTime { get; set; }
        [JsonProperty("requestId")]
        public string RequestId { get; set; }
    }

    public class Result
    {
        [JsonProperty("alternatives")]
        public Alternative[] Alternatives { get; set; }
        [JsonProperty("resultEndTime")]
        public string ResultEndTime { get; set; }
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }
    }

    public class Alternative
    {
        [JsonProperty("transcript")]
        public string Transcript { get; set; }
        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }
}