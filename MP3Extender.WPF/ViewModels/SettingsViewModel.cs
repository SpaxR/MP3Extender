using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.Application;
using MP3Extender.WPF.Services;

namespace MP3Extender.WPF.ViewModels
{
	public interface ISettingsViewModel
	{
		public ColorTheme Theme          { get; }
		public string     RootFolderPath { get; }

		public ICommand ChangeColorTheme { get; }
		public ICommand ChangeRootFolder { get; }
	}

	public class SettingsViewModel : ViewModelBase, ISettingsViewModel
	{
		private ColorTheme _theme;
		private string     _rootFolderPath;

		public ColorTheme Theme
		{
			get => _theme;
			private set => SetProperty(ref _theme, value);
		}

		public string RootFolderPath
		{
			get => _rootFolderPath;
			private set => SetProperty(ref _rootFolderPath, value);
		}

		public ICommand ChangeColorTheme { get; }
		public ICommand ChangeRootFolder { get; }

		/// <inheritdoc />
		public SettingsViewModel(IMessenger         messenger,
								 IColorThemeService themeService,
								 IFileSystemService fileSystemService)
			: base(messenger)
		{
			_theme          = themeService.Theme;
			_rootFolderPath = fileSystemService.RootDirectoryPath;

			ChangeColorTheme = new RelayCommand<ColorTheme>(themeService.SetTheme);
			ChangeRootFolder = new RelayCommand(fileSystemService.ChangeRootDirectory);
			messenger.Register<ColorThemeChangedEvent>(this, (_, ev) => Theme          = ev.Theme);
			messenger.Register<DirectoryChangedEvent>(this, (_,  ev) => RootFolderPath = ev.Path);
		}
	}
}