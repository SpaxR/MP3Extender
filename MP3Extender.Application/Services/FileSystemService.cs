using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LibVLCSharp.Shared;
using MP3Extender.Domain.Entities;

namespace MP3Extender.Application.Services
{
	public interface IFileSystemService
	{
		public IEnumerable<AudioFile> LoadFiles(string path, bool loadSubFolders);

		public void SaveFile(AudioFile file);
	}

	public class FileSystemService : IFileSystemService
	{
		private readonly LibVLC         _vlc;
		private readonly IMetaDataStore _store;

		public FileSystemService(LibVLC vlc, IMetaDataStore store)
		{
			_vlc   = vlc;
			_store = store;
		}


		/// <inheritdoc />
		public IEnumerable<AudioFile> LoadFiles(string path, bool loadSubFolders)
		{
			if (!Directory.Exists(path)) yield break;

			var files = Directory.EnumerateFiles(path,
												 string.Empty,
												 loadSubFolders
													 ? SearchOption.AllDirectories
													 : SearchOption.TopDirectoryOnly)
								 .Where(file => IsFileReadable(file).Result);

			foreach (string file in files)
			{
				using var media = new Media(_vlc, file);

				yield return new AudioFile
				{
					Location  = file,
					Interpret = media.Meta(MetadataType.Artist),
					Title     = media.Meta(MetadataType.Title),
					Data      = _store.LoadMetaData(MD5.HashData(File.ReadAllBytes(file)))
				};
			}
		}

		/// <inheritdoc />
		public void SaveFile(AudioFile file)
		{
			_store.SaveMetaData(file.Data);
		}

		private async Task<bool> IsFileReadable(string path)
		{
			using var media = new Media(_vlc, path);

			if (media.Type != MediaType.File) return false;

			var status = await media.Parse();

			return status == MediaParsedStatus.Done;
		}
	}
}