using App.Infrastructure.Interfaces.Datas;
using App.Infrastructure.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UI.Infrastructure.ViewModels.Abstractions
{
    public abstract class TranslationViewModelAbstract : ViewModelAbstract
    {
        protected ILocalService _localService;
        protected List<IKeyText> _keyTexts;
        public TranslationViewModelAbstract(ILocalService localService) { 
            _localService = localService;
            _keyTexts=new List<IKeyText>();
            _localService.AfterLanguageChnaged += UpdateTranslation;
            InitTranslation();
        }
        protected abstract void InitTranslation();
        protected virtual void UpdateTranslation()
        {
            foreach(var item in _keyTexts)
                item.Text = _localService[item.Key];
        }
    }
}
