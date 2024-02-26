using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Services.Abstracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UI.Infrastructure.Services
{
    public class PathHelp : AbstractService
    {        
        public string AppTemp => FileSystem.Current.CacheDirectory;
        public string UserPath => FileSystem.Current.AppDataDirectory;
        public string CreateTempFile => System.IO.Path.Combine(AppTemp, CreateTimeStr(true));
        public string AppPath => AppDomain.CurrentDomain.BaseDirectory;
        public string LanguageDataPath => System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LanguageDatas");
        public string ProcessPath => System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Process");
        public string PlugInPath => System.IO.Path.Combine(UserPath, "plugins");
        public string ModelsnPath => System.IO.Path.Combine(UserPath, "models");
        public string TraineddataPath => System.IO.Path.Combine(UserPath, "Traineddata");

        public void ClearAssetsInTemplate()
        {
            ActionWithExceptionHandler(() =>
            {
                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(AppTemp);
                var dirs = directoryInfo.GetDirectories();
                foreach (var item in dirs)
                {
                    if (item.Exists)
                        item.Delete(true);
                }
                var items = directoryInfo.GetFiles();
                foreach (var item in items)
                {
                    if (item.Exists)
                        item.Delete();
                }
            });
        }
        
        
        private string CreateTimeStr(bool useMs)
        {
            return useMs ? string.Format("{0:yyyyMMdd_HHmmss_fff}", DateTime.Now) : string.Format("{0:yyyyMMdd_HHmmss", DateTime.Now);
        }
        public bool CheckAndCreatePath(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                return true;
            }
            directoryInfo.Create();
            return false;
        }
        public string GetTraineddataPath(string ln)
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(AppPath, "Traineddata", $"{ln}.traineddata"));
            if (fileInfo.Exists)
            {
                return Path.Combine(AppPath, "Traineddata");
            }
            return TraineddataPath;
        }
    }
}
