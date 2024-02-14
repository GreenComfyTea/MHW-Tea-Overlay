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

	public async void Init()
	{
		TeaLog.Info("LocalizationManager: Initializing...");

		TeaLog.Info("-1");
		Default = new Localization();
		TeaLog.Info("0");
		await Default.Init();

		TeaLog.Info("1");
		SetCurrentLocalization(Default);
		TeaLog.Info("2");
		await LoadAllLocalizations();
		TeaLog.Info("3");
		LocalizationWatcherInstance = new LocalizationWatcher();
		TeaLog.Info("LocalizationManager: Done!");
	}

	public void SetCurrentLocalization(Localization localization)
	{
		Current = localization;

		LocalizationInfo = localization.LocalizationInfo;
		UI = localization.UI;
		ImGui = localization.ImGui;
	}

	public async Task LoadAllLocalizations()
	{

		TeaLog.Info("LocalizationManager: Loading All Localizations...");

		Localizations = new();
		Localizations[Default.Name] = Default;


		foreach (var localalizationFileNamePath in Directory.EnumerateFiles(Constants.LOCALIZATIONS_PATH, "*.json"))
		{
			await LoadLocalization(localalizationFileNamePath);
		}
	}

	public async Task LoadLocalization(string localizationFileNamePath)
	{
		try
		{
			var localizationName = Path.GetFileNameWithoutExtension(localizationFileNamePath);

			if (localizationName.Equals(Default.Name)) return;

			TeaLog.Info($"Localization {localizationName}: Loading...");

			var json = await JsonManager.ReadFromFile(localizationFileNamePath);

			var localization = await JsonSerializer.Deserialize<Localization>(json, JsonManager.JsonSerializerOptionsInstance).Init(localizationName);

			Localizations[localizationName] = localization;

			//if(localizationName.Equals(Current.Name))
			//{
				//Current = localization;
			//}
		}
		catch(Exception exception)
		{
			TeaLog.Info(exception.ToString());
		}
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
