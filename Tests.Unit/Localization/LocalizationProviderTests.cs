using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using NSubstitute;
using UI.Localization;
using Xunit;

namespace Tests.Unit.Localization
{
	public class LocalizationProviderTests : TestBase<LocalizationProvider>
	{
		private readonly ResourceManager _resourceManagerMock = Substitute.For<ResourceManager>();

		/// <inheritdoc />
		protected override LocalizationProvider CreateSUT() => new(_resourceManagerMock);


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

			_resourceManagerMock.Received(1).GetString("TEST", Thread.CurrentThread.CurrentUICulture);
		}
	}
}