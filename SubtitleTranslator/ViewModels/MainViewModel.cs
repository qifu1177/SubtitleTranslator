using App.UI.Infrastructure.ViewModels.Abstractions;

namespace SubtitleTranslator.ViewModels
{
    
    public class MainViewModel : ViewModelAbstract
    {
        public UiSettingViewModel SettingViewModel { get; private set; }
        public TextViewModel TextViewModel {  get; private set; }
       
        private int _times = 1;
        public int Times { get => _times; set => SetProperty((v) => _times = v, value); }
        public MainViewModel(UiSettingViewModel uiSettingViewModel, TextViewModel textViewModel) { 
            SettingViewModel = uiSettingViewModel;
            TextViewModel = textViewModel;
        }
        
        
    }
}
