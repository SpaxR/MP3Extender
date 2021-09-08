using MP3Extender.Application;
using MP3Extender.WPF.Services;
using Xunit;

namespace Tests.Unit.Services
{
	public class ColorThemeChangedEventTests : TestBase<ColorThemeChangedEvent>
	{
		private ColorTheme _theme = ColorTheme.Light;

		/// <inheritdoc />
		protected override ColorThemeChangedEvent CreateSUT() => new(_theme);

		[Fact]
		public void GivenInstance_WhenThemeSet_ThenThemeReturnsGivenTheme()
		{
			Assert.Equal(_theme, SUT.Theme);
		}
	}
}