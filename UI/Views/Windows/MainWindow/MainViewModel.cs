using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;

namespace UI
{
	public class MainViewModel : ViewModelBase
	{
		private readonly ISender _mediator;
		private          string  _currentDirectory = "No Directory Selected";

		public string CurrentDirectory
		{
			get => _currentDirectory;
			private set => SetProperty(ref _currentDirectory, value);
		}

		public IAsyncRelayCommand PickFolder { get; }

		public MainViewModel(ISender mediator, IMessenger messenger) : base(messenger)
		{
			_mediator  = mediator;
			PickFolder = new AsyncRelayCommand(UpdateDirectory);
		}

		private async Task UpdateDirectory(CancellationToken token)
		{
			string directory = await _mediator.Send(new ChangeCurrentDirectoryRequest(), token);

			if (directory != null)
			{
				CurrentDirectory = directory;
			}
		}
	}
}