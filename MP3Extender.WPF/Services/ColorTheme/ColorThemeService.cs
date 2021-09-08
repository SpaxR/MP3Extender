using System;
using System.Diagnostics.CodeAnalysis;
using AdonisUI;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.Application;

namespace MP3Extender.WPF.Services
{
	public interface IColorThemeService
	{
		public ColorTheme Theme { get; }

		public void SetTheme(ColorTheme theme);
	}

	public class ColorThemeService : IColorThemeService
	{
		private readonly IMessenger _messenger;
		private readonly ISettings  _settings;

		public ColorTheme Theme => _settings.Theme;

		public ColorThemeService(IMessenger messenger, ISettings settings)
		{
			_messenger = messenger;
			_settings  = settings;
		}

		[ExcludeFromCodeCoverage(Justification = "Uses static class and Resources of AdonisUI")]
		public void SetTheme(ColorTheme theme)
		{
			Uri adonisTheme = theme switch
			{
				ColorTheme.Light   => ResourceLocator.LightColorScheme,
				ColorTheme.Dark    => ResourceLocator.DarkColorScheme,
				ColorTheme.Classic => ResourceLocator.ClassicTheme,
				_                  => null
			};

			if (adonisTheme != null)
			{
				ResourceLocator.SetColorScheme(System.Windows.Application.Current.Resources, adonisTheme);
				_settings.Theme = theme;
				_messenger.Send(new ColorThemeChangedEvent(theme));
			}
		}
	}
}