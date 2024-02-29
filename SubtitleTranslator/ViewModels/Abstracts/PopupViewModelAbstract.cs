

using App.UI.Infrastructure.ViewModels.Abstractions;

namespace SubtitleTranslator.ViewModels.Abstracts
{
    public abstract class PopupViewModelAbstract<T> : ViewModelAbstract, IPopupViewModel
    {
        public T Result { get; protected set; }
       
    }
}
