using SharpPluginLoader.Core.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeaOverlay;

public interface LocalizedStrings { }

public class LocalizedStrings_LocalizationInfo : LocalizedStrings
{
	public string Translator { get; set; } = "GreenComfyTea";
}

public class LocalizedStrings_UI : LocalizedStrings
{
	public string UI { get; set; } = "UI";
}

public class LocalizedStrings_ImGui : LocalizedStrings
{

	// Config

	public string Config { get; set; } = "Config";

	public string ConfigName { get; set; } = "Config Name";

	public string New { get; set; } = "New";
	public string Duplicate { get; set; } = "Duplicate";
	public string Reset { get; set; } = "Reset";
	public string Delete { get; set; } = "Delete";

	// Language

	// Bar
	public string Visible { get; set; } = "Visible";
	public string Settings { get; set; } = "Settings";

	public string FillDirection { get; set; } = "Fill Direction";
	public string LeftToRight { get; set; } = "Left to Right";
	public string RightToLeft { get; set; } = "Right to Left";
	public string TopToBottom { get; set; } = "Top to Bottom";
	public string BottomToTop { get; set; } = "Bottom to Top";

	public string Offset { get; set; } = "Offset";
	public string X { get; set; } = "X";
	public string Y { get; set; } = "Y";

	public string Size { get; set; } = "Size";
	public string Width { get; set; } = "Width";
	public string Height { get; set; } = "Height";

	public string Outline { get; set; } = "Outline";
	public string Thickness { get; set; } = "Thickness";

	public string Mode { get; set; } = "Mode";
	public string Outside { get; set; } = "Outside";
	public string Center { get; set; } = "Center";
	public string Inside { get; set; } = "Inside";

	public string Colors { get; set; } = "Colors";
	public string Fill { get; set; } = "Fill";
	public string Background { get; set; } = "Background";


	[JsonIgnore]
	public string[] FillDirections { get; set; } = Array.Empty<string>();
	[JsonIgnore]
	public string[] OutlineModes { get; set; } = Array.Empty<string>();

	public LocalizedStrings_ImGui()
	{
		FillDirections = [LeftToRight, RightToLeft, TopToBottom, BottomToTop];
		OutlineModes = [Outside, Center, Inside];
	}
}