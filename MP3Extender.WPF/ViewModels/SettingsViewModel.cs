using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.Application;
using MP3Extender.WPF.Commands;

namespace MP3Extender.WPF.ViewModels
{
	public class SettingsViewModel : ViewModelBase, IRecipient<SettingsChangedEvent>
	{
		private readonly ISettings _settings;

		public string ColorTheme     => _settings.ColorTheme;
		public string RootFolderPath => _settings.RootFolder;

		public ICommand ChangeColorTheme { get; }
		public ICommand ChangeRootFolder { get; }

		/// <inheritdoc />
		public SettingsViewModel(IMessenger messenger, ISettings settings) : base(messenger)
		{
			_settings        = settings;
			ChangeColorTheme = new RelayCommand<string>(theme => Messenger.Send(new ChangeColorThemeRequest(theme)));
			ChangeRootFolder = new RelayCommand(() => Messenger.Send<ChangeDirectoryRequest>());
		}

		/// <inheritdoc />
		public void Receive(SettingsChangedEvent message) => OnPropertyChanged();
	}
}