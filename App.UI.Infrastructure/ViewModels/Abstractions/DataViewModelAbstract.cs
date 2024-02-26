
namespace App.UI.Infrastructure.ViewModels.Abstractions
{
    public abstract class DataViewModelAbstract<T> : ViewModelAbstract  where T : class
    {
        public T Data { get; private set; }
        public DataViewModelAbstract(T data)
        {
            this.Data = data;
        }
    }
}
