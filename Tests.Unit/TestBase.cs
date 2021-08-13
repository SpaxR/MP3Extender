using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.Messaging;
using Moq;

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


		protected readonly Mock<ILogger<T>> LoggerMock   = new();
		protected readonly Mock<IMediator>  MediatorMock = new();

		protected readonly IMessenger Messenger = new StrongReferenceMessenger();

		protected abstract T CreateSUT();
	}
}