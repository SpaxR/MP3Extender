using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.Application;
using MP3Extender.WPF.Commands;
using MP3Extender.WPF.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class SettingsViewModelTests : TestBase<SettingsViewModel>
	{
		private readonly ISettings _settingsMock = Substitute.For<ISettings>();

		/// <inheritdoc />
		protected override SettingsViewModel CreateSUT()
		{
			return new SettingsViewModel(MessengerMock, _settingsMock);
		}

		[Fact]
		public void GivenConfig_WhenNotNull_ThenThemeEqualsConfig()
		{
			_settingsMock.ColorTheme.Returns("THEME");

			string result = SUT.ColorTheme;

			Assert.Equal("THEME", result);
		}

		[Fact]
		public void GivenConfig_WhenNotNull_ThenRootFolderPathEqualsConfig()
		{
			_settingsMock.RootFolder.Returns("SOME PATH");

			string result = SUT.RootFolderPath;

			Assert.Equal("SOME PATH", result);
		}

		[Fact]
		public void GivenConfig_WhenChangeThemeExecuted_ThenChangeThemeRequestGetsRaised()
		{
			SUT.ChangeColorTheme.Execute("THEME");

			MessengerMock
				.Received(1)
				.Send(Arg.Is<ChangeColorThemeRequest>(req => "THEME".Equals(req.Theme)));
		}

		[Fact]
		public void GivenInstance_WhenChangeRootFolderExecuted_ThenRaisesChangeDirectoryRequest()
		{
			SUT.ChangeRootFolder.Execute(null);

			MessengerMock
				.Received(1)
				.Send(Arg.Any<ChangeDirectoryRequest>());
		}

		[Fact]
		public void GivenValidSettingsChangedEvent_WhenReceived_RaisesOnPropertyChanged()
		{
			var settingsChangedEvent = new SettingsChangedEvent();

			Assert.PropertyChanged(SUT, nameof(SUT.Receive), () => SUT.Receive(settingsChangedEvent));
		}
	}
}