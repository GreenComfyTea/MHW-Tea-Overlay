using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaOverlay;

internal class LabelFontCustomization : SingletonAccessor
{
	private int size = 26;
	public int Size { get => size; set => size = value; }

	public LabelFontCustomization Init()
	{
		return this;
	}

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Font))
		{
			changed = ImGui.SliderInt(localizationManager.ImGui.Size, ref size, 1, 128) || changed;


			ImGui.TreePop();
		}

		return changed;
	}
}
