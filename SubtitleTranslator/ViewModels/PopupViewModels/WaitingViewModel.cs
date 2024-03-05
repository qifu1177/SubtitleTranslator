

using App.NetWork.Models.Responses;
using SubtitleTranslator.ViewModels.Abstracts;

namespace SubtitleTranslator.ViewModels.PopupViewModels
{
    public class WaitingViewModel : PopupViewModelAbstract
    {
        public SizeViewModel Size { get; private set; }
        public Func<Task> ExcuitProcess { get; private set; }
        public WaitingViewModel(SizeViewModel size) { 
            Size = size;
        }
        public void Init(Func<Task> excuitProcess)
        {
            ExcuitProcess = excuitProcess;
        }
    }
}
