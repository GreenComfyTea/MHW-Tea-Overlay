using ABI.System;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Exception = System.Exception;

namespace MHWTeaOverlay;

public class LocalizationManager
{
	// Singleton Pattern
	private static readonly LocalizationManager singleton = new();

	public static LocalizationManager Instance { get { return singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static LocalizationManager() {}

	// Singleton Pattern End

	public Dictionary<string, Localization> Localizations { get; set; } = new();

	public Localization Default { get; set; }
	
	public Localization Current { get; set; }

	private LocalizationWatcher LocalizationWatcherInstance { get; set; }


	private LocalizationManager() { }

	public async void Init()
	{
		TeaLog.Info("LocalizationManager: Initializing...");

		Default = await new Localization().Init();
		Current = Default;

		await LoadAllLocalizations();

		LocalizationWatcherInstance = new LocalizationWatcher();

		TeaLog.Info("LocalizationManager: Done!");
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

			if(localizationName.Equals(Current))
			{
				Current = localization;
			}
		}
		catch(Exception exception)
		{
			TeaLog.Info(exception.ToString());
		}
	}

	public string UI(string key)
	{
		string localizedString;
		
		var currentSuccess = Current.UI.TryGetValue(key, out localizedString);
		if (currentSuccess) return localizedString;

		var defaultSuccess = Default.UI.TryGetValue(key, out localizedString);
		if (defaultSuccess) return localizedString;

		return key;
	}

	public string ImGui(string key)
	{
		string localizedString;

		var currentSuccess = Current.ImGui.TryGetValue(key, out localizedString);
		if (currentSuccess) return localizedString;

		var defaultSuccess = Default.ImGui.TryGetValue(key, out localizedString);
		if (defaultSuccess) return localizedString;

		return key;
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
