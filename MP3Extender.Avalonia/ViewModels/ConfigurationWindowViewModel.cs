using System.Windows.Input;
using MP3Extender.Application;
using ReactiveUI;

namespace MP3Extender.Avalonia.ViewModels
{
	public class ConfigurationWindowViewModel : ViewModelBase
	{
		private readonly ISettings  _settings;
		private          ColorTheme _theme;
		private          string     _rootFolderPath;

		public ColorTheme Theme
		{
			get => _theme;
			private set
			{
				_settings.Theme = value;
				this.RaiseAndSetIfChanged(ref _theme, value);
			}
		}

		public string RootFolderPath
		{
			get => _rootFolderPath;
			private set => this.RaiseAndSetIfChanged(ref _rootFolderPath, value);
		}

		public ICommand ChangeColorTheme { get; }
		public ICommand ChangeRootFolder { get; }

		/// <inheritdoc />
		public ConfigurationWindowViewModel(ISettings settings)
		{
			_settings       = settings;
			_theme          = settings.Theme;
			_rootFolderPath = settings.RootFolder;

			ChangeColorTheme = ReactiveCommand.Create<ColorTheme>(theme => settings.Theme = theme);
			ChangeRootFolder = ReactiveCommand.Create<string>(dir => settings.RootFolder  = dir);
		}
	}
}