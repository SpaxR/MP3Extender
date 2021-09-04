using System.Threading.Tasks;
using MediatR;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.WPF;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Commands
{
	public class NotificationMessageAdapterTests : TestBase<NotificationMessageAdapter<INotification>>
	{
		/// <inheritdoc />
		protected override NotificationMessageAdapter<INotification> CreateSUT() => new(MessengerMock);

		[Fact]
		public async Task GivenNotification_ThenPublishesMessage()
		{
			var notificationMock = Substitute.For<INotification>();

			await SUT.Handle(notificationMock, default);

			MessengerMock.Received(1).Send(notificationMock);
		}
	}
}