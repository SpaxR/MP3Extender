using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MP3Extender.MetroUI.Common;
using MP3Extender.MetroUI.Localization;

namespace MP3Extender.MetroUI
{
	public static class StartupExtensions
	{
		public static IServiceCollection AddViewModels(this IServiceCollection services)
		{
			var viewModels = Assembly.GetExecutingAssembly()
									 .GetTypes()
									 .Where(type => type.IsSubclassOf(typeof(ViewModelBase)));

			foreach (var viewModel in viewModels)
			{
				services.AddTransient(viewModel);
			}

			return services;
		}

		public static IServiceCollection AddLocalization(this IServiceCollection services)
		{
			return services.AddSingleton<ILocalizationProvider>(new LocalizationProvider(Strings.ResourceManager));
		}
	}
}