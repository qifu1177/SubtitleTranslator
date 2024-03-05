

using App.Infrastructure.Datas;
using SubtitleTranslator.Resources;
using SubtitleTranslator.ViewModels.Abstracts;

namespace SubtitleTranslator.ViewModels.PopupViewModels
{
    public class ExportFileTypeViewModel : PopupViewModelAbstract
    {
        public TextViewModel TextViewModel { get; private set; }
        private bool _type1 = false;
        public bool Type1 { get => _type1; set => SetProperty(ref _type1, value); }
        private bool _type2 = false;
        public bool Type2 { get => _type2; set => SetProperty(ref _type2, value); }
        public string Type1Text { get; private set; }
        public string Type2Text { get; private set; }
        private bool _useCombinationWithOriginal = false;
        public bool UseCombinationWithOriginal { get => _useCombinationWithOriginal; set => SetProperty(ref _useCombinationWithOriginal, value); }
        private UserSetting _userSetting;
        public ExportFileTypeViewModel(TextViewModel textViewModel)
        {
            TextViewModel = textViewModel;
            Type1Text = ConstantValues.SubtitleFileType1;
            Type2Text = ConstantValues.SubtitleFileType2;
        }
        public void Init(UserSetting userSetting)
        {
            _userSetting = userSetting;
            Type1 = false;
            Type2 = false;
            foreach (var fileType in _userSetting.FileTypes)
            {
                if (fileType == ConstantValues.SubtitleFileType1)
                    Type1 = true;
                else if (fileType == ConstantValues.SubtitleFileType2)
                    Type2 = true;
            }
            UseCombinationWithOriginal = _userSetting.UseCombinationWithOriginal;
        }
        public void Save()
        {
            List<string> list= new List<string>();
            if (Type1)
                list.Add(ConstantValues.SubtitleFileType1);
            if (Type2)
                list.Add(ConstantValues.SubtitleFileType2);
            _userSetting.FileTypes=list.ToArray();  
            _userSetting.UseCombinationWithOriginal = UseCombinationWithOriginal;
        }
    }
}
