using App.Infrastructure.Datas;
using System.Text;


namespace App.NetWork.Services
{
    public class GoogleClient : ApiClient
    {
        protected override async Task<(string, string)> Translate(LanguageItem item, string text)
        {
            var requestData = _apiDataService.CreateRequestDto(text, item.ShortName);
            HttpResponseMessage response = await _httpClient.PostAsync(_apiDataService.CreateApiUrl(_apiConfig), new StringContent(requestData, Encoding.UTF8, "application/json"));

            // Lies die Antwort als JSON
            string responseContent = await response.Content.ReadAsStringAsync();
            string resultStr = _apiDataService.GetResultStr(responseContent, out string errorMessage);
            return (resultStr, errorMessage);
        }
        protected override async Task<(string, string)> RecognizeText(LanguageItem item, string audioFile, int sampleRateHertz)
        {
            string json = _apiDataService.EncodeAudioData(item.ShortName, audioFile, sampleRateHertz);
            string errorMessage = string.Empty;
            if (!string.IsNullOrEmpty(json))
            {
                string url = $"{_apiConfig.SpeechRecognizeUrl}?key={_apiConfig.ApiKey}";
                HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
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
