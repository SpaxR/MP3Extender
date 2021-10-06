using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MP3Extender.Application.Services;
using MP3Extender.Domain.Entities;
using Xunit;

namespace Tests.Unit.Services
{
	public sealed class FileSystemServiceTests : TestBaseDefault<FileSystemService>, IDisposable
	{
		private readonly string _tempFolder;

		public FileSystemServiceTests()
		{
			_tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(_tempFolder);
		}

		/// <inheritdoc />
		public void Dispose()
		{
			Directory.Delete(_tempFolder, true);
		}

		private string CreateTempFile(string subDirectory = null)
		{
			string file = Path.GetRandomFileName();

			if (!string.IsNullOrEmpty(subDirectory))
			{
				Directory.CreateDirectory(Path.Combine(_tempFolder, subDirectory));
			}

			File.Create(Path.Combine(_tempFolder, subDirectory ?? string.Empty, file))
				.Close();

			return file;
		}

		[Fact]
		public void GivenValidPath_WhenOnlyFiles_ThenReturnsAllFiles()
		{
			string expectation = CreateTempFile();

			var result = SUT.LoadFiles(_tempFolder, false);

			Assert.Contains(result, file => expectation.Equals(Path.GetFileName(file.Location)));
		}

		[Fact]
		public void GivenValidPath_WhenSubDirectoriesTrue_ThenReturnsFilesInSubDirs()
		{
			string expectation = CreateTempFile("SubDirectory");

			var result = SUT.LoadFiles(_tempFolder, true);

			Assert.Contains(result, file => expectation.Equals(Path.GetFileName(file.Location)));
		}

		[Fact]
		public void GivenValidPath_WhenSubDirectoriesFalse_ThenDoesNotContainFilesInSubDirs()
		{
			CreateTempFile("SubDirectory");

			var result = SUT.LoadFiles(_tempFolder, false);

			Assert.Empty(result);
		}

		[Fact]
		public void GivenValidPath_WhenNoFilesExist_ThenYieldNoResults()
		{
			var result = SUT.LoadFiles(_tempFolder, false);

			Assert.Empty(result);
		}
		
		[Fact]
		public void GivenInvalidPath_WhenLoading_ThenYieldNoResults()
		{
			string invalidPath = Path.GetRandomFileName();

			var result = SUT.LoadFiles(invalidPath, false);

			Assert.Empty(result);
		}

		[Fact]
		public void GivenAudioFiles_WhenDetectingColumns_ThenReturnsDistinctColumns()
		{
			var files = new[]
			{
				new AudioFile
				{
					MetaData = new Dictionary<string, string>
					{
						{ "A", "1" },
						{ "C", "3" }
					}
				},
				new AudioFile()
				{
					MetaData = new Dictionary<string, string>
					{
						{ "A", "1" },
						{ "B", "2" }
					}
				},
				new AudioFile()
				{
					MetaData = new Dictionary<string, string>
					{
						{ "A", "1" },
					}
				}
			};
			var result = SUT.DetectColumns(files);

			Assert.Equal(new[] { "A", "B", "C" }.OrderBy(_ => _), result.OrderBy(_ => _));
		}
	}
}