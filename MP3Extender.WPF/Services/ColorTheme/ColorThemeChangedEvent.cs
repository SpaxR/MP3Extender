using MP3Extender.Application;

namespace MP3Extender.WPF.Services
{
	public class ColorThemeChangedEvent
	{
		public ColorTheme Theme { get; }

		public ColorThemeChangedEvent(ColorTheme theme) => Theme = theme;
	}
}