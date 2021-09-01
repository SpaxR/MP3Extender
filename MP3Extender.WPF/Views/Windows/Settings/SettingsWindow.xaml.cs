using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace UI.Settings
{
	[ExcludeFromCodeCoverage]
	public partial class SettingsWindow
	{
		public SettingsWindow() => InitializeComponent();

		private void CloseWindow(object sender, RoutedEventArgs e) => Close();
	}
}