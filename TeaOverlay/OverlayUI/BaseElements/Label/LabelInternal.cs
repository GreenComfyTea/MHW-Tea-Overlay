using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeaOverlay;

internal class LabelInternal
{
	public Label LabelInstance { get; }

	public Vector2 Position { get; set; } = Vector2.Zero;
	public Vector2 ShadowPosition { get; set; } = Vector2.Zero;

	public string Format { get; set; } = "{0:0}";
	public string FormattedText { get; set; } = string.Empty;

	public uint TextColorDrawAbgr { get; set; } = 0;
	public uint ShadowColorDrawAbgr { get; set; } = 0;

	public LabelInternal(Label label)
	{
		LabelInstance = label;
	}

	public LabelInternal CalculateFromText1()
	{
		var text = LabelInstance.Text;

		var settings = LabelInstance.Customization.Settings;

		Format = $"{{0,{settings.RightAlignmentShift}}}";
		FormattedText = string.Format(Format, text);

		return this;
	}

	public LabelInternal CalculateFromPosition2()
	{
		var position = LabelInstance.Position;

		var customization = LabelInstance.Customization;
		var offset = customization.Offset;
		var shadowCustomization = customization.Shadow;
		var shadowOffset = shadowCustomization.Offset;

		Position = new(
			position.X + offset.X,
			position.Y + offset.Y
		);


		if (shadowCustomization.Visibility)
		{
			ShadowPosition = new(
				Position.X + shadowOffset.X,
				Position.Y + shadowOffset.Y
			);
		}

		return this;
	}

	public LabelInternal ScaleOpacity3()
	{
		var opacityScale = LabelInstance.OpacityScale;

		var customization = LabelInstance.Customization;
		var shadowCustomization = customization.Shadow;

		if (shadowCustomization.Visibility)
		{
			ShadowColorDrawAbgr = shadowCustomization.ColorDrawAbgr;

			if (opacityScale < 1f)
			{
				ShadowColorDrawAbgr = ColorUtils.ScaleColorOpacity(shadowCustomization.ColorRed, shadowCustomization.ColorGreen, shadowCustomization.ColorBlue,
					shadowCustomization.ColorAlpha, opacityScale);
			}

		}

		// Text

		TextColorDrawAbgr = customization.TextColorDrawAbgr;

		if (opacityScale < 1f)
		{
			TextColorDrawAbgr = ColorUtils.ScaleColorOpacity(customization.TextColorRed, customization.TextColorGreen, customization.TextColorBlue, 
				customization.TextColorAlpha, opacityScale);
		}

		return this;
	}
}
