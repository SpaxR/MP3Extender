using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MP3Extender.Application.Services;
using MP3Extender.Domain.Entities;
using Xunit;

namespace Tests.Unit.Services
{
	public sealed class MetaDataStoreTests : TestBase<MetaDataStore>, IDisposable
	{
		/// <inheritdoc />
		protected override MetaDataStore CreateSUT() => new(SettingsMock);

		private readonly string _store = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

		public MetaDataStoreTests()
		{
			SettingsMock.MetaDataStorePath = _store;
		}

		/// <inheritdoc />
		public void Dispose()
		{
			File.Delete(_store);
		}

		[Fact]
		public void SaveMetaData_PersistsDataInPathFromSettings()
		{
			var data = new MetaData { Hash = "HASH" };

			SUT.SaveMetaData(data);

			Assert.True(File.Exists(_store));
			var store = JsonSerializer.Deserialize<IList<MetaData>>(File.ReadAllText(_store));

			Assert.NotNull(store);
			Assert.Equal(data.Hash, store.Single().Hash);
			Assert.Equal(data.Data, store.Single().Data);
		}

		[Fact]
		public void SaveMetaData_WithExistingData_AddsNewData()
		{
			File.WriteAllText(_store, JsonSerializer.Serialize(new List<MetaData> { new() { Hash = "HASH1" } }));

			SUT.SaveMetaData(new MetaData { Hash = "HASH2" });

			var store = JsonSerializer.Deserialize<List<MetaData>>(File.ReadAllText(_store));
			Assert.NotNull(store);
			Assert.Equal(2, store.Count);
			Assert.Contains(store, entry => entry.Hash.Equals("HASH1"));
			Assert.Contains(store, entry => entry.Hash.Equals("HASH2"));
		}

		[Fact]
		public void SaveMetaData_WithExistingData_UpdatesExistingData()
		{
			var expectation = new Dictionary<string, string[]>();
			File.WriteAllText(_store, JsonSerializer.Serialize(new List<MetaData> { new() { Hash = "HASH1" } }));

			SUT.SaveMetaData(new MetaData { Hash = "HASH1", Data = expectation });

			var store = JsonSerializer.Deserialize<List<MetaData>>(File.ReadAllText(_store));
			Assert.NotNull(store);
			
			var result = Assert.Single(store);
			Assert.Equal(expectation, result.Data);
		}


		[Fact]
		public void LoadMetaData_WithoutExistingData_ReturnsNewInstance()
		{
			const string hash = "HASH";

			var result = SUT.LoadMetaData(hash);

			Assert.NotNull(result);
			Assert.Equal(hash, result.Hash);
		}

		[Fact]
		public void LoadMetaData_ReturnsExistingData()
		{
			const string hash  = "HASH";
			var          store = new List<MetaData> { new MetaData { Hash = hash } };
			File.WriteAllText(_store, JsonSerializer.Serialize(store));

			var result = SUT.LoadMetaData(hash);

			Assert.Equal(store.Single().Hash, result.Hash);
			Assert.Equal(store.Single().Data, result.Data);
		}
	}
}