using System.Collections.ObjectModel;
using System.IO;
using ReactiveUI;

namespace MP3Extender.Avalonia.ViewModels
{
	public class FileListViewModel : ViewModelBase
	{
		private string _currentDirectory;

		public string CurrentDirectory
		{
			get => _currentDirectory;
			private set => this.RaiseAndSetIfChanged(ref _currentDirectory, value);
		}

		public ObservableCollection<string> Files { get; } = new();

		public FileListViewModel()
		{
			Files = new ObservableCollection<string>
			{
				"TestFile 1",
				"TestFile 2",
				"TestFile 3",
			};
		}

		// /// <inheritdoc />
		// public void Receive()
		// {
		// 	if (Directory.Exists(message.Path))
		// 	{
		// 		CurrentDirectory = message.Path;
		// 		LoadFiles();
		// 	}
		// 	else
		// 	{
		// 		CurrentDirectory = "Directory not Found";
		// 		Files.Clear();
		// 	}
		// }

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