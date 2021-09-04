using System;
using System.IO;
using MP3Extender.WPF.Commands;
using MP3Extender.WPF.ViewModels;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public sealed class FileListViewModelTests : TestBase<FileListViewModel>, IDisposable
	{
		private readonly string _tempDir;

		public FileListViewModelTests()
		{
			string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			_tempDir = Directory.CreateDirectory(path).FullName;
		}


		/// <inheritdoc />
		protected override FileListViewModel CreateSUT() => new(MessengerMock);

		public void Dispose() => Directory.Delete(_tempDir, true);

		private string CreateTempFile()
		{
			string path = Path.Combine(_tempDir, Path.GetRandomFileName());
			File.Create(path).Close();
			return path;
		}

		[Fact]
		public void GivenDirectoryChangedEvent_WhenReceived_ThenPathIsCurrentDirectory()
		{
			var request = new DirectoryChangedEvent(_tempDir);

			SUT.Receive(request);

			Assert.Equal(_tempDir, SUT.CurrentDirectory);
		}

		[Fact]
		public void GivenDirectoryChangedEvent_WhenDirectoryContainsFiles_ThenFilesContainsFilenames()
		{
			string expectedFile = CreateTempFile();
			var    request      = new DirectoryChangedEvent(_tempDir);

			SUT.Receive(request);

			Assert.Contains(expectedFile, SUT.Files);
		}

		[Fact]
		public void GivenDirectoryChangedEvent_WhenFilesAlreadyLoaded_ThenFilesHasNoDuplicates()
		{
			var    request      = new DirectoryChangedEvent(_tempDir);
			string expectedFile = CreateTempFile();
			SUT.Receive(request);

			SUT.Receive(request);

			Assert.Contains(expectedFile, SUT.Files);
			Assert.Single(SUT.Files);
		}

		[Fact]
		public void GivenDirectoryChangedEvent_WhenDirectoryDoesNotExist_ThenCurrentDirectoryIsErrorText()
		{
			var request = new DirectoryChangedEvent("INVALID");

			SUT.Receive(request);

			Assert.Equal("Directory not Found", SUT.CurrentDirectory);
		}

		[Fact]
		public void GivenDirectoryChangedEvent_WhenDirectoryDoesNotExist_ThenFilesIsEmpty()
		{
			var request = new DirectoryChangedEvent("INVALID");

			SUT.Receive(request);

			Assert.Empty(SUT.Files);
		}
	}
}