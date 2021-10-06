using System.Collections.Generic;
using System.IO;
using System.Linq;
using MP3Extender.Domain.Entities;

namespace MP3Extender.Application.Services
{
	public interface IFileSystemService
	{
		public IEnumerable<AudioFile> LoadFiles(string path, bool loadSubFolders);

		public IEnumerable<string> DetectColumns(IEnumerable<AudioFile> files);
	}


	public class FileSystemService : IFileSystemService
	{
		/// <inheritdoc />
		public IEnumerable<AudioFile> LoadFiles(string path, bool loadSubFolders)
		{
			if (!Directory.Exists(path))
			{
				return Enumerable.Empty<AudioFile>();
			}

			var files = Directory.EnumerateFiles(
				path, string.Empty, loadSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

			return files.Select(file => new AudioFile()
			{
				Location = file,
				MetaData = new Dictionary<string, string>()
				// 	MetaData = new Dictionary<string, string>
				// 	{
				// 		{ "Filename", "SOME FILE.mp3" },
				// 		{ "Title", "SOME TITLE" },
				// 		{ "Interpret", "SOME INTERPRET" },
				// 		{ "SomeColumn", "SOME COLUMN" }
				// 	}
			});
		}

		/// <inheritdoc />
		public IEnumerable<string> DetectColumns(IEnumerable<AudioFile> files)
		{
			var keys = new List<string>();

			foreach (var file in files)
			{
				keys.AddRange(file.MetaData.Keys);
			}

			return keys.Distinct();
		}
	}
}