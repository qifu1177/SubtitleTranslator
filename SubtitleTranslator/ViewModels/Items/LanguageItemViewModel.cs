

using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Datas;
using App.UI.Infrastructure.ViewModels.Abstractions;
using System.Windows.Input;

namespace SubtitleTranslator.ViewModels.Items
{
    public class LanguageItemViewModel : DataViewModelAbstract<LanguageItem>, IKeyText
    {
        public event Action ValueChanged;
        public string Key { get; set; }
        private string _text;

        public string Text { get => _text; set => SetProperty(ref _text, value); }
        private bool _value = false;
        public bool Value { get => _value; set => SetProperty((v) =>
        {
            _value = v;
            if (ValueChanged != null)
                ValueChanged.Invoke();
        }, value); }
        private bool _isSelected=false;
        public bool IsSelected { get => _isSelected;set=>SetProperty(ref _isSelected, value); }
        public ICommand ItemClicked { get; set; }
        public LanguageItemViewModel(LanguageItem data) : base(data)
        {
        }

        
    }
}
