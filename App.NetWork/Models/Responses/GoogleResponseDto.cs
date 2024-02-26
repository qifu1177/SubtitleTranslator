using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.NetWork.Models.Responses
{
    public class GoogleResponseDto
    {
        [JsonProperty("data")]
        public GoogleResponseDataDto Data { get; set; }
    }
    public class GoogleResponseDataDto 
    {
        [JsonProperty("translations")]
        public GoogleResponseTranslationItemDto[] Translations { get; set; }
    }
    public class GoogleResponseTranslationItemDto
    {
        [JsonProperty("translatedText")]
        public string TranslatedText { get; set; }
        [JsonProperty("detectedSourceLanguage")]
        public string DetectedSourceLanguage { get; set; }
    }
}
