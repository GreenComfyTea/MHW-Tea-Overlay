using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeaOverlay
{
	internal class Draw
	{
		// Draw List uses int color in ABGR format
		// ImGui uses Vector4 color in RGBA format

		// Singleton Pattern
		private static readonly Draw singleton = new();

		public static Draw Instance { get { return singleton; } }

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static Draw() { }

		private ImDrawListPtr backgroundDrawList;

		// Singleton Pattern End

		private Draw() {
		}

		public void OutlineRectangle(Vector2 position, Vector2 positionBottomRight, uint color, float thickness)
		{
			backgroundDrawList = ImGui.GetBackgroundDrawList();
			backgroundDrawList.AddRect(position, positionBottomRight, color, 0f, 0x0, thickness);
		}

		public void FilledRectangle(Vector2 position, Vector2 positionBottomRight, uint color)
		{
			backgroundDrawList = ImGui.GetBackgroundDrawList();
			backgroundDrawList.AddRectFilled(position, positionBottomRight, color, 0f);
		}

		public void Text(string text, float fontSize, Vector2 position, uint color)
		{
			backgroundDrawList = ImGui.GetBackgroundDrawList();
			backgroundDrawList.AddText(ImGui.GetFont(), fontSize, position, color, text);
		}
	}
}
