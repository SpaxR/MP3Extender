using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace MP3Extender.MetroUI.Common
{
	public abstract class ViewModelBase : ObservableRecipient, IDisposable
	{
		protected ViewModelBase() : this(StrongReferenceMessenger.Default) { }

		/// <inheritdoc />
		protected ViewModelBase(IMessenger messenger) : base(messenger)
		{
			IsActive = true; // TODO: Figure out why IsActive is not automatically true
		}

		/// <inheritdoc />
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IsActive = false;
			}
		}
	}
}