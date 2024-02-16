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
	public async Task Init()
	{
		try
		{
			TeaLog.Info("Plugin Loaded!");

			var localizationManager = LocalizationManager.Instance;
			var configManager = ConfigManager.Instance;
			var draw = Draw.Instance;

			localizationManager.Init();
			configManager.Init();
		}
		catch (Exception exception)
		{
			TeaLog.Info(exception.ToString());
		}
	}

	public void OnLoad()
	{
		_ = Init();
	}

	public void OnImGuiRender()
	{
		try
		{
			ImGui.SetWindowFontScale(1.5f);

			if (ImGui.Button($"{Constants.MOD_NAME}"))
			{
				CustomizationWindow.Instance.IsOpened = !CustomizationWindow.Instance.IsOpened;
			}
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());
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
			TeaLog.Error(exception.ToString());
		}
	}
}
