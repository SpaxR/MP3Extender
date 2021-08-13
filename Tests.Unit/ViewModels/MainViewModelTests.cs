using System.Threading;
using Moq;
using UI;
using UI.Commands;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class MainViewModelTests : TestBase<MainViewModel>
	{
		protected override MainViewModel CreateSUT() => new(MediatorMock.Object, Messenger);


		[Fact]
		public void GivenViewModel_WhenUnchanged_ThenCurrentDirectoryIsDefaultText()
		{
			Assert.NotEmpty(SUT.CurrentDirectory);
		}

		[Fact]
		public void GivenViewModel_WhenExecutePickFolder_SendsChangeDirectoryRequest()
		{
			SUT.PickFolder.Execute(null);

			MediatorMock.Verify(m => m.Send(It.IsAny<ChangeCurrentDirectoryRequest>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public void GivenViewModel_WhenExecutePickFolder_ThenUpdatesCurrentDirectory()
		{
			MediatorMock.Setup(m => m.Send(It.IsAny<ChangeCurrentDirectoryRequest>(), It.IsAny<CancellationToken>()))
						.ReturnsAsync("NEW DIRECTORY");

			SUT.PickFolder.Execute(null);

			Assert.Equal("NEW DIRECTORY", SUT.CurrentDirectory);
		}
	}
}