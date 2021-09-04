using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.Messaging;
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


		protected readonly ILogger<T> LoggerMock = Substitute.For<ILogger<T>>();

		protected readonly IMessenger MessengerMock = Substitute.For<IMessenger>();

		protected readonly IMediator MediatorMock = Substitute.For<IMediator>();

		protected abstract T CreateSUT();
	}
}