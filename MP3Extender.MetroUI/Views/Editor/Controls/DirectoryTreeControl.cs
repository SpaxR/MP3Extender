using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MP3Extender.MetroUI.Views.Editor
{
	public class DirectoryTreeControl : TreeView
	{
		public DirectoryTreeControl()
		{
			SelectedItemChanged += OnSelectedItemChanged;
		}

#region Properties

		public static readonly DependencyProperty RootProperty = DependencyProperty.Register(
			nameof(Root), typeof(string), typeof(DirectoryTreeControl), new PropertyMetadata(OnRootChanged));

		public string Root
		{
			get => (string)GetValue(RootProperty);
			set => SetValue(RootProperty, value);
		}

		public static readonly DependencyProperty SelectedCommandProperty = DependencyProperty.Register(
			nameof(SelectedCommand), typeof(ICommand), typeof(DirectoryTreeControl));

		public ICommand SelectedCommand
		{
			get => (ICommand)GetValue(SelectedCommandProperty);
			set => SetValue(SelectedCommandProperty, value);
		}

#endregion

#region EventHandler

		private static void OnRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is TreeView dtc)
			{
				dtc.ItemsSource = LoadDirectories(e.NewValue?.ToString());
			}
		}

		private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (e.NewValue is TreeViewItem item && SelectedCommand?.CanExecute(item.Tag) == true)
			{
				SelectedCommand.Execute(item.Tag);
			}
		}

#endregion

#region Methods

		private static IEnumerable<TreeViewItem> LoadDirectories(string path)
		{
			if (!Directory.Exists(path))
			{
				yield break;
			}

			foreach (string directory in EnumerateVisibleDirectories(path))
			{
				var item = new TreeViewItem
				{
					Header = Path.GetFileName(directory),
					Tag    = directory,
					ItemsSource = HasSubFolders(directory)
						? new[] { new TreeViewItem() }
						: Enumerable.Empty<TreeViewItem>()
				};

				item.Expanded += (_, args) =>
				{
					args.Handled     = true;
					item.ItemsSource = LoadDirectories(directory);
				};

				yield return item;
			}
		}

		private static IEnumerable<string> EnumerateVisibleDirectories(string path)
		{
			return Directory
				   .EnumerateDirectories(path)
				   .Where(dir => !new FileInfo(dir).Attributes.HasFlag(FileAttributes.Hidden));
		}

		private static bool HasSubFolders(string path)
		{
			try
			{
				return Directory.EnumerateDirectories(path).Any();
			}
			catch (Exception)
			{
				/* Directory not Accessible */
			}

			return false;
		}

#endregion
	}
}