using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Datas;
using App.UI.Infrastructure.Services;
using App.UI.Infrastructure.ViewModels.Abstractions;
using SubtitleTranslator.Datas;
using SubtitleTranslator.Resources;
using SubtitleTranslator.Services;
using SubtitleTranslator.ViewModels.Items;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SubtitleTranslator.ViewModels
{
    public class UiSettingViewModel : TranslationViewModelAbstract
    {
        private ISettingSerivice _settingSerivice;
        private AppService _appService;
        private PathHelp _pathHelp;
        public UiSetting Setting { get; private set; }
        public TextViewModel TextViewModel { get; private set; }
        public SettingItemViewModel LanguageItem { get; private set; }
        public SettingItemViewModel ExtensionItem { get; private set; }
        public SettingItemType SelectedItemType { get; private set; }
        public ICommand ItemSelected { get; private set; }
        public ICommand GetOpenAiKey { get; private set; }
        public ObservableCollection<LanguageItemViewModel> LanguageItems { get; private set; }
        private List<LanguageItemViewModel> _languages;
        private LanguageItemViewModel _appLanguage;
        public LanguageItemViewModel AppLanguage
        {
            get => _appLanguage;
            set => SetProperty((v) =>
            {
                if(value!=null)
                {
                    _appLanguage = value;
                    Setting.AppLanguage = _appLanguage.Data;
                    _localService.AppLanguaeCode = _appLanguage.Data.TessData.ToString();
                }
               
            }, value);
        }
        private LanguageItemViewModel _originalLanguage;
        public LanguageItemViewModel OriginalLanguage
        {
            get => _originalLanguage; 
            set => SetProperty((v) =>
            {
                if(value != null)
                {
                    _originalLanguage = value;
                    Setting.OriginalLanguage = _originalLanguage.Data;
                }
               
            }, value);
        }
        

        public bool UseOpenAi
        {
            get => Setting.UserSetting.UseOpenAi; set => SetProperty((v) =>
            {
                Setting.UserSetting.UseOpenAi = value;
                UpdateApiClient();
            }, value);
        }
        public string OpenAiKey
        {
            get => Setting.UserSetting.OpenAiKey;
            set => SetProperty((v) =>
            {
                Setting.UserSetting.OpenAiKey = v;
            }, value);
        }
        public TextItemViewModel AppLanguageText { get; private set; }
        public TextItemViewModel OriginalLanguageText { get; private set; }
        public TextItemViewModel TranslationLanguageText { get; private set; }
        public TextItemViewModel GetOpenAiKeyText { get; private set; }
        public TextItemViewModel UseOpenAiText { get; private set; }
        public event Action UpdateTranslationsLanguages;
        public UiSettingViewModel(ISettingSerivice settingSerivice, ILocalService localService, PathHelp pathHelp, TextViewModel textViewModel,AppService appService) : base(localService)
        {
            _settingSerivice = settingSerivice;
            _pathHelp=pathHelp;
            TextViewModel = textViewModel;
            _appService = appService;
            ItemSelected = new Command<SettingItemType>((itemType) =>
            {
                SelectedItemType = itemType;
                LanguageItem.IsEnabled = SelectedItemType != LanguageItem.SettingItemType;
                ExtensionItem.IsEnabled = SelectedItemType != ExtensionItem.SettingItemType;
            });
            GetOpenAiKey = new Command(async () =>
            {
                var uri = new Uri($"https://platform.openai.com/account/api-keys");
                await Launcher.OpenAsync(uri);
            });
            _languages = new List<LanguageItemViewModel>();
            LanguageItems = new ObservableCollection<LanguageItemViewModel>();
        }

        public void SetData(UiSetting setting)
        {
            this.Setting = setting;
            LanguageItems.Clear();
            ISet<LanguageItem> seletedLanguageItems= new HashSet<LanguageItem>();
            foreach (var item in this.Setting.LanguageList.Items)
            {
                string key = string.Format("language.{0}", item.TessData.ToString());
                var itemViewModel = new LanguageItemViewModel(item)
                {
                    Key = key,
                    Value = false,
                    Text = _localService[key]
                };
                _languages.Add(itemViewModel);
                LanguageItems.Add(itemViewModel);
                if(Setting.UserSetting.LanguageSetting.AppLanguage==item.ShortName)
                {
                    AppLanguage = itemViewModel;
                }
                if (Setting.UserSetting.LanguageSetting.OriginalLanguage == item.ShortName)
                {
                    OriginalLanguage = itemViewModel;
                }
                if(Setting.UserSetting.LanguageSetting.TranslationLanguage.Contains(item.ShortName))
                {
                    itemViewModel.Value = true;
                    seletedLanguageItems.Add(item);
                }
            }
            foreach (var item in _languages)
            {
                item.ValueChanged += LanguageItem_ValueChanged;
            }
            Setting.TranslationLanguages = seletedLanguageItems.ToList();
            _keyTexts.AddRange(_languages);
        }

        private void LanguageItem_ValueChanged()
        {
            Setting.TranslationLanguages.Clear();
            foreach (var item in LanguageItems)
            {
                if(item.Value)
                {
                    Setting.TranslationLanguages.Add(item.Data);
                }
            }
            Setting.UpdateTranslationLanguagesInUserSetting();
            if (UpdateTranslationsLanguages != null)
                UpdateTranslationsLanguages.Invoke();
        }
        public void UpdateApiClient()
        {
            _appService.UpdateApiClient(Setting.UserSetting);
        }
        public void Save()
        {
            string settingPath = System.IO.Path.Combine(_pathHelp.UserPath, ConstantValues.SETTINGFILE);
            _settingSerivice.SaveUserSetting(Setting.UserSetting, settingPath);
        }
        public bool AfterAppLanguageChanged(LanguageItemViewModel selectedItem)
        {
            if (selectedItem.Data.TessData == _appLanguage.Data.TessData)
                return false;
            AppLanguage = selectedItem;
            return true;
        }
        protected override void InitTranslation()
        {
            LanguageItem = new SettingItemViewModel
            {
                Key = string.Format("{0}.{1}", "settingView", SettingItemType.Language.ToString()),
                Text = SettingItemType.Language.ToString(),
                SettingItemType = SettingItemType.Language,
                IsEnabled = false
            };
            ExtensionItem = new SettingItemViewModel
            {
                Key = string.Format("{0}.{1}", "settingView", SettingItemType.Extension.ToString()),
                Text = SettingItemType.Extension.ToString(),
                SettingItemType = SettingItemType.Extension,
                IsEnabled = true
            };
            SelectedItemType = LanguageItem.SettingItemType;
            _keyTexts.Add(LanguageItem);
            _keyTexts.Add(ExtensionItem);
            AppLanguageText = new TextItemViewModel { Key = "settingView.AppLanguage", Text = "" };
            _keyTexts.Add(AppLanguageText);
            OriginalLanguageText = new TextItemViewModel { Key = "settingView.OriginalLanguage", Text = "" };
            _keyTexts.Add(OriginalLanguageText);
            TranslationLanguageText = new TextItemViewModel { Key = "settingView.TranslationLanguage", Text = "" };
            _keyTexts.Add(TranslationLanguageText);
            GetOpenAiKeyText = new TextItemViewModel { Key = "settingView.GetOpenAiKey", Text = "" };
            _keyTexts.Add(GetOpenAiKeyText);
            UseOpenAiText = new TextItemViewModel { Key = "settingView.UseOpenAi", Text = "" };
            _keyTexts.Add(UseOpenAiText);
        }

    }
}
