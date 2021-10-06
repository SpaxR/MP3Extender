using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace MP3Extender.Application
{
	public interface ISettings
	{
		public string[] AvailableThemes { get; }

		public bool   UseDarkColors      { get; set; }
		public string ColorTheme         { get; set; }
		public string RootFolder         { get; set; }
		public bool   LoadRecursiveFiles { get; set; }

		public string[] FileEditorColumns { get; set; }
	}

	[ExcludeFromCodeCoverage]
	public sealed class Settings : ApplicationSettingsBase, ISettings
	{
		public string[] AvailableThemes { get; } = { "Blue", "Red" };

		[UserScopedSetting, DefaultSettingValue("true")]
		public bool UseDarkColors
		{
			get => (bool)this[nameof(UseDarkColors)];
			set => this[nameof(UseDarkColors)] = value;
		}

		[UserScopedSetting, DefaultSettingValue("Blue")]
		public string ColorTheme
		{
			get => (string)this[nameof(ColorTheme)];
			set => this[nameof(ColorTheme)] = value;
		}

		[UserScopedSetting, DefaultSettingValue("C:\\")]
		public string RootFolder
		{
			get => (string)this[nameof(RootFolder)];
			set => this[nameof(RootFolder)] = value;
		}

		[UserScopedSetting, DefaultSettingValue("false")]
		public bool LoadRecursiveFiles
		{
			get => (bool)this[nameof(LoadRecursiveFiles)];
			set => this[nameof(LoadRecursiveFiles)] = value;
		}

		[UserScopedSetting]
		public string[] FileEditorColumns
		{
			get => (string[])this[nameof(FileEditorColumns)];
			set => this[nameof(FileEditorColumns)] = value;
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