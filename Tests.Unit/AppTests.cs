using UI;
using Xunit;

namespace Tests.Unit
{
	public class AppTests : TestBase<App>
	{
		/// <inheritdoc />
		protected override App CreateSUT() => App.Current ?? new();

		[Fact]
		public void GivenCreated_WhenCreated_ThenHasServices()
		{
			Assert.NotNull(SUT.Services);
		}

		[Fact]
		public void GivenCreated_WhenCreated_ThenCurrentIsCurrentInstance()
		{
			Assert.Equal(SUT, App.Current);
		}
	}
}