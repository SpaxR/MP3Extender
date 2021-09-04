using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MP3Extender.WPF.ViewModels;

namespace MP3Extender.WPF.Commands
{
	public class OpenSettingsWindowRequest : IRequest<SettingsWindow> { }

	public class OpenSettingsWindowHandler : IRequestHandler<OpenSettingsWindowRequest, SettingsWindow>
	{
		private readonly SettingsViewModel _viewModel;

		public OpenSettingsWindowHandler(SettingsViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		/// <inheritdoc />
		[ExcludeFromCodeCoverage(Justification = "Instantiates and Shows Window, blocks until closed")]
		public Task<SettingsWindow> Handle(OpenSettingsWindowRequest request, CancellationToken cancellationToken)
		{
			var window = new SettingsWindow
			{
				Owner       = System.Windows.Application.Current?.MainWindow,
				DataContext = _viewModel
			};

			window.ShowDialog();

			return Task.FromResult(window);
		}
	}
}