namespace MP3Extender.WPF
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