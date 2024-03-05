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

        private void AppShell_SizeChanged(object? sender, EventArgs e)
        {
            var view = sender;
        }

        public void SetUiSetting(UiSetting setting)
        {
            _viewModel.SettingViewModel.SetData(setting);

        }
    }
}
