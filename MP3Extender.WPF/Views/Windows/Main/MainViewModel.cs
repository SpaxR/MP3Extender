using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;
using UI.Controls.FileList;

namespace UI
{
	public class MainViewModel : ViewModelBase
	{
		public FileListViewModel Files { get; }

		public IRelayCommand OpenSettingsWindow { get; }


		public MainViewModel(IMessenger messenger) : base(messenger)
		{
			Files              = new FileListViewModel(messenger);
			OpenSettingsWindow = new RelayCommand(() => Messenger.Send<OpenSettingsWindowRequest>());
		}
	}
}