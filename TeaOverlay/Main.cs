﻿using SharpPluginLoader.Core;
using ImGuiNET;
using System.Diagnostics;
using SharpPluginLoader.Core.Memory;

namespace TeaOverlay;

internal class TeaOverlayPlugin : IPlugin
{
	private delegate void StartRequestDelegate(nint mtNetRequestPointer);
	private Hook<StartRequestDelegate> _startRequestDelegate;

	public string Name => $"{Constants.MOD_NAME} v{Constants.VERSION}";

	public string Author => Constants.MOD_AUTHOR;

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

	public static ImFontPtr font;

	public void OnLoad()
	{
		_ = Init();

		//_startRequestDelegate = Hook.Create<StartRequestDelegate>(0x1421e2430, (mtNetRequestPointer) =>
		//{
		//var mtNetRequestMtObject = new MtObject(mtNetRequestPointer);
		//Log.Info($"{mtNetRequestMtObject.ToString()}: startRequest");

		//_startRequestDelegate.Original(mtNetRequestPointer);
		//});


		

		//ImGui.PushFont(font);
	}

	public void OnImGuiRender()
	{
		var font = ImGui.GetFont();
		var oldScale = font.Scale;
		font.Scale *= 1.5f;

		try
		{
			ImGui.PushFont(font);

			if (ImGui.Button($"{Constants.MOD_NAME} v{Constants.VERSION}"))
			{
				CustomizationWindow.Instance.IsOpened = !CustomizationWindow.Instance.IsOpened;
			}

			font.Scale = oldScale;
			ImGui.PopFont();
		}
		catch (Exception exception)
		{
			TeaLog.Error(exception.ToString());

			font.Scale = oldScale;
			ImGui.PopFont();
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
