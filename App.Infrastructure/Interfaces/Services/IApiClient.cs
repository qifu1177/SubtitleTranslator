using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Interfaces.Services
{
    public interface IApiClient
    {
        void SetConfig(IApiConfig apiConfig);
        void SetDataService(IApiDataService apiDataService);
        Task<(string, string)> ExecuteTranslation(LanguageItem item, string text);
        Task<(string, string)> ExecuteRecognizeText(LanguageItem item, string audioFile, int sampleRateHertz);
        Task<bool> DownloadFile(string url, string targetPath);
    }
}
