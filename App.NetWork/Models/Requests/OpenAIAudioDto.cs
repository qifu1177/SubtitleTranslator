using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.NetWork.Models.Requests
{
    public class OpenAIAudioDto
    {
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("response_format")]
        public string ResponseFormat { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
