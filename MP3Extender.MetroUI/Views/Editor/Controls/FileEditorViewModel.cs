using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.Input;
using MP3Extender.Application;
using MP3Extender.Application.Services;
using MP3Extender.Domain.Entities;
using MP3Extender.MetroUI.Common;

namespace MP3Extender.MetroUI.Views.Editor
{
	public class FileEditorViewModel : ViewModelBase
	{
		public AudioFile SelectedFile { get; set; }

		public IEnumerable<AudioFile> Files       { get; private set; }
		public IRelayCommand<string>  ReloadFiles { get; }

		// public ICommand  CommitChanges { get; }
		// public ICommand  RevertChanges { get; }

		public FileEditorViewModel(ISettings settings, IFileSystemService fileSystem)
		{
			ReloadFiles = new RelayCommand<string>(path => LoadFilesAndColumns(settings, fileSystem, path));

			LoadFilesAndColumns(settings, fileSystem);

			// CommitChanges = new RelayCommand(() => { });
			// RevertChanges = new RelayCommand(() => { });
		}

		private void LoadFilesAndColumns(ISettings settings, IFileSystemService fileSystem, string path = null)
		{
			OnPropertyChanging(nameof(Files));
			Files = fileSystem.LoadFiles(path ?? settings.RootFolder, settings.LoadRecursiveFiles);
			OnPropertyChanged(nameof(Files));
		}
	}
}