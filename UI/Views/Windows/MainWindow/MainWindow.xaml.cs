namespace UI
{
	/// <summary>Interaction logic for MainWindow.xaml</summary>
	public partial class MainWindow
	{
		public MainWindow(MainViewModel model)
		{
			DataContext = model;
			InitializeComponent();
		}
	}
}