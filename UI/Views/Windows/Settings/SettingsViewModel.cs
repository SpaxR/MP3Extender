using Microsoft.Toolkit.Mvvm.Messaging;

namespace UI.Settings
{
	public class SettingsViewModel : ViewModelBase
	{
		/// <inheritdoc />
		public SettingsViewModel(IMessenger messenger) : base(messenger) { }
	}
}