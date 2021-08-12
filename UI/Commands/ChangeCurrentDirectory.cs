using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ookii.Dialogs.Wpf;

namespace UI.Commands
{
	public class ChangeCurrentDirectoryRequest : IRequest { }

	public class CurrentDirectoryChangedEvent : INotification
	{
		public string Path { get; set; }
	}

	public class ChangeCurrentDirectoryHandler : IRequestHandler<ChangeCurrentDirectoryRequest>
	{
		private readonly IMediator _mediator;

		public ChangeCurrentDirectoryHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <inheritdoc />
		public async Task<Unit> Handle(ChangeCurrentDirectoryRequest request, CancellationToken token)
		{
			var dialog = new VistaFolderBrowserDialog();

			if (dialog.ShowDialog() == true)
			{
				await _mediator.Publish(new CurrentDirectoryChangedEvent { Path = dialog.SelectedPath }, token);
			}

			return Unit.Value;
		}
	}
}