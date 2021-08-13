using UI;
using Xunit;

namespace Tests.Unit.Commands
{
	public class MessageNotificationAdapterTests : TestBase<MessageNotificationAdapter<object>>
	{
		/// <inheritdoc />
		protected override MessageNotificationAdapter<object> CreateSUT() => new(MediatorMock.Object);


		[Fact]
		public void GivenMessage_WhenReceive_PublishesMessage()
		{
			object message = new();

			SUT.Receive(message);

			MediatorMock.Verify(m => m.Publish(message, default));
		}
	}
}