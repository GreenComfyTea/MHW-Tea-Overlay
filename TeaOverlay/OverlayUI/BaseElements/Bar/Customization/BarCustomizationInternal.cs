using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TeaOverlay;

public class BarCustomizationInternal
{
    private BarCustomization Customization { get; }

    public float ActualOutlinePositionOffsetX { get; set; } = 0f;
    public float ActualOutlinePositionOffsetY { get; set; } = 0f;

    public float ActualOutlineWidth { get; set; } = 0f;
    public float ActualOutlineHeight { get; set; } = 0f;

    public float ActualPositionOffsetX { get; set; } = 0f;
    public float ActualPositionOffsetY { get; set; } = 0f;

    public float ActualWidth { get; set; } = 0f;
    public float ActualHeight { get; set; } = 0f;

    public float OutlineWidth { get; set; } = 0f;
    public float OutlineHeight { get; set; } = 0f;

    public float FillWidth { get; set; } = 0f;
    public float FillHeight { get; set; } = 0f;

    public float BackgroundWidth { get; set; } = 0f;
    public float BackgroundHeight { get; set; } = 0f;

    public BarCustomizationInternal(BarCustomization customization)
    {
        Customization = customization;
    }

    public void Precalculate()
    {

        var offset = Customization.Offset;
        var size = Customization.Size;
        var outline = Customization.Outline;

        var outlineHalfThickness = outline.Thickness / 2f;

        var outlineOffset = outline.Offset;
        var outlineHalfOffset = outlineOffset / 2f;
        var outlineDoubleOffset = outlineOffset + outlineOffset;

        if (Utils.IsApproximatelyEqual(outline.Thickness, 0f))
        {
            outlineOffset = 0f;
        }

        switch (outline.ModeEnum)
        {
            case OutlineModes.Inside:
                ActualOutlinePositionOffsetX = offset.X + outlineHalfThickness;
                ActualOutlinePositionOffsetY = offset.Y + outlineHalfThickness;

                ActualOutlineWidth = size.Width - outline.Thickness;
                ActualOutlineHeight = size.Height - outline.Thickness;

                ActualPositionOffsetX = outlineHalfThickness + outlineOffset;
                ActualPositionOffsetY = outlineHalfThickness + outlineOffset;

                ActualWidth = ActualOutlineWidth - outline.Thickness - outlineDoubleOffset;
                ActualHeight = ActualOutlineHeight - outline.Thickness - outlineDoubleOffset;

                OutlineWidth = ActualOutlineWidth;
                OutlineHeight = ActualOutlineHeight;

                break;

            case OutlineModes.Center:

                ActualOutlinePositionOffsetX = offset.X - outlineHalfOffset;
                ActualOutlinePositionOffsetY = offset.Y - outlineHalfOffset;

                ActualOutlineWidth = size.Width + outlineOffset;
                ActualOutlineHeight = size.Height + outlineOffset;

                ActualPositionOffsetX = outlineHalfThickness + outlineOffset;
                ActualPositionOffsetY = outlineHalfThickness + outlineOffset;

                ActualWidth = ActualOutlineWidth - outline.Thickness - outlineDoubleOffset;
                ActualHeight = ActualOutlineHeight - outline.Thickness - outlineDoubleOffset;

                OutlineWidth = ActualOutlineWidth;
                OutlineHeight = ActualOutlineHeight;


                break;

            case OutlineModes.Outside:
            default:

                ActualOutlinePositionOffsetX = outlineHalfThickness + outlineOffset;
                ActualOutlinePositionOffsetY = outlineHalfThickness + outlineOffset;

                ActualOutlineWidth = outline.Thickness + outlineDoubleOffset;
                ActualOutlineHeight = outline.Thickness + outlineDoubleOffset;

                ActualPositionOffsetX = offset.X;
                ActualPositionOffsetY = offset.Y;

                ActualWidth = size.Width;
                ActualHeight = size.Height;

                OutlineWidth = ActualWidth + ActualOutlineWidth;
                OutlineHeight = ActualHeight + ActualOutlineHeight;

                break;
        }

        FillWidth = 0f;
        FillHeight = 0f;

        BackgroundWidth = 0f;
        BackgroundHeight = 0f;

        switch (Customization.Settings.FillDirectionEnum)
        {
            case FillDirections.RightToLeft:
                FillHeight = ActualHeight;
                BackgroundHeight = ActualHeight;

                break;

            case FillDirections.TopToBottom:

                FillWidth = ActualWidth;
                BackgroundWidth = ActualWidth;

                break;

            case FillDirections.BottomToTop:

                FillWidth = ActualWidth;
                BackgroundWidth = ActualWidth;
                break;

            case FillDirections.LeftToRight:
            default:

                FillHeight = ActualHeight;
                BackgroundHeight = ActualHeight;

                break;
        }
    }

}
