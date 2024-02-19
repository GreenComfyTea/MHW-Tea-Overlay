using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace TeaOverlay;

internal class Label : SingletonAccessor
{
	public LabelCustomization Customization { get; set; }

	public LabelInternal Internal { get; }

	private string text = string.Empty;

	public string Text
	{
		get => text;
		set { text = value; Internal.CalculateFromText1(); }
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

	public Label()
	{
		Customization = new();
		Customization.Labels.Add(this);
		Internal = new(this);

		Customization.Init();

		Internal
			.CalculateFromText1()
			.CalculateFromPosition2()
			.ScaleOpacity3();
	}

	public Label(LabelCustomization customization)
	{
		Customization = customization;
		customization.Labels.Add(this);
		Internal = new(this);
		Internal
			.CalculateFromText1()
			.CalculateFromPosition2()
			.ScaleOpacity3();
	}

	public Label Draw(Vector2 position, float opacityScale)
	{
		Position = position;
		OpacityScale = opacityScale;
		Draw();

		return this;
	}

	public Label Draw(Vector2 position)
	{
		Position = position;
		Draw();

		return this;
	}

	public Label Draw(float opacityScale)
	{
		OpacityScale = opacityScale;
		Draw();

		return this;
	}

	public Label Draw()
	{
		if (!Customization.Visibility) return this;
		if (Utils.IsApproximatelyEqual(OpacityScale, 0f)) return this;
		if (Text.Equals(string.Empty)) return this;


		var formattedText = Internal.FormattedText;


		if (Customization.Shadow.Visibility)
		{
			draw.Text(formattedText, Customization.Font.Size, Internal.ShadowPosition, Internal.ShadowColorDrawAbgr);
		}

		draw.Text(formattedText, Customization.Font.Size, Internal.Position, Internal.TextColorDrawAbgr);

		return this;
	}
}
