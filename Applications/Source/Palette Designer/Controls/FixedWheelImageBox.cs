#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Controls;

/// <summary>
/// ImageBox variant that forces one zoom level change per mouse-wheel detent regardless of the
/// user’s global wheel-scroll settings (SystemInformation.MouseWheelScrollDelta).
/// </summary>
internal sealed class FixedWheelImageBox : ImageBox
{
    private const int MinZoomPercent = 1;
    private const int MaxZoomPercent = 800;

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        // Ignore if no modifier keys change behaviour; Ctrl will be handled by parent container
        int ticks = e.Delta / SystemInformation.MouseWheelScrollDelta;
        if (ticks == 0)
        {
            ticks = Math.Sign(e.Delta);
        }

        int step = 10;
        if ((ModifierKeys & Keys.Control) == Keys.Control)
        {
            step = 50; // honour existing Ctrl + wheel behaviour
        }

        int newZoom = Zoom + (step * ticks);
        newZoom = Math.Max(MinZoomPercent, Math.Min(MaxZoomPercent, newZoom));
        if (newZoom != Zoom)
        {
            Zoom = newZoom;
        }
        // Do NOT call base.OnMouseWheel to suppress default double-handling.
    }
}