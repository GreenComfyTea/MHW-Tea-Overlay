﻿using ImGuiNET;
using SharpPluginLoader.Core;
using SharpPluginLoader.Core.Configuration;
using SharpPluginLoader.Core.Memory;
using SharpPluginLoader.Core.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeaOverlay
{

	internal class CustomizationWindow : SingletonAccessor
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
		private Label label = new();

		private CustomizationWindow() { }

		public void Render()
		{
			if (!IsOpened) return;

			var font = ImGui.GetFont();
			var oldScale = font.Scale;
			font.Scale *= 1.5f;

			try
			{
				var changed = false;

				//ImGui.GetMainViewport().WorkSize = new Vector2(2880f, 1620f);
				//ImGui.GetMainViewport().Size = new Vector2(2880f, 1620f);

				ImGui.PushFont(font);
				ImGui.Begin($"{Constants.MOD_NAME} v{Constants.VERSION}", ref isOpened);

				ImGui.ShowMetricsWindow();

				//var io = ImGui.GetIO();
				//var drawData = ImGui.GetDrawData();
				//var mainViewport = ImGui.GetMainViewport();

				//var fbHeight = io.DisplaySize.Y * io.DisplayFramebufferScale.Y;
				//var fbWidth = io.DisplaySize.X * io.DisplayFramebufferScale.X;



				ImGui.Text(localizationManager.ImGui.MadeBy);
				ImGui.SameLine();
				ImGui.TextColored(Constants.MOD_AUTHOR_COLOR, Constants.MOD_AUTHOR);

				if (ImGui.Button(localizationManager.ImGui.NexusMods)) Utils.OpenLink(Constants.NEXUSMODS_LINK);
				ImGui.SameLine();
				if (ImGui.Button(localizationManager.ImGui.GitHubRepo)) Utils.OpenLink(Constants.GITHUB_REPO_LINK);

				if (ImGui.Button(localizationManager.ImGui.Twitch)) Utils.OpenLink(Constants.TWITCH_LINK);
				ImGui.SameLine();
				if (ImGui.Button(localizationManager.ImGui.Twitter)) Utils.OpenLink(Constants.TWITTER_LINK);
				ImGui.SameLine();
				if (ImGui.Button(localizationManager.ImGui.ArtStation)) Utils.OpenLink(Constants.ARTSTATION_LINK);

				ImGui.Text(localizationManager.ImGui.DonationMessage1);
				ImGui.Text(localizationManager.ImGui.DonationMessage2);

				if (ImGui.Button(localizationManager.ImGui.Donate)) Utils.OpenLink(Constants.STREAMELEMENTS_TIP_LINK);
				ImGui.SameLine();
				if (ImGui.Button(localizationManager.ImGui.PayPal)) Utils.OpenLink(Constants.PAYPAL_LINK);
				ImGui.SameLine();
				if (ImGui.Button(localizationManager.ImGui.BuyMeATea)) Utils.OpenLink(Constants.KOFI_LINK);

				ImGui.Separator();
				ImGui.NewLine();
				ImGui.Separator();

				bar.Position = new Vector2(1200f, 100f);
				bar.Draw();

				label.Position = new Vector2(1000f, 50f);
				label.Text = "Test Label";
				label.Draw();

				configManager.Customization.RenderImGui();
				changed = localizationManager.Customization.RenderImGui() || changed;

				changed = bar.Customization.RenderImGui() || changed;
				changed = label.Customization.RenderImGui() || changed;

				//draw.FilledRectangle(new Vector2(0, 0), new SizeF(5000, 5000), 0xAA0000FF);

				//ImGui.Text($"WorkSize: {ImGui.GetMainViewport().WorkSize}");
				//ImGui.Text($"DpiScale: {ImGui.GetMainViewport().DpiScale}");
				//ImGui.Text($"Size: {ImGui.GetMainViewport().Size}");
				//ImGui.Text($"IO.DisplaySize: {ImGui.GetIO().DisplaySize}");
				//ImGui.Text($"IO.DisplayFramebufferScale: {ImGui.GetIO().DisplayFramebufferScale}");

				font.Scale = oldScale;
				ImGui.PopFont();

				ImGui.End();

				if (changed)
				{
					configManager.Current.Save();
				}
			}
			catch (Exception e)
			{
				TeaLog.Info(e.ToString());

				font.Scale = oldScale;
				ImGui.PopFont();
			}
		}
	}
}
