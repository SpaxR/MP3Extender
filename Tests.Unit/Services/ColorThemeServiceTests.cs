using MP3Extender.Application;
using MP3Extender.WPF.Services;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Services
{
	public class ColorThemeServiceTests : TestBase<ColorThemeService>
	{
		private readonly ISettings _settingsMock = Substitute.For<ISettings>();

		/// <inheritdoc />
		protected override ColorThemeService CreateSUT() => new(MessengerMock, _settingsMock);

		[Fact]
		public void CanBeInstantiated()
		{
			Assert.NotNull(SUT);
		}
	}
}