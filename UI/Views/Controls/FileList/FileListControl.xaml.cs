using Microsoft.Extensions.DependencyInjection;

namespace UI.Controls.FileList
{
	public partial class FileListControl
	{
		public FileListControl()
		{
			DataContext = App.Current.Services.GetService<FileListViewModel>();
			InitializeComponent();
		}
	}
}