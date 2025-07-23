#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace PaletteDesigner.Utilities;

/// <summary>
/// Provides helper functionality for working with window bounds and screen visibility.
/// </summary>
public static class WindowBoundsHelper
{
    /// <summary>
    /// Ensures the given bounds are at least partially visible on a connected screen. If not, centers the window
    /// on the primary screen and adjusts size as necessary.
    /// </summary>
    /// <param name="bounds">The window bounds to adjust.</param>
    /// <returns>An adjusted <see cref="Rectangle"/> guaranteed to be visible.</returns>
    public static Rectangle AdjustBoundsToVisibleScreens(Rectangle bounds, bool preserveSize = false)
    {
        // Check if the stored rectangle intersects with any screen working area.
        bool intersects = Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(bounds));

        if (intersects)
        {
            Screen screen = Screen.FromRectangle(bounds);
            Rectangle wa = screen.WorkingArea;

            int width = preserveSize ? bounds.Width : Math.Min(bounds.Width, wa.Width);
            int height = preserveSize ? bounds.Height : Math.Min(bounds.Height, wa.Height);

            int x = Math.Min(Math.Max(bounds.X, wa.Left), wa.Right - width);
            int y = Math.Min(Math.Max(bounds.Y, wa.Top), wa.Bottom - height);
            return new Rectangle(x, y, width, height);
        }

        // Otherwise, center within the primary screen.
        Screen primaryScreen = Screen.PrimaryScreen!; // PrimaryScreen is never null on standard desktop systems

        Rectangle primary = primaryScreen.WorkingArea;
        int w = preserveSize ? bounds.Width : Math.Min(bounds.Width, primary.Width);
        int h = preserveSize ? bounds.Height : Math.Min(bounds.Height, primary.Height);
        int centeredX = primary.Left + (primary.Width - w) / 2;
        int centeredY = primary.Top + (primary.Height - h) / 2;
        return new Rectangle(centeredX, centeredY, w, h);
    }
}