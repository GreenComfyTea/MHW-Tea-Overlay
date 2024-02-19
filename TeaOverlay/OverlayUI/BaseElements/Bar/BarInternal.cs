using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeaOverlay;

internal class BarInternal
{
	private Bar BarInstance {  get; }

	public float FillWidth { get; set; } = 0f;
	public float FillHeight { get; set; } = 0f;

	public float BackgroundWidth { get; set; } = 0f;
	public float BackgroundHeight { get; set; } = 0f;


	public float FillShiftX { get; set; } = 0f;
	public float FillShiftY { get; set; } = 0f;

	public float BackgroundShiftX { get; set; } = 0f;
	public float BackgroundShiftY { get; set; } = 0f;

	public Vector2 FillPosition { get; set; } = Vector2.Zero;
	public Vector2 FillPositionBottomRight { get; set; } = Vector2.Zero;

	public Vector2 BackgroundPosition { get; set; } = Vector2.Zero;
	public Vector2 BackgroundPositionBottomRight { get; set; } = Vector2.Zero;

	public Vector2 OutlinePosition { get; set; } = Vector2.Zero;
	public Vector2 OutlinePositionBottomRight { get; set; } = Vector2.Zero;

	public uint FillColorDrawAbgr { get; set; } = 0;
	public uint BackgroundColorDrawAbgr { get; set; } = 0;
	public uint OutlineColorDrawAbgr { get; set; } = 0;

	public BarInternal(Bar bar)
	{
		BarInstance = bar;
	}

	public BarInternal CalculateFromPercentage1()
	{
		var percentage = BarInstance.Percentage;
		var customization = BarInstance.Customization;
		var customizationInternal = customization.Internal;

		FillWidth = customizationInternal.FillWidth;
		FillHeight = customizationInternal.FillHeight;

		BackgroundWidth = customizationInternal.BackgroundWidth;
		BackgroundHeight = customizationInternal.BackgroundHeight;

		FillShiftX = 0f;
		FillShiftY = 0f;
		BackgroundShiftX = 0f;
		BackgroundShiftY = 0f;

		switch (customization.Settings.FillDirectionEnum)
		{
			case FillDirections.RightToLeft:

				FillWidth = customizationInternal.ActualWidth * percentage;
				BackgroundWidth = customizationInternal.ActualWidth - FillWidth;
				
				FillShiftX = BackgroundWidth;

				break;

			case FillDirections.TopToBottom:

				FillHeight = customizationInternal.ActualHeight * percentage;
				BackgroundHeight = customizationInternal.ActualHeight - FillHeight;
				
				BackgroundShiftY = FillHeight;

				break;

			case FillDirections.BottomToTop:

				FillHeight = customizationInternal.ActualHeight * percentage;
				BackgroundHeight = customizationInternal.ActualHeight - FillHeight;
				
				FillShiftY = BackgroundHeight;

				break;

			case FillDirections.LeftToRight:
			default:

				FillWidth = customizationInternal.ActualWidth * percentage;
				BackgroundWidth = customizationInternal.ActualWidth - FillWidth;
				
				BackgroundShiftX = FillWidth;

				break;
		}

		return this;
	}

	public BarInternal CalculateFromPosition2()
	{
		var position = BarInstance.Position;

		var customization = BarInstance.Customization;
		var customizationInternal = customization.Internal;

		var positionX = 0f;
		var positionY = 0f;

		switch (customization.Outline.ModeEnum)
		{
			case OutlineModes.Inside:

				OutlinePosition = new(
					position.X + customizationInternal.ActualOutlinePositionOffsetX,
					position.Y + customizationInternal.ActualOutlinePositionOffsetY
				);

				positionX = OutlinePosition.X + customizationInternal.ActualPositionOffsetX;
				positionY = OutlinePosition.Y + customizationInternal.ActualPositionOffsetY;

				break;

			case OutlineModes.Center:

				OutlinePosition = new(
					position.X + customizationInternal.ActualOutlinePositionOffsetX,
					position.Y + customizationInternal.ActualOutlinePositionOffsetY
				);


				positionX = OutlinePosition.X + customizationInternal.ActualPositionOffsetX;
				positionY = OutlinePosition.Y + customizationInternal.ActualPositionOffsetY;

				break;

			case OutlineModes.Outside:
			default:

				positionX = position.X + customizationInternal.ActualPositionOffsetX;
				positionY = position.Y + customizationInternal.ActualPositionOffsetY;

				OutlinePosition = new(
					positionX - customizationInternal.ActualOutlinePositionOffsetX,
					positionY - customizationInternal.ActualOutlinePositionOffsetY
				);

				break;
		}

		FillPosition = new(
			positionX + FillShiftX,
			positionY + FillShiftY
		);

		BackgroundPosition = new(
			positionX + BackgroundShiftX,
			positionY + BackgroundShiftY
		);

		FillPositionBottomRight = new(
			FillPosition.X + FillWidth,
			FillPosition.Y + FillHeight
		);

		BackgroundPositionBottomRight = new(
			BackgroundPosition.X + BackgroundWidth,
			BackgroundPosition.Y + BackgroundHeight
		);

		OutlinePositionBottomRight = new(
			OutlinePosition.X + customizationInternal.OutlineWidth,
			OutlinePosition.Y + customizationInternal.OutlineHeight
		);

		return this;
	}

	public BarInternal ScaleOpacity3()
	{
		var opacityScale = BarInstance.OpacityScale;

		var colors = BarInstance.Customization.Colors;

		FillColorDrawAbgr = colors.FillColorDrawAbgr;
		BackgroundColorDrawAbgr = colors.BackgroundColorDrawAbgr;
		OutlineColorDrawAbgr = colors.OutlineColorDrawAbgr;

		if (opacityScale < 1)
		{
			FillColorDrawAbgr = ColorUtils.ScaleColorOpacity(
				colors.FillColorRed, colors.FillColorGreen, colors.FillColorBlue, colors.FillColorAlpha, opacityScale);

			BackgroundColorDrawAbgr = ColorUtils.ScaleColorOpacity(
				colors.BackgroundColorRed, colors.BackgroundColorGreen, colors.BackgroundColorBlue, colors.BackgroundColorAlpha, opacityScale);

			OutlineColorDrawAbgr = ColorUtils.ScaleColorOpacity(
				colors.OutlineColorRed, colors.OutlineColorGreen, colors.OutlineColorBlue, colors.OutlineColorAlpha, opacityScale);
		}

		return this;
	}
}
