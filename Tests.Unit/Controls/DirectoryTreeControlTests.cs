using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MP3Extender.MetroUI.Views.Editor;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Controls
{
	public sealed class DirectoryTreeControlTests : TestBaseDefault<DirectoryTreeControl>, IDisposable
	{
		private readonly IList<string> _createdFolders = new List<string>();

		public DirectoryTreeControlTests()
		{
			SUT.BeginInit();
			SUT.EndInit();
		}

		/// <inheritdoc />
		public void Dispose()
		{
			foreach (string folder in _createdFolders)
			{
				Directory.Delete(folder, true);
			}
		}

		private string CreateTempFolder()
		{
			string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

			string folder = Directory.CreateDirectory(path).FullName;
			_createdFolders.Add(folder);
			return folder;
		}

		[UIFact]
		public void GivenInvalidPath_WhenLoaded_ThenItemsIsEmpty()
		{
			SUT.Root = "Invalid Path";

			Assert.Empty(SUT.Items);
			Assert.Equal("Invalid Path", SUT.Root);
		}

		[UIFact]
		public void GivenValidPath_WhenLoaded_ThenContainsTopLevelFolders()
		{
			string testFolder = CreateTempFolder();
			SUT.Root = Path.GetTempPath();
			Assert.Contains(SUT.Items.SourceCollection.Cast<TreeViewItem>(), item => item.Tag.Equals(testFolder));
		}

		[UIFact]
		public void GivenFolderWithSubFolders_WhenLoaded_ThenFoldersItemsIsNotEmpty()
		{
			string testFolder = CreateTempFolder();
			Directory.CreateDirectory(Path.Combine(testFolder, Path.GetRandomFileName()));
			SUT.Root = Path.GetTempPath();

			var testFolderItem = SUT.Items.SourceCollection.Cast<TreeViewItem>()
									.First(item => item.Tag.Equals(testFolder));

			Assert.NotEmpty(testFolderItem.Items);
		}

		[UIFact]
		public void GivenValidPath_WhenItemExpanded_ThenLoadsSubFolders()
		{
			string rootTestFolder = CreateTempFolder();
			string subTestFolder = Directory.CreateDirectory(Path.Combine(rootTestFolder, Path.GetRandomFileName()))
											.FullName;
			SUT.Root = Path.GetTempPath();

			var sutItem = SUT.Items
							 .Cast<TreeViewItem>()
							 .First(item => item.Tag.Equals(rootTestFolder));
			sutItem.IsExpanded = true;

			Assert.Contains(sutItem.Items.Cast<TreeViewItem>(), item => item.Tag.Equals(subTestFolder));
		}

		[UIFact]
		public void GivenCommand_WhenItemSelected_ThenCommandGetsExecuted()
		{
			CreateTempFolder();
			var command = Substitute.For<ICommand>();
			command.CanExecute(Arg.Any<string>()).Returns(true);
			SUT.SelectedCommand = command;
			SUT.Root            = Path.GetTempPath();

			var item = SUT.Items.Cast<TreeViewItem>().First();
			var args = new RoutedPropertyChangedEventArgs<object>(null, item, TreeView.SelectedItemChangedEvent);
			SUT.RaiseEvent(args);

			command.Received(1).Execute(Arg.Any<string>());
		}

		[UIFact]
		public void GivenCommand_WhenItemSelectedAndCommandCannotExecute_ThenExecuteIsNotCalled()
		{
			CreateTempFolder();
			var command = Substitute.For<ICommand>();
			command.CanExecute(Arg.Any<string>()).Returns(false);
			SUT.SelectedCommand = command;
			SUT.Root            = Path.GetTempPath();

			var item = SUT.Items.Cast<TreeViewItem>().First();
			var args = new RoutedPropertyChangedEventArgs<object>(null, item, TreeView.SelectedItemChangedEvent);
			SUT.RaiseEvent(args);

			command.Received(1).CanExecute(Arg.Any<string>());
			command.DidNotReceive().Execute(Arg.Any<string>());
		}
	}
}