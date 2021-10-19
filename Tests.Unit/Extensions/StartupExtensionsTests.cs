using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MP3Extender.MetroUI;
using MP3Extender.MetroUI.Common;
using MP3Extender.MetroUI.Localization;
using Xunit;

namespace Tests.Unit.Extensions
{
	public class StartupExtensionsTests : DefaultTestBase<ServiceCollection>
	{
		[Fact]
		public void AddViewModels_AddsAllClassesNamedViewModelToCollection()
		{
			SUT.AddViewModels();

			int viewModelCount = typeof(App).Assembly
											.GetTypes()
											.Count(type => type.IsSubclassOf(typeof(ViewModelBase)));

			Assert.Equal(viewModelCount, SUT.Count);
		}

		[Fact]
		public void AddLocalization_AddsILocalizationProvider()
		{
			SUT.AddLocalization();

			var localizationService = SUT.Single(service => service.ServiceType == typeof(ILocalizationProvider));

			Assert.NotNull(localizationService);
		}
	}
}