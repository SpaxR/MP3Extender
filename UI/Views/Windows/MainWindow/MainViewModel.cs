using MediatR;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;

namespace UI
{
	public class MainViewModel : ObservableRecipient, IRecipient<CurrentDirectoryChangedEvent>
	{
		private string _currentDirectory = "No Directory Selected";

		public string CurrentDirectory
		{
			get => _currentDirectory;
			private set => SetProperty(ref _currentDirectory, value);
		}

		public IAsyncRelayCommand PickFolder { get; set; }

		public MainViewModel(ISender mediator, IMessenger messenger) : base(messenger)
		{
			PickFolder = new AsyncRelayCommand(token => mediator.Send(new ChangeCurrentDirectoryRequest(), token));
			IsActive   = true; // TODO: Figure out why IsActive is not automatically true
		}

		/// <inheritdoc />
		public void Receive(CurrentDirectoryChangedEvent message)
		{
			CurrentDirectory = message.Path;
		}
	}
}