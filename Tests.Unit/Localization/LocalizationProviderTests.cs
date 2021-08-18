using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using Moq;
using UI.Localization;
using Xunit;

namespace Tests.Unit.Localization
{
	public class LocalizationProviderTests : TestBase<LocalizationProvider>
	{
		private readonly Mock<ResourceManager> _resourceManagerMock = new();

		/// <inheritdoc />
		protected override LocalizationProvider CreateSUT() => new(_resourceManagerMock.Object);


		[Fact]
		public void GivenInstance_WhenUnchanged_ThenCurrentCultureEqualsUICulture()
		{
			Assert.Equal(CultureInfo.CurrentUICulture, SUT.CurrentCulture);
		}

		[Fact]
		public void GivenInstance_WhenCultureChanged_ThenTriggersOnPropertyChanged()
		{
			PropertyChangedEventArgs args = null;
			SUT.PropertyChanged += (sender, eventArgs) => args = eventArgs;

			SUT.CurrentCulture = new CultureInfo("en");

			Assert.NotNull(args);
			Assert.Equal("en", SUT.CurrentCulture.Name);
		}

		[Fact]
		public void GivenKey_WhenKeyRequested_ThenCallsResourceManager()
		{
			SUT.CurrentCulture = new CultureInfo("de");

			_ = SUT["TEST"];

			_resourceManagerMock.Verify(r => r.GetString("TEST", Thread.CurrentThread.CurrentUICulture));
		}
	}
}