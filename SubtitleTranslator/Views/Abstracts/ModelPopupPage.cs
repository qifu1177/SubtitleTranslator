using App.Infrastructure.Common;
using App.UI.Infrastructure.Bases;
using App.UI.Infrastructure.ViewModels.Abstractions;
using CommunityToolkit.Maui.Views;


namespace SubtitleTranslator.Views.Abstracts
{
    public abstract class ModelPopupPage<T> : Popup where T : ViewModelAbstract
    {
        public T ViewModel { get; private set; }
        public ModelPopupPage() {
            ViewModel = InstanceMap<AppContainer>.Instance.GetService<T>();
            BindingContext = ViewModel;
        }
    }
}
