using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	public Dictionary<string, Locale> Locales { get; set; } = new();

	public Locale Default { get; set; }
	
	public Locale Current { get; set; }


	private LocalizationManager()
	{
		Default = new Locale();

		Current = Default;

		Init();
	}

	public void Init()
	{

		LoadAllLocales();
	}

	public void LoadAllLocales()
	{

		TeaLog.Info("Loading All Locales...");

		Locales = new();
		Locales[Default.LocaleName] = Default;


		foreach (var localeFileNamePath in Directory.EnumerateFiles(Constants.LOCALES_PATH, "*.json"))
		{
			var localeName = Path.GetFileNameWithoutExtension(localeFileNamePath);

			if (localeName.Equals(Default.LocaleName)) continue;

			TeaLog.Info($"Locale {localeName}: Loading...");

			var json = JsonManager.ReadJsonFromFile(localeFileNamePath);
			Locales[localeName] = new Locale(localeName, json);
		}
	}

	public string Get(string key)
	{
		var currentSuccess = Current.TryGetLocalizedString(key, out string currentLocalizedString);
		if (currentSuccess) return currentLocalizedString;

		return key;
	}
}
