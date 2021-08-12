using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;

namespace UI.Controls.FileList
{
	public class FileListViewModel : ViewModelBase, IRecipient<CurrentDirectoryChangedEvent>
	{
		public FileListViewModel(IMessenger messenger) : base(messenger) { }

		public ObservableCollection<string> Files { get; } = new();
		
		/// <inheritdoc />
		public void Receive(CurrentDirectoryChangedEvent message)
		{
			string[] files = Directory.GetFiles(message.Path);
			Files.Clear();

			foreach (string file in files)
			{
				Files.Add(file);
			}
		}
	}
}