using Microsoft.Toolkit.Mvvm.Messaging;
using UI;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class ViewModelBaseTests : TestBase<ViewModelBase>
	{
		private class DerivedViewModelBase : ViewModelBase
		{
			public DerivedViewModelBase(IMessenger messenger) : base(messenger) { }
		}

		/// <inheritdoc />
		protected override ViewModelBase CreateSUT() => new DerivedViewModelBase(Messenger);

		[Fact]
		public void GivenViewModel_WhenUnchanged_ThenIsActiveIsTrue()
		{
			Assert.True(SUT.IsActive);
		}

		[Fact]
		public void GivenViewModel_WhenDisposing_ThenIsActiveIsFalse()
		{
			SUT.Dispose();

			Assert.False(SUT.IsActive);
		}
	}
}