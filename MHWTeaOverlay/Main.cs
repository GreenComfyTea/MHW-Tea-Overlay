using SharpPluginLoader.Core;
using ImGuiNET;
using System.Diagnostics;

namespace MHWTeaOverlay;

public class MHWTeaOverlayPlugin : IPlugin
{
	public static string Version => "v1.0";
	public string Name => "MH:World Tea Overlay " + Version;

	public PluginData Initialize()
	{
		return new PluginData
		{
			OnImGuiRender = true,
			OnImGuiFreeRender = true,
			ImGuiWrappedInTreeNode = false
		};
	}

	public void OnLoad()
	{
		try
		{
			TeaLog.Info("Plugin Loaded!");

			LocalizationManager.Instance.Init();
			ConfigManager.Instance.Init();
		}
		catch(Exception exception)
		{
			TeaLog.Info(exception.ToString());
		}
	}

	public void OnImGuiRender()
	{
		try
		{
			ImGui.SetWindowFontScale(1.5f);

			if (ImGui.Button("TeaTime Overlay"))
			{
				CustomizationWindow.Instance.IsOpened = !CustomizationWindow.Instance.IsOpened;
			}
		}
		catch (Exception exception)
		{
			TeaLog.Info(exception.ToString());
		}
	}

	public void OnImGuiFreeRender()
	{
		try
		{
			CustomizationWindow.Instance.Render();
		}
		catch (Exception exception)
		{
			TeaLog.Info(exception.ToString());
		}
	}
}
