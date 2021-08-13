using Microsoft.Extensions.DependencyInjection;

namespace UI.Controls.FileList
{
	public partial class FileList
	{
		public FileList()
		{
			DataContext = App.Current.Services.GetService<FileListViewModel>();
			InitializeComponent();
		}
	}
}