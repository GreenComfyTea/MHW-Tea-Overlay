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

	public class CustomizationWindow
	{
		// Singleton Pattern
		private static readonly CustomizationWindow singleton = new();

		public static CustomizationWindow Instance { get { return singleton; } }

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static CustomizationWindow() { }

		// Singleton Pattern End

		public bool isOpened = false;

		private CustomizationWindow() { }

		public void Render()
		{
			try
			{
				//var color = new Vector4(0.1f, 0.2f, 0.3f, 0.4f);

				if (!isOpened) return;

				ImGui.SetWindowFontScale(1.5f);
				ImGui.Begin("MH:World Tea Overlay", ref isOpened);
				ImGui.SetWindowFontScale(1.5f);

				ImGui.End();
			}
			catch (Exception e)
			{
				TeaLog.Info(e.ToString());
			}
		}
	}
}
