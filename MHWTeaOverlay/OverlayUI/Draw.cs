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
		// Singleton Pattern
		private static readonly Draw singleton = new();

		public static Draw Instance { get { return singleton; } }

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static Draw() { }

		private ImDrawListPtr foregroundDrawList;

		// Singleton Pattern End

		private Draw() {
			foregroundDrawList = ImGui.GetForegroundDrawList();
		}

		public void DrawRectangle()
		{
			foregroundDrawList.AddRect(new Vector2(100, 100), new Vector2(150, 150), 0x1a334d66);
		}
	}
}
