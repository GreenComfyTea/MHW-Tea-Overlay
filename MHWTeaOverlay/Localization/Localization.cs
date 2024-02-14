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

namespace MHWTeaOverlay
{
	public class Localization : SingletonAccessor
	{
		[JsonIgnore]
		public string Name { get; set; } = "";

		[JsonIgnore]
		public bool IsDefault { get; set; } = false;

		public LocalizedStrings_LocalizationInfo LocalizationInfo { get; set; } = new();

		public LocalizedStrings_UI UI { get; set; } = new();

		public LocalizedStrings_ImGui ImGui { get; set; } = new();

		public Localization() { }

		public async Task<Localization> Init()
		{
			Name = Constants.DEFAULT_LOCALIZATION;

			TeaLog.Info($"Localization {Name}: Initializing...");

			IsDefault = true;
			await Save();

			TeaLog.Info($"Localization {Name}: Done!");

			return this;
		}

		public async Task<Localization> Init(string name)
		{
			Name = name;

			TeaLog.Info($"Localization {Name}: Initializing...");

			IsDefault = false;
			await Save();

			TeaLog.Info($"Localization {Name}: Done!");

			return this;
		}

		public async Task Save()
		{
			TeaLog.Info($"Localization {Name}: Saving...");

			await JsonManager.SearializeToFile(Path.Combine(Constants.LOCALIZATIONS_PATH, $"{Name}.json"), this);
		}

		public override string ToString()
		{
			return JsonManager.Serialize(this);
		}
	}
}
