using System;
using System.IO;
using UI.Commands;
using UI.Controls.FileList;
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
		protected override FileListViewModel CreateSUT() => new(Messenger);

		public void Dispose() => Directory.Delete(_tempDir, true);

		private string CreateTempFile()
		{
			string path = Path.Combine(_tempDir, Path.GetRandomFileName());
			File.Create(path).Close();
			return path;
		}

		[Fact]
		public void GivenDirectoryChangedEvent_WhenDirectoryContainsFiles_ThenFilesContainsFilenames()
		{
			string expectedFile = CreateTempFile();
			var    request      = new CurrentDirectoryChangedEvent { Path = _tempDir };

			SUT.Receive(request);

			Assert.Contains(expectedFile, SUT.Files);
		}

		[Fact]
		public void GivenDirectoryChangedEvent_WhenFilesAlreadyLoaded_ThenFilesHasNoDuplicates()
		{
			var    request      = new CurrentDirectoryChangedEvent { Path = _tempDir };
			string expectedFile = CreateTempFile();
			SUT.Receive(request);

			SUT.Receive(request);

			Assert.Contains(expectedFile, SUT.Files);
			Assert.Single(SUT.Files);
		}
	}
}