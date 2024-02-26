using App.UI.Infrastructure.Datas;
using SubtitleTranslator.ViewModels;

namespace SubtitleTranslator
{
    public partial class AppShell : Shell
    {
        private MainViewModel _viewModel;
        public AppShell(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }
        public void SetUiSetting(UiSetting setting)
        {
            _viewModel.SettingViewModel.SetData(setting);

        }
    }
}
