using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.Application;
using MP3Extender.Application.Services;
using NSubstitute;

namespace Tests.Unit
{
	public abstract class TestBase<T>
	{
		private T _sut;

		protected T SUT
		{
			get
			{
				_sut ??= CreateSUT();
				return _sut;
			}
		}

		protected readonly ISettings          SettingsMock   = Substitute.For<ISettings>();
		protected readonly ILogger<T>         LoggerMock     = Substitute.For<ILogger<T>>();
		protected readonly IMessenger         MessengerMock  = Substitute.For<IMessenger>();
		protected readonly IFileSystemService FileSystemMock = Substitute.For<IFileSystemService>();


		protected abstract T CreateSUT();
	}

	public abstract class TestBaseDefault<T> : TestBase<T> where T : new()
	{
		/// <inheritdoc />
		protected override T CreateSUT() => new();
	}
}