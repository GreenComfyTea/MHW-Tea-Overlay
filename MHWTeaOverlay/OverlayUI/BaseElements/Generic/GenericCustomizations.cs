using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public class OffsetCustomization : SingletonAccessor
{
	[JsonIgnore]
	private float x = 0f;
	public float X { get => x; set => x = value; }

	[JsonIgnore]
	private float y = 0f;
	public float Y { get => y; set => y = value; }

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Offset))
		{
			changed = ImGui.DragFloat(localizationManager.ImGui.X, ref x,
				Constants.DRAG_FLOAT_SPEED, Constants.DRAG_FLOAT_MIN, Constants.DRAG_FLOAT_MAX, X.ToString(Constants.DRAG_FLOAT_FORMAT)) || changed;

			changed = ImGui.DragFloat(localizationManager.ImGui.Y, ref y,
				Constants.DRAG_FLOAT_SPEED, Constants.DRAG_FLOAT_MIN, Constants.DRAG_FLOAT_MAX, Y.ToString(Constants.DRAG_FLOAT_FORMAT)) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}

public class SizeCustomization : SingletonAccessor
{
	[JsonIgnore]
	private float width = 200f;
	public float Width { get => width; set => width = value; }

	[JsonIgnore]
	private float height = 20f;
	public float Height { get => height; set => height = value; }

	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Size))
		{
			changed = ImGui.DragFloat(localizationManager.ImGui.Width, ref width,
				Constants.DRAG_FLOAT_SPEED, 0f, Constants.DRAG_FLOAT_MAX, Width.ToString("0.0")) || changed;

			changed = ImGui.DragFloat(localizationManager.ImGui.Height, ref height,
				Constants.DRAG_FLOAT_SPEED, 0f, Constants.DRAG_FLOAT_MAX, Height.ToString("0.0")) || changed;

			ImGui.TreePop();
		}

		return changed;
	}
}