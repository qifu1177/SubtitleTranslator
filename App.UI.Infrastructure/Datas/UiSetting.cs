using App.Infrastructure.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UI.Infrastructure.Datas
{
    public class UiSetting
    {
        private UserSetting _userSetting;
        private LanguageItem _appLanguage;
        private LanguageItem _originalLanguage;
        private List<LanguageItem> _translationLanguages;
        public Languages LanguageList { get; private set; }
        public UiSetting(UserSetting userSetting)
        {
            _userSetting = userSetting;
            LanguageList = new Languages();
            _translationLanguages = new List<LanguageItem>();
            //Update();
        }
        //private void Update()
        //{
        //    foreach (var item in LanguageList.Items)
        //    {
        //        if (item.ShortName == _userSetting.LanguageSetting.AppLanguage)
        //            AppLanguage = item;
        //        if (item.ShortName == _userSetting.LanguageSetting.OriginalLanguage)
        //            OriginalLanguage = item;

        //    }
        //}
        public UserSetting UserSetting => _userSetting;
        public LanguageItem AppLanguage
        {
            get => _appLanguage; set
            {
                _appLanguage = value;
                _userSetting.LanguageSetting.AppLanguage = value.ShortName;
            }
        }
        public LanguageItem OriginalLanguage
        {
            get => _originalLanguage; set
            {
                _originalLanguage = value;
                _userSetting.LanguageSetting.OriginalLanguage = value.ShortName;
            }
        }
        public List<LanguageItem> TranslationLanguages
        {
            get => _translationLanguages;
            set
            {
                _translationLanguages = value;
                UpdateTranslationLanguagesInUserSetting();
            }
        }
        public void UpdateTranslationLanguagesInUserSetting()
        {
            _userSetting.LanguageSetting.TranslationLanguage.Clear();
            foreach (var item in _translationLanguages)
            {
                _userSetting.LanguageSetting.TranslationLanguage.Add(item.ShortName);
            }
        }
    }
}
