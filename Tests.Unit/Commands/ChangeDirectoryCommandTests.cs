using System.Threading;
using MP3Extender.WPF;
using MP3Extender.WPF.Commands;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Commands
{
	public class ChangeDirectoryCommandTests : TestBase<ChangeDirectoryHandler>
	{
		private readonly IDialogFactory _dialogFactoryMock = Substitute.For<IDialogFactory>();

		/// <inheritdoc />
		protected override ChangeDirectoryHandler CreateSUT()
			=> new(MediatorMock, _dialogFactoryMock);

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


		[Fact]
		public void GivenValidRequest_ThenShowsFolderBrowserDialog()
		{
			var dialog = SetupFolderBrowser(null);

			SUT.Handle(new ChangeDirectoryRequest(), CancellationToken.None);

			dialog.Received(1).ShowDialog();
		}


		[Fact]
		public void GivenRequest_WhenFolderSelected_ThenPublishesEvent()
		{
			SetupFolderBrowser("SOME PATH");

			SUT.Handle(new ChangeDirectoryRequest(), default);

			MediatorMock
				.Received(1)
				.Publish(Arg.Is<DirectoryChangedEvent>(ev => "SOME PATH".Equals(ev.Path)));
		}
	}
}