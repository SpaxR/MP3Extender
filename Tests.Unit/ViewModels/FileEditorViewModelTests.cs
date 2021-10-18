using MP3Extender.Application.Services;
using MP3Extender.Domain.Entities;
using MP3Extender.MetroUI.Views.Editor;
using NSubstitute;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class FileEditorViewModelTests : TestBase<FileEditorViewModel>
	{
		private readonly IFileSystemService _fileSystemServiceMock = Substitute.For<IFileSystemService>();

		/// <inheritdoc />
		protected override FileEditorViewModel CreateSUT() => new(SettingsMock, _fileSystemServiceMock);

		[Fact]
		public void Files_WithSettings_IsLoadedFromFileSystem()
		{
			// Setup Settings
			const string rootFolder         = "SOME PATH";
			const bool   loadRecursiveFiles = false;
			SettingsMock.RootFolder.Returns(rootFolder);
			SettingsMock.LoadRecursiveFiles.Returns(loadRecursiveFiles);

			// Setup FileSystem
			var expectedFiles = new[]
			{
				new AudioFile(),
				new AudioFile(),
				new AudioFile()
			};
			_fileSystemServiceMock.LoadFiles(rootFolder, loadRecursiveFiles).Returns(expectedFiles);

			var files = SUT.Files;

			Assert.Equal(expectedFiles, files);
		}

		[Fact]
		public void ReloadFiles_WithoutParameter_ReloadsFilesFromRootFolder()
		{
			// Setup Settings
			const string rootFolder         = "SOME PATH";
			const bool   loadRecursiveFiles = false;
			SettingsMock.RootFolder.Returns(rootFolder);
			SettingsMock.LoadRecursiveFiles.Returns(loadRecursiveFiles);

			// Setup FileSystem
			var expectedFiles = new[]
			{
				new AudioFile(),
				new AudioFile(),
				new AudioFile()
			};
			_fileSystemServiceMock.LoadFiles(rootFolder, loadRecursiveFiles).Returns(expectedFiles);

			SUT.ReloadFiles.Execute(null);

			Assert.Equal(expectedFiles, SUT.Files);
		}

		[Fact]
		public void ReloadFiles_WithValidPath_ReloadsFilesFromPath()
		{
			const string folderPath = "SOME PATH";

			// Setup FileSystem
			var expectedFiles = new[]
			{
				new AudioFile(),
				new AudioFile(),
				new AudioFile()
			};
			_fileSystemServiceMock
				.LoadFiles(folderPath, Arg.Any<bool>())
				.Returns(expectedFiles);

			SUT.ReloadFiles.Execute(folderPath);

			Assert.Equal(expectedFiles, SUT.Files);
		}

		[Fact]
		public void ReloadFiles_WhenChanged_TriggersPropertyChangeOfFiles()
		{
			Assert.PropertyChanged(SUT, nameof(SUT.Files), () => SUT.ReloadFiles.Execute(null));
		}
	}
}