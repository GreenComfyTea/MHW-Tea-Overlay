using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public class BarCustomization : SingletonAccessor
{
	[JsonIgnore]
	private bool visibility = true;
	public bool Visibility { get => visibility; set => visibility = value; }

	public BarSettingsCustomization Settings { get; set; } = new();
	public OffsetCustomization Offset { get; set; } = new();
	public SizeCustomization Size { get; set; } = new();
	public BarOutlineCustomization Outline { get; set; } = new();
	public BarColorsCustomization Colors { get; set; } = new();

	[JsonIgnore]
	public BarCustomizationInternal Internal { get; set; }

	[JsonIgnore]
	public List<Bar> Bars { get; set; } = new();

	public BarCustomization()
	{
		Internal = new(this);
	}

	public BarCustomization Init()
	{
		Settings.Init();
		Outline.Init();
		Colors.Init();

		Internal.Precalculate();

		return this;
	}


	public bool RenderImGui()
	{
		var changed = false;

		if (ImGui.TreeNode("Bar"))
		{
			changed = ImGui.Checkbox(localizationManager.ImGui.Visible, ref visibility) || changed;

			changed = Settings.RenderImGui() || changed;
			changed = Offset.RenderImGui() || changed;
			changed = Size.RenderImGui() || changed;
			changed = Outline.RenderImGui() || changed;
			changed = Colors.RenderImGui() || changed;

			ImGui.TreePop();
		}

		if (changed)
		{
			Internal.Precalculate();

			foreach (var bar in Bars)
			{
				bar.Internal
					.CalculateFromPercentage1()
					.CalculateFromPosition2()
					.ScaleOpacity3();
			}
		}

		return changed;
	}
}