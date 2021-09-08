using MP3Extender.Application;
using MP3Extender.WPF.Services;
using MP3Extender.WPF.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class SettingsViewModelTests : TestBase<SettingsViewModel>
	{
		private readonly IColorThemeService _colorThemeMock = Substitute.For<IColorThemeService>();
		private readonly IFileSystemService _fileSystemMock = Substitute.For<IFileSystemService>();

		/// <inheritdoc />
		protected override SettingsViewModel CreateSUT()
		{
			return new SettingsViewModel(MessengerMock, _colorThemeMock, _fileSystemMock);
		}

		[Fact]
		public void GivenConfig_WhenNotNull_ThenThemeEqualsConfig()
		{
			_colorThemeMock.Theme.Returns(ColorTheme.Light);

			ColorTheme result = SUT.Theme;

			Assert.Equal(ColorTheme.Light, result);
		}

		[Fact]
		public void GivenConfig_WhenNotNull_ThenRootFolderPathEqualsConfig()
		{
			_fileSystemMock.RootDirectoryPath.Returns("SOME PATH");

			string result = SUT.RootFolderPath;

			Assert.Equal("SOME PATH", result);
		}

		// [Fact]
		// public void GivenConfig_WhenChangeThemeExecuted_ThenChangeThemeRequestGetsRaised()
		// {
		// 	SUT.ChangeColorTheme.Execute("THEME");
		//
		// 	MessengerMock
		// 		.Received(1)
		// 		.Send(Arg.Is<ChangeColorThemeRequest>(req => "THEME".Equals(req.Theme)));
		// }
		//
		// [Fact]
		// public void GivenInstance_WhenChangeRootFolderExecuted_ThenRaisesChangeDirectoryRequest()
		// {
		// 	SUT.ChangeRootFolder.Execute(null);
		//
		// 	MessengerMock
		// 		.Received(1)
		// 		.Send(Arg.Any<ChangeDirectoryRequest>());
		// }
		//
		// [Fact]
		// public void GivenValidSettingsChangedEvent_WhenReceived_RaisesOnPropertyChanged()
		// {
		// 	var settingsChangedEvent = new SettingsChangedEvent();
		//
		// 	Assert.PropertyChanged(SUT, nameof(SUT.Receive), () => SUT.Receive(settingsChangedEvent));
		// }
	}
}