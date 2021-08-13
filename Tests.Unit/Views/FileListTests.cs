using UI.Controls.FileList;
using Xunit;

namespace Tests.Unit.Views
{
	public class FileListTests : TestBase<FileList>, IClassFixture<ApplicationFixture>
	{
		/// <inheritdoc />
		protected override FileList CreateSUT() => new();

		[UIFact]
		public void GivenNewInstance_WhenUnchanged_ThenDataContextIsFileListViewModel()
		{
			Assert.IsAssignableFrom<FileListViewModel>(SUT.DataContext);
		}
	}
}