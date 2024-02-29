using App.UI.Infrastructure.Bases;
using CommunityToolkit.Maui.Views;
using SubtitleTranslator.ViewModels;

namespace SubtitleTranslator.ContentPages;

public partial class HomePage : ModelHomePage
{
	public HomePage()
	{
        InitializeComponent();
    }
	
    private void PageLoaded(object sender, EventArgs e)
    {
		ViewModel.UpdateLanguageItems();
    }
}
public abstract class ModelHomePage : ModelContentPage<HomeViewModel>
{ 	
}