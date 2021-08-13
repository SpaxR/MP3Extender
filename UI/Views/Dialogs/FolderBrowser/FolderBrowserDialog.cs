using System.Windows.Forms;

namespace UI.Dialogs.FolderBrowser
{
	public interface IFolderBrowserDialog
	{
		public string SelectedPath { get; }

		public bool? ShowDialog();
	}

	public class FolderBrowserDialog : IFolderBrowserDialog
	{
		private readonly System.Windows.Forms.FolderBrowserDialog _dialog = new();

		/// <inheritdoc />
		public string SelectedPath => _dialog.SelectedPath;

		/// <inheritdoc />
		public bool? ShowDialog() => _dialog.ShowDialog() == DialogResult.OK;
	}
}