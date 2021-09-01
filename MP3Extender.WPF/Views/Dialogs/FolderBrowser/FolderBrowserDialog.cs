using System.Diagnostics.CodeAnalysis;
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
		private readonly System.Windows.Forms.FolderBrowserDialog _instance = new();

		/// <inheritdoc />
		public string SelectedPath => _instance.SelectedPath;

		/// <inheritdoc />
		[ExcludeFromCodeCoverage]
		public bool? ShowDialog() => _instance.ShowDialog() == DialogResult.OK;
	}
}