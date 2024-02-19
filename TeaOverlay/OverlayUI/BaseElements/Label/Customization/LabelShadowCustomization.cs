using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeaOverlay;

internal class LabelShadowCustomization : SingletonAccessor
{
	[JsonIgnore]
	private bool visibility = true;
	public bool Visibility { get => visibility; set => visibility = value; }

	public OffsetCustomization Offset { get; set; } = new(2f, 2f);

	// Shadow Color

	// For Config, RGBA
	[JsonIgnore]
	private string colorRgbaString = "0x000000FF";
	public string Color { get => colorRgbaString; set => colorRgbaString = value; }

	// For Draw, ABGR
	[JsonIgnore]
	private uint colorDrawAbgr = 0xFF000000;
	[JsonIgnore]
	public uint ColorDrawAbgr { get => colorDrawAbgr; set => colorDrawAbgr = value; }

	// For ImGui, RGBA
	[JsonIgnore]
	private Vector4 colorImGuiRgba = new(0f, 0f, 0f, 1f);
	[JsonIgnore]
	public Vector4 ColorImGuiRgba { get => colorImGuiRgba; set => colorImGuiRgba = value; }

	// For OpacityScale
	[JsonIgnore]
	private byte colorRed = 0;
	[JsonIgnore]
	public byte ColorRed { get => colorRed; set => colorRed = value; }

	[JsonIgnore]
	private byte colorGreen = 0;
	[JsonIgnore]
	public byte ColorGreen { get => colorGreen; set => colorGreen = value; }

	[JsonIgnore]
	private byte colorBlue = 0;
	[JsonIgnore]
	public byte ColorBlue { get => colorBlue; set => colorBlue = value; }

	[JsonIgnore]
	private byte colorAlpha = 255;
	[JsonIgnore]
	public byte ColorAlpha { get => colorAlpha; set => colorAlpha = value; }

	public LabelShadowCustomization Init()
	{
		_ = ColorUtils.UpdateColorsFromRgbaString(ref colorRgbaString, ref colorDrawAbgr, colorImGuiRgba,
			ref colorRed, ref colorGreen, ref colorBlue, ref colorAlpha);

		return this;
	}

	public async Task OnShadowColorChanged()
	{
		ColorUtils.UpdateColorsFromRgbaVector(colorImGuiRgba, ref colorRgbaString, ref colorDrawAbgr,
			ref colorRed, ref colorGreen, ref colorBlue, ref colorAlpha);
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Shadow))
		{
			changed = ImGui.Checkbox(localizationManager.ImGui.Visible, ref visibility) || changed;

			changed = Offset.RenderImGui() || changed;

			if (ImGui.TreeNode(localizationManager.ImGui.Color))
			{
				tempChanged = ImGui.ColorPicker4("", ref colorImGuiRgba);
				if (tempChanged) _ = OnShadowColorChanged();

				changed = changed || tempChanged;

				ImGui.TreePop();
			}


			ImGui.TreePop();
		}

		return changed;
	}
}
