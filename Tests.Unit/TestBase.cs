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


		protected Mock<ILogger<T>> LoggerMock    = new();
		protected Mock<IMediator>  MediatorMock  = new();
		protected Mock<IMessenger> MessengerMock = new();

		protected abstract T CreateSUT();
	}
}