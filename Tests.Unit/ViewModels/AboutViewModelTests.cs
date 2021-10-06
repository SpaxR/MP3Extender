using MP3Extender.MetroUI.Views.About;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class AboutViewModelTests : TestBaseDefault<AboutViewModel>
	{
		[Fact]
		public void Sanity()
		{
			Assert.NotNull(SUT);
		}
	}
}