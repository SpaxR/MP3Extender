using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MP3Extender.Domain.Entities;

namespace MP3Extender.Application.Services
{
	public interface IMetaDataStore
	{
		public MetaData LoadMetaData(string   hash);
		public void     SaveMetaData(MetaData data);
	}


	public sealed class MetaDataStore : IMetaDataStore
	{
		private readonly ISettings _settings;

		public MetaDataStore(ISettings settings)
		{
			_settings = settings;
		}

		/// <inheritdoc />
		public MetaData LoadMetaData(string hash)
		{
			IList<MetaData> store = LoadStore();

			var data = store?.FirstOrDefault(meta => meta.Hash.Equals(hash));

			data ??= new MetaData
			{
				Hash = hash,
				Data = new Dictionary<string, string[]>()
			};

			return data;
		}

		/// <inheritdoc />
		public void SaveMetaData(MetaData data)
		{
			IList<MetaData> store = LoadStore();

			// Update Store
			var existingMeta = store.FirstOrDefault(meta => meta.Hash.Equals(data.Hash));

			if (existingMeta == null)
			{
				store.Add(data);
			}
			else
			{
				existingMeta.Data = data.Data;
			}

			SaveStore(store);
		}

		private IList<MetaData> LoadStore()
		{
			IList<MetaData> store = null;

			if (File.Exists(_settings.MetaDataStorePath))
			{
				string rawStore = File.ReadAllText(_settings.MetaDataStorePath);
				store = JsonSerializer.Deserialize<IList<MetaData>>(rawStore);
			}

			return store ?? new List<MetaData>();
		}

		private void SaveStore(IList<MetaData> store)
		{
			File.WriteAllText(_settings.MetaDataStorePath, JsonSerializer.Serialize(store));
		}
	}
}