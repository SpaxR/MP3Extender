using UI.Commands;
using Xunit;

namespace Tests.Unit.Commands
{
	public class ChangeColorThemeTests : TestBase<ChangeColorTheme>
	{
		/// <inheritdoc />
		protected override ChangeColorTheme CreateSUT() => new();


		[Fact]
		public void GivenAnyParameter_WhenCanExecute_ThenReturnsTrue()
		{
			Assert.True(SUT.CanExecute(null));
		}
	}
}