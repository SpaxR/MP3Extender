using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace MP3Extender.Application
{
	public interface ISettings
	{
		public string ColorTheme { get; set; }
		public string RootFolder { get; set; }
	}

	[ExcludeFromCodeCoverage]
	public sealed class Settings : ApplicationSettingsBase, ISettings
	{
		[UserScopedSetting, DefaultSettingValue("Light")]
		public string ColorTheme
		{
			get => (string)this[nameof(ColorTheme)];
			set => this[nameof(ColorTheme)] = value;
		}

		[UserScopedSetting]
		public string RootFolder
		{
			get => (string)this[nameof(RootFolder)];
			set => this[nameof(RootFolder)] = value;
		}

		// Auto-Save Changes
		/// <inheritdoc />
		public override object this[string propertyName]
		{
			get => base[propertyName];
			set
			{
				base[propertyName] = value;
				Save();
			}
		}
	}
}