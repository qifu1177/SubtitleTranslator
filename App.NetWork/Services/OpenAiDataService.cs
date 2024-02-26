using App.Infrastructure.Interfaces.Datas;
using App.Infrastructure.Interfaces.Services;
using App.NetWork.Models.Requests;
using App.NetWork.Models.Responses;
using Newtonsoft.Json;

namespace App.NetWork.Services
{
    public class OpenAiDataService : IApiDataService
    {
        public const string GptModel = "gpt-3.5-turbo";
        public const string RoleSystem = "system";
        public const string RoleUser = "user";
        public const string ContentSystem = "You are a helpful assistant.";
        public const string ContentUser = "Translate the following text to {0}: {1}";
        private ChatGptDto _gptDto;
        private ChatGptDto CreateGptDtoTemplate()
        {
            return new ChatGptDto
            {
                Model = GptModel,
                Messages = new MessageItemDto[]
                {
                    new MessageItemDto{Role=RoleSystem,Content=ContentSystem },
                    new MessageItemDto{Role=RoleUser,Content=null}
                }
            };
        }
        public OpenAiDataService()
        {
            _gptDto = CreateGptDtoTemplate();
        }
        public string CreateRequestDto(string text, string ln)
        {
            _gptDto.Messages[1].Content = string.Format(ContentUser, ln, text);
            return JsonConvert.SerializeObject(_gptDto);
        }
        public string GetResultStr(string jsonStr, out string errorMessage)
        {
            errorMessage = null;
            string result = null;
            try
            {
                var dto = JsonConvert.DeserializeObject<ChatGptResponseDto>(jsonStr);
                if (dto.Choices != null && dto.Choices.Length > 0)
                {
                    if (dto.Choices[0].Message != null)
                        result = dto.Choices[0].Message.Content;
                }
                if (string.IsNullOrEmpty(result))
                {
                    errorMessage = "No Result";
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var errorDto = JsonConvert.DeserializeObject<ChatGptErrorResponseDto>(jsonStr);
                    errorMessage = errorDto.Error.Message;
                }
                catch (Exception ex2)
                {
                    errorMessage = "unknown error";
                }
            }
            return result;
        }

        public string CreateApiUrl(IApiConfig config)
        {
            return config.ApiUrl;
        }
        public string EncodeAudioData(string languageCode, string audioFile, int sampleRateHertz)
        {
            return string.Empty;
        }
        public HttpContent CreateAudioRequestContent(string languageCode, string audioFile)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(audioFile);
                byte[] audioBytes = File.ReadAllBytes(audioFile);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("whisper-1"), "model");
                content.Add(new ByteArrayContent(audioBytes), "file", fileInfo.Name);
                return content;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public string GetAudioResultStr(string jsonStr, out string errorMessage)
        {
            errorMessage = null;
            string result = null;
            try
            {
                var dto = JsonConvert.DeserializeObject<WhisperResponseDto>(jsonStr);
                if (dto != null)
                {
                    result = dto.Text;
                }
                if (string.IsNullOrEmpty(result))
                {
                    errorMessage = "No Result";
                }
            }
            catch (Exception ex)
            {
                errorMessage = "unknown error";
            }
            return result;
        }

        
    }
}
