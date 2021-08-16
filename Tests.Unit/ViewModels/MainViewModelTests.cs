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
		public void GivenNewInstance_WhenUnchanged_ThenFilesIsNotNull()
		{
			Assert.NotNull(SUT.Files);
		}

		[Fact]
		public void GivenNewInstance_WhenPickFolderExecuting_ThenSendsChangeDirectoryRequest()
		{
			SUT.PickFolder.Execute(null);

			MediatorMock.Verify(m => m.Send(It.IsAny<ChangeCurrentDirectoryRequest>(), It.IsAny<CancellationToken>()));
		}
	}
}