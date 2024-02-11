﻿using SharpPluginLoader.Core;
using ImGuiNET;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

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

			var localizationManager = LocalizationManager.Instance;
			localizationManager.Init();
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
				CustomizationWindow.Instance.isOpened = !CustomizationWindow.Instance.isOpened;
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
