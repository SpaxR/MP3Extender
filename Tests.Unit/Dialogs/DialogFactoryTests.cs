using UI.Dialogs;
using Xunit;

namespace Tests.Unit.Dialogs
{
	public class DialogFactoryTests : TestBase<DialogFactory>
	{
		/// <inheritdoc />
		protected override DialogFactory CreateSUT() => new();


		[Fact]
		public void GivenInstance_WhenCreateFolderBrowserDialog_ThenReturnsNewFolderBrowserDialog()
		{
			Assert.NotNull(SUT.CreateFolderBrowserDialog());
		}
	}
}