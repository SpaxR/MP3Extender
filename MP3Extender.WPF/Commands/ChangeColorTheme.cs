using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using AdonisUI;
using MediatR;
using MP3Extender.Application;

namespace MP3Extender.WPF.Commands
{
	public class ChangeColorThemeRequest : IRequest
	{
		public ChangeColorThemeRequest(string theme) => Theme = theme;

		public string Theme { get; }
	}

	public class SettingsChangedEvent : INotification { }

	public class ChangeColorThemeHandler : IRequestHandler<ChangeColorThemeRequest>
	{
		private readonly IMediator _mediator;
		private readonly ISettings _settings;

		public ChangeColorThemeHandler(IMediator mediator, ISettings settings)
		{
			_mediator = mediator;
			_settings = settings;
		}

		/// <inheritdoc />
		[ExcludeFromCodeCoverage(Justification = "Uses static class and Resources of AdonisUI")]
		public Task<Unit> Handle(ChangeColorThemeRequest request, CancellationToken token)
		{
			Uri theme = request.Theme switch
			{
				"Light"   => ResourceLocator.LightColorScheme,
				"Dark"    => ResourceLocator.DarkColorScheme,
				"Classic" => ResourceLocator.ClassicTheme,
				_         => null
			};

			if (theme != null)
			{
				ResourceLocator.SetColorScheme(System.Windows.Application.Current.Resources, theme);
				_settings.ColorTheme = request.Theme;
				_mediator.Publish(new SettingsChangedEvent(), token);
			}

			return Unit.Task;
		}
	}
}