

using App.Infrastructure.Interfaces.Datas;
using App.UI.Infrastructure.ViewModels.Abstractions;

namespace SubtitleTranslator.ViewModels.Items
{
    public class LegalItemViewModel : MenuItemViewModel
    {       
        public string UrlKey { get; set; } = string.Empty;
    }
}
