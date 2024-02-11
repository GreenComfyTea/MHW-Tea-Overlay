using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MHWTeaOverlay;

public class LocalizationWatcher
{
	private LocalizationManager localizationManager = LocalizationManager.Instance;

	private Dictionary<string, DateTime> LastEventTimes = new();

	private FileSystemWatcher Watcher { get; }

	public LocalizationWatcher()
	{
		TeaLog.Info("LocalizationChangeWatcher: Initializing...");

		Watcher = new(Constants.LOCALIZATIONS_PATH);

		Watcher.NotifyFilter = NotifyFilters.Attributes
							 | NotifyFilters.CreationTime
							 | NotifyFilters.FileName
							 | NotifyFilters.LastWrite
							 | NotifyFilters.Security
							 | NotifyFilters.Size;

		Watcher.Changed += OnLocalizationFileChanged;
		Watcher.Created += OnLocalizationFileCreated;
		Watcher.Deleted += OnLocalizationFileDeleted;
		Watcher.Renamed += OnLocalizationFileRenamed;
		Watcher.Error += OnLocalizationFileError;

		Watcher.Filter = "*.json";
		Watcher.EnableRaisingEvents = true;

		TeaLog.Info("LocalizationChangeWatcher: Done!");
	}

	private void OnLocalizationFileChanged(object sender, FileSystemEventArgs e)
	{
		if (e.ChangeType != WatcherChangeTypes.Changed) return;

		TeaLog.Info($"LocalizationChangeWatcher: Changed {e.Name}");

		UpdateLocalization(e.FullPath, e.Name);
	}

	private void OnLocalizationFileCreated(object sender, FileSystemEventArgs e)
	{

		TeaLog.Info($"LocalizationChangeWatcher: Created {e.Name}");

		UpdateLocalization(e.FullPath, e.Name);
	}

	private void OnLocalizationFileDeleted(object sender, FileSystemEventArgs e)
	{
		TeaLog.Info($"LocalizationChangeWatcher: Deleted {e.Name}");
	}

	private void OnLocalizationFileRenamed(object sender, RenamedEventArgs e)
	{
		TeaLog.Info($"LocalizationChangeWatcher: Renamed {e.OldName} to {e.Name}");

		localizationManager.Localizations.Remove(e.OldName);

		UpdateLocalization(e.FullPath, e.Name);
	}

	private void OnLocalizationFileError(object sender, ErrorEventArgs e)
	{
		TeaLog.Info(e.GetException().ToString());
	}

	private void UpdateLocalization(string filePathName, string fileName)
	{
		DateTime currentEventTime = DateTime.Now;
		DateTime lastEventTime;

		var contains = LastEventTimes.TryGetValue(filePathName, out lastEventTime);

		if (contains && (currentEventTime - lastEventTime).Seconds < 1) return;

		LastEventTimes[filePathName] = currentEventTime;

		//_ = localizationManager.LoadLocalization(filePathName);
		Timers.SetTimeout(() => _ = localizationManager.LoadLocalization(filePathName), 250);
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
