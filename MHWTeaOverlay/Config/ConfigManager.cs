using Iced.Intel;
using ImGuiNET;
using SharpPluginLoader.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public class ConfigManager
{
	// Singleton Pattern
	private static readonly ConfigManager singleton = new();

	public static ConfigManager Instance { get { return singleton; } }

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static ConfigManager() { }

	// Singleton Pattern End
	private ConfigWatcher ConfigWatcherInstance { get; set; }

	private SelectedConfigWatcher SelectedConfigWatcherInstance { get; set; }

	public Dictionary<string, Config> Configs { get; set; } = new();
	public Config Default { get; set; }
	public Config Current { get; set; }

	public SelectedConfigClass SelectedConfigInstance { get; set; }

	private ConfigManager() { }

	public async void Init()
	{
		TeaLog.Info("ConfigManager: Initializing...");

		Default = await new Config().Init();
		//SetCurrentConfig(Default);

		await LoadAllConfigs();
		await LoadSelectedConfig();

		ConfigWatcherInstance = new();
		SelectedConfigWatcherInstance = new();


		TeaLog.Info("ConfigManager: Done!");
	}

	public void SetCurrentConfig(Config config)
	{
		Current = config;
		SelectedConfigInstance.SelectedConfig = config.Name;
		_ = SelectedConfigInstance.Save();
	}

	public async Task LoadAllConfigs()
	{

		TeaLog.Info("ConfigManager: Loading All Configs...");

		Configs = new();

		foreach (var localalizationFileNamePath in Directory.EnumerateFiles(Constants.CONFIGS_PATH, "*.json"))
		{
			await LoadConfig(localalizationFileNamePath);
		}
	}

	public async Task LoadConfig(string configFileNamePath)
	{
		try
		{
			var configName = Path.GetFileNameWithoutExtension(configFileNamePath);

			TeaLog.Info($"Config {configName}: Loading...");

			var json = await JsonManager.ReadFromFile(configFileNamePath);

			var config = await JsonSerializer.Deserialize<Config>(json, JsonManager.JsonSerializerOptionsInstance).Init(configName);

			Configs[configName] = config;

			//if(configName.Equals(Current.Name))
			//{
			//Current = config;
			//}
		}
		catch (Exception exception)
		{
			TeaLog.Info(exception.ToString());
		}
	}

	public async Task LoadSelectedConfig()
	{
		try
		{
			TeaLog.Info($"Selected Config File: Loading...");

			var json = await JsonManager.ReadFromFile(Constants.CONFIG_FILE_PATH_NAME);

			var selectedConfig = JsonSerializer.Deserialize<SelectedConfigClass>(json, JsonManager.JsonSerializerOptionsInstance);

			Config newConfig;
			
			var success = Configs.TryGetValue(selectedConfig.SelectedConfig, out newConfig);

			if (!success) {
				await SelectedConfigInstance.Save();
				return;
			}

			SelectedConfigInstance = selectedConfig;
			SetCurrentConfig(newConfig);
		}
		catch (Exception exception)
		{
			TeaLog.Info(exception.ToString());
		}
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
