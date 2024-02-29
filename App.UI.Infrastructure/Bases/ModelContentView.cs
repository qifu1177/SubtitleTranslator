using App.Infrastructure.Common;
using App.UI.Infrastructure.ViewModels.Abstractions;


namespace App.UI.Infrastructure.Bases
{
    public class ModelContentView<T> : ContentView where T : ViewModelAbstract
    {
        public T ViewModel { get; private set; }
        public ModelContentView() : this(null) { }
        public ModelContentView(T viewModel)
        {
            if (viewModel == null)
                viewModel = InstanceMap<AppContainer>.Instance.GetService<T>();
            ViewModel = viewModel;
            BindingContext = ViewModel;
        }
    }
}
