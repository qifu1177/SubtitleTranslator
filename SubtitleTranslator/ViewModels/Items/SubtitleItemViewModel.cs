using App.UI.Infrastructure.ViewModels.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SubtitleTranslator.ViewModels.Items
{
    public class SubtitleItemViewModel : ViewModelAbstract
    {
        public int Index { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        private string _subtitle;
        public string Subtitle { get => _subtitle; set => SetProperty(ref _subtitle, value); }
        public ICommand ItemEditCommand { get; set; }
    }
}
