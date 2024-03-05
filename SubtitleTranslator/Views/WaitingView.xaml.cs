using SubtitleTranslator.ViewModels.PopupViewModels;
using SubtitleTranslator.Views.Abstracts;

namespace SubtitleTranslator.Views;

public partial class WaitingView : WaitingViewAbstract
{
	public WaitingView()
	{
		InitializeComponent();
	}

    private async void PageLoaded(object sender, EventArgs e)
    {
		await ViewModel.ExcuitProcess();
		this.Close();
    }
}
public abstract class WaitingViewAbstract : ModelPopupPage<WaitingViewModel>
{ }