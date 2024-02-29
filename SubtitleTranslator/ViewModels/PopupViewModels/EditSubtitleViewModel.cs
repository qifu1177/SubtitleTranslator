using App.UI.Infrastructure.ViewModels.Abstractions;
using SubtitleTranslator.ViewModels.Abstracts;

namespace SubtitleTranslator.ViewModels.PopupViewModels
{
    public class EditSubtitleViewModel : PopupViewModelAbstract<string>
    {
        private string _subtitle;
        public string SubTitle { get => _subtitle; set => SetProperty(ref _subtitle, value); }
        public TextViewModel TextViewModel { get;private set; }
        public EditSubtitleViewModel(TextViewModel textViewModel)
        {
            TextViewModel = textViewModel;
        }

        public void Init(string text)
        {
            Result = text;
            SubTitle = text;
        }
        public void Save()
        {
            Result = _subtitle;
        }
    }
}
