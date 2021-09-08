using System.Windows.Input;
using ReactiveUI;

namespace MP3Extender.Avalonia.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public FileListViewModel Files { get; }

		public ICommand OpenSettingsWindow { get; }

		public MainWindowViewModel()
		{
			Files              = new FileListViewModel();
			// OpenSettingsWindow = ReactiveCommand.Create(() => dialogFactory.CreateSettingsWindow().ShowDialog());
			OpenSettingsWindow = ReactiveCommand.Create(() => {});
		}
	}
}