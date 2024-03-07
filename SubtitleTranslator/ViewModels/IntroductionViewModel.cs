using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Services;
using App.UI.Infrastructure.ViewModels.Abstractions;
using SubtitleTranslator.ViewModels.Items;
using System.Windows.Input;


namespace SubtitleTranslator.ViewModels
{

    public class IntroductionViewModel : TranslationViewModelAbstract
    {
        private PathHelp _pathHelp;
        public TextViewModel TextViewModel { get; private set; }
        public LegalItemViewModel AboutUsItem { get; private set; }
        public LegalItemViewModel DataPrivacyItem { get; private set; }
        public LegalItemViewModel InstructionsItem { get; private set; }
        //public LegalItemViewModel ThirdPartiesItem { get; private set; }
        private LegalItemViewModel[] _legalItemViewModels;
        public LegalItemViewModel Item { get; private set; }
        public ICommand ItemSelected { get; private set; }
        private string _currentHtml;
        public string CurrentHtml { get => _currentHtml; set => SetProperty(ref _currentHtml, value); }
        private string _selectedUrlKey;
        public IntroductionViewModel(ILocalService localService, TextViewModel textViewModel, PathHelp pathHelp) : base(localService)
        {
            _pathHelp = pathHelp;
            TextViewModel = textViewModel;
            ItemSelected = new Command<string>((urlKey) =>
            {
                _selectedUrlKey = urlKey;
                foreach (var item in _legalItemViewModels)
                    item.IsEnabled = item.UrlKey != _selectedUrlKey;
                UpdateSeletedUrl();
            });
        }
        public void Init()
        {
            UpdateTranslation();
        }
        protected override void InitTranslation()
        {
            AboutUsItem = new LegalItemViewModel { Key = "LegalView.AboutUsText", UrlKey = "AboutUs", IsEnabled = false };
            DataPrivacyItem = new LegalItemViewModel { Key = "LegalView.DataPrivacyText", UrlKey = "DataPrivacy" };
            InstructionsItem = new LegalItemViewModel { Key = "LegalView.InstructionsText", UrlKey = "Instructions" };
            //ThirdPartiesItem = new LegalItemViewModel { Key = "LegalView.ThirdPartiesText", UrlKey = "ThirdParties" };
            List<LegalItemViewModel> list = new List<LegalItemViewModel>();
            list.Add(AboutUsItem);
            list.Add(DataPrivacyItem);
            list.Add(InstructionsItem);
            //list.Add(ThirdPartiesItem);
            _keyTexts.AddRange(list);
            _legalItemViewModels = list.ToArray();
            _selectedUrlKey = AboutUsItem.UrlKey;
        }
        protected override void UpdateTranslation()
        {
            base.UpdateTranslation();
            UpdateSeletedUrl();
        }
        private async void UpdateSeletedUrl()
        {
            CurrentHtml =await GetCurrentHtml( System.IO.Path.Combine( "Legals", _selectedUrlKey, $"{_localService.AppLanguaeCode}.html"));
        }
        private async Task<string> GetCurrentHtml(string file)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(file);
            using var reader = new StreamReader(stream);

            var contents = reader.ReadToEnd();
            return contents;
        }
    }
}
