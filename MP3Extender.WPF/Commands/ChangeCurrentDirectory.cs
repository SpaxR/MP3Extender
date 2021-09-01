using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Dialogs;

namespace UI.Commands
{
	public class ChangeCurrentDirectoryRequest { }

	public class CurrentDirectoryChangedEvent
	{
		public string Path { get; set; }
	}

	public class ChangeCurrentDirectoryHandler : IRecipient<ChangeCurrentDirectoryRequest>
	{
		private readonly IMessenger     _messenger;
		private readonly IDialogFactory _dialogs;

		public ChangeCurrentDirectoryHandler(IMessenger messenger, IDialogFactory dialogs)
		{
			_messenger = messenger;
			_dialogs   = dialogs;
		}

		/// <inheritdoc />
		public void Receive(ChangeCurrentDirectoryRequest message)
		{
			var dialog = _dialogs.CreateFolderBrowserDialog();

			dialog.ShowDialog();

			_messenger.Send(new CurrentDirectoryChangedEvent { Path = dialog.SelectedPath });
		}
	}
}