using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MP3Extender.WPF.Commands
{
	public class ChangeDirectoryRequest : IRequest { }

	public class DirectoryChangedEvent : INotification
	{
		public DirectoryChangedEvent(string path) => Path = path;

		public string Path { get; }
	}

	public class ChangeDirectoryHandler : IRequestHandler<ChangeDirectoryRequest>
	{
		private readonly IMediator      _mediator;
		private readonly IDialogFactory _dialogFactory;

		public ChangeDirectoryHandler(IMediator mediator, IDialogFactory dialogFactory)
		{
			_mediator      = mediator;
			_dialogFactory = dialogFactory;
		}

		/// <inheritdoc />
		public Task<Unit> Handle(ChangeDirectoryRequest request, CancellationToken token)
		{
			var dialog = _dialogFactory.CreateFolderBrowserDialog();

			dialog.ShowDialog();

			_mediator.Publish(new DirectoryChangedEvent(dialog.SelectedPath), token);
			return Unit.Task;
		}
	}
}