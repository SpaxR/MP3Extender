using System.Threading;
using System.Threading.Tasks;
using Moq;
using UI.Commands;
using UI.Dialogs;
using UI.Dialogs.FolderBrowser;
using Xunit;

namespace Tests.Unit.Commands
{
	public class ChangeDirectoryCommandTests : TestBase<ChangeCurrentDirectoryHandler>
	{
		private readonly ChangeCurrentDirectoryRequest _request = new();
		private readonly CancellationToken             _token   = CancellationToken.None;

		private readonly Mock<IDialogFactory> _dialogFactoryMock = new();

		/// <inheritdoc />
		protected override ChangeCurrentDirectoryHandler CreateSUT()
			=> new(MediatorMock.Object, _dialogFactoryMock.Object);

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
		public async Task GivenRequest_ThenShowsFolderBrowserDialog()
		{
			var dialog = SetupFolderBrowser(null);

			await SUT.Handle(_request, _token);

			dialog.Verify(d => d.ShowDialog());
		}

		[Fact]
		public async Task GivenRequest_WhenFolderSelected_ThenReturnsPath()
		{
			SetupFolderBrowser("SOME PATH");

			string result = await SUT.Handle(_request, _token);

			Assert.Equal("SOME PATH", result);
		}

		[Fact]
		public async Task GivenRequest_WhenSelectionAborted_ThenReturnNull()
		{
			SetupFolderBrowser(null);

			string result = await SUT.Handle(_request, _token);

			Assert.Null(result);
		}

		[Fact]
		public async Task GivenRequest_WhenFolderSelected_ThenPublishesEvent()
		{
			SetupFolderBrowser("SOME PATH");

			await SUT.Handle(_request, _token);

			MediatorMock.Verify(m => m.Publish(It.IsAny<CurrentDirectoryChangedEvent>(), _token));
		}

		[Fact]
		public async Task GivenRequest_WhenEventPublished_ThenEventContainsPath()
		{
			SetupFolderBrowser("SOME PATH");
			CurrentDirectoryChangedEvent publishedEvent = null;

			MediatorMock.Setup(med => med.Publish(It.IsAny<CurrentDirectoryChangedEvent>(), _token))
						.Callback<CurrentDirectoryChangedEvent, CancellationToken>((e, _) => publishedEvent = e);

			await SUT.Handle(_request, _token);

			Assert.Equal("SOME PATH", publishedEvent.Path);
		}
	}
}