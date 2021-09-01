using Microsoft.Toolkit.Mvvm.Messaging;
using NSubstitute;
using UI.Commands;
using UI.Dialogs;
using UI.Dialogs.FolderBrowser;
using Xunit;

namespace Tests.Unit.Commands
{
	public class ChangeDirectoryCommandTests : TestBase<ChangeCurrentDirectoryHandler>
	{
		private readonly IMessenger     _messenger         = Substitute.For<IMessenger>();
		private readonly IDialogFactory _dialogFactoryMock = Substitute.For<IDialogFactory>();

		/// <inheritdoc />
		protected override ChangeCurrentDirectoryHandler CreateSUT()
			=> new(_messenger, _dialogFactoryMock);

		private IFolderBrowserDialog SetupFolderBrowser(string path)
		{
			var dialog = Substitute.For<IFolderBrowserDialog>();

			if (path != null)
			{
				dialog.ShowDialog().Returns(true);
				dialog.SelectedPath.Returns(path);
			}
			else
			{
				dialog.ShowDialog().Returns(false);
			}

			_dialogFactoryMock.CreateFolderBrowserDialog().Returns(dialog);

			return dialog;
		}


		// GIVEN WHEN THEN
		[Fact]
		public void GivenRequest_ThenShowsFolderBrowserDialog()
		{
			var dialog = SetupFolderBrowser(null);

			SUT.Receive(new ChangeCurrentDirectoryRequest());

			dialog.Received(1).ShowDialog();
		}


		[Fact]
		public void GivenRequest_WhenFolderSelected_ThenPublishesEvent()
		{
			SetupFolderBrowser("SOME PATH");

			SUT.Receive(new ChangeCurrentDirectoryRequest());

			_messenger.Received().Send(Arg.Is<CurrentDirectoryChangedEvent>(ev => ev.Path.Equals("SOME PATH")));
		}
	}
}