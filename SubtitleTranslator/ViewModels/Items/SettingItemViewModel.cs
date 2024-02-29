

using App.Infrastructure.Interfaces.Datas;
using App.UI.Infrastructure.ViewModels.Abstractions;
using SubtitleTranslator.Datas;

namespace SubtitleTranslator.ViewModels.Items
{
    public class SettingItemViewModel : MenuItemViewModel
    {        
        public SettingItemType SettingItemType { get; set; }

    }
}
