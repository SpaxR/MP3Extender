using System;
using MP3Extender.WPF.Factories;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Dialogs
{
	public class DialogFactoryTests : TestBase<DialogFactory>
	{
		private readonly IServiceProvider _servicesMock = Substitute.For<IServiceProvider>();

		/// <inheritdoc />
		protected override DialogFactory CreateSUT() => new(_servicesMock);

		[UIFact]
		public void CreatingFolderBrowserDialog_ReturnsNewFolderBrowserDialog()
		{
			Assert.NotNull(SUT.CreateFolderBrowserDialog());
		}
	}
}