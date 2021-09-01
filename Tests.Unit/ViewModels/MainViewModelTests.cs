using Microsoft.Toolkit.Mvvm.Messaging;
using NSubstitute;
using UI;
using UI.Commands;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class MainViewModelTests : TestBase<MainViewModel>
	{
		private readonly IMessenger _messengerMock = Substitute.For<IMessenger>();
		
		protected override MainViewModel CreateSUT() => new(_messengerMock);

		[Fact]
		public void GivenMainViewModel_WhenNotLoaded_ThenFilesIsNotNull()
		{
			Assert.NotNull(SUT.Files);
		}
		
		
		[Fact]
		public void GivenMainViewModel_WhenOpenSettingsCommandExecuted_ThenSendsOpenSettingsWindowRequest()
		{
			SUT.OpenSettingsWindow.Execute(null);

			_messengerMock.Received().Send(Arg.Any<OpenSettingsWindowRequest>());

		}
	}
}