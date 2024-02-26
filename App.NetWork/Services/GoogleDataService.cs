using App.Infrastructure.Interfaces.Datas;
using App.Infrastructure.Interfaces.Services;
using App.NetWork.Models.Requests;
using App.NetWork.Models.Responses;
using Newtonsoft.Json;
using System.Text;

namespace App.NetWork.Services
{
    public class GoogleDataService : IApiDataService
    {
        private GoogleDto _requestDto;
        public GoogleDataService()
        {
            _requestDto = new GoogleDto
            {
                Format = "text"
            };
        }

        public string CreateApiUrl(IApiConfig config)
        {
            return $"{config.ApiUrl}?key={config.ApiKey}";
        }

        public string CreateRequestDto(string text, string ln)
        {
            _requestDto.Text = text;
            _requestDto.Target = ln;
            return JsonConvert.SerializeObject(_requestDto);
        }

        public string EncodeAudioData(string languageCode, string audioFile, int sampleRateHertz)
        {
            string base64EncodedAudioData = string.Empty;
            try
            {
                // Read the WAV file as bytes
                byte[] audioBytes = File.ReadAllBytes(audioFile);

                // Convert the bytes to base64
                base64EncodedAudioData = Convert.ToBase64String(audioBytes);
                GoogleAudioDto dto = new GoogleAudioDto
                {
                    Config = new GoogleAudioConfig
                    {
                        Encoding = "LINEAR16",
                        SampleRateHertz = sampleRateHertz,
                        LanguageCode = languageCode,
                    },
                    Audio = new Audio
                    {
                        Content = base64EncodedAudioData
                    }
                };
                return JsonConvert.SerializeObject(dto);
            }
            catch (Exception ex)
            {

            }
            return base64EncodedAudioData;
        }

        public string GetResultStr(string jsonStr, out string errorMessage)
        {
            errorMessage = null;
            string result = null;
            try
            {
                var dto = JsonConvert.DeserializeObject<GoogleResponseDto>(jsonStr);
                if (dto != null && dto.Data != null && dto.Data.Translations != null && dto.Data.Translations.Any())
                    result = dto.Data.Translations[0].TranslatedText;
            }
            catch (Exception ex)
            {

                errorMessage = "unknown error";
            }
            return result;
        }
        public string GetAudioResultStr(string jsonStr, out string errorMessage)
        {
            errorMessage = null;
            string result = null;
            try
            {
                var dto = JsonConvert.DeserializeObject<GoogleAudioResponseDto>(jsonStr);
                if (dto != null && dto.Results != null )
                {
                    StringBuilder sb=new StringBuilder();
                    foreach( var item in dto.Results )
                    {
                        if(item.Alternatives!=null)
                        {
                            foreach(var data in item.Alternatives)
                            {
                                sb.AppendLine(data.Transcript);
                            }
                        }
                    }
                    result = sb.ToString();
                }
            }
            catch (Exception ex)
            {

                errorMessage = "unknown error";
            }
            return result;
        }
        public HttpContent CreateAudioRequestContent(string languageCode, string audioFile)
        {
            return null;
        }
    }
}
