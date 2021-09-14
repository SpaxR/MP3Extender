using MP3Extender.MetroUI.Common;
using MP3Extender.MetroUI.Views.About;
using MP3Extender.MetroUI.Views.Editor;
using MP3Extender.MetroUI.Views.Settings;

namespace MP3Extender.MetroUI.Views
{
	public class MainViewModel : ViewModelBase
	{
		public EditorViewModel   EditorVM   { get; }
		public SettingsViewModel SettingsVM { get; }
		public AboutViewModel    AboutVM    { get; }

		public MainViewModel(EditorViewModel editorVM, SettingsViewModel settingsVM, AboutViewModel aboutVM)
		{
			EditorVM   = editorVM;
			SettingsVM = settingsVM;
			AboutVM    = aboutVM;
		}
	}
}