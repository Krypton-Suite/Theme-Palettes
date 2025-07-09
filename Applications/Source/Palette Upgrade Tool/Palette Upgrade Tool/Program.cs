#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

using System.Runtime.InteropServices;

namespace PaletteUpgradeTool
{
    internal static class Program
    {
#if NETFRAMEWORK
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
#endif
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Enable High-DPI support for Windows Forms
#if NETFRAMEWORK
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
#else
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI.PaletteUpgradeToolOld());
        }
    }
}
