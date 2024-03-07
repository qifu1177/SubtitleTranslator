
using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Services;
using SubtitleTranslator.Resources;

namespace SubtitleTranslator
{
    public class AppSetup : ISetup
    {
        private readonly ISettingSerivice _settingSerivice;
        private readonly ILocalService _localService;
        private readonly PathHelp _pathHelp;
        public AppSetup(ISettingSerivice settingSerivice, ILocalService localService, PathHelp pathHelp)
        {
            _settingSerivice = settingSerivice;
            _localService = localService;
            _pathHelp = pathHelp;
        }
        public UserSetting GetUserSetting()
        {
            string settingPath = System.IO.Path.Combine(_pathHelp.UserPath, ConstantValues.SETTINGFILE);
            return _settingSerivice.ReadUserSetting(settingPath);
        }
        public void InitAppLanguage()
        {
            //string path = System.IO.Path.Combine(_pathHelp.AppPath, "Resources", "LanguageDatas");
            _localService.Init("LanguageDatas", async (file) =>
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(file);
                using var reader = new StreamReader(stream);

                var contents = reader.ReadToEnd();
                return contents;
            });

        }
        public void UpdateResourcen()
        {
            //var (colorResources, styleResources) = GetResources();
            //UpdateColors(colorResources);
            //UpdateStyles(styleResources);
        }
        private void UpdateColors(ResourceDictionary colorResources)
        {
            if (colorResources == null)
                return;
        }
        private void UpdateStyles(ResourceDictionary styleResources)
        {
            if (styleResources == null)
                return;
           
        }
        private (ResourceDictionary, ResourceDictionary) GetResources()
        {
            ResourceDictionary colorResources = null;
            ResourceDictionary styleResources = null;
            foreach (var v in App.Current.Resources.MergedDictionaries)
            {
                if (colorResources == null)
                    colorResources = v;
                else if (styleResources == null)
                    styleResources = v;
            }
            return (colorResources, styleResources);
        }
    }
}
