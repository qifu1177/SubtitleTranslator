using App.UI.Infrastructure.ViewModels.Abstractions;

namespace SubtitleTranslator.ViewModels
{
    public class SizeViewModel :  ViewModelAbstract
    {
        private double _width;
        private double _height;
        public double Width { get => _width;set=>SetProperty(ref _width, value); }
        public double Height { get => _height;set=>SetProperty(ref _height, value);}
    }
}
