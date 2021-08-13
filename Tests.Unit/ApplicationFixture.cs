using System;
using UI;

namespace Tests.Unit
{
	public sealed class ApplicationFixture : IDisposable
	{
		public App Instance { get; }

		public ApplicationFixture()
		{
			Instance = new App();
		}

		/// <inheritdoc />
		public void Dispose() => Instance.Shutdown();
	}
}