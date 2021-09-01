using Microsoft.Toolkit.Mvvm.Messaging;
using NSubstitute;
using UI.Commands;
using UI.Configuration;
using UI.Settings;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class SettingsViewModelTests : TestBase<SettingsViewModel>
	{
		private readonly IMessenger _messengerMock = Substitute.For<IMessenger>();
		private readonly IConfig    _configMock    = Substitute.For<IConfig>();

		/// <inheritdoc />
		protected override SettingsViewModel CreateSUT()
		{
			return new SettingsViewModel(_messengerMock, _configMock);
		}

		[Fact]
		public void GivenConfig_WhenNotNull_ThenUseDarkThemeEqualsConfig()
		{
			_configMock.UseDarkTheme.Returns(true);

			bool result = SUT.UseDarkTheme;

			Assert.True(result);
		}

		[Fact]
		public void GivenConfig_WhenNotNull_ThenRootFolderPathEqualsConfig()
		{
			_configMock.RootFolder.Returns("SOME PATH");

			string result = SUT.RootFolderPath;

			Assert.Equal("SOME PATH", result);
		}

		[Fact]
		public void GivenConfig_WhenChangeThemeExecuted_ThenDarkThemeConfigGetsToggled()
		{
			_configMock.UseDarkTheme.Returns(false);

			SUT.ChangeTheme.Execute(null);
			Assert.True(_configMock.UseDarkTheme);

			SUT.ChangeTheme.Execute(null);
			Assert.False(_configMock.UseDarkTheme);
		}

		[Fact]
		public void GivenMessenger_WhenChangeRootFolderExecuted_ThenChangeCurrentDirectoryRequestGetsSend()
		{
			SUT.ChangeRootFolder.Execute(null);

			_messengerMock.Received().Send(Arg.Any<ChangeCurrentDirectoryRequest>());
		}
	}
}