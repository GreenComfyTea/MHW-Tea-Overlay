using ImGuiNET;
using MHWTeaOverlay.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MHWTeaOverlay;

public class BarOutlineCustomization : SingletonAccessor
{
    [JsonIgnore]
    private bool visibility = true;
    public bool Visibility { get => visibility; set => visibility = value; }

    [JsonIgnore]
    private float thickness = 2f;
    public float Thickness { get => thickness; set => thickness = value; }

    [JsonIgnore]
    private float offset = 0f;
    public float Offset { get => offset; set => offset = value; }

    public string Mode { get; set; }

    [JsonIgnore]
    private OutlineModes modeEnum = OutlineModes.Outside;
    [JsonIgnore]
    public OutlineModes ModeEnum { get => modeEnum; set => modeEnum = value; }

    public BarOutlineCustomization()
    {
        Mode = localizationManager.Default.ImGui.Outside;
    }

    public BarOutlineCustomization Init()
    {
        //ModeEnum = (OutlineModes)Array.FindIndex(
        //LocalizationManager.Instance.Default.ImGui.OutlineModes, arrayString => arrayString.Equals(Mode)
        //);

        var success = Enum.TryParse(Mode, out modeEnum);

        return this;
    }

    public bool RenderImGui()
    {
        var changed = false;
        var tempChanged = false;
        var selectedIndex = 0;

        if (ImGui.TreeNode(localizationManager.ImGui.Outline))
        {
            changed = ImGui.Checkbox(localizationManager.ImGui.Visible, ref visibility) || changed;

            changed = ImGui.DragFloat(localizationManager.ImGui.Thickness, ref thickness,
                Constants.DRAG_FLOAT_SPEED, 0f, Constants.DRAG_FLOAT_MAX, Thickness.ToString(Constants.DRAG_FLOAT_FORMAT)) || changed;

            changed = ImGui.DragFloat(localizationManager.ImGui.Offset, ref offset,
                Constants.DRAG_FLOAT_SPEED, Constants.DRAG_FLOAT_MIN, Constants.DRAG_FLOAT_MAX, Offset.ToString(Constants.DRAG_FLOAT_FORMAT)) || changed;

            selectedIndex = (int)ModeEnum;
            tempChanged = ImGui.Combo(localizationManager.ImGui.Mode, ref selectedIndex, localizationManager.ImGui.OutlineModes, 3);
            if (tempChanged)
            {
                ModeEnum = (OutlineModes)selectedIndex;
                Mode = localizationManager.Default.ImGui.OutlineModes[selectedIndex];
            }
            changed = changed || tempChanged;

            ImGui.TreePop();
        }

        return changed;
    }
}
