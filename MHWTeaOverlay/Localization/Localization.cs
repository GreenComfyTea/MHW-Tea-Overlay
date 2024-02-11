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
	public class Localization
	{
		[JsonIgnore]
		private readonly LocalizationManager localizationManager = LocalizationManager.Instance;

		[JsonIgnore]
		public string Name { get; set; } = "";

		[JsonIgnore]
		public bool IsDefault { get; set; } = false;


		public Dictionary<string, string> UI { get; set; } = new();

		public Dictionary<string, string> ImGui { get; set; } = new();

		public Localization() {
			InitializeDefaultValues();
		}

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

			RemoveExtraProperties();
			await Save();

			TeaLog.Info($"Localization {Name}: Done!");

			return this;
		}

		private void InitializeDefaultValues()
		{
			UI["test"] = "1";
			UI["test2"] = "2";
			UI["test3"] = "3";

			ImGui["test4"] = "4";
			ImGui["test5"] = "5";
			ImGui["test6"] = "6";
		}


		public void RemoveExtraProperties()
		{
			TeaLog.Info($"Localization {Name}: Removing Extra Strings...");

			var currentToDefaultMap = new Dictionary<Dictionary<string, string>, Dictionary<string, string>>
			{
				[UI] = localizationManager.Default.UI,
				[ImGui] = localizationManager.Default.ImGui
			};

			foreach (var localizationMapPair in currentToDefaultMap)
			{
				var currentCategory = localizationMapPair.Key;
				var defaultCategory = localizationMapPair.Value;

				var keysToRemove = new LinkedList<string>();

				foreach (var localizationStringPair in currentCategory)
				{
					var localizedStringKey = localizationStringPair.Key;
					var localizedString = localizationStringPair.Value;

					if (!defaultCategory.ContainsKey(localizedStringKey))
					{
						keysToRemove.AddLast(localizedStringKey);
					}
				}

				foreach (var keyToRemove in keysToRemove)
				{
					TeaLog.Info($"Removing {keyToRemove}");

					currentCategory.Remove(keyToRemove);
				}
			}
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
