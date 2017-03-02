using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ShellES.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"), CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DefaultSettingValue("Файлы оболочки ЭС (*.esdb)|*.esdb"), UserScopedSetting, DebuggerNonUserCode]
		public string FilePathFilter
		{
			get
			{
				return (string)this["FilePathFilter"];
			}
			set
			{
				this["FilePathFilter"] = value;
			}
		}

		[DefaultSettingValue("Текстовые файлы (*.txt)|*.txt"), UserScopedSetting, DebuggerNonUserCode]
		public string ExportPathFilter
		{
			get
			{
				return (string)this["ExportPathFilter"];
			}
			set
			{
				this["ExportPathFilter"] = value;
			}
		}
	}
}
