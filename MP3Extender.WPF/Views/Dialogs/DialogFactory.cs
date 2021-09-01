using UI.Dialogs.FolderBrowser;

namespace UI.Dialogs
{
	public interface IDialogFactory
	{
		public IFolderBrowserDialog CreateFolderBrowserDialog();
	}

	public class DialogFactory : IDialogFactory
	{
		/// <inheritdoc />
		public IFolderBrowserDialog CreateFolderBrowserDialog() => new FolderBrowserDialog();
	}
}