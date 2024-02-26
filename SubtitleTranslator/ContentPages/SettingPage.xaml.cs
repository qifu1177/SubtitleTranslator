using App.UI.Infrastructure.Bases;
using SubtitleTranslator.ViewModels;

namespace SubtitleTranslator.ContentPages;

public partial class SettingPage : ModelSettingPage
{
    public SettingPage() : this(null) { }
    public SettingPage(UiSettingViewModel viewModel) : base(viewModel)
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
}
public abstract class ModelSettingPage : ModelContentPage<UiSettingViewModel>
{
    public ModelSettingPage(UiSettingViewModel viewModel) : base(viewModel) { }
}