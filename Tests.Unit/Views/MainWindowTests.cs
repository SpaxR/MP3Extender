using UI;
using Xunit;

namespace Tests.Unit.Views
{
	public class MainWindowTests : TestBase<MainWindow>, IClassFixture<ApplicationFixture>
	{
		/// <inheritdoc />
		protected override MainWindow CreateSUT() => new();

		[UIFact]
		public void GivenNewInstance_WhenNothingChanged_ThenDataContextIsMainViewModel()
		{
			Assert.IsAssignableFrom<MainViewModel>(SUT.DataContext);
		}
	}
}