using MP3Extender.WPF;
using MP3Extender.WPF.Factories;
using MP3Extender.WPF.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class MainViewModelTests : TestBase<MainViewModel>
	{
		private readonly IDialogFactory _dialogFactoryMock = Substitute.For<IDialogFactory>();

		protected override MainViewModel CreateSUT() => new(MessengerMock, _dialogFactoryMock);

		[Fact]
		public void GivenMainViewModel_WhenNotLoaded_ThenFilesIsNotNull()
		{
			Assert.NotNull(SUT.Files);
		}
	}
}