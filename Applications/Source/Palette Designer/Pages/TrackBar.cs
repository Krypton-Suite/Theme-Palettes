#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace PaletteDesigner.Pages
{
    public partial class TrackBar : UserControl
    {
        private readonly List<KryptonTrackBar> _tBars;
        public TrackBar()
        {
            InitializeComponent();

            _tBars = new List<KryptonTrackBar>(new[]
            {
                kryptonTrackBar1,
                kryptonTrackBar5,
                kryptonTrackBar2,
                kryptonTrackBar6,
                kryptonTrackBar3,
                kryptonTrackBar7,
                kryptonTrackBar4,
                kryptonTrackBar8,
                kryptonTrackBar9,
                kryptonTrackBar10,
                kryptonTrackBar11,
                kryptonTrackBar12,
                kryptonTrackBar13,
                kryptonTrackBar14,
                kryptonTrackBar15,
                kryptonTrackBar16

            });
            // TrackBar fixed states
            kryptonTrackBar1.SetFixedState(PaletteState.Normal);
            kryptonTrackBar5.SetFixedState(PaletteState.Normal);
            kryptonTrackBar15.SetFixedState(PaletteState.Normal);
            kryptonTrackBar16.SetFixedState(PaletteState.Normal);

            kryptonTrackBar2.SetFixedState(PaletteState.Tracking);
            kryptonTrackBar6.SetFixedState(PaletteState.Tracking);
            kryptonTrackBar13.SetFixedState(PaletteState.Tracking);
            kryptonTrackBar14.SetFixedState(PaletteState.Tracking);

            kryptonTrackBar3.SetFixedState(PaletteState.Pressed);
            kryptonTrackBar7.SetFixedState(PaletteState.Pressed);
            kryptonTrackBar11.SetFixedState(PaletteState.Pressed);
            kryptonTrackBar12.SetFixedState(PaletteState.Pressed);

            kryptonTrackBar4.SetFixedState(PaletteState.Disabled);
            kryptonTrackBar8.SetFixedState(PaletteState.Disabled);
            kryptonTrackBar9.SetFixedState(PaletteState.Disabled);
            kryptonTrackBar10.SetFixedState(PaletteState.Disabled);
        }

        public void ApplyPalette(KryptonCustomPaletteBase palette)
        {
            _tBars.ForEach(bar => bar.LocalCustomPalette = palette);

            kryptonPanel1.Palette = palette;
        }
    }
}
