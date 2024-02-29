using App.Infrastructure.Common;
using App.UI.Infrastructure.ViewModels.Abstractions;

namespace App.UI.Infrastructure.Bases
{
    public abstract class ModelContentPage<T> : ContentPage where T : ViewModelAbstract
    {
        public T ViewModel { get; private set; }
        public ModelContentPage() {            
            ViewModel = InstanceMap<AppContainer>.Instance.GetService<T>();
            BindingContext = ViewModel;
        }
    }
}
