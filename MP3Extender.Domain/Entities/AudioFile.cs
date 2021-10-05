using System.Collections.Generic;

namespace MP3Extender.Domain.Entities
{
	public class AudioFile
	{
		public string                      Location { get; set; }
		public IDictionary<string, string> MetaData { get; set; }
	}
}