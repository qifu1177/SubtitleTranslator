using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Datas
{
    public class UserSetting
    {
        public UserSetting()
        {
            //WindowSetting = new WindowSetting();
            LanguageSetting = new LanguageSetting();
        }
        //[JsonProperty("window")]
        //public WindowSetting WindowSetting { get; set; }
        [JsonProperty("languages")]
        public LanguageSetting LanguageSetting { get; set; }
                
        [JsonProperty("use_openAi")]
        public bool UseOpenAi { get; set; } = false;
        [JsonProperty("openai_key")]
        public string OpenAiKey { get; set; } = string.Empty;
        [JsonProperty("file_types")]
        public string[] FileTypes { get; set; } = [];
        [JsonProperty("useCombinationWithOriginal")]
        public bool UseCombinationWithOriginal { get; set; } = false;

    }
    //public class WindowSetting
    //{
    //    public int Left { get; set; } = 100;
    //    public int Top { get; set; } = 100;
    //    public int Width { get; set; } = 1024;
    //    public int Height { get; set; } = 800;
    //}
    public class LanguageSetting
    {
        [JsonProperty("appLanguage")]
        public string AppLanguage { get; set; } = "en";
        [JsonProperty("originalLanguage")]
        public string OriginalLanguage { get; set; } = "en";
        [JsonProperty("translationLanguages")]
        public List<string> TranslationLanguage { get; set; } = new List<string> {  };
       
        
    }
}
