using App.Infrastructure.Common;
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Bases;
using App.UI.Infrastructure.Datas;
using SubtitleTranslator.Services;
using SubtitleTranslator.ViewModels;

namespace SubtitleTranslator
{
    public partial class App : UiApp
    {
        private ILocalService _localService;
        private UiSettingViewModel _settingViewModel;
        private AppService _appService;
        public App(IServiceProvider provider, AppShell appShell, ILocalService localService, AppService appService, UiSettingViewModel settingViewModel, ISetup setup) :base(setup)
        {
            InitializeComponent();

            InstanceMap<AppContainer>.Instance = new AppContainer();
            InstanceMap<AppContainer>.Instance.SetProvider(provider);
            _localService = localService;
            _settingViewModel = settingViewModel;
            MainPage = appShell;
            _appService = appService;
        }
        protected override void OnStart()
        {
            base.OnStart();
            InstanceMap<UiSetting>.Instance = _uiSetting;
            _settingViewModel.SetData(_uiSetting);
            _localService.AppLanguaeCode = _uiSetting.AppLanguage.TessData.ToString();                     
            _appService.GetApiClient(_uiSetting.UserSetting);
        }
        
    }
    public abstract class UiApp : AppAbstract<ISetup>
    {
        public UiApp(ISetup setup)
        {
            _setup = setup;

        }
    }
}
