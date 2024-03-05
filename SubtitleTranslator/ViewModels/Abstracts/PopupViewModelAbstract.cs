

using App.UI.Infrastructure.ViewModels.Abstractions;

namespace SubtitleTranslator.ViewModels.Abstracts
{
    public abstract class PopupViewModelAbstract<T> : PopupViewModelAbstract
    {
        public T Result { get; protected set; }
       
    }
    public abstract class PopupViewModelAbstract : ViewModelAbstract, IPopupViewModel
    {
    }
}
