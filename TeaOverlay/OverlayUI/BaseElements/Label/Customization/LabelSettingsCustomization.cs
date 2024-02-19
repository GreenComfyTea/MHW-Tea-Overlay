using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeaOverlay;

internal class LabelSettingsCustomization : SingletonAccessor
{

	private int rightAlignmentShift = 0;
	public int RightAlignmentShift { get => rightAlignmentShift; set => rightAlignmentShift = value; }


	public LabelSettingsCustomization() {}

	public LabelSettingsCustomization Init()
	{
		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;
		var selectedIndex = 0;

		if (ImGui.TreeNode(localizationManager.ImGui.Settings))
		{
			changed = ImGui.SliderInt(localizationManager.ImGui.RightAlignmentShift, ref rightAlignmentShift, 0, 64) || changed;

			ImGui.TreePop();

		}

		return changed;
	}
}
