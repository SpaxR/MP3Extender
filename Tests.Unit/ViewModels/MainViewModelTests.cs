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
		public void FilesViewModel_IsNotNull()
		{
			Assert.NotNull(SUT.Files);
		}

		[Fact]
		public void OpenSettingsWindowCommand_IsNotNull()
		{
			Assert.NotNull(SUT.OpenSettingsWindow);
		}
	}
}