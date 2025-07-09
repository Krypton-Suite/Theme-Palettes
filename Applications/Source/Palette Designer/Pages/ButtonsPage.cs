/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 */

using Krypton.Toolkit;

namespace PaletteDesigner.Pages;

public partial class ButtonsPage : UserControl
{
    private readonly List<KryptonButton> _pageButtons;
    private readonly List<KryptonDropButton> _pageButtons2;
    private readonly List<KryptonGroupBox> _kryptonGroupBoxs;

    public ButtonsPage()
    {
        InitializeComponent();
        // Button fixed states
        buttonDisabled.SetFixedState(PaletteState.Disabled);
        buttonDefaultFocus.SetFixedState(PaletteState.NormalDefaultOverride);
        buttonNormal.SetFixedState(PaletteState.Normal);
        buttonTracking.SetFixedState(PaletteState.Tracking);
        buttonPressed.SetFixedState(PaletteState.Pressed);
        buttonCheckedNormal.SetFixedState(PaletteState.CheckedNormal);
        buttonCheckedTracking.SetFixedState(PaletteState.CheckedTracking);
        buttonCheckedPressed.SetFixedState(PaletteState.CheckedPressed);

        kryptonDropButton1.SetFixedState(PaletteState.Disabled);
        kryptonDropButton2.SetFixedState(PaletteState.NormalDefaultOverride);
        kryptonDropButton5.SetFixedState(PaletteState.Normal);
        kryptonDropButton3.SetFixedState(PaletteState.Tracking);
        kryptonDropButton6.SetFixedState(PaletteState.Pressed);

        kryptonButton10.SetFixedState(PaletteState.Disabled);
        kryptonButton11.SetFixedState(PaletteState.NormalDefaultOverride);
        kryptonButton14.SetFixedState(PaletteState.Normal);
        kryptonButton12.SetFixedState(PaletteState.Tracking);
        kryptonButton15.SetFixedState(PaletteState.Pressed);

        _pageButtons =
        [
            ..new []
            {
                buttonDisabled,
                buttonDefaultFocus,
                buttonNormal,
                buttonTracking,
                buttonPressed,
                buttonCheckedNormal,
                buttonCheckedTracking,
                buttonCheckedPressed,
                buttonLive,
                kryptonButton10,
                kryptonButton11,
                kryptonButton12,
                kryptonButton13,
                kryptonButton14,
                kryptonButton15
            }
        ];

        _pageButtons2 =
        [
            ..new[]
            {
                kryptonDropButton1,
                kryptonDropButton2,
                kryptonDropButton3,
                kryptonDropButton4,
                kryptonDropButton5,
                kryptonDropButton6,
            }
        ];
        _kryptonGroupBoxs =
        [
            ..new[]
            {
                kryptonGroupBox1,
                kryptonGroupBox2,
                kryptonGroupBox3
            }
        ];
        //_pageButtons =
        //[
        //    ..new[]
        //    {
        //        pageButtonsStandalone,
        //        pageButtonsLowProfile,
        //        pageButtonsButtonSpec,
        //        pageButtonsCustom1,
        //        pageButtonsCustom2,
        //        pageButtonsCustom3,
        //        pageButtonsNavigatorStack,
        //        pageButtonsForm,
        //        pageButtonsAlternate,
        //        pageButtonsRibbonCluster,
        //        pageButtonsNavigatorMini,
        //        pageButtonsInputControl,
        //        pageButtonsListItem,
        //        pageButtonsGallery,
        //        pageButtonsNavigatorOverflow,
        //        pageButtonsBreadCrumb,
        //        pageButtonCalendarDay,
        //        pageButtonsFormClose,
        //        pageButtonsCommand
        //    }
        //];

    }

    public void ApplyPalette(KryptonCustomPaletteBase palette)
    {
        _pageButtons.ForEach(control => control.LocalCustomPalette = palette);
        _pageButtons2.ForEach(control => control.LocalCustomPalette = palette);
        _kryptonGroupBoxs.ForEach(control => control.LocalCustomPalette = palette);
        kryptonColorButton1.LocalCustomPalette = palette;

        kryptonPanel1.Palette = palette;
    }

    private void kryptonNavigatorDesignButtons_SelectedPageChanged(object sender, EventArgs e)
    {
        if (kryptonNavigatorDesignButtons.SelectedPage == null)
        {
            return;
        }
        // Update the design page text with the selected style information
        //pageDesignButtons.TextTitle = kryptonNavigatorDesignButtons.SelectedPage.Text;
        //pageDesignButtons.TextDescription = kryptonNavigatorDesignButtons.SelectedPage.TextDescription;

        // Work out the button style to be used
        ButtonStyle bs = kryptonNavigatorDesignButtons.SelectedIndex switch
        {
            0 => ButtonStyle.Standalone,
            1 => ButtonStyle.Alternate,
            2 => ButtonStyle.LowProfile,
            3 => ButtonStyle.BreadCrumb,
            4 => ButtonStyle.CalendarDay,
            5 => ButtonStyle.ButtonSpec,
            6 => ButtonStyle.Cluster,
            7 => ButtonStyle.NavigatorStack,
            8 => ButtonStyle.NavigatorOverflow,
            9 => ButtonStyle.NavigatorMini,
            10 => ButtonStyle.InputControl,
            11 => ButtonStyle.ListItem,
            12 => ButtonStyle.Gallery,
            13 => ButtonStyle.Form,
            14 => ButtonStyle.FormClose,
            15 => ButtonStyle.Command,
            16 => ButtonStyle.Custom1,
            17 => ButtonStyle.Custom2,
            18 => ButtonStyle.Custom3,
            _ => ButtonStyle.Standalone
        };

        // Update all the displayed buttons with the new style
        _pageButtons.ForEach(control => control.ButtonStyle = bs);
        _pageButtons2.ForEach(control => control.ButtonStyle = bs);
        kryptonColorButton1.ButtonStyle = bs;
    }
}