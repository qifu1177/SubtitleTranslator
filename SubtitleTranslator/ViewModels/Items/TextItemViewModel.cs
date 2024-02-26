using App.Infrastructure.Interfaces.Datas;
using App.UI.Infrastructure.ViewModels.Abstractions;

namespace SubtitleTranslator.ViewModels.Items
{
    public class TextItemViewModel : ViewModelAbstract, IKeyText
    {
        public string Key { get; set; }
        private string _text;

        public string Text { get => _text; set => SetProperty(ref _text,value); }
    }
}
