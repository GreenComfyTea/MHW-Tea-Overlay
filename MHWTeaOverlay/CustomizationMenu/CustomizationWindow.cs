using ImGuiNET;
using SharpPluginLoader.Core;
using SharpPluginLoader.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MHWTeaOverlay
{

	public class CustomizationWindow : SingletonAccessor
	{
		// Singleton Pattern
		private static readonly CustomizationWindow singleton = new();

		public static CustomizationWindow Instance { get { return singleton; } }

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static CustomizationWindow() { }

		// Singleton Pattern End

		private bool isOpened = false;
		public bool IsOpened { get => isOpened; set => isOpened = value; }

		private Bar bar = new();

		private CustomizationWindow() { }

		public void Render()
		{
			try
			{
				//var color = new Vector4(0.1f, 0.2f, 0.3f, 0.4f);

				if (!IsOpened) return;

				var font = ImGui.GetFont();
				var oldScale = font.Scale;
				font.Scale *= 1.5f;

				//ImGui.GetMainViewport().WorkSize = new Vector2(2880, 1620);
				//ImGui.GetMainViewport().Size = new Vector2(2880, 1620);

				ImGui.PushFont(font);
				ImGui.Begin("MH:World Tea Overlay", ref isOpened);

				bar.Position = new Vector2(1200f, 100f);
				bar.Draw();

				//draw.FilledRectangle(0f, 0f, 5000f, 5000f, 0x800000FF);

				bar.Customization.RenderImGui();

				//ImGui.Text($"WorkSize: {ImGui.GetMainViewport().WorkSize}");
				//ImGui.Text($"DpiScale: {ImGui.GetMainViewport().DpiScale}");
				//ImGui.Text($"Size: {ImGui.GetMainViewport().Size}");

				font.Scale = oldScale;
				ImGui.PopFont();

				ImGui.End();
			}
			catch (Exception e)
			{
				TeaLog.Info(e.ToString());
			}
		}
	}
}
