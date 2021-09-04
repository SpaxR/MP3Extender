using MP3Extender.Application;
using MP3Extender.WPF.Commands;
using MP3Extender.WPF.ViewModels;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Commands
{
	public class OpenSettingsWindowTests : TestBase<OpenSettingsWindowHandler>
	{
		private readonly ISettings         _settingsMock = Substitute.For<ISettings>();
		private          SettingsViewModel _settingsViewModelMock;

		/// <inheritdoc />
		protected override OpenSettingsWindowHandler CreateSUT()
		{
			_settingsViewModelMock ??= Substitute.For<SettingsViewModel>(MessengerMock, _settingsMock);

			return new OpenSettingsWindowHandler(_settingsViewModelMock);
		}

		[Fact]
		public void CanBeCreated()
		{
			Assert.NotNull(SUT);
		}
	}
}