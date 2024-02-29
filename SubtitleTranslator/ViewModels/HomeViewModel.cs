﻿using App.Infrastructure.Datas;
using App.Infrastructure.Interfaces.Services;
using App.UI.Infrastructure.Services;
using App.UI.Infrastructure.ViewModels.Abstractions;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls;
using SubtitleTranslator.ContentPages;
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
        public HomeViewModel(ILocalService localService, TextViewModel textViewModel, UiSettingViewModel uiSettingViewModel, AppService appService) : base(localService)
        {
            TextViewModel = textViewModel;
            SettingViewModel = uiSettingViewModel;
            _appService = appService;
            LanguageItems = new ObservableCollection<LanguageItemViewModel>();
            SubtitleItems = new ObservableCollection<SubtitleItemViewModel>();
            _subtitleItems = new Dictionary<string, List<SubtitleItemViewModel>>();
            ItemSelected = new Command<string>(async (key) =>
            {
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
                _selectedLanguageItemKey = key;
                UpdateSelectedLanguageItem();
            });
            SubtitleItemEditCommand = new Command<SubtitleItemViewModel>(async (item) =>
            {
                item.Subtitle = await _appService.ShowPopupAsync<EditSubtitleView, EditSubtitleViewModel, string>((vm) =>
                {
                    vm.Init(item.Subtitle);
                });

            });
            SettingViewModel.UpdateTranslationsLanguages += SettingViewModel_UpdateTranslationsLanguages;
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

            TranslationItem = new MenuItemViewModel { Key = "homeView.TranslationItem", IsEnabled = true, Text = "" };
            _menuItems.Add(TranslationItem);
            ExportItem = new MenuItemViewModel { Key = "homeView.ExportItem", IsEnabled = true, Text = "" };
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
                var list = _appService.GetSubtitleItemsFromFile(fileResult.FullPath);
                SubtitleItems.Clear();
                foreach (var item in list)
                {
                    item.ItemEditCommand = SubtitleItemEditCommand;
                    SubtitleItems.Add(item);
                }
                _subtitleItems[_originalLanguageItem.Key] = list;
            }
        }
        private async Task Translation()
        {
            if (_subtitleItems.TryGetValue(_originalLanguageItem.Key, out var originals))
            {
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
                            return;
                        }
                    }

                }
            }

        }
        public async Task Save()
        {
            foreach (var item in LanguageItems)
            {
                try
                {
                    if (_subtitleItems.TryGetValue(item.Key, out var originals))
                    {

                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return;
                }
            }
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
