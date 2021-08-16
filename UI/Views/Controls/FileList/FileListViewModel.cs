using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;

namespace UI.Controls.FileList
{
	public interface IFileListViewModel : IRecipient<CurrentDirectoryChangedEvent>
	{
		public string                       CurrentDirectory { get; }
		public ObservableCollection<string> Files            { get; }
	}

	public class FileListViewModel : ViewModelBase, IFileListViewModel
	{
		private string _currentDirectory;

		public string CurrentDirectory
		{
			get => _currentDirectory;
			private set => SetProperty(ref _currentDirectory, value);
		}

		public ObservableCollection<string> Files { get; } = new();

		public FileListViewModel(IMessenger messenger) : base(messenger) { }

		/// <inheritdoc />
		public void Receive(CurrentDirectoryChangedEvent message)
		{
			if (Directory.Exists(message.Path))
			{
				CurrentDirectory = message.Path;
				LoadFiles();
			}
			else
			{
				CurrentDirectory = "Directory not Found";
				Files.Clear();
			}
		}

		private void LoadFiles()
		{
			Files.Clear();
			foreach (string file in Directory.EnumerateFiles(CurrentDirectory))
			{
				Files.Add(file);
			}
		}
	}
}