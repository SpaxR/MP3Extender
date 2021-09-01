using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace UI.Configuration
{
	public interface IConfig
	{
		public bool   UseDarkTheme { get; set; }
		public string RootFolder   { get; set; }
	}

	[ExcludeFromCodeCoverage]
	public sealed class Config : ApplicationSettingsBase, IConfig
	{
		[UserScopedSetting, DefaultSettingValue("False")]
		public bool UseDarkTheme
		{
			get => (bool)this[nameof(UseDarkTheme)];
			set => this[nameof(UseDarkTheme)] = value;
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