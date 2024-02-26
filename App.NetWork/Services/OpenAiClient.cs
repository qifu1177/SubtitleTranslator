using App.Infrastructure.Datas;
using System.Text;
namespace App.NetWork.Services
{
    public class OpenAiClient : ApiClient
    {
        protected override async Task<(string, string)> Translate(LanguageItem item, string text)
        {
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiConfig.ApiKey);
            var requestData = _apiDataService.CreateRequestDto(text, item.FullName);
            HttpResponseMessage response = await _httpClient.PostAsync(_apiConfig.ApiUrl, new StringContent(requestData, Encoding.UTF8, "application/json"));

            // Lies die Antwort als JSON
            string responseContent = await response.Content.ReadAsStringAsync();
            string resultStr = _apiDataService.GetResultStr(responseContent, out string errorMessage);
            return (resultStr, errorMessage);
        }
        protected override async Task<(string, string)> RecognizeText(LanguageItem item, string audioFile, int sampleRateHertz)
        {
            if (_httpClient.DefaultRequestHeaders.Authorization == null)
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiConfig.ApiKey);           
            //_httpClient.DefaultRequestHeaders.Add("Content-Type", "multipart/form-data");
            HttpContent content = _apiDataService.CreateAudioRequestContent(item.ShortName, audioFile);
            string errorMessage = string.Empty;
            if (content != null)
            {
                HttpResponseMessage response = await _httpClient.PostAsync(_apiConfig.SpeechRecognizeUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = _apiDataService.GetAudioResultStr(jsonResponse, out errorMessage);
                    return (result, errorMessage);
                }
                else
                {
                    errorMessage = await response.Content.ReadAsStringAsync(); ;
                }
            }
            return (string.Empty, errorMessage);
        }
    }
}
