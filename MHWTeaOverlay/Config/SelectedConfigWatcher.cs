using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MHWTeaOverlay;

public class SelectedConfigWatcher : SingletonAccessor
{
	private DateTime LastEventTime = DateTime.Now;

	private FileSystemWatcher Watcher { get; }

	public SelectedConfigWatcher()
	{
		TeaLog.Info("SelectedConfigChangeWatcher: Initializing...");

		Watcher = new(Constants.PLUGIN_DATA_PATH);

		Watcher.NotifyFilter = NotifyFilters.Attributes
							 | NotifyFilters.CreationTime
							 | NotifyFilters.FileName
							 | NotifyFilters.LastWrite
							 | NotifyFilters.Security
							 | NotifyFilters.Size;

		Watcher.Changed += OnSelectedConfigFileChanged;
		Watcher.Created += OnSelectedConfigFileCreated;
		Watcher.Deleted += OnSelectedConfigFileDeleted;
		Watcher.Renamed += OnSelectedConfigFileRenamed;
		Watcher.Error += OnSelectedConfigFileError;

		Watcher.Filter = $"{Constants.DEFAULT_CONFIG}.json";
		Watcher.EnableRaisingEvents = true;

		TeaLog.Info("SelectedConfigChangeWatcher: Done!");
	}

	private void OnSelectedConfigFileChanged(object sender, FileSystemEventArgs e)
	{
		if (e.ChangeType != WatcherChangeTypes.Changed) return;

		TeaLog.Info($"SelectedConfigChangeWatcher: Changed {e.Name}");

		UpdateConfig(e.FullPath, e.Name);
	}

	private void OnSelectedConfigFileCreated(object sender, FileSystemEventArgs e)
	{

		TeaLog.Info($"SelectedConfigChangeWatcher: Created {e.Name}");

		UpdateConfig(e.FullPath, e.Name);
	}

	private void OnSelectedConfigFileDeleted(object sender, FileSystemEventArgs e)
	{
		TeaLog.Info($"SelectedConfigChangeWatcher: Deleted {e.Name}");
	}

	private void OnSelectedConfigFileRenamed(object sender, RenamedEventArgs e)
	{
		TeaLog.Info($"SelectedConfigChangeWatcher: Renamed {e.OldName} to {e.Name}");

		configManager.Configs.Remove(e.OldName);

		//UpdateConfig(e.FullPath, e.Name);
	}

	private void OnSelectedConfigFileError(object sender, ErrorEventArgs e)
	{
		TeaLog.Info(e.GetException().ToString());
	}

	private void UpdateConfig(string filePathName, string fileName)
	{
		if (!fileName.Equals(Watcher.Filter)) return;

		DateTime currentEventTime = DateTime.Now;
		if ((currentEventTime - LastEventTime).Seconds < 1) return;
		LastEventTime = currentEventTime;

		//_ = configManager.LoadConfig(filePathName);
		Timers.SetTimeout(() => _ = configManager.LoadSelectedConfig(), 250);
	}

	public override string ToString()
	{
		return JsonManager.Serialize(this);
	}
}
