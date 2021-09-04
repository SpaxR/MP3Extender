using MP3Extender.WPF;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Commands
{
	public class MessageNotificationAdapterTests : TestBase<MessageNotificationAdapter<object>>
	{
		/// <inheritdoc />
		protected override MessageNotificationAdapter<object> CreateSUT() => new(MediatorMock);


		[Fact]
		public void GivenMessage_WhenReceive_PublishesMessage()
		{
			object message = new();

			SUT.Receive(message);

			MediatorMock.Received(1).Publish(message);
		}
	}
}