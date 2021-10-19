using System.Collections.Generic;

namespace MP3Extender.Domain.Entities
{
	public class MetaData
	{
		public string                        Hash { get; set; }
		public IDictionary<string, string[]> Data { get; set; }
	}
}