using App.UI.Infrastructure.Bases;
using SubtitleTranslator.ViewModels;

namespace SubtitleTranslator.ContentPages;

public partial class HomePage : ModelHomePage
{
	public HomePage():this(null)
	{
	}
	public HomePage(HomeViewModel model):base(model)
	{
		InitializeComponent();

    }
}
public abstract class ModelHomePage : ModelContentPage<HomeViewModel>
{ 
	public ModelHomePage(HomeViewModel model):base(model)
	{ }
}