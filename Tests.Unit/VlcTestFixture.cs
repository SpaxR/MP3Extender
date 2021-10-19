using System;
using LibVLCSharp.Shared;

namespace Tests.Unit
{
	public sealed class VlcTestFixture : IDisposable
	{
		public const string SampleFilename = "sample.mp3";

		public LibVLC VLC { get; }

		public VlcTestFixture()
		{
			Core.Initialize();
			VLC = new LibVLC("--no-audio", "--no-video");
		}

		/// <inheritdoc />
		public void Dispose()
		{
			VLC?.Dispose();
		}
	}
}