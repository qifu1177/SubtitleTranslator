using App.Infrastructure.Interfaces.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTranslator.Resources.Configurations
{
    public class OpenAiConfig : IApiConfig
    {
        private const string Url = "https://api.openai.com/v1/chat/completions";
        public string ApiKey { get; set; }

        public string ApiUrl => Url;

        public string SpeechRecognizeUrl => "https://api.openai.com/v1/audio/transcriptions";
    }
}
