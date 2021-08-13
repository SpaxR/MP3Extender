using System.Threading.Tasks;
using MediatR;
using Microsoft.Toolkit.Mvvm.Messaging;
using Moq;
using UI;
using Xunit;

namespace Tests.Unit.Commands
{
	public class NotificationMessageAdapterTests : TestBase<NotificationMessageAdapter<INotification>>
	{
		/// <inheritdoc />
		protected override NotificationMessageAdapter<INotification> CreateSUT() => new(Messenger);

		[Fact]
		public async Task GivenNotification_ThenPublishesMessage()
		{
			var notificationMock = new Mock<INotification>();
			var recipient        = new Mock<IRecipient<INotification>>();
			Messenger.Register(recipient.Object);

			await SUT.Handle(notificationMock.Object, default);

			recipient.Verify(r => r.Receive(notificationMock.Object));
		}
	}
}