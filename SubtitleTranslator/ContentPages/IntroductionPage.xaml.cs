using App.UI.Infrastructure.Bases;
using SubtitleTranslator.ViewModels;

namespace SubtitleTranslator.ContentPages;

public partial class IntroductionPage : ModelIntroductionPage
{
    public IntroductionPage()
    {
        InitializeComponent();
    }
   
    private void PageLoaded(object sender, EventArgs e)
    {
        ViewModel.Init();
    }
}
public abstract class ModelIntroductionPage : ModelContentPage<IntroductionViewModel>
{
}