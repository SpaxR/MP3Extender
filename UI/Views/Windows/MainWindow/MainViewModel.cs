using MediatR;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Commands;
using UI.Controls.FileList;

namespace UI
{
	public interface IMainViewModel
	{
		public IAsyncRelayCommand PickFolder { get; }
		public IFileListViewModel  Files      { get; }
	}

	public class MainViewModel : ViewModelBase, IMainViewModel
	{
		public IAsyncRelayCommand PickFolder { get; }

		/// <inheritdoc />
		public IFileListViewModel Files { get; }

		public MainViewModel(ISender mediator, IMessenger messenger) : base(messenger)
		{
			Files      = new FileListViewModel(messenger);
			PickFolder = new AsyncRelayCommand(token => mediator.Send(new ChangeCurrentDirectoryRequest(), token));
		}
	}
}