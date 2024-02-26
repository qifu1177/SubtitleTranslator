using App.Infrastructure.Interfaces.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Interfaces.Services
{
    public interface ILocalService
    {
        ILocalService Init(string languageDataPath);
        string AppLanguaeCode { get; set; }
        string this[string key] { get; }
        void UpdaeKeyTexts();
        void AddKeyText(IKeyText keyText);
        void AddKeyTexts(IEnumerable<IKeyText> keyTexts);
        event Action AfterLanguageChnaged;
    }
}
