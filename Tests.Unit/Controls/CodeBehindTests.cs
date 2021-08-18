using UI;
using UI.Controls;
using UI.Controls.FileList;
using Xunit;

namespace Tests.Unit.Controls
{
	public abstract class DefaultControlTest<T> : TestBase<T>
	{
		[StaFact]
		public void CanBeConstructed() => Assert.NotNull(SUT);
	}


	public class MainWindowTests : DefaultControlTest<MainWindow>
	{
		/// <inheritdoc />
		protected override MainWindow CreateSUT() => new();
	}

	public class DirectoryTreeTests : DefaultControlTest<DirectoryTree>
	{
		/// <inheritdoc />
		protected override DirectoryTree CreateSUT() => new();
	}

	public class FileListTests : DefaultControlTest<FileList>
	{
		/// <inheritdoc />
		protected override FileList CreateSUT() => new();
	}
}