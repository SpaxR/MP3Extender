using MP3Extender.Application;
using MP3Extender.MetroUI.Views.Settings;
using NSubstitute;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class SettingsViewModelTests : TestBase<SettingsViewModel>
	{
		private readonly ISettings _settingsMock = Substitute.For<ISettings>();

		/// <inheritdoc />
		protected override SettingsViewModel CreateSUT() => new(_settingsMock);


		[Fact]
		public void GivenSettings_WhenInitialised_SettingsAreAvailable()
		{
			Assert.Equal(_settingsMock, SUT.Settings);
		}
	}
}