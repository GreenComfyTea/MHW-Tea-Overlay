using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TeaOverlay;

internal class BarColorsCustomization : SingletonAccessor
{
	// Colors

	// Fill Color

	// For Config, RGBA
	[JsonIgnore]
	private string fillColorRgbaString = "0xFFFFFFFF";
	public string FillColor { get => fillColorRgbaString; set => fillColorRgbaString = value; }

	// For Draw, ABGR
	[JsonIgnore]
	private uint fillColorDrawAbgr = 0xFFFFFFFF;
	[JsonIgnore]
	public uint FillColorDrawAbgr { get => fillColorDrawAbgr; set => fillColorDrawAbgr = value; }

	// For ImGui, RGBA
	[JsonIgnore]
	private Vector4 fillColorImGuiRgba = new(1f, 1f, 1f, 1f);
	[JsonIgnore]
	public Vector4 FillColorImGuiRgba { get => fillColorImGuiRgba; set => fillColorImGuiRgba = value; }

	// For OpacityScale
	[JsonIgnore]
	private byte fillColorRed = 255;
	[JsonIgnore]
	public byte FillColorRed { get => fillColorRed; set => fillColorRed = value; }

	[JsonIgnore]
	private byte fillColorGreen = 255;
	[JsonIgnore]
	public byte FillColorGreen { get => fillColorGreen; set => fillColorGreen = value; }

	[JsonIgnore]
	private byte fillColorBlue = 255;
	[JsonIgnore]
	public byte FillColorBlue { get => fillColorBlue; set => fillColorBlue = value; }

	[JsonIgnore]
	private byte fillColorAlpha = 255;
	[JsonIgnore]
	public byte FillColorAlpha { get => fillColorAlpha; set => fillColorAlpha = value; }

	// Background Color

	// For Config, RGBA
	[JsonIgnore]
	private string backgroundColorRgbaString = "0x808080FF";
	public string BackgroundColor { get => backgroundColorRgbaString; set => backgroundColorRgbaString = value; }

	// For Draw, ABGR
	[JsonIgnore]
	private uint backgroundColorDrawAbgr = 0xFF808080;
	[JsonIgnore]
	public uint BackgroundColorDrawAbgr { get => backgroundColorDrawAbgr; set => backgroundColorDrawAbgr = value; }

	// For ImGui, RGBA
	[JsonIgnore]
	private Vector4 backgroundColorImGuiRgba = new(0.5f, 0.5f, 0.5f, 1f);
	[JsonIgnore]
	public Vector4 BackgroundColorImGuiRgba { get => backgroundColorImGuiRgba; set => backgroundColorImGuiRgba = value; }

	// For OpacityScale
	[JsonIgnore]
	private byte backgroundColorRed = 128;
	[JsonIgnore]
	public byte BackgroundColorRed { get => backgroundColorRed; set => backgroundColorRed = value; }

	[JsonIgnore]
	private byte backgroundColorGreen = 128;
	[JsonIgnore]
	public byte BackgroundColorGreen { get => backgroundColorGreen; set => backgroundColorGreen = value; }

	[JsonIgnore]
	private byte backgroundColorBlue = 128;
	[JsonIgnore]
	public byte BackgroundColorBlue { get => backgroundColorBlue; set => backgroundColorBlue = value; }

	[JsonIgnore]
	private byte backgroundColorAlpha = 255;
	[JsonIgnore]
	public byte BackgroundColorAlpha { get => backgroundColorAlpha; set => backgroundColorAlpha = value; }

	// Outline Color

	// For Config, RGBA
	[JsonIgnore]
	private string outlineColorRgbaString = "0x000000FF";
	public string OutlineColor { get => outlineColorRgbaString; set => outlineColorRgbaString = value; }

	// For Draw, ABGR
	[JsonIgnore]
	private uint outlineColorDrawAbgr = 0xFF000000;
	[JsonIgnore]
	public uint OutlineColorDrawAbgr { get => outlineColorDrawAbgr; set => outlineColorDrawAbgr = value; }

	// For ImGui, RGBA
	[JsonIgnore]
	private Vector4 outlineColorImGuiRgba = new(0f, 0f, 0f, 1f);
	[JsonIgnore]
	public Vector4 OutlineColorImGuiRgba { get => outlineColorImGuiRgba; set => outlineColorImGuiRgba = value; }

	// For OpacityScale
	[JsonIgnore]
	private byte outlineColorRed = 0;
	[JsonIgnore]
	public byte OutlineColorRed { get => outlineColorRed; set => outlineColorRed = value; }

	[JsonIgnore]
	private byte outlineColorGreen = 0;
	[JsonIgnore]
	public byte OutlineColorGreen { get => outlineColorGreen; set => outlineColorGreen = value; }

	[JsonIgnore]
	private byte outlineColorBlue = 0;
	[JsonIgnore]
	public byte OutlineColorBlue { get => outlineColorBlue; set => outlineColorBlue = value; }

	[JsonIgnore]
	private byte outlineColorAlpha = 255;
	[JsonIgnore]
	public byte OutlineColorAlpha { get => outlineColorAlpha; set => outlineColorAlpha = value; }


	public BarColorsCustomization Init()
	{
		_ = ColorUtils.UpdateColorsFromRgbaString(ref fillColorRgbaString, ref fillColorDrawAbgr, fillColorImGuiRgba,
			ref fillColorRed, ref fillColorGreen, ref fillColorBlue, ref fillColorAlpha);

		_ = ColorUtils.UpdateColorsFromRgbaString(ref backgroundColorRgbaString, ref backgroundColorDrawAbgr, backgroundColorImGuiRgba,
			ref backgroundColorRed, ref backgroundColorGreen, ref backgroundColorBlue, ref backgroundColorAlpha);

		_ = ColorUtils.UpdateColorsFromRgbaString(ref outlineColorRgbaString, ref outlineColorDrawAbgr, outlineColorImGuiRgba,
			ref outlineColorRed, ref outlineColorGreen, ref outlineColorBlue, ref outlineColorAlpha);

		return this;
	}

	public async Task OnFillColorChanged()
	{
		ColorUtils.UpdateColorsFromRgbaVector(fillColorImGuiRgba, ref fillColorRgbaString, ref fillColorDrawAbgr,
			ref fillColorRed, ref fillColorGreen, ref fillColorBlue, ref fillColorAlpha);
	}

	public async Task OnBackgroundColorChanged()
	{
		ColorUtils.UpdateColorsFromRgbaVector(backgroundColorImGuiRgba, ref backgroundColorRgbaString, ref backgroundColorDrawAbgr,
			ref backgroundColorRed, ref backgroundColorGreen, ref backgroundColorBlue, ref backgroundColorAlpha);
	}

	public async Task OnOutlineColorChanged()
	{
		ColorUtils.UpdateColorsFromRgbaVector(outlineColorImGuiRgba, ref outlineColorRgbaString, ref outlineColorDrawAbgr,
			ref outlineColorRed, ref outlineColorGreen, ref outlineColorBlue, ref outlineColorAlpha);
	}

	public bool RenderImGui()
	{
		var changed = false;
		var tempChanged = false;

		if (ImGui.TreeNode(localizationManager.ImGui.Colors))
		{
			if (ImGui.TreeNode(localizationManager.ImGui.Fill))
			{
				tempChanged = ImGui.ColorPicker4("", ref fillColorImGuiRgba);
				if (tempChanged) _ = OnFillColorChanged();

				changed = changed || tempChanged;

				ImGui.TreePop();
			}

			if (ImGui.TreeNode(localizationManager.ImGui.Background))
			{
				tempChanged = ImGui.ColorPicker4("", ref backgroundColorImGuiRgba);
				if (tempChanged) _ = OnBackgroundColorChanged();

				changed = changed || tempChanged;

				ImGui.TreePop();
			}

			if (ImGui.TreeNode(localizationManager.ImGui.Outline))
			{
				tempChanged = ImGui.ColorPicker4("", ref outlineColorImGuiRgba);
				if (tempChanged) _ = OnOutlineColorChanged();

				changed = changed || tempChanged;

				ImGui.TreePop();
			}

			ImGui.TreePop();
		}

		return changed;
	}
}
