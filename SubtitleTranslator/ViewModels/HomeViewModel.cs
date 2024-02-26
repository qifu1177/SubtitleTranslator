using App.UI.Infrastructure.Services;
using App.UI.Infrastructure.ViewModels.Abstractions;

namespace SubtitleTranslator.ViewModels
{
    public class HomeViewModel : ViewModelAbstract
    {
        public TextViewModel TextViewModel { get; private set; }
        public HomeViewModel(TextViewModel textViewModel) {
            TextViewModel = textViewModel;
        }
    }
}
