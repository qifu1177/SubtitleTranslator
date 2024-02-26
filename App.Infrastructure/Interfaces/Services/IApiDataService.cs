

using App.Infrastructure.Interfaces.Datas;

namespace App.Infrastructure.Interfaces.Services
{
    public interface IApiDataService
    {
        string CreateApiUrl(IApiConfig config);
        string CreateRequestDto(string text, string ln);
        string GetResultStr(string jsonStr, out string errorMessage);
        string EncodeAudioData(string languageCode,string audioFile, int sampleRateHertz);
        HttpContent CreateAudioRequestContent(string languageCode, string audioFile);
        string GetAudioResultStr(string jsonStr, out string errorMessage);
    }
}
