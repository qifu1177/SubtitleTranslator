using App.UI.Infrastructure.Bases;
using SubtitleTranslator.ViewModels;
using SubtitleTranslator.ViewModels.Items;

namespace SubtitleTranslator.ContentPages;

public partial class SettingPage : ModelSettingPage
{

    public SettingPage()
    {
        InitializeComponent();
    }

    private void UseOpenAiClieck(object sender, TappedEventArgs e)
    {
        ViewModel.UseOpenAi = !ViewModel.UseOpenAi;
    }

    private void ModelSettingPage_Unloaded(object sender, EventArgs e)
    {
        ViewModel.Save();
    }


    private void OpenAiKeyChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel.UpdateApiClient();
    }

    private void AppLanguageChanged(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        if (picker.SelectedItem is LanguageItemViewModel selecedItem)
        {
            if (ViewModel.AfterAppLanguageChanged(selecedItem))
            {
                
            }
        }
           
    }
}
public abstract class ModelSettingPage : ModelContentPage<UiSettingViewModel>
{
}