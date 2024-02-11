using Newtonsoft.Json.Linq;
using SharpPluginLoader.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace MHWTeaOverlay
{
	public class Locale
	{
		private static readonly LocalizationManager localizationManager = LocalizationManager.Instance;

		public string LocaleName { get; set; } = "";

		public bool IsDefault { get; set; } = false;

		private string Json { get; set; } = "";

		public JObject JsonData { get; set; }

		public Dictionary<string, string> Data { get; set; } = new();


		public Locale()
		{
			TeaLog.Info("Locale en-us: Initializing...");

			LocaleName = "en-us";
			Json = DefaultLocale.Json;
			JsonData = JsonManager.ParseJson(Json);
			IsDefault = true;

			GenerateDictionary();

			SaveToJson();

			TeaLog.Info("Locale en-us: Done!");
		}

		public Locale(string localeName, string json)
		{
			TeaLog.Info($"Locale {localeName}: Initializing...");

			LocaleName = localeName;
			Json = json;
			JsonData = JsonManager.ParseJson(Json);

			SaveToJson();

			TeaLog.Info($"Locale {localeName}: Done!");
		}

		public bool TryGetLocalizedString(string key, out string localizedString)
		{
			localizedString = Data[key];
			
			if (localizedString == null)
			{
				localizedString = string.Empty;
				return false;
			}

			return true;
		}

		public void MergeWithDefault()
		{
			TeaLog.Info($"Locale {LocaleName}: Merging with Default Locale...");

			Locale defaultLocale = localizationManager.Default;

			// Add Missing Fields

			JsonData.Merge(defaultLocale.JsonData);

			// Remove Unnecessary Fields

			List<string> categoryKeysToRemove = new();

			foreach (var categoryPair in JsonData)
			{
				var categoryObject = (JObject) categoryPair.Value;
				var defaultCategoryObject = defaultLocale.JsonData[categoryPair.Key];

				if (defaultCategoryObject == null)
				{
					categoryKeysToRemove.Add(categoryPair.Key);
					continue;
				}

				var stringKeysToRemove = new List<string>();

				foreach (var stringPair in categoryObject)
				{
					if (defaultCategoryObject[stringPair.Key] == null)
					{
						stringKeysToRemove.Add(stringPair.Key);
						continue;
					}
				}

				foreach (var stringKeyToRemove in stringKeysToRemove)
				{
					categoryObject.Remove(stringKeyToRemove);
				}
			}

			foreach (var categoryKeyToRemove in categoryKeysToRemove)
			{
				JsonData.Remove(categoryKeyToRemove);
			}
		}

		public void GenerateDictionary()
		{
			Data = new();

			foreach (var categoryPair in JsonData)
			{
				var categoryObject = (JObject)categoryPair.Value;

				foreach (var stringPair in categoryObject)
				{
					Data[stringPair.Key] = (string) stringPair.Value;
				}
			}
		}

		public void SaveToJson()
		{
			TeaLog.Info($"Locale {LocaleName}: Saving...");

			JsonManager.SaveJson(Path.Combine(Constants.LOCALES_PATH, $"{LocaleName}.json"), Json);
		}
	}
}
