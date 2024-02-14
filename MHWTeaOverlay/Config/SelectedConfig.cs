using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MHWTeaOverlay;

public class SelectedConfigClass
{
	public string SelectedConfig { get; set; } = "default";

	public async Task Save()
	{
		TeaLog.Info($"Selected Config File: Saving...");

		await JsonManager.SearializeToFile(Constants.CONFIG_FILE_PATH_NAME, this);
	}
}
