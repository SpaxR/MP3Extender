using System.ComponentModel;
using System.Globalization;
using System.Resources;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MP3Extender.WPF.Localization
{
	public interface ILocalizationProvider : INotifyPropertyChanged
	{
		public CultureInfo CurrentCulture { get; set; }
		public string this[string key] { get; }
	}


	public class LocalizationProvider : ObservableObject, ILocalizationProvider
	{
		private readonly ResourceManager _manager;

		public string this[string key] => _manager.GetString(key, CurrentCulture);
		
		public CultureInfo CurrentCulture
		{
			get => CultureInfo.CurrentUICulture;
			set
			{
				CultureInfo.CurrentUICulture = value;
				OnPropertyChanged(new PropertyChangedEventArgs(null));
			}
		}

		public LocalizationProvider(ResourceManager manager) => _manager = manager;
	}
}