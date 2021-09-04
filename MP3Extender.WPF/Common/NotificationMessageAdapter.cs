using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace MP3Extender.WPF
{
	public class NotificationMessageAdapter<T> : INotificationHandler<T> where T : class, INotification
	{
		private readonly IMessenger _messenger;

		public NotificationMessageAdapter(IMessenger messenger) => _messenger = messenger;

		public Task Handle(T notification, CancellationToken token)
		{
			_messenger.Send(notification);
			return Task.CompletedTask;
		}
	}
}