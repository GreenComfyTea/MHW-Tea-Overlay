using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public static class DefaultLocalization
{
	public static void Initialize(Localization localization)
	{
		var UI = localization.UI;
		var ImGui = localization.ImGui;

		UI["test"] = "1";
		UI["test2"] = "2";
		UI["test3"] = "3";

		ImGui["test4"] = "4";
		ImGui["test5"] = "5";
		ImGui["test6"] = "6";
	}
}
