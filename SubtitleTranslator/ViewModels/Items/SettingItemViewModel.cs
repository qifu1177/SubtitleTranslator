

using App.Infrastructure.Interfaces.Datas;
using App.UI.Infrastructure.ViewModels.Abstractions;
using SubtitleTranslator.Datas;

namespace SubtitleTranslator.ViewModels.Items
{
    public class SettingItemViewModel : ViewModelAbstract, IKeyText
    {
        private string _text = string.Empty;
        public string Text { get => _text;set=>SetProperty(ref _text, value); }
        public string Key { get; set; }
        public SettingItemType SettingItemType { get; set; }
        private bool _iIsEnabled = false;
        public bool IsEnabled { get => _iIsEnabled; set=>SetProperty(ref _iIsEnabled, value); }

    }
}
