using UI.Dialogs.FolderBrowser;
using Xunit;

namespace Tests.Unit.Dialogs
{
	public class FolderBrowserDialogTests : TestBase<FolderBrowserDialog>
	{
		/// <inheritdoc />
		protected override FolderBrowserDialog CreateSUT() => new();

		[Fact]
		public void GivenNewInstance_WhenGetSelectedPath_ThenIsEmpty()
		{
			Assert.Empty(SUT.SelectedPath);
		}
	}
}