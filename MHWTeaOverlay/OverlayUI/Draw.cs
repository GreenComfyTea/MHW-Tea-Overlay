using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MHWTeaOverlay
{
	public class Draw
	{
		// Draw List uses int color in ABGR format
		// ImGui uses Vector4 color in RGBA format

		// Singleton Pattern
		private static readonly Draw singleton = new();

		public static Draw Instance { get { return singleton; } }

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static Draw() { }

		private ImDrawListPtr foregroundDrawList;

		// Singleton Pattern End

		private Draw() {
		}

		public void OutlineRectangle(Vector2 position, SizeF size, uint color, float thickness)
		{
			foregroundDrawList = ImGui.GetForegroundDrawList();

			foregroundDrawList.AddRect(position, position + size.ToVector2(), color, 0f, 0x0, thickness);
		}

		public void OutlineRectangle(float x, float y, float width, float height, uint color, float thickness)
		{
			foregroundDrawList = ImGui.GetForegroundDrawList();

			var position = new Vector2(x, y);
			var size = new Vector2(width, height);

			foregroundDrawList.AddRect(position, position + size, color, 0f, 0x0, thickness);
		}

		public void FilledRectangle(Vector2 position, SizeF size, uint color)
		{
			foregroundDrawList = ImGui.GetForegroundDrawList();

			foregroundDrawList.AddRectFilled(position, position + size.ToVector2(), color, 0f);
		}

		public void FilledRectangle(float x, float y, float width, float height, uint color)
		{
			foregroundDrawList = ImGui.GetForegroundDrawList();

			var position = new Vector2(x, y);
			var size = new Vector2(width, height);

			foregroundDrawList.AddRectFilled(position, position + size, color, 0f);
		}
	}
}
