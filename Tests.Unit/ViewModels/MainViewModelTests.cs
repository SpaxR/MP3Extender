using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.WPF.Commands;
using MP3Extender.WPF.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class MainViewModelTests : TestBase<MainViewModel>
	{
		protected override MainViewModel CreateSUT() => new(MessengerMock);

		[Fact]
		public void GivenMainViewModel_WhenNotLoaded_ThenFilesIsNotNull()
		{
			Assert.NotNull(SUT.Files);
		}


		[Fact]
		public void GivenMainViewModel_WhenOpenSettingsCommandExecuted_ThenSendsOpenSettingsWindowRequest()
		{
			SUT.OpenSettingsWindow.Execute(null);

			MessengerMock.Received(1).Send(Arg.Any<OpenSettingsWindowRequest>());
		}
	}
}