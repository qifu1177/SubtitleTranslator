using SubtitleTranslator.ViewModels.PopupViewModels;
using SubtitleTranslator.Views.Abstracts;

namespace SubtitleTranslator.Views;

public partial class ExportFileTypeView : ExportFileTypeViewAbstract
{
    public ExportFileTypeView()
    {
        InitializeComponent();
    }

    private void OkClick(object sender, EventArgs e)
    {
        ViewModel.Save();
        this.Close(true);
    }

    private void CloseClick(object sender, EventArgs e)
    {
        this.Close();
    }
}
public abstract class ExportFileTypeViewAbstract : ModelPopupPage<ExportFileTypeViewModel>
{ }