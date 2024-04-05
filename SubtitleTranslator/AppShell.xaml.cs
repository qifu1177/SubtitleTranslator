using App.Infrastructure.Common;
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Datas;
using App.UI.Infrastructure.Services;
using SubtitleTranslator.Resources;
using SubtitleTranslator.ViewModels;

namespace SubtitleTranslator
{
    public partial class AppShell : Shell
    {
        private MainViewModel _viewModel;
        private ISettingSerivice _settingSerivice;
        private PathHelp _pathHelp;
        
        public AppShell(MainViewModel viewModel, ISettingSerivice settingSerivice, PathHelp pathHelp)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
            _settingSerivice = settingSerivice;
            _pathHelp = pathHelp;
            Disappearing += AppShell_Disappearing;
        }

        private void AppShell_Disappearing(object? sender, EventArgs e)
        {
            var uiSetting = InstanceMap<UiSetting>.Instance;
            if (uiSetting == null)
                return;
            string settingPath = System.IO.Path.Combine(_pathHelp.UserPath, ConstantValues.SETTINGFILE);
            _settingSerivice.SaveUserSetting(uiSetting.UserSetting, settingPath);
        }
        
    }
}
