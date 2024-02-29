

using App.Infrastructure.Interfaces.Datas;
using App.UI.Infrastructure.ViewModels.Abstractions;

namespace SubtitleTranslator.ViewModels.Items
{
    public class MenuItemViewModel : ViewModelAbstract, IKeyText
    {
        public string Key { get; set; }
        private string _text;
        public string Text { get => _text; set => SetProperty(ref _text, value); }
        private bool _iIsEnabled = true;
        public bool IsEnabled { get => _iIsEnabled; set => SetProperty(ref _iIsEnabled, value); }
    }
}
