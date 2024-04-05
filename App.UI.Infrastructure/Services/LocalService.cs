

using App.Infrastructure.Interfaces.Datas;
using App.Infrastructure.Interfaces.Services;
using App.Infrastructure.Models;
using Newtonsoft.Json;

namespace App.UI.Infrastructure.Services
{
    public class LocalService : ILocalService
    {
        private string _languageDataPath;
        private string _appLanguageCode;
        private Dictionary<string, string> _datas;
        public event Action AfterLanguageChnaged;
        public List<IKeyText> KeyTexts { get; private set; }
        private Func<string, Task<string>> ReadContentOfFile;
        public LocalService()
        {
            KeyTexts = new List<IKeyText>();
            _datas = new Dictionary<string, string>();
        }
        public ILocalService Init(string languageDataPath, Func<string, Task<string>> readContentOfFile)
        {
            _languageDataPath = languageDataPath;
            ReadContentOfFile = readContentOfFile;
            return this;
        }
        public string AppLanguaeCode
        {
            get => _appLanguageCode;
            set
            {
                _appLanguageCode = value;
                Update();
            }
        }
        private async void Update()
        {
            string languageDataPath = System.IO.Path.Combine(_languageDataPath, $"{_appLanguageCode}.json");
            string str = await ReadContentOfFile(languageDataPath);
            if (string.IsNullOrEmpty(str))
                return;
            var data = JsonConvert.DeserializeObject<LanguageData>(str);
            _datas = data.Datas;
            UpdaeKeyTexts();
            if (AfterLanguageChnaged != null)
                AfterLanguageChnaged();
        }
        public void UpdaeKeyTexts()
        {
            foreach (var item in KeyTexts)
            {
                item.Text = this[item.Key];
            }
        }
        public void AddKeyText(IKeyText keyText)
        {
            KeyTexts.Add(keyText);
        }
        public void AddKeyTexts(IEnumerable<IKeyText> keyTexts)
        {
            KeyTexts.AddRange(keyTexts);
        }
        public string this[string key]
        {
            get
            {
                if (_datas.TryGetValue(key, out string value))
                    return value;
                return key;
            }
        }
    }
}
