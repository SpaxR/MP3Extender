using System;
using System.IO;
using System.Linq;
using LibVLCSharp.Shared;
using MP3Extender.Application.Services;
using MP3Extender.Domain.Entities;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Services
{
	public sealed class FileSystemServiceTests : TestBase<FileSystemService>, IClassFixture<VlcTestFixture>, IDisposable
	{
		private readonly VlcTestFixture _vlcFixture;
		private readonly string         _tempFolder;
		private readonly IMetaDataStore _metaDataStoreMock = Substitute.For<IMetaDataStore>();

		public FileSystemServiceTests(VlcTestFixture vlcFixture)
		{
			_vlcFixture = vlcFixture;
			_tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(_tempFolder);
		}

		/// <inheritdoc />
		protected override FileSystemService CreateSUT() => new(_vlcFixture.VLC, _metaDataStoreMock);

		/// <inheritdoc />
		public void Dispose()
		{
			Directory.Delete(_tempFolder, true);
		}

		private string CreateTempFile(string subDirectory = null, bool useRealFile = true)
		{
			string path = Path.Combine(_tempFolder, subDirectory ?? string.Empty, Path.GetRandomFileName());

			if (!string.IsNullOrEmpty(subDirectory))
			{
				Directory.CreateDirectory(Path.Combine(_tempFolder, subDirectory));
			}

			if (useRealFile)
			{
				File.Copy(VlcTestFixture.SampleFilename, path);
			}
			else
			{
				File.Create(path).Close();
			}


			return Path.GetFileName(path);
		}

		[Fact]
		public void LoadFiles_WithUnsupportedFiles_ReturnsOnlySupportedFiles()
		{
			string[] expected = { CreateTempFile() };
			CreateTempFile(useRealFile: false);

			var result = SUT.LoadFiles(_tempFolder, false);

			Assert.Equal(expected.OrderBy(_ => _),
						 result.Select(file => Path.GetFileName(file.Location)).OrderBy(_ => _));
		}

		[Fact]
		public void LoadFiles_WithoutSubDirectories_ReturnsOnlyTopLevelFiles()
		{
			string[] expectation = { CreateTempFile() };
			CreateTempFile("SubDirectory");

			var result = SUT.LoadFiles(_tempFolder, false);

			Assert.Equal(expectation, result.Select(file => Path.GetFileName(file.Location)).ToArray());
		}

		[Fact]
		public void LoadFiles_WithSubDirectories_ReturnsAllFiles()
		{
			string[] expectation = { CreateTempFile(), CreateTempFile("SubDirectory") };

			var result = SUT.LoadFiles(_tempFolder, true);

			Assert.Equal(expectation, result.Select(file => Path.GetFileName(file.Location)));
		}

		[Fact]
		public void LoadFiles_WithoutFiles_YieldsNoResults()
		{
			var result = SUT.LoadFiles(_tempFolder, false);

			Assert.Empty(result);
		}

		[Fact]
		public void LoadFiles_WithInvalidPath_YieldsNoResults()
		{
			string invalidPath = Path.GetRandomFileName();

			var result = SUT.LoadFiles(invalidPath, false);

			Assert.Empty(result);
		}

		[Fact]
		public void LoadFiles_ResultContainsMetaData()
		{
			const string expectedTitle     = "SOME TITLE";
			const string expectedInterpret = "SOME INTERPRET";
			string       file              = CreateTempFile();

			using (var media = new Media(_vlcFixture.VLC, file))
			{
				media.SetMeta(MetadataType.Title,  expectedTitle);
				media.SetMeta(MetadataType.Artist, expectedInterpret);
				Assert.True(media.SaveMeta());
			}

			var result = SUT.LoadFiles(_tempFolder, false).Single();

			Assert.Equal(expectedInterpret, result.Interpret);
			Assert.Equal(expectedTitle,     result.Title);
		}

		[Fact]
		public void SaveFile_SavesCustomMetaDataInStore()
		{
			var file = new AudioFile
			{
				Data = new MetaData()
			};

			SUT.SaveFile(file);

			_metaDataStoreMock.Received(1).SaveMetaData(file.Data);
		}
	}
}