using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.WPF.Factories;

namespace MP3Extender.WPF.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public FileListViewModel Files { get; }

		public ICommand OpenSettingsWindow { get; }

		public MainViewModel(IMessenger messenger, IDialogFactory dialogFactory) : base(messenger)
		{
			Files              = new FileListViewModel(messenger);
			OpenSettingsWindow = new RelayCommand(() => dialogFactory.CreateSettingsWindow().ShowDialog());
		}
	}
}