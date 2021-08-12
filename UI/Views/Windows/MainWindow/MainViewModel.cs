using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Toolkit.Mvvm.Input;
using UI.Commands;

namespace UI
{
	public class MainViewModel : ViewModelBase
	{
		private string            _currentDirectory = "No Directory Selected";

		public string CurrentDirectory
		{
			get => _currentDirectory;
			private set => SetProperty(ref _currentDirectory, value);
		}

		public IAsyncRelayCommand PickFolder { get; set; }

		public MainViewModel(ISender mediator)
		{
			PickFolder = new AsyncRelayCommand(token => mediator.Send(new ChangeCurrentDirectoryRequest(), token));
		}

		public class NotificationHandler : INotificationHandler<CurrentDirectoryChangedEvent>
		{
			private readonly MainViewModel _model;

			public NotificationHandler(MainViewModel model) => _model = model;

			public Task Handle(CurrentDirectoryChangedEvent notification, CancellationToken cancellationToken)
			{
				_model.CurrentDirectory = notification.Path;
				return Task.CompletedTask;
			}
		}
	}
}