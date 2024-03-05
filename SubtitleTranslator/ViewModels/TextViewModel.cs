
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.ViewModels.Abstractions;
using SubtitleTranslator.ViewModels.Items;


namespace SubtitleTranslator.ViewModels
{
    public class TextViewModel : TranslationViewModelAbstract
    {
       
        public string InstallPlugInErrorMessage { get; set; }
        public string InstallPlugInStateText { get; set; }
        public TextItemViewModel InstallModelErrorMessage { get; private set; }
        public TextItemViewModel HomePageTitle { get; private set; }
        public TextItemViewModel SettingPageTitle { get; private set; }
        public TextItemViewModel IntroductionPageTitle { get; private set; }
        public TextItemViewModel ViewOKText { get; private set; }
        public TextItemViewModel ViewCancelText { get; private set; }
        public TextItemViewModel ExportFileTypeText { get; private set; }
        public TextItemViewModel UseCombinationWithOriginalText { get; private set; }
        public TextViewModel(ILocalService localService):base(localService)
        {
        }
        protected override void InitTranslation()
        {
            HomePageTitle = new TextItemViewModel { Key = "mainPage.HomeViewTitle", Text = "" };
            _keyTexts.Add(HomePageTitle);
            SettingPageTitle = new TextItemViewModel { Key = "mainPage.SettingViewTitle", Text = "" };
            _keyTexts.Add(SettingPageTitle);
            IntroductionPageTitle = new TextItemViewModel { Key = "mainPage.IntroductionViewTitle", Text = "" };
            _keyTexts.Add(IntroductionPageTitle);
            InstallModelErrorMessage = new TextItemViewModel { Key = "plugInView.InstallModelErrorMessage", Text = "" };
            _keyTexts.Add(InstallModelErrorMessage);
            ViewOKText = new TextItemViewModel { Key = "view.OK", Text = "" };
            _keyTexts.Add(ViewOKText);
            ViewCancelText = new TextItemViewModel { Key = "view.Cancel", Text = "" };
            _keyTexts.Add(ViewCancelText);
            ExportFileTypeText = new TextItemViewModel { Key = "view.ExportFileTypeText", Text = "" };
            _keyTexts.Add(ExportFileTypeText);
            UseCombinationWithOriginalText = new TextItemViewModel { Key = "view.UseCombinationWithOriginalText", Text = "" };
            _keyTexts.Add(UseCombinationWithOriginalText);
        }
        protected override void UpdateTranslation()
        {
            base.UpdateTranslation();
            InstallPlugInErrorMessage = _localService["settingView.InstallPlugInErrorMessage"];
            InstallPlugInStateText = _localService["settingView.InstallPlugInStateText"];
        }

    }
}
