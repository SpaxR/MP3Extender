using System;
using UI;

namespace Tests.Unit
{
	public sealed class ApplicationFixture : IDisposable
	{
		private readonly App _instance;

		public ApplicationFixture()
		{
			_instance = new App();
		}

		/// <inheritdoc />
		public void Dispose() => _instance.Shutdown();
	}
}