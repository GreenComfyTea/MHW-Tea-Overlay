using ImGuiNET;
using SharpPluginLoader.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public class LocalizationManager
{
	// Singleton Pattern
	private static readonly LocalizationManager singleton = new();

	public static LocalizationManager Instance { get { return singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static LocalizationManager() { }

	// Singleton Pattern End
	private LocalizationWatcher LocalizationWatcherInstance { get; set; }
	private LocalizationManager() { }

	public Dictionary<string, Localization> Localizations { get; set; } = new();
	public Localization Default { get; set; }
	public Localization Current { get; set; }

	public LocalizedStrings_LocalizationInfo LocalizationInfo { get; set; }
	public LocalizedStrings_UI UI { get; set; }
	public LocalizedStrings_ImGui ImGui { get; set; }

	public LocalizationManager Init()
	{
		TeaLog.Info("LocalizationManager: Initializing...");

		Default = new Localization();
		Default.Init();

		SetCurrentLocalization(Default);
		LoadAllLocalizations();

		LocalizationWatcherInstance = new LocalizationWatcher();
		TeaLog.Info("LocalizationManager: Done!");

		return this;
	}

	public LocalizationManager SetCurrentLocalization(Localization localization)
	{
		Current = localization;

		LocalizationInfo = localization.LocalizationInfo;
		UI = localization.UI;
		ImGui = localization.ImGui;

		return this;
	}

	public LocalizationManager LoadAllLocalizations()
	{

		TeaLog.Info("LocalizationManager: Loading All Localizations...");

		Localizations = new();
		Localizations[Default.Name] = Default;


		foreach (var localalizationFileNamePath in Directory.EnumerateFiles(Constants.LOCALIZATIONS_PATH, "*.json"))
		{
			LoadLocalization(localalizationFileNamePath);
		}

		return this;
	}

	public LocalizationManager LoadLocalization(string localizationFileNamePath)
	{
		try
		{
			var localizationName = Path.GetFileNameWithoutExtension(localizationFileNamePath);

			if (localizationName.Equals(Default.Name)) return this;

			TeaLog.Info($"Localization {localizationName}: Loading...");

			var json = JsonManager.ReadFromFile(localizationFileNamePath);

			var localization = JsonSerializer.Deserialize<Localization>(json, JsonManager.JsonSerializerOptionsInstance).Init(localizationName);

			Localizations[localizationName] = localization;

			//if(localizationName.Equals(Current.Name))
			//{
			//Current = localization;
			//}

			return this;
		}
		catch(Exception exception)
		{
			TeaLog.Error(exception.ToString());
			return this;
		}
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
