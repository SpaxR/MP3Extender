using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.WPF.Commands;

namespace MP3Extender.WPF.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public FileListViewModel Files { get; }

		public ICommand OpenSettingsWindow { get; }

		public MainViewModel(IMessenger messenger) : base(messenger)
		{
			Files              = new FileListViewModel(messenger);
			OpenSettingsWindow = new RelayCommand(() => messenger.Send<OpenSettingsWindowRequest>());
		}
	}
}