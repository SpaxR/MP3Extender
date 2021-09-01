using Microsoft.Toolkit.Mvvm.Messaging;
using Moq;
using NSubstitute;
using UI.Commands;
using UI.Dialogs;
using UI.Dialogs.FolderBrowser;
using Xunit;

namespace Tests.Unit.Commands
{
	public class ChangeDirectoryCommandTests : TestBase<ChangeCurrentDirectoryHandler>
	{
		private readonly IMessenger           _messenger          = Substitute.For<IMessenger>();
		private readonly Mock<IDialogFactory> _dialogFactoryMock = new();

		/// <inheritdoc />
		protected override ChangeCurrentDirectoryHandler CreateSUT()
			=> new(_messenger, _dialogFactoryMock.Object);

		private Mock<IFolderBrowserDialog> SetupFolderBrowser(string path)
		{
			var dialog = new Mock<IFolderBrowserDialog>();

			if (path != null)
			{
				dialog.Setup(d => d.ShowDialog()).Returns(true);
				dialog.Setup(d => d.SelectedPath).Returns(path);
			}
			else
			{
				dialog.Setup(d => d.ShowDialog()).Returns(false);
			}

			_dialogFactoryMock
				.Setup(fac => fac.CreateFolderBrowserDialog())
				.Returns(dialog.Object);

			return dialog;
		}


		// GIVEN WHEN THEN
		[Fact]
		public void GivenRequest_ThenShowsFolderBrowserDialog()
		{
			var dialog = SetupFolderBrowser(null);

			SUT.Receive(new ChangeCurrentDirectoryRequest());

			dialog.Verify(d => d.ShowDialog());
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