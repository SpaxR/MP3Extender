using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
													 : SearchOption.TopDirectoryOnly);

			foreach (string file in files)
			{
				// TODO Clean up this mess
				string hash = null;

				try
				{
					hash = Encoding.UTF8.GetString(MD5.HashData(File.ReadAllBytes(file)));
				}
				catch (Exception e)
				{
					/* Failed to read File */
				}

				if (hash == null)
				{
					/* Failed to read File */
					continue;
				}

				using var media = new Media(_vlc, file);
				if (media.Parse().Result == MediaParsedStatus.Done)
				{
					yield return new AudioFile
					{
						Location  = file,
						Interpret = media.Meta(MetadataType.Artist),
						Title     = media.Meta(MetadataType.Title),
						Data      = _store.LoadMetaData(hash)
					};
				}
			}
		}

		/// <inheritdoc />
		public void SaveFile(AudioFile file)
		{
			using (var media = new Media(_vlc, file.Location))
			{
				media.SetMeta(MetadataType.Artist, file.Interpret);
				media.SetMeta(MetadataType.Title,  file.Title);
				media.SaveMeta();
			}

			_store.SaveMetaData(file.Data);
		}
	}
}