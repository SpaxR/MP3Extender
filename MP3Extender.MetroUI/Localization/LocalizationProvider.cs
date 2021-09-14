using System.Globalization;
using System.Resources;

namespace MP3Extender.MetroUI.Localization
{
	public class LocalizationProvider
	{
		private readonly ResourceManager _manager;

		public string this[string key] => _manager.GetString(key, CurrentCulture);

		public CultureInfo CurrentCulture
		{
			get => CultureInfo.CurrentUICulture;
			set => CultureInfo.CurrentUICulture = value;
		}

		// Todo Refactor Singleton-Pattern
		public static LocalizationProvider Instance { get; } = new();

		public LocalizationProvider()
		{
			_manager = Strings.ResourceManager;
		}
	}
}