namespace MP3Extender.Domain.Entities
{
	public class AudioFile
	{
		public string Location { get; set; }

		public string Title     { get; set; }
		public string Interpret { get; set; }

		public MetaData Data { get; set; }
	}
}