using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Toolkit.Mvvm.Input;
using MP3Extender.Application;
using MP3Extender.Application.Services;
using MP3Extender.Domain.Entities;
using MP3Extender.MetroUI.Common;

namespace MP3Extender.MetroUI.Views.Editor
{
	public class FileEditorViewModel : ViewModelBase
	{
		public IEnumerable<AudioFile>               Files                  { get; private set; }
		public IRelayCommand<string>                ReloadFiles            { get; }
		public ObservableCollection<DataGridColumn> EditorColumns          { get; private set; }
		public IRelayCommand<string>                ToggleColumnVisibility { get; }


		public IRelayCommand<string> AddColumn { get; }
		// public ICommand  CommitChanges { get; }
		// public ICommand  RevertChanges { get; }

		public FileEditorViewModel(ISettings settings, IFileSystemService fileSystem)
		{
			ToggleColumnVisibility = new RelayCommand<string>(OnToggleColumnVisibility);
			ReloadFiles            = new RelayCommand<string>(path => LoadFilesAndColumns(settings, fileSystem, path));
			AddColumn              = new RelayCommand<string>(name => EditorColumns.Add(CreateColumn(name)));

			LoadFilesAndColumns(settings, fileSystem);

			// CommitChanges = new RelayCommand(() => { });
			// RevertChanges = new RelayCommand(() => { });
		}

		private void LoadFilesAndColumns(ISettings settings, IFileSystemService fileSystem, string path = null)
		{
			OnPropertyChanging(nameof(Files));
			Files = fileSystem.LoadFiles(path ?? settings.RootFolder, settings.LoadRecursiveFiles);
			OnPropertyChanged(nameof(Files));

			var columns = fileSystem
						  .DetectColumns(Files)
						  .Select(column => CreateColumn(column, settings.FileEditorColumns.Contains(column)));

			OnPropertyChanging(nameof(EditorColumns));
			EditorColumns = new ObservableCollection<DataGridColumn>(columns);
			OnPropertyChanged(nameof(EditorColumns));
		}

		private static DataGridColumn CreateColumn(string name, bool isVisible = true)
		{
			return new DataGridTextColumn
			{
				Header     = name,
				Binding    = new Binding($"{nameof(AudioFile.MetaData)}[{name}]"),
				Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed
			};
		}

		private void OnToggleColumnVisibility(string name)
		{
			var column = EditorColumns.FirstOrDefault(col => col.Header.Equals(name));
			if (column != null)
			{
				column.Visibility = column.Visibility == Visibility.Visible
					? Visibility.Collapsed
					: Visibility.Visible;
			}
		}
	}
}