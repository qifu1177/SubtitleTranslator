using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Datas;
using App.Infrastructure.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace App.NetWork.Services
{
    public abstract class ApiClient : IApiClient
    {
        protected HttpClient _httpClient;
        protected IApiConfig _apiConfig;
        protected IApiDataService _apiDataService;
        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout=TimeSpan.FromSeconds(30);
        }
        public void  SetConfig(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
            
        }
        public void SetDataService(IApiDataService apiDataService)
        {
            _apiDataService = apiDataService;
        }
        public async Task<(string, string)> ExecuteTranslation(LanguageItem item, string text)
        {
            string errorMessage = string.Empty;
            try
            {
                return await Translate(item,text);
            }catch(Exception ex)
            {
                errorMessage=ex.Message;
            }
            return (string.Empty, errorMessage);
        }
        public async Task<(string, string)> ExecuteRecognizeText(LanguageItem item, string audioFile, int sampleRateHertz)
        {
            string errorMessage = string.Empty;
            try
            {
                return await RecognizeText(item, audioFile, sampleRateHertz);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (string.Empty, errorMessage);
        }
        protected abstract Task<(string, string)> Translate(LanguageItem item, string text);

        protected abstract Task<(string, string)> RecognizeText(LanguageItem item, string audioFile, int sampleRateHertz);
        public async Task<bool> DownloadFile(string url, string targetPath)
        {
            bool b = false;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (FileStream fileStream = File.Create(targetPath))
                        {
                            await contentStream.CopyToAsync(fileStream);
                            b = true;
                        }
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"HTTP Request Error: {e.Message}");
            }
            return b;
        }

       
    }
}
