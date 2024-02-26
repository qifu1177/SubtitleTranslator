using Newtonsoft.Json;

namespace App.Infrastructure.Models
{
    public class LanguageData
    {
        [JsonProperty("datas")]
        public Dictionary<string, string> Datas { get;set; }
    }
}
