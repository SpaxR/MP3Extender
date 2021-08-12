using Microsoft.Extensions.DependencyInjection;

namespace UI
{
	/// <summary>Interaction logic for MainWindow.xaml</summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			DataContext = App.Current.Services.GetService<MainViewModel>();
			InitializeComponent();
		}
	}
}