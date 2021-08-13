using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UI.Dialogs;

namespace UI.Commands
{
	public class ChangeCurrentDirectoryRequest : IRequest<string> { }

	public class CurrentDirectoryChangedEvent : INotification
	{
		public string Path { get; set; }
	}

	public class ChangeCurrentDirectoryHandler : IRequestHandler<ChangeCurrentDirectoryRequest, string>
	{
		private readonly IMediator      _mediator;
		private readonly IDialogFactory _dialogs;

		public ChangeCurrentDirectoryHandler(IMediator mediator, IDialogFactory dialogs)
		{
			_mediator = mediator;
			_dialogs  = dialogs;
		}

		/// <inheritdoc />
		public async Task<string> Handle(ChangeCurrentDirectoryRequest request, CancellationToken token)
		{
			var dialog = _dialogs.CreateFolderBrowserDialog();

			dialog.ShowDialog();
			
			await _mediator.Publish(new CurrentDirectoryChangedEvent { Path = dialog.SelectedPath }, token);
			return dialog.SelectedPath;
		}
	}
}