using UI;
using Xunit;

namespace Tests.Unit
{
	public class AppTests : TestBase<App>, IClassFixture<ApplicationFixture>
	{
		private readonly ApplicationFixture _fixture;

		public AppTests(ApplicationFixture fixture) => _fixture = fixture;

		/// <inheritdoc />
		protected override App CreateSUT() => _fixture.Instance;

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