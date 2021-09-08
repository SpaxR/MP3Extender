using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.WPF.Services;

namespace MP3Extender.WPF.ViewModels
{
	public class FileListViewModel : ViewModelBase, IRecipient<DirectoryChangedEvent>
	{
		private string _currentDirectory;

		public string CurrentDirectory
		{
			get => _currentDirectory;
			private set => SetProperty(ref _currentDirectory, value);
		}

		public ObservableCollection<string> Files { get; } = new();

		public FileListViewModel(IMessenger messenger) : base(messenger)
		{
			Files = new ObservableCollection<string>
			{
				"TestFile 1",
				"TestFile 2",
				"TestFile 3",
			};
		}

		/// <inheritdoc />
		public void Receive(DirectoryChangedEvent message)
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