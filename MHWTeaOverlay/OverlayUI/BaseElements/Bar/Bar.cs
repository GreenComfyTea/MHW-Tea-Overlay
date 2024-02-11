using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public class Bar
{
	// General

	public bool Visibility { get; set; } = true;

	public FillDirections FillDirection { get; set; } = FillDirections.LeftToRight;

	public Vector2 Offset { get; set; } = Vector2.Zero;

	public SizeF Size { get; set; } = SizeF.Empty;

	// Outline

	public bool OutlineVisibility { get; set; } = true;

	public OutlineModes OutlineMode { get; set; } = OutlineModes.Outside;

	public float OutlineOffset { get; set; } = 0f;

	public float OutlineThickness { get; set; } = 1.0f;

	// Colors

	public Color FillColor { get; set; } = Color.FromArgb(200, 255, 255, 255);

	public Color BackgroundColor { get; set; } = Color.FromArgb(200, 127, 127, 127);

	public Color OutlineColor { get; set; } = Color.FromArgb(200, 0, 0, 0);


	public Bar()
	{

	}

	public void Draw(Vector2 position, float opacityScale = 1f, float fillPercentage = 0.69f)
	{
		opacityScale = Utils.Clamp(opacityScale, 0f, 1f);
		fillPercentage = Utils.Clamp(fillPercentage, 0f, 1f);

		if (!Visibility) return;

		float outlineHalfThickness = 0.5f * OutlineThickness;

		float outlinePositionX = 0f;
		float outlinePositionY = 0f;

		float outlineWidth = 0f;
		float outlineHeight = 0f;

		float positionX = 0f;
		float positionY = 0f;

		float width = 0f;
		float height = 0f;

		float fillWidth = 0f;
		float fillHeight = 0f;

		float backgroundWidth = 0f;
		float backgroundHeight = 0f;

		float backgroundHorizontalShift = 0f;

		switch (OutlineMode)
		{
			case OutlineModes.Inside:

				outlinePositionX = position.X + Offset.X + outlineHalfThickness;
				outlinePositionY = position.Y + Offset.Y + outlineHalfThickness;

				outlineWidth = Size.Width - OutlineThickness;
				outlineHeight = Size.Height - OutlineThickness;

				positionX = outlinePositionX + outlineHalfThickness + OutlineOffset;
				positionY = outlinePositionY + outlineHalfThickness + OutlineOffset;

				width = outlineWidth - 2 * OutlineOffset - OutlineThickness;
				height = outlineWidth - 2 * OutlineOffset - OutlineThickness;

				break;
			case OutlineModes.Center:

				outlinePositionX = position.X + Offset.X - outlineHalfThickness;
				outlinePositionY = position.Y + Offset.Y - outlineHalfThickness;

				outlineWidth = Size.Width + OutlineThickness;
				outlineHeight = Size.Height + OutlineThickness;

				positionX = outlinePositionX + outlineHalfThickness + OutlineOffset;
				positionY = outlinePositionY + outlineHalfThickness + OutlineOffset;

				width = outlineWidth - 2 * OutlineOffset - OutlineThickness;
				height = outlineWidth - 2 * OutlineOffset - OutlineThickness;

				break;
			case OutlineModes.Outside:
			default:

				positionX = position.X + Offset.X;
				positionY = positionY + Offset.Y;

				width = Size.Width;
				height = Size.Height;

				outlinePositionX = position.X - OutlineOffset - outlineHalfThickness;
				outlinePositionY = position.Y - OutlineOffset - outlineHalfThickness;

				outlineWidth = Size.Width + 2 * OutlineOffset + OutlineThickness;
				outlineHeight = Size.Height + 2 * OutlineOffset + OutlineThickness;

				break;
		}

		switch (FillDirection)
		{
			case FillDirections.TopToBottom:

				fillWidth = width;
				fillHeight = height * fillPercentage;

				backgroundWidth = width;
				backgroundHeight = height - fillHeight;

				backgroundHorizontalShift = fillWidth;

				break;
			case FillDirections.BottomToTop:

				fillWidth = width;
				fillHeight = height * fillPercentage;

				backgroundWidth = width;
				backgroundHeight = height - fillHeight;

				backgroundHorizontalShift = backgroundWidth;

				break;
			case FillDirections.RightToLeft:

				fillWidth = width * fillPercentage;
				fillHeight = height;

				backgroundWidth = width - fillWidth;
				backgroundHeight = height;

				backgroundHorizontalShift = backgroundWidth;

				break;
			case FillDirections.LeftToRight:
			default:

				fillWidth = width * fillPercentage;
				fillHeight = height;

				backgroundWidth = width - fillWidth;
				backgroundHeight = height;

				backgroundHorizontalShift = fillWidth;

				break;
		}

		int fillColorInt = FillColor.ToArgb();


		//local foreground_color = bar.colors.foreground;
		//local background_color = bar.colors.background;
		//local outline_color = bar.colors.outline;

		//if opacity_scale < 1 then
		//	foreground_color = this.scale_color_opacity(foreground_color, opacity_scale);
		//background_color = this.scale_color_opacity(background_color, opacity_scale);
		//outline_color = this.scale_color_opacity(outline_color, opacity_scale);
		//end
	}
}
