
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Services;
using App.UI.Infrastructure.ViewModels.Abstractions;
using CommunityToolkit.Maui.Storage;
using SubtitleTranslator.Resources;
using SubtitleTranslator.Services;
using SubtitleTranslator.ViewModels.Items;
using SubtitleTranslator.ViewModels.PopupViewModels;
using SubtitleTranslator.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SubtitleTranslator.ViewModels
{
    public class HomeViewModel : TranslationViewModelAbstract
    {
        private string _selectedKey = string.Empty;
        private List<MenuItemViewModel> _menuItems;
        private LanguageItemViewModel _originalLanguageItem;
        public TextViewModel TextViewModel { get; private set; }
        public UiSettingViewModel SettingViewModel { get; private set; }
        public MenuItemViewModel ImportItem { get; private set; }
        public MenuItemViewModel TranslationItem { get; private set; }
        public MenuItemViewModel ExportItem { get; private set; }
        public ICommand ItemSelected { get; private set; }
        private ICommand LanguageItemCliecked;
        private ICommand SubtitleItemEditCommand;
        private string _selectedLanguageItemKey;
        public ObservableCollection<LanguageItemViewModel> LanguageItems { get; private set; }
        public ObservableCollection<SubtitleItemViewModel> SubtitleItems { get; private set; }

        private AppService _appService;
        private Dictionary<string, List<SubtitleItemViewModel>> _subtitleItems;
        private string _errorMessage = string.Empty;
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }
        private string _originalFile = string.Empty;
        public SizeViewModel SizeViewModel { get; private set; }
        public Action StartLoad { get; set; }
        public Action StopLoad { get; set; }
        public bool IsLoading { get; private set; } = false;
        public HomeViewModel(ILocalService localService, TextViewModel textViewModel, UiSettingViewModel uiSettingViewModel, AppService appService, SizeViewModel sizeViewModel) : base(localService)
        {
            TextViewModel = textViewModel;
            SettingViewModel = uiSettingViewModel;
            SizeViewModel = sizeViewModel;
            _appService = appService;
            LanguageItems = new ObservableCollection<LanguageItemViewModel>();
            SubtitleItems = new ObservableCollection<SubtitleItemViewModel>();
            _subtitleItems = new Dictionary<string, List<SubtitleItemViewModel>>();
            ItemSelected = new Command<string>(async (key) =>
            {
                if (IsLoading) return;
                _selectedKey = key;
                if (_selectedKey == ImportItem.Key)
                    await OpenFile();
                else if (_selectedKey == TranslationItem.Key)
                    await Translation();
                else if (_selectedKey == ExportItem.Key)
                    await Save();
            });
            LanguageItemCliecked = new Command<string>((key) =>
            {
                if (IsLoading) return;
                _selectedLanguageItemKey = key;
                UpdateSelectedLanguageItem();
            });
            SubtitleItemEditCommand = new Command<SubtitleItemViewModel>(async (item) =>
            {
                if (IsLoading) return;
                item.Subtitle = await _appService.ShowPopupAsync<EditSubtitleView, EditSubtitleViewModel, string>((vm) =>
                {
                    vm.Init(item.Subtitle);
                });

            });
            SettingViewModel.UpdateTranslationsLanguages += SettingViewModel_UpdateTranslationsLanguages;

        }
        private void Run(bool b)
        {
            IsLoading = b;
            if (b)
            {
                StartLoad();
            }
            else
            {
                StopLoad();
            }
        }
        private void SettingViewModel_UpdateTranslationsLanguages()
        {
            UpdateLanguageItems();
        }
        public void UpdateLanguageItems()
        {
            LanguageItems.Clear();
            if (_originalLanguageItem == null)
            {
                _originalLanguageItem = new LanguageItemViewModel(SettingViewModel.OriginalLanguage.Data)
                {
                    Key = "settingView.OriginalLanguage",
                    Value = false,
                    Text = _localService["settingView.OriginalLanguage"],
                    IsSelected = true,
                    ItemClicked = LanguageItemCliecked
                };
                _keyTexts.Add(_originalLanguageItem);
                _selectedLanguageItemKey = _originalLanguageItem.Key;
            }

            LanguageItems.Add(_originalLanguageItem);
            foreach (var item in SettingViewModel.LanguageItems)
            {
                if (item.Value)
                {
                    item.IsSelected = false;
                    item.ItemClicked = LanguageItemCliecked;
                    LanguageItems.Add(item);

                }

            }
        }
        private void UpdateSelectedLanguageItem()
        {
            foreach (var item in LanguageItems)
            {
                item.IsSelected = item.Key == _selectedLanguageItemKey;
                if (item.IsSelected)
                {
                    SubtitleItems.Clear();
                    if (_subtitleItems.TryGetValue(_selectedLanguageItemKey, out var list))
                    {
                        foreach (var data in list)
                        {
                            data.ItemEditCommand = SubtitleItemEditCommand;
                            SubtitleItems.Add(data);
                        }
                    }

                }
            }
        }
        protected override void InitTranslation()
        {
            _menuItems = new List<MenuItemViewModel>();
            ImportItem = new MenuItemViewModel { Key = "homeView.ImportItem", IsEnabled = true, Text = "" };
            _menuItems.Add(ImportItem);

            TranslationItem = new MenuItemViewModel { Key = "homeView.TranslationItem", IsEnabled = false, Text = "" };
            _menuItems.Add(TranslationItem);
            ExportItem = new MenuItemViewModel { Key = "homeView.ExportItem", IsEnabled = false, Text = "" };
            _menuItems.Add(ExportItem);
            _keyTexts.AddRange(_menuItems);
        }
        private async Task OpenFile()
        {
            PickOptions options = new()
            {
                PickerTitle = "Please select a subtitle file",
                FileTypes = ConstantValues.SubtitleFileTypes
            };
            FileResult fileResult = await PickAndShow(options);
            if (fileResult != null)
            {
                Run(true);
                _originalFile = fileResult.FullPath;
                var list = _appService.GetSubtitleItemsFromFile(fileResult.FullPath);
                SubtitleItems.Clear();
                foreach (var item in list)
                {
                    item.ItemEditCommand = SubtitleItemEditCommand;
                    SubtitleItems.Add(item);
                }
                _subtitleItems[_originalLanguageItem.Key] = list;
                TranslationItem.IsEnabled = true;
                Run(false);
            }
        }
        private async Task Translation()
        {
            if (_subtitleItems.TryGetValue(_originalLanguageItem.Key, out var originals))
            {
                //await _appService.ShowPopupAsync<WaitingView, WaitingViewModel>((vm) =>
                //{
                //    vm.Init(async () =>
                //    {                        
                //    });
                //});
                Run(true);
                foreach (var item in LanguageItems)
                {
                    if (item.Key != _originalLanguageItem.Key)
                    {
                        try
                        {
                            var list = await _appService.Translation(originals, item.Data);
                            _subtitleItems[item.Key] = list;
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = ex.Message;
                            break;
                        }
                    }
                }
                ExportItem.IsEnabled = _subtitleItems.Count > 1;
                Run(false);
            }

        }
        public async Task Save()
        {
            if (string.IsNullOrEmpty(_originalFile))
                return;
            await _appService.ShowPopupAsync<ExportFileTypeView, ExportFileTypeViewModel>((vm) =>
            {
                vm.Init(SettingViewModel.Setting.UserSetting);
            });
#if ANDROID
           
            //var dir = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads);
            var dir = Android.OS.Environment.ExternalStorageDirectory;
            await SaveFiles(Path.Combine(dir.AbsolutePath,Android.OS.Environment.DirectoryDownloads));
#else
            var result = await FolderPicker.Default.PickAsync(CancellationToken.None);
            if (result.IsSuccessful)
            {
                await SaveFiles(result.Folder.Path);
            }
#endif
        }
        private async Task SaveFiles(string path)
        {
            Run(true);
            Dictionary<int, SubtitleItemViewModel> dic = _subtitleItems[_originalLanguageItem.Key].ToDictionary(item => item.Index);
            foreach (string fileType in SettingViewModel.Setting.UserSetting.FileTypes)
            {
                var (fileName, typeName) = _appService.GetFileNameAndType(_originalFile);
                if (SettingViewModel.Setting.UserSetting.UseCombinationTranslation)
                {
                    string file = Path.Combine(path, $"{fileName}_translations.{fileType}");
                    await _appService.SaveAllSubtitleInFile(file, fileType, _subtitleItems, _originalLanguageItem.Key, SettingViewModel.Setting.UserSetting.UseCombinationWithOriginal);
                }
                else
                {
                    foreach (var item in _subtitleItems)
                    {
                        if (item.Key == _originalLanguageItem.Key)
                            continue;
                        string ln = item.Key;
                        try
                        {
                            if (_subtitleItems.TryGetValue(item.Key, out var list))
                            {
                                string file = Path.Combine(path, $"{fileName}_{ln}.{fileType}");
                                if (SettingViewModel.Setting.UserSetting.UseCombinationWithOriginal)
                                    await _appService.SaveSubtitleFileWithOriginal(file, fileType, list, dic);
                                else
                                    await _appService.SaveSubtitleFile(file, fileType, list);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = ex.Message;
                            break;
                        }
                    }
                }

            }
            Run(false);
        }

        public async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith(ConstantValues.SubtitleFileType1, StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith(ConstantValues.SubtitleFileType2, StringComparison.OrdinalIgnoreCase))
                    {
                        using var stream = await result.OpenReadAsync();
                        var image = ImageSource.FromStream(() => stream);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }
    }
}
