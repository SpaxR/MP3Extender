using MediatR;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;

namespace UI
{
	public class MainViewModel : ViewModelBase, IRecipient<CurrentDirectoryChangedEvent>
	{
		private string _currentDirectory = "No Directory Selected";

		public string CurrentDirectory
		{
			get => _currentDirectory;
			private set => SetProperty(ref _currentDirectory, value);
		}

		public IAsyncRelayCommand PickFolder { get; }

		public MainViewModel(ISender mediator, IMessenger messenger) : base(messenger)
		{
			PickFolder = new AsyncRelayCommand(token => mediator.Send(new ChangeCurrentDirectoryRequest(), token));
		}

		/// <inheritdoc />
		public void Receive(CurrentDirectoryChangedEvent message)
		{
			CurrentDirectory = message.Path;
		}
	}
}