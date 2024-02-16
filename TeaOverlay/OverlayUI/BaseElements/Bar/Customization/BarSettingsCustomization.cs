using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeaOverlay;

public class BarSettingsCustomization : SingletonAccessor
{

    public string FillDirection { get; set; }

    [JsonIgnore]
    public FillDirections FillDirectionEnum { get; set; } = FillDirections.LeftToRight;

    public BarSettingsCustomization()
    {
        FillDirection = localizationManager.Default.ImGui.LeftToRight;
    }

    public BarSettingsCustomization Init()
    {
        FillDirectionEnum = (FillDirections)Array.FindIndex(
            LocalizationManager.Instance.Default.ImGui.FillDirections, arrayString => arrayString.Equals(FillDirection)
        );

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;
        var tempChanged = false;
        var selectedIndex = 0;

        if (ImGui.TreeNode(localizationManager.ImGui.Settings))
        {
            selectedIndex = (int)FillDirectionEnum;
            tempChanged = ImGui.Combo(localizationManager.ImGui.FillDirection, ref selectedIndex, localizationManager.ImGui.FillDirections, 4);
            if (tempChanged)
            {
                FillDirectionEnum = (FillDirections)selectedIndex;
                FillDirection = localizationManager.Default.ImGui.FillDirections[selectedIndex];
            }
            changed = changed || tempChanged;

            ImGui.TreePop();

        }

        return changed;
    }
}
