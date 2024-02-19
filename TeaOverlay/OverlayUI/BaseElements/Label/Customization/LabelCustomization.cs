using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeaOverlay;

internal class LabelCustomization : SingletonAccessor
{
	[JsonIgnore]
	private bool visibility = true;
	public bool Visibility { get => visibility; set => visibility = value; }

	public LabelSettingsCustomization Settings { get; set; } = new();

	public LabelFontCustomization Font { get; set; } = new();

	public OffsetCustomization Offset { get; set; } = new();

	// Text Color

	// For Config, RGBA
	[JsonIgnore]
	private string textColorRgbaString = "0xFFFFFFFF";
	public string Color { get => textColorRgbaString; set => textColorRgbaString = value; }

	// For Draw, ABGR
	[JsonIgnore]
	private uint textColorDrawAbgr = 0xFFFFFFFF;
	[JsonIgnore]
	public uint TextColorDrawAbgr { get => textColorDrawAbgr; set => textColorDrawAbgr = value; }

	// For ImGui, RGBA
	[JsonIgnore]
	private Vector4 textColorImGuiRgba = new(1f, 1f, 1f, 1f);
	[JsonIgnore]
	public Vector4 TextColorImGuiRgba { get => textColorImGuiRgba; set => textColorImGuiRgba = value; }

	// For OpacityScale
	[JsonIgnore]
	private byte textColorRed = 255;
	[JsonIgnore]
	public byte TextColorRed { get => textColorRed; set => textColorRed = value; }

	[JsonIgnore]
	private byte textColorGreen = 255;
	[JsonIgnore]
	public byte TextColorGreen { get => textColorGreen; set => textColorGreen = value; }

	[JsonIgnore]
	private byte textColorBlue = 255;
	[JsonIgnore]
	public byte TextColorBlue { get => textColorBlue; set => textColorBlue = value; }

	[JsonIgnore]
	private byte textColorAlpha = 255;
	[JsonIgnore]
	public byte TextColorAlpha { get => textColorAlpha; set => textColorAlpha = value; }


	public LabelShadowCustomization Shadow { get; set; } = new();

	[JsonIgnore]
	public List<Label> Labels { get; set; } = new();

	public LabelCustomization()
	{
	}

	public LabelCustomization Init()
	{
		Settings.Init();
		Font.Init();

		_ = ColorUtils.UpdateColorsFromRgbaString(ref textColorRgbaString, ref textColorDrawAbgr, textColorImGuiRgba,
			ref textColorRed, ref textColorGreen, ref textColorBlue, ref textColorAlpha);
		
		return this;
	}

	public async Task OnTextColorChanged()
	{
		ColorUtils.UpdateColorsFromRgbaVector(textColorImGuiRgba, ref textColorRgbaString, ref textColorDrawAbgr,
			ref textColorRed, ref textColorGreen, ref textColorBlue, ref textColorAlpha);
	}

	public async Task OnChanged()
	{
		foreach (var label in Labels)
		{
			label.Internal
				.CalculateFromText1()
				.CalculateFromPosition2()
				.ScaleOpacity3();
		}
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		if (ImGui.TreeNode("Label"))
		{
			changed = ImGui.Checkbox(localizationManager.ImGui.Visible, ref visibility) || changed;

			changed = Settings.RenderImGui() || changed;
			changed = Font.RenderImGui() || changed;
			changed = Offset.RenderImGui() || changed;

			if (ImGui.TreeNode(localizationManager.ImGui.Color))
			{
				tempChanged = ImGui.ColorPicker4("", ref textColorImGuiRgba);
				if (tempChanged) _ = OnTextColorChanged();

				changed = changed || tempChanged;

				ImGui.TreePop();
			}

			changed = Shadow.RenderImGui() || changed;

			ImGui.TreePop();
		}

		if (changed) _ = OnChanged();

		return changed;
	}
}