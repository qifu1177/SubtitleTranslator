using CommunityToolkit.Maui.Views;
using SubtitleTranslator.ViewModels.PopupViewModels;
using SubtitleTranslator.Views.Abstracts;

namespace SubtitleTranslator.Views;

public partial class EditSubtitleView : EditSubtitleViewAbstract
{
    public EditSubtitleView()
    {
        InitializeComponent();
        
    }

    private void CloseClick(object sender, EventArgs e)
    {
        this.Close();
    }

    private void OkClick(object sender, EventArgs e)
    {
        ViewModel.Save();
        this.Close(true);
    }
}
public abstract class EditSubtitleViewAbstract : ModelPopupPage<EditSubtitleViewModel>
{ }
