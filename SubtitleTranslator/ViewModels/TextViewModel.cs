
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
        public TextItemViewModel UseCombinationTranslation { get; private set; }
        public TextViewModel(ILocalService localService):base(localService)
        {
        }
        protected override void InitTranslation()
        {
            HomePageTitle = CreateTextItemViewModelAndAddInDic( "mainPage.HomeViewTitle");
            SettingPageTitle = CreateTextItemViewModelAndAddInDic("mainPage.SettingViewTitle");
            IntroductionPageTitle = CreateTextItemViewModelAndAddInDic("mainPage.IntroductionViewTitle");
            InstallModelErrorMessage = CreateTextItemViewModelAndAddInDic("plugInView.InstallModelErrorMessage");
            ViewOKText = CreateTextItemViewModelAndAddInDic("view.OK");
            ViewCancelText = CreateTextItemViewModelAndAddInDic("view.Cancel");
            ExportFileTypeText = CreateTextItemViewModelAndAddInDic("view.ExportFileTypeText");
            UseCombinationWithOriginalText = CreateTextItemViewModelAndAddInDic("view.UseCombinationWithOriginalText");
            UseCombinationTranslation = CreateTextItemViewModelAndAddInDic("view.UseCombinationTranslation");
        }
        private TextItemViewModel CreateTextItemViewModelAndAddInDic(string key)
        {
            TextItemViewModel textItemViewModel = new TextItemViewModel { Key = key, Text = string.Empty };
            _keyTexts.Add(textItemViewModel);
            return textItemViewModel;
        }
        protected override void UpdateTranslation()
        {
            base.UpdateTranslation();
            InstallPlugInErrorMessage = _localService["settingView.InstallPlugInErrorMessage"];
            InstallPlugInStateText = _localService["settingView.InstallPlugInStateText"];
        }

    }
}
