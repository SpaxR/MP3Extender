using System.Threading.Tasks;
using MP3Extender.Application;
using MP3Extender.WPF.Commands;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Commands
{
	public class ChangeColorThemeTests : TestBase<ChangeColorThemeHandler>
	{
		private readonly ISettings _settingsMock = Substitute.For<ISettings>();

		/// <inheritdoc />
		protected override ChangeColorThemeHandler CreateSUT() => new(MediatorMock, _settingsMock);

		[Fact]
		public async Task GivenInvalidRequest_WhenCalled_ThenReturnsTank()
		{
			await SUT.Handle(new ChangeColorThemeRequest("INVALID"), default);
		}
	}
}