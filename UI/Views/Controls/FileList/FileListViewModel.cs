using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;

namespace UI.Controls.FileList
{
	public class FileListViewModel : ViewModelBase, IRecipient<CurrentDirectoryChangedEvent>
	{
		public ObservableCollection<string> Files { get; } = new();

		public FileListViewModel(IMessenger messenger) : base(messenger) { }

		/// <inheritdoc />
		public void Receive(CurrentDirectoryChangedEvent message)
		{
			Files.Clear();

			foreach (string file in Directory.GetFiles(message.Path))
			{
				Files.Add(file);
			}
		}
	}
}