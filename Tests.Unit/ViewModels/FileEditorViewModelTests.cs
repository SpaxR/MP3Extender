using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
		public void ReloadFiles_UpdatesEditorColumns()
		{
			// Instantiate before Reloading
			_ = SUT;
			string[] expectedNames = { "Column1", "Column2" };
			_fileSystemServiceMock.DetectColumns(Arg.Any<IEnumerable<AudioFile>>())
								  .Returns(expectedNames);

			SUT.ReloadFiles.Execute(null);

			foreach (string expectedName in expectedNames)
			{
				Assert.Contains(SUT.EditorColumns, column => column.Header.Equals(expectedName));
			}
		}

		[Fact]
		public void ReloadFiles_WhenChanged_TriggersPropertyChangeOfFiles()
		{
			Assert.PropertyChanged(SUT, nameof(SUT.Files), () => SUT.ReloadFiles.Execute(null));
		}

		[Fact]
		public void ReloadFiles_WhenChanged_TriggersPropertyChangeOfEditorColumns()
		{
			Assert.PropertyChanged(SUT, nameof(SUT.EditorColumns), () => SUT.ReloadFiles.Execute(null));
		}

		[Fact]
		public void EditorColumns_WithDetectedColumns_ContainsAllColumns()
		{
			string[] expectedNames = { "Column1", "Column2" };

			_fileSystemServiceMock.DetectColumns(Arg.Any<IEnumerable<AudioFile>>())
								  .Returns(expectedNames);

			foreach (string expectedName in expectedNames)
			{
				Assert.Contains(SUT.EditorColumns, column => column.Header.Equals(expectedName));
			}
		}

		[Fact]
		public void EditorColumns_WithSettings_ColumnIsVisibleIfIsInSettings()
		{
			string[] expected = { "Column1", "Column2" };

			SettingsMock.FileEditorColumns.Returns(expected);
			_fileSystemServiceMock.DetectColumns(Arg.Any<IEnumerable<AudioFile>>())
								  .Returns(expected.Concat(new[] { "Invisible" }));

			var columns = SUT.EditorColumns;

			var visibleColumns = columns
								 .Where(col => col.Visibility == Visibility.Visible)
								 .Select(col => col.Header);
			var invisibleColumns = columns
								   .Where(col => col.Visibility == Visibility.Collapsed)
								   .Select(col => col.Header);

			Assert.Equal(expected, visibleColumns);
			Assert.Single(invisibleColumns, "Invisible");
		}

		[Fact]
		public void EditorColumns_Binding_IsSetToMetadataAndName()
		{
			_fileSystemServiceMock.DetectColumns(Arg.Any<IEnumerable<AudioFile>>())
								  .Returns(new[] { "Column1", "Column2" });

			var columns = SUT.EditorColumns.Cast<DataGridTextColumn>();

			Assert.All(columns, column => Assert.Equal($"{nameof(AudioFile.MetaData)}[{column.Header}]", ((Binding)column.Binding)?.Path.Path));
		}

		[Fact]
		public void ToggleColumnVisibility_WithExistingColumns_TogglesVisibility()
		{
			const string columnName = "SOME COLUMN";
			SettingsMock.FileEditorColumns.Returns(new[] { columnName });
			_fileSystemServiceMock.DetectColumns(Arg.Any<IEnumerable<AudioFile>>())
								  .Returns(new[] { columnName });

			SUT.ToggleColumnVisibility.Execute(columnName);
			Assert.Equal(Visibility.Collapsed, SUT.EditorColumns.Single().Visibility);

			SUT.ToggleColumnVisibility.Execute(columnName);
			Assert.Equal(Visibility.Visible, SUT.EditorColumns.Single().Visibility);
		}

		[Fact]
		public void AddColumn_WithNewColumn_AddsColumnToEditorColumns()
		{
			const string column = "SOME COLUMN";
			SUT.AddColumn.Execute(column);

			Assert.Contains(SUT.EditorColumns, col => col.Header.Equals(column));
		}
	}
}