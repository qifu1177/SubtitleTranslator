using App.Infrastructure.Interfaces.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTranslator.Resources.Configurations
{
    public class GoogleConfig : IApiConfig
    {
        private const string Key = "your api key";
        private const string Url = "https://translation.googleapis.com/language/translate/v2";
        private const string GoogleSpeechRecognizeUrlUrl = "https://speech.googleapis.com/v1/speech:recognize";
        public string ApiKey => Key;

        public string ApiUrl => Url;

        public string SpeechRecognizeUrl => GoogleSpeechRecognizeUrlUrl;
    }
}
