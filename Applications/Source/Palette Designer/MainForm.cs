#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace PaletteDesigner
{
    public partial class MainForm : KryptonForm
    {
        #region Instance Fields
        private bool _dirty;
        private bool _loaded;
        private string _filename;
        private KryptonCustomPaletteBase? _palette;
        private FormChromeTMS _chromeTMS;
        private FormChromeTMS _chromeTMS2;
        private FormChromeRibbon _chromeRibbon;
        private readonly MostRecentlyUsedDocumentsManager _recentlyUsedDocumentsManager;
        private readonly SettingsManager _settingsManager = new();
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the Form1 class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _recentlyUsedDocumentsManager = new MostRecentlyUsedDocumentsManager(recentThemesToolStripMenuItem, "Krypton Palette Designer", MyOwnRecentPaletteFileGotClicked_Handler, MyOwnRecentPaletteFilesGotCleared_Handler);

            _applyPalettesToBases = new List<VisualControlBase>(new VisualControlBase[]
            {
            buttonSpecT1,
            buttonSpecT2,
            buttonSpecT3,
            buttonSpecT4,
            buttonSpecG1,
            buttonSpecG2,
            buttonSpecG3,
            buttonSpecG4,
            buttonDisabled,
            buttonDefaultFocus,
            buttonNormal,
            buttonTracking,
            buttonPressed,
            buttonCheckedNormal,
            buttonCheckedTracking,
            buttonCheckedPressed,
            buttonLive,
            control1Disabled,
            control1Normal,
            headerGroup1Disabled,
            headerGroup1Normal,
            header1Disabled,
            header1Normal,
            cbLive,
            cbFocus,
            cbUncheckedDisabled,
            cbUncheckedNormal,
            cbUncheckedTracking,
            cbUncheckedPressed,
            cbCheckedDisabled,
            cbCheckedNormal,
            cbCheckedTracking,
            cbCheckedPressed,
            cbIndeterminateDisabled,
            cbIndeterminateNormal,
            cbIndeterminateTracking,
            cbIndeterminatePressed,
            rbFocus,
            rbLive1,
            rbLive2,
            rbCheckedNormal,
            rbCheckedTracking,
            rbCheckedPressed,
            rbUncheckedDisabled,
            rbUncheckedNormal,
            rbUncheckedTracking,
            rbUncheckedPressed,
            label1Disabled,
            label1Normal,
            label1Visited,
            label1NotVisited,
            label1Pressed,
            label1Live,
            kryptonNavigatorTabs,
            kryptonNavigator,
            kryptonGroupBox1,
            kryptonGroupBox2,
            kryptonGroupBox3,
            separator1Disabled,
            separator1Normal,
            separator1Tracking,
            separator1Pressed,
            separator1Live,
            monthCalendarEnabled,
            monthCalendarDisabled,
            kryptonListBox1
            }
            );


            _applyPalettesToPanels = new List<KryptonPanel>(new[]
                {
            panel1Disabled,
            panel1Normal,
            panelLabelsBackground,
            borderDesignSeparators,
            borderDesignLabels,
            borderDesignHeaders,
            borderDesignPanels,
            borderDesignButtons,
            borderDesignControls,
            borderDesignNavigator,
            borderDesignTabs,
            panelLabelsBackground,
            kryptonPanelMainFill,
                }
            );

            _applyPalettesToPages = new List<KryptonPage>(new[]
            {
        pageTopButtonSpecs,
        pageTopButtons,
        pageTopControls,
        pageTopHeaderGroup,
        pageTopHeaders,
        pageTopLabels,
        pageTopNavigator,
        pageTopPanels,
        pageTopSeparators,
        pageTopChromeTMS,
        pageDesignButtonSpecs,
        pageDesignButtons,
        pageDesignControls,
        pageDesignHeaderGroup,
        pageDesignHeaders,
        pageDesignLabels,
        pageDesignNavigator,
        pageDesignPanels,
        pageDesignSeparators,
        pageDesignChromeTMS,
        pageButtonsStandalone,
        pageButtonsLowProfile,
        pageButtonsButtonSpec,
        pageButtonsCustom1,
        pageButtonsCustom2,
        pageButtonsCustom3,
        pageControlsClient,
        pageControlsAlternate,
        pageControlsCustom1,
        pagePanelsClient,
        pagePanelsAlternate,
        pagePanelsCustom1,
        pageHeadersPrimary,
        pageHeadersSecondary,
        pageHeadersCustom1,
        pageHeadersCustom2,
        pageLabelsNormalControl,
        pageLabelsTitleControl,
        pageLabelsCustom1,
        pageLabelsCustom2,
        pageLabelsCustom3,
        pageSeparatorLowProfile,
        pageSeparatorHighProfile,
        pageSeparatorCustom1,
        navigatorPage1,
        navigatorPage2,
        pageNavigatorBarCheckButtonGroupOutside,
        pageNavigatorBarCheckButtonGroupInside,
        pageNavigatorBarCheckButtonGroupOnly,
        navigatorPage3,
        pageTopTabs,
        pageDesignTabs,
        pageTabHighProfile,
        pageTabStandardProfile,
        pageTabLowProfile,
        pageTabOneNote,
        pageTabCustom1,
        pageTabCustom2,
        pageTabCustom3,
        kryptonNavigatorTabs1,
        kryptonNavigatorTabs2,
        kryptonNavigatorTabs3,
        pageButtonsNavigatorStack,
        pageButtonsForm,
        pageHeadersForm,
        kryptonPage1,
        pageLabelsNormalPanel,
        pageLabelsTitlePanel,
        pageButtonsAlternate,
        pageButtonsRibbonCluster,
        pageControlsToolTip,
        pageLabelsToolTip,
        pageTopRibbon,
        pageDesignRibbon,
        pageControlsRibbon,
        pageButtonsNavigatorMini,
        pageLabelsKeyTip,
        pageTopCheckBox,
        pageDesignCheckBox,
        pageTopRadioButton,
        pageDesignRadioButton,
        pageDesignGrid,
        pageTopGrids,
        kryptonGridList,
        kryptonGridSheet,
        kryptonGridCustom1,
        pageLabelsSuperTip,
        pageButtonsInputControl,
        pageTopInputControls,
        pageDesignInputControls,
        pageSeparatorHighInternalProfile,
        pageButtonsListItem,
        pageButtonsGallery,
        pageButtonsNavigatorOverflow,
        pageButtonsBreadCrumb,
        pageButtonCalendarDay,
        pageHeadersCalendar,
        pageTopDateTime,
        pageDesignDateTime,
        pageHeadersDockActive,
        pageHeadersDockInactive,
        pageTabDock,
        pageTabDockAutoHidden,
        pageControlsGroupBox,
        pageLabelsGroupBoxCaption,
        pageButtonsFormClose,
        pagePanelsRibbonInactive,
        pageTopTrackBar,
        pageDesignTrackBar,
        pageButtonsCommand,
        pageLabelsBoldControl,
        pageLabelsItalicControl,
        pageLabelsBoldPanel,
        pageLabelsItalicPanel,
        pageTopLists,
        pageLists,
            });
        }
        #endregion

        #region Operations
        private void New()
        {
            // If the current palette has been changed
            if (_dirty)
            {
                // Ask user if the current palette should be saved
                switch (MessageBox.Show(this,
                                        @"Save changes to the current palette?",
                                        @"Palette Changed",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        // Use existing save method
                        Save();
                        break;
                    case DialogResult.Cancel:
                        // Cancel out entirely
                        return;
                }
            }

            // Generate a fresh palette from scratch
            CreateNewPalette();
        }

        private void Open()
        {
            // If the current palette has been changed
            if (_dirty)
            {
                // Ask user if the current palette should be saved
                switch (KryptonMessageBox.Show(this,
                                        @"Save changes to the current palette?",
                                        @"Palette Changed",
                                        KryptonMessageBoxButtons.YesNoCancel,
                                        KryptonMessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        // Use existing save method
                        Save();
                        break;
                    case DialogResult.Cancel:
                        // Cancel out entirely
                        return;
                }
            }

            // Create a fresh palette instance for loading into
            var palette = new KryptonCustomPaletteBase();

            // Get the name of the file we imported from
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            string? filename;
            try
            {
                filename = palette.Import();
            }
            catch
            {
                // Do not abort due to un supported xml file
                filename = string.Empty;
            }

            Cursor = Cursors.Default;

            // If the load succeeded
            if (!string.IsNullOrWhiteSpace(filename))
            {
                // Need to unhook from any existing palette
                if (_palette != null)
                {
                    _palette.PalettePaint -= OnPalettePaint;
                    _palette.BasePaletteChanged -= OnBaseChanged;
                }

                // Use the new instance instead
                _palette = palette;
                _chromeTMS.Palette = palette;
                _chromeTMS2.Palette = palette;
                _chromeRibbon.OverridePalette = _palette;

                // We need to know when a change occurs to the palette settings
                _palette.PalettePaint += OnPalettePaint;
                _palette.BasePaletteChanged += OnBaseChanged;

                // Hook up the property grid to the palette
                labelGridNormal.SelectedObject = _palette;

                // Use the loaded filename
                _filename = filename!;

                // Reset the state flags
                _loaded = true;
                _dirty = false;

                // Apply the new palette to the design controls
                ApplyPalette();

                // Define the initial title bar string
                UpdateTitleBar();
                _recentlyUsedDocumentsManager.AddRecentFile(filename!);
            }

        }

        private void Save()
        {
            // If we already have a file associated with the palette...
            if (_loaded)
            {
                // ...then just save it straight away
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                _palette!.Export(_filename, true, false);
                Cursor = Cursors.Default;

                // No longer dirty
                _dirty = false;

                // Define the initial title bar string
                UpdateTitleBar();
            }
            else
            {
                // No association and so perform a save as instead
                SaveAs();
            }
        }

        private void SaveAs()
        {
            // Get back the filename selected by the user
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            var filename = _palette?.Export();
            Cursor = Cursors.Default;

            // If the save succeeded
            if (!string.IsNullOrWhiteSpace(filename))
            {
                // Remember associated file details
                _filename = filename!;
                _loaded = true;

                // No longer dirty
                _dirty = false;

                // Define the initial title bar string
                UpdateTitleBar();
            }

            _recentlyUsedDocumentsManager.AddRecentFile(filename!);
        }

        private void Exit()
        {
            // If the current palette has been changed
            if (_dirty)
            {
                // Ask user if the current palette should be saved
                switch (MessageBox.Show(this,
                                        @"Save changes to the current palette?",
                                        @"Palette Changed",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        // Use existing save method
                        Save();
                        break;
                    case DialogResult.Cancel:
                        // Cancel out entirely
                        return;
                }
            }

            Close();
        }
        #endregion

        #region Palettes

        private readonly List<VisualControlBase> _applyPalettesToBases;
        private readonly List<KryptonPanel> _applyPalettesToPanels;
        private readonly List<KryptonPage> _applyPalettesToPages;
        private void CreateNewPalette()
        {
            // Need to unhook from any existing palette
            if (_palette != null)
            {
                _palette.PalettePaint -= OnPalettePaint;
                _palette.BasePaletteChanged -= OnBaseChanged;
            }

            // Create a fresh palette instance
            _palette = new KryptonCustomPaletteBase();
            _chromeTMS.Palette = _palette;
            _chromeTMS2.Palette = _palette;
            _chromeRibbon.OverridePalette = _palette;

            // We need to know when a change occurs to the palette settings
            _palette.PalettePaint += OnPalettePaint;
            _palette.BasePaletteChanged += OnBaseChanged;

            // Hook up the property grid to the palette
            labelGridNormal.SelectedObject = _palette;

            // Does not have a filename as yet
            _filename = "(New Palette)";

            // Reset the state flags
            _dirty = false;
            _loaded = false;

            // Apply the new palette to the design controls
            ApplyPalette();

            // Define the initial title bar string
            UpdateTitleBar();
        }

        private void OnBaseChanged(object sender, EventArgs e)
        {
            ApplyPalette();
        }

        private void ApplyPalette()
        {
            if (_palette == null)
            {
                return;
            }

            _applyPalettesToBases.ForEach(vcb => vcb.Palette = _palette);
            _applyPalettesToPanels.ForEach(pnl => pnl.Palette = _palette);

            dataGridViewDisabled.Palette = _palette;
            dataGridViewNormal.Palette = _palette;
            kryptonListView1.Palette = _palette;

            inputControls1.ApplyPalette(_palette);
            trackBar1.ApplyPalette(_palette);
            menuPage1.ApplyPalette(_palette);
            toolTipsPage1.ApplyPalette(_palette);

            UpdateChromeTMS();

            // Hack until the pages are separated out:
            var backClr = _palette.GetBackColor1(PaletteBackStyle.PanelClient, PaletteState.Normal);
            _applyPalettesToPages.ForEach(pg => pg.StateCommon.Page.Color1 = backClr);
        }

        private void UpdateChromeTMS()
        {
            if (_palette == null)
            {
                return;
            }

            // Get the global renderer
            IRenderer renderer = _palette.GetRenderer();

            // Get the new toolstrip renderer based on the design palette
            _chromeTMS.OverrideToolStripRenderer = renderer.RenderToolStrip(_palette);
            _chromeTMS2.OverrideToolStripRenderer = renderer.RenderToolStrip(_palette);
        }

        private void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            // Only interested the first time the palette is changed
            if (!_dirty)
            {
                _dirty = true;
                UpdateTitleBar();
            }

            // Do we need to setup a new renderer for the ToolMenuStatus page?
            if (e.NeedColorTable)
            {
                UpdateChromeTMS();
            }
        }
        #endregion

        #region Event Handlers
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = _settingsManager.GetMaximised() ? FormWindowState.Maximized : FormWindowState.Normal;

            // Populate the sample data set
            dataTable1.Rows.Add(@"One", @"Two", @"Three");
            dataTable1.Rows.Add(@"Uno", @"Dos", @"Tres");
            dataTable1.Rows.Add(@"Un", @"Deux", @"Trios");

            // Add the chrome window to the Chrome + Strips page
            _chromeTMS = new FormChromeTMS
            {
                TopLevel = false,
                Parent = pageDesignChromeTMS,
                Dock = DockStyle.Top,
                InertForm = true
            };
            _chromeTMS.Show();
            _chromeTMS2 = new FormChromeTMS
            {
                TopLevel = false,
                Parent = pageDesignChromeTMS,
                Dock = DockStyle.Bottom,
                InertForm = true,
                Enabled = false,
                TextExtra = @"Disabled"
            };
            _chromeTMS2.Show();

            // Add the chrome window with embedded Ribbon
            _chromeRibbon = new FormChromeRibbon
            {
                TopLevel = false,
                Parent = pageDesignRibbon,
                Dock = DockStyle.Fill,
                InertForm = true
            };
            _chromeRibbon.Show();

            // Button fixed states
            buttonDisabled.SetFixedState(PaletteState.Disabled);
            buttonDefaultFocus.SetFixedState(PaletteState.NormalDefaultOverride);
            buttonNormal.SetFixedState(PaletteState.Normal);
            buttonTracking.SetFixedState(PaletteState.Tracking);
            buttonPressed.SetFixedState(PaletteState.Pressed);
            buttonCheckedNormal.SetFixedState(PaletteState.CheckedNormal);
            buttonCheckedTracking.SetFixedState(PaletteState.CheckedTracking);
            buttonCheckedPressed.SetFixedState(PaletteState.CheckedPressed);

            // CheckBox fixed states
            cbFocus.SetFixedState(true, true, false, false);
            cbUncheckedDisabled.SetFixedState(false, false, false, false);
            cbUncheckedNormal.SetFixedState(false, true, false, false);
            cbUncheckedTracking.SetFixedState(false, true, true, false);
            cbUncheckedPressed.SetFixedState(false, true, false, true);
            cbCheckedDisabled.SetFixedState(false, false, false, false);
            cbCheckedNormal.SetFixedState(false, true, false, false);
            cbCheckedTracking.SetFixedState(false, true, true, false);
            cbCheckedPressed.SetFixedState(false, true, false, true);
            cbIndeterminateDisabled.SetFixedState(false, false, false, false);
            cbIndeterminateNormal.SetFixedState(false, true, false, false);
            cbIndeterminateTracking.SetFixedState(false, true, true, false);
            cbIndeterminatePressed.SetFixedState(false, true, false, true);

            // RadioButton fixed states
            rbFocus.SetFixedState(true, true, false, false);
            rbCheckedDisabled.SetFixedState(false, false, false, false);
            rbCheckedNormal.SetFixedState(false, true, false, false);
            rbCheckedTracking.SetFixedState(false, true, true, false);
            rbCheckedPressed.SetFixedState(false, true, false, true);
            rbUncheckedDisabled.SetFixedState(false, false, false, false);
            rbUncheckedNormal.SetFixedState(false, true, false, false);
            rbUncheckedTracking.SetFixedState(false, true, true, false);
            rbUncheckedPressed.SetFixedState(false, true, false, true);

            // Control fixed states
            control1Disabled.SetFixedState(PaletteState.Disabled);
            control1Normal.SetFixedState(PaletteState.Normal);

            // HeaderGroup fixed states
            headerGroup1Disabled.SetFixedState(PaletteState.Disabled);
            headerGroup1Normal.SetFixedState(PaletteState.Normal);

            // Headers fixed states
            header1Disabled.SetFixedState(PaletteState.Disabled);
            header1Normal.SetFixedState(PaletteState.Normal);


            // Labels fixed states
            label1Disabled.SetFixedState(PaletteState.Disabled);
            label1Normal.SetFixedState(PaletteState.Normal);
            label1Visited.SetFixedState(PaletteState.Normal);
            label1NotVisited.SetFixedState(PaletteState.Normal);
            label1Pressed.SetFixedState(PaletteState.Pressed);

            // Panel fixed states
            panel1Disabled.SetFixedState(PaletteState.Disabled);
            panel1Normal.SetFixedState(PaletteState.Normal);

            // Separator fixed states
            separator1Disabled.SetFixedState(PaletteState.Disabled, PaletteState.Disabled);
            separator1Normal.SetFixedState(PaletteState.Normal, PaletteState.Normal);
            separator1Tracking.SetFixedState(PaletteState.Normal, PaletteState.Tracking);
            separator1Pressed.SetFixedState(PaletteState.Normal, PaletteState.Pressed);


            // Remove the context menu from the design navigator, we only show this
            // during design time to make it easier to switch around pages for updating
            // the design. At runtime it should always be in sync with the top navigator.
            kryptonNavigatorDesign.Button.ButtonDisplayLogic = ButtonDisplayLogic.None;

            // Define initial display pages
            kryptonNavigatorTop.SelectedPage = pageTopButtons;
            kryptonNavigatorDesign.SelectedPage = pageDesignButtons;

            CreateNewPalette();
        }

        private void MenuNew_Click(object sender, EventArgs e) => New();

        private void MenuOpen_Click(object sender, EventArgs e) => Open();

        private void MenuSave_Click(object sender, EventArgs e) => Save();

        private void MenuSaveAs_Click(object sender, EventArgs e) => SaveAs();

        private void MenuExit_Click(object sender, EventArgs e) => Exit();

        private void KryptonNavigatorTop_SelectedPageChanged(object sender, EventArgs e) =>
            // Reflect change in the design navigator
            kryptonNavigatorDesign.SelectedIndex = kryptonNavigatorTop.SelectedIndex;

        private void KryptonNavigatorDesign_SelectedPageChanged(object sender, EventArgs e) =>
            // Reflect change in the top navigator
            kryptonNavigatorTop.SelectedIndex = kryptonNavigatorDesign.SelectedIndex;

        private void KryptonNavigatorDesignButtons_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignButtons.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignButtons.TextTitle = kryptonNavigatorDesignButtons.SelectedPage.Text;
            pageDesignButtons.TextDescription = kryptonNavigatorDesignButtons.SelectedPage.TextDescription;

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
            buttonDisabled.ButtonStyle = bs;
            buttonDefaultFocus.ButtonStyle = bs;
            buttonNormal.ButtonStyle = bs;
            buttonTracking.ButtonStyle = bs;
            buttonPressed.ButtonStyle = bs;
            buttonCheckedNormal.ButtonStyle = bs;
            buttonCheckedTracking.ButtonStyle = bs;
            buttonCheckedPressed.ButtonStyle = bs;
            buttonLive.ButtonStyle = bs;
        }

        private void KryptonNavigatorDesignControls_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignControls.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignControls.TextTitle = kryptonNavigatorDesignControls.SelectedPage.Text;
            pageDesignControls.TextDescription = kryptonNavigatorDesignControls.SelectedPage.TextDescription;

            PaletteBackStyle backStyle;
            PaletteBorderStyle borderStyle;

            // Work out the group styles to be used
            switch (kryptonNavigatorDesignControls.SelectedIndex)
            {
                default:
                case 0:
                    backStyle = PaletteBackStyle.ControlClient;
                    borderStyle = PaletteBorderStyle.ControlClient;
                    break;
                case 1:
                    backStyle = PaletteBackStyle.ControlAlternate;
                    borderStyle = PaletteBorderStyle.ControlAlternate;
                    break;
                case 2:
                    backStyle = PaletteBackStyle.ControlGroupBox;
                    borderStyle = PaletteBorderStyle.ControlGroupBox;
                    break;
                case 3:
                    backStyle = PaletteBackStyle.ControlToolTip;
                    borderStyle = PaletteBorderStyle.ControlToolTip;
                    break;
                case 4:
                    backStyle = PaletteBackStyle.ControlRibbon;
                    borderStyle = PaletteBorderStyle.ControlRibbon;
                    break;
                case 5:
                    backStyle = PaletteBackStyle.ControlCustom1;
                    borderStyle = PaletteBorderStyle.ControlCustom1;
                    break;
            }

            // Update all the displayed controls with the new styles
            control1Disabled.GroupBackStyle = backStyle;
            control1Disabled.GroupBorderStyle = borderStyle;
            control1Normal.GroupBackStyle = backStyle;
            control1Normal.GroupBorderStyle = borderStyle;
        }

        private void KryptonNavigatorDesignHeaders_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignHeaders.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignHeaders.TextTitle = kryptonNavigatorDesignHeaders.SelectedPage.Text;
            pageDesignHeaders.TextDescription = kryptonNavigatorDesignHeaders.SelectedPage.TextDescription;

            // Work out the header style to be used
            HeaderStyle hs = kryptonNavigatorDesignHeaders.SelectedIndex switch
            {
                0 => HeaderStyle.Primary,
                1 => HeaderStyle.Secondary,
                2 => HeaderStyle.DockActive,
                3 => HeaderStyle.DockInactive,
                4 => HeaderStyle.Calendar,
                5 => HeaderStyle.Form,
                6 => HeaderStyle.Custom1,
                7 => HeaderStyle.Custom2,
                _ => HeaderStyle.Primary
            };

            // Update all the displayed controls with the new styles
            header1Disabled.HeaderStyle = hs;
            header1Normal.HeaderStyle = hs;
        }

        private void KryptonNavigatorDesignLabels_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignLabels.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignLabels.TextTitle = kryptonNavigatorDesignLabels.SelectedPage.Text;
            pageDesignLabels.TextDescription = kryptonNavigatorDesignLabels.SelectedPage.TextDescription;

            // Work out the label style to be used
            LabelStyle ls = kryptonNavigatorDesignLabels.SelectedIndex switch
            {
                0 => LabelStyle.NormalControl,
                1 => LabelStyle.BoldControl,
                2 => LabelStyle.ItalicControl,
                3 => LabelStyle.TitleControl,
                4 => LabelStyle.NormalPanel,
                5 => LabelStyle.BoldPanel,
                6 => LabelStyle.ItalicPanel,
                7 => LabelStyle.TitlePanel,
                8 => LabelStyle.GroupBoxCaption,
                9 => LabelStyle.ToolTip,
                10 => LabelStyle.SuperTip,
                11 => LabelStyle.KeyTip,
                12 => LabelStyle.Custom1,
                13 => LabelStyle.Custom2,
                14 => LabelStyle.Custom3,
                _ => LabelStyle.NormalControl
            };

            // Update all the displayed controls with the new styles
            label1Disabled.LabelStyle = ls;
            label1Normal.LabelStyle = ls;
            label1Visited.LabelStyle = ls;
            label1NotVisited.LabelStyle = ls;
            label1Pressed.LabelStyle = ls;
            label1Live.LabelStyle = ls;
        }

        private void KryptonCheckSetLabels_CheckedButtonChanged(object sender, EventArgs e)
        {
            panelLabelsBackground.PanelBackStyle = kryptonCheckSetLabels.CheckedIndex switch
            {
                0 => PaletteBackStyle.PanelClient,
                1 => PaletteBackStyle.PanelAlternate,
                2 => PaletteBackStyle.PanelCustom1,
                3 => PaletteBackStyle.ControlClient,
                4 => PaletteBackStyle.ControlAlternate,
                5 => PaletteBackStyle.ControlCustom1,
                6 => PaletteBackStyle.ControlToolTip,
                _ => panelLabelsBackground.PanelBackStyle
            };

            panelLabelsBackground.Refresh();
        }

        private void KryptonNavigatorDesignTabs_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignTabs.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignTabs.TextTitle = kryptonNavigatorDesignTabs.SelectedPage.Text;
            pageDesignTabs.TextDescription = kryptonNavigatorDesignTabs.SelectedPage.TextDescription;

            // Work out the tab style to show in the navigator
            kryptonNavigatorTabs.Bar.TabStyle = kryptonNavigatorDesignTabs.SelectedIndex switch
            {
                0 => TabStyle.HighProfile,
                1 => TabStyle.StandardProfile,
                2 => TabStyle.LowProfile,
                3 => TabStyle.OneNote,
                4 => TabStyle.Dock,
                5 => TabStyle.DockAutoHidden,
                6 => TabStyle.Custom1,
                7 => TabStyle.Custom2,
                8 => TabStyle.Custom3,
                _ => TabStyle.HighProfile
            };
        }

        private void KryptonNavigatorDesignNavigator_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignNavigator.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignNavigator.TextTitle = kryptonNavigatorDesignNavigator.SelectedPage.Text;
            pageDesignNavigator.TextDescription = kryptonNavigatorDesignNavigator.SelectedPage.TextDescription;

            // Work out the navigator mode required
            kryptonNavigator.NavigatorMode = kryptonNavigatorDesignNavigator.SelectedIndex switch
            {
                0 => NavigatorMode.BarCheckButtonGroupOutside,
                1 => NavigatorMode.BarCheckButtonGroupInside,
                2 => NavigatorMode.BarCheckButtonGroupOnly,
                _ => NavigatorMode.BarCheckButtonGroupOutside
            };
        }

        private void KryptonNavigatorDesignPanels_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignPanels.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignPanels.TextTitle = kryptonNavigatorDesignPanels.SelectedPage.Text;
            pageDesignPanels.TextDescription = kryptonNavigatorDesignPanels.SelectedPage.TextDescription;

            // Work out the panel style to be used
            PaletteBackStyle backStyle = kryptonNavigatorDesignPanels.SelectedIndex switch
            {
                0 => PaletteBackStyle.PanelClient,
                1 => PaletteBackStyle.PanelAlternate,
                2 => PaletteBackStyle.PanelRibbonInactive,
                3 => PaletteBackStyle.PanelCustom1,
                _ => PaletteBackStyle.PanelClient
            };

            // Update all the displayed controls with the new styles
            panel1Disabled.PanelBackStyle = backStyle;
            panel1Normal.PanelBackStyle = backStyle;
        }

        private void KryptonNavigatorDesignSeparators_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignSeparators.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignSeparators.TextTitle = kryptonNavigatorDesignSeparators.SelectedPage.Text;
            pageDesignSeparators.TextDescription = kryptonNavigatorDesignSeparators.SelectedPage.TextDescription;

            // Work out the navigator mode required
            SeparatorStyle separatorStyle = kryptonNavigatorDesignSeparators.SelectedIndex switch
            {
                0 => SeparatorStyle.LowProfile,
                1 => SeparatorStyle.HighProfile,
                2 => SeparatorStyle.HighInternalProfile,
                3 => SeparatorStyle.Custom1,
                _ => SeparatorStyle.LowProfile
            };

            // Update all the displayed controls with the new styles
            separator1Disabled.SeparatorStyle = separatorStyle;
            separator1Normal.SeparatorStyle = separatorStyle;
            separator1Tracking.SeparatorStyle = separatorStyle;
            separator1Pressed.SeparatorStyle = separatorStyle;
            separator1Live.SeparatorStyle = separatorStyle;
        }

        private void KryptonNavigatorDesignGrids_SelectedPageChanged(object sender, EventArgs e)
        {
            if (kryptonNavigatorDesignGrids.SelectedPage == null)
            {
                return;
            }
            // Update the design page text with the selected style information
            pageDesignGrid.TextTitle = kryptonNavigatorDesignGrids.SelectedPage.Text;
            pageDesignGrid.TextDescription = kryptonNavigatorDesignGrids.SelectedPage.TextDescription;

            // Work out the navigator mode required
            DataGridViewStyle gridStyle = kryptonNavigatorDesignGrids.SelectedIndex switch
            {
                0 => DataGridViewStyle.List,
                1 => DataGridViewStyle.Sheet,
                2 => DataGridViewStyle.Custom1,
                _ => DataGridViewStyle.List
            };

            // Update all the displayed controls with the new styles
            dataGridViewDisabled.GridStyles.Style = gridStyle;
            dataGridViewNormal.GridStyles.Style = gridStyle;
        }
        #endregion

        #region Implementation
        private void UpdateTitleBar() =>
            // Mark a changed file with a star
            Text = $@"Palette Designer - {_filename}{(_dirty ? "*" : string.Empty)}";

        #endregion

        #region Recently Used

        private void MyOwnRecentPaletteFileGotClicked_Handler(object sender, EventArgs e)
        {
            var fileName = (sender as ToolStripItem)?.Text;
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            if (!File.Exists(fileName))
            {
                if (KryptonMessageBox.Show($"{fileName} doesn't exist. Remove from `Recent Themes`?",
                        "File not found",
                        KryptonMessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _recentlyUsedDocumentsManager.RemoveRecentFile(fileName!);
                }
                return;
            }

            try
            {
                _palette.Import(fileName!, false);
            }
            catch
            {
                // https://github.com/Krypton-Suite/Theme-Palettes/issues/43
                // Do nothing as the MB will displayed due to silent = `false`
            }
        }

        private void MyOwnRecentPaletteFilesGotCleared_Handler(object sender, EventArgs e)
        {

        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => _settingsManager.SaveSettings();

        private void LaunchPaletteUpgradeToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPaletteUpgradeTool paletteUpgradeTool = new();

            paletteUpgradeTool.Show();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var controlPanel = new SettingsControlPanel();

            controlPanel.Show();
        }
    }
}