using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;
using UI.Configuration;

namespace UI.Settings
{
	public class SettingsViewModel : ViewModelBase
	{
		private readonly IConfig _config;

		public bool   UseDarkTheme   => _config.UseDarkTheme;
		public string RootFolderPath => _config.RootFolder;

		public ICommand ChangeTheme      { get; }
		public ICommand ChangeRootFolder { get; }

		/// <inheritdoc />
		public SettingsViewModel(IMessenger messenger, IConfig config) : base(messenger)
		{
			_config          = config;
			ChangeTheme      = new RelayCommand(() => _config.UseDarkTheme = !_config.UseDarkTheme);
			ChangeRootFolder = new RelayCommand(() => Messenger.Send<ChangeCurrentDirectoryRequest>());
		}
	}
}