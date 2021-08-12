using System;
using System.Reflection;
using System.Windows;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UI
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App
	{
		public IServiceProvider Services { get; }

		public App() => Services = ConfigureServices();

		/// <inheritdoc />
		protected override void OnStartup(StartupEventArgs e)
		{
			Services.GetRequiredService<MainWindow>().Show();
		}

		private static IServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();
			services.AddMediatR(Assembly.GetExecutingAssembly());

			services.AddSingleton<MainWindow>();
			services.AddSingleton<MainViewModel>();


			return services.BuildServiceProvider();
		}
	}
}