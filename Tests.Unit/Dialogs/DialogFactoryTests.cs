using MP3Extender.WPF;
using Xunit;

namespace Tests.Unit.Dialogs
{
	public class DialogFactoryTests : TestBase<DialogFactory>
	{
		/// <inheritdoc />
		protected override DialogFactory CreateSUT() => new();


		[UIFact]
		public void GivenInstance_WhenCreateFolderBrowserDialog_ThenReturnsNewFolderBrowserDialog()
		{
			Assert.NotNull(SUT.CreateFolderBrowserDialog());
		}
	}
}