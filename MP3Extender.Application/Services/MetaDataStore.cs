using System.Collections.Generic;
using System.Text;
using MP3Extender.Domain.Entities;

namespace MP3Extender.Application.Services
{
	public interface IMetaDataStore
	{
		public MetaData LoadMetaData(byte[] hash);

		public void SaveMetaData(MetaData data);

	}


	public class MetaDataStore : IMetaDataStore
	{
		private readonly IDictionary<string, MetaData> _store = new Dictionary<string, MetaData>();

		/// <inheritdoc />
		public MetaData LoadMetaData(byte[] hash)
		{
			// string key = Encoding.UTF8.GetString(hash);
			//
			// if (_store.ContainsKey(key))
			// {
			// 	return _store[key];
			// }
			//
			// var data = new MetaData
			// {
			// 	Data = new Dictionary<string, string[]>()
			// };
			// _store.Add(key, data);
			// return data;

			return null;
		}

		/// <inheritdoc />
		public void SaveMetaData(MetaData data)
		{
			// string key = Encoding.UTF8.GetString(data.Hash);
			// if (_store.ContainsKey(key))
			// {
			// 	_store[key] = data;
			// }
			// else
			// {
			// 	_store.Add(key, data);
			// }
		}

	}
}