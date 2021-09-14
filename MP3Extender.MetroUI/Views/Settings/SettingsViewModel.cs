using MP3Extender.Application;
using MP3Extender.MetroUI.Common;

namespace MP3Extender.MetroUI.Views.Settings
{
	public class SettingsViewModel : ViewModelBase
	{
		public ISettings Settings { get; }

		public SettingsViewModel(ISettings settings)
		{
			Settings = settings;
		}
	}
}