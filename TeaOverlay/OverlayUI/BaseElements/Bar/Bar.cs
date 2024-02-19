using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace TeaOverlay;

internal class Bar : SingletonAccessor
{
	public BarCustomization Customization { get; set; }

	public BarInternal Internal { get; }

	private float percentage = 0.69f;
	public float Percentage
	{
		get => percentage;
		set { percentage = Utils.Clamp(value, 0f, 1f); Internal.CalculateFromPercentage1(); }
	}

	private Vector2 position = Vector2.Zero;
	public Vector2 Position
	{
		get => position;
		set { position = value; Internal.CalculateFromPosition2(); }
	}

	private float opacityScale = 1f;
	public float OpacityScale
	{
		get => opacityScale;
		set { opacityScale = Utils.Clamp(value, 0f, 1f); Internal.ScaleOpacity3(); }
	}

	public Bar() {
		Customization = new();
		Customization.Bars.Add(this);
		Internal = new(this);

		Customization.Init();
		Internal
			.CalculateFromPercentage1()
			.CalculateFromPosition2()
			.ScaleOpacity3();
	}

	public Bar(BarCustomization customization)
	{
		Customization = customization;
		customization.Bars.Add(this);
		Internal = new(this);
		Internal
			.CalculateFromPercentage1()
			.CalculateFromPosition2()
			.ScaleOpacity3();
	}

	public Bar Draw(Vector2 position, float opacityScale)
	{
		Position = position;
		OpacityScale = opacityScale;
		Draw();

		return this;
	}

	public Bar Draw(Vector2 position)
	{
		Position = position;
		Draw();

		return this;
	}

	public Bar Draw(float opacityScale)
	{
		OpacityScale = opacityScale;
		Draw();

		return this;
	}

	public Bar Draw()
	{
		if (!Customization.Visibility) return this;
		if (Utils.IsApproximatelyEqual(OpacityScale, 0f)) return this;

		var outline = Customization.Outline;

		// Background
		draw.FilledRectangle(
			Internal.BackgroundPosition,
			Internal.BackgroundPositionBottomRight,
			Internal.BackgroundColorDrawAbgr
		);

		// Fill
		draw.FilledRectangle(
			Internal.FillPosition,
			Internal.FillPositionBottomRight,
			Internal.FillColorDrawAbgr
		);

		// Outline
		if (!outline.Visibility || Utils.IsApproximatelyEqual(outline.Thickness, 0f))
		{
			return this;
		}

		draw.OutlineRectangle(
			Internal.OutlinePosition,
			Internal.OutlinePositionBottomRight,
			Internal.OutlineColorDrawAbgr,
			outline.Thickness
		);

		return this;
	}
}
