using ImGuiNET;
using SharpPluginLoader.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MHWTeaOverlay;

public class Config : SingletonAccessor
{
	[JsonIgnore]
	public string Name { get; set; } = "";

	public Config() { }

	public async Task<Config> Init()
	{
		Name = Constants.DEFAULT_CONFIG;

		TeaLog.Info($"Config {Name}: Initializing...");
		
		await Save();

		TeaLog.Info($"Config {Name}: Done!");

		return this;
	}

	public async Task<Config> Init(string name)
	{
		Name = name;

		TeaLog.Info($"Config {Name}: Initializing...");

		await Save();

		TeaLog.Info($"Config {Name}: Done!");

		return this;
	}

	public async Task Save()
	{
		TeaLog.Info($"Config {Name}: Saving...");

		await JsonManager.SearializeToFile(Path.Combine(Constants.CONFIGS_PATH, $"{Name}.json"), this);
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
