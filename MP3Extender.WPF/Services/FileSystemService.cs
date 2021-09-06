using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.WPF.Factories;

namespace MP3Extender.WPF.Services
{
	public class DirectoryChangedEvent
	{
		public string Path { get; }

		public DirectoryChangedEvent(string path) => Path = path;
	}

	public interface IFileSystemService
	{
		public string RootDirectoryPath { get; }

		public void ChangeRootDirectory();
	}
	
	public class FileSystemService : IFileSystemService
	{
		private readonly IMessenger     _messenger;
		private readonly IDialogFactory _dialogFactory;

		public string RootDirectoryPath { get; private set; }

		public FileSystemService(IMessenger messenger, IDialogFactory dialogFactory)
		{
			_messenger     = messenger;
			_dialogFactory = dialogFactory;
		}

		public void ChangeRootDirectory()
		{
			var dialog = _dialogFactory.CreateFolderBrowserDialog();

			dialog.ShowDialog();

			RootDirectoryPath = dialog.SelectedPath;
			_messenger.Send(new DirectoryChangedEvent(dialog.SelectedPath));
		}
	}
}