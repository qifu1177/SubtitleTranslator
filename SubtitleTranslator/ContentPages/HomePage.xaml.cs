using App.UI.Infrastructure.Bases;
using CommunityToolkit.Maui.Views;
using SubtitleTranslator.ViewModels;

namespace SubtitleTranslator.ContentPages;

public partial class HomePage : ModelHomePage
{
    public HomePage()
    {
        InitializeComponent();
        ViewModel.StartLoad = () =>
        {
            refreshView.IsRefreshing = true;
        };
        ViewModel.StopLoad = () =>
        {
            refreshView.IsRefreshing = false;
        };

    }

    private void PageLoaded(object sender, EventArgs e)
    {
        ViewModel.UpdateLanguageItems();
    }

    private void SizeChanged(object sender, EventArgs e)
    {
        ContentPage view = sender as ContentPage;
        ViewModel.SizeViewModel.Width = view.Frame.Width;
        ViewModel.SizeViewModel.Height = view.Frame.Height;
    }

    private void RefreshingLoad(object sender, EventArgs e)
    {
        if (!ViewModel.IsLoading)
            refreshView.IsRefreshing = false;
    }

    private void ErrorMessageClick(object sender, EventArgs e)
    {
        ViewModel.ErrorMessage = string.Empty;
    }
}
public abstract class ModelHomePage : ModelContentPage<HomeViewModel>
{
}