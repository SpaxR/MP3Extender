using MP3Extender.MetroUI.Views.Editor;
using NSubstitute;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class EditorViewModelTests : TestBase<EditorViewModel>
	{
		private readonly FileEditorViewModel _fileEditorViewModelMock;

		public EditorViewModelTests()
		{
			_fileEditorViewModelMock = Substitute.For<FileEditorViewModel>(SettingsMock, FileSystemMock);
		}
		
		/// <inheritdoc />
		protected override EditorViewModel CreateSUT() => new(SettingsMock, _fileEditorViewModelMock);

		[Fact]
		public void Sanity()
		{
			Assert.NotNull(SUT);
			Assert.Equal(SettingsMock,             SUT.Settings);
			Assert.Equal(_fileEditorViewModelMock, SUT.FileEditorVM);
		}
	}
}