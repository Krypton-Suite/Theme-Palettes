#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
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
        private SettingsControlPanel? _settingsControlPanel;
        private FormPaletteUpgradeTool? _paletteUpgradeToolForm;

        // Undo stack for color edits
        private readonly Stack<(SchemeBaseColors EnumValue, Color OldColor)> _undoStack = new();
        // Tracks last known PropertyGrid path for each SchemeBaseColors entry
        private readonly Dictionary<SchemeBaseColors, string> _enumToPath = new();
        // Maps each SchemeBaseColors value to its DataGridView row index for fast updates
        // This makes access future-proof in case there are additions to `SchemeBaseColors`!
        private readonly Dictionary<SchemeBaseColors, int> _enumToGridRow = new();

        // Font size limits for grids/properties
        private const float MinFontSize = 6f;
        private const float MaxFontSize = 24f;

        private Color? _activeColorFilter;
        private string? _activeNameFilter;
        private string? _lastColorFilterInput;

        // Context menu for grid operations
        private ContextMenuStrip? _contextMenu;
        private ToolStripMenuItem? _toggleFormatMenuItem;
        private ToolStripMenuItem? _autoFillMenuItem;
        private bool _autoFillFromViewer;
        private bool _displayRgb = true; // default display format per property grid
        // regex instance to detect full RGB triplet strings in the filter box (exact 3 digits per component)
        private Regex _regexFilterBoxFontColor = new(@"^\d{1,3};\d{1,3};\d{1,3}$");
        // regex instance to detect full hex color strings (#RRGGBB or #AARRGGBB)
        private Regex _regexFilterBoxHexColor = new(@"^#(?:[0-9A-Fa-f]{6}|[0-9A-Fa-f]{8})$");

        #endregion

        #region Identity

        /// <summary>
        /// Initialize a new instance of the MainForm class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _filename = string.Empty;
            _chromeTMS = new FormChromeTMS();
            _chromeTMS2 = new FormChromeTMS();
            _chromeRibbon = new FormChromeRibbon();

            _recentlyUsedDocumentsManager = new MostRecentlyUsedDocumentsManager(
                recentThemesToolStripMenuItem,
                "Krypton Palette Designer",
                MyOwnRecentPaletteFileGotClicked_Handler,
                MyOwnRecentPaletteFilesGotCleared_Handler);

            KryptonManager.GlobalPaletteChanged += OnGlobalPaletteChanged;

            // Apply saved global palette from settings
            kryptonManager.GlobalPaletteMode = _settingsManager.GetTheme();

            // Keep colorTableGrid visible and fixed width during window resize
            kryptonSplitContainerProperties.Panel2MinSize = colorTableGrid.PreferredSize.Width;
            kryptonSplitContainerProperties.FixedPanel = FixedPanel.Panel2;

            colorTableGrid.Columns.Add("#", "#");
            colorTableGrid.Columns.Add("Name", "Scheme Colors");
            colorTableGrid.Columns.Add("Value", "Color");
            colorTableGrid.Columns.Add("Color", "");
            colorTableGrid.Columns[3].Width = 60;
            colorTableGrid.Columns[3].ReadOnly = true;
            colorTableGrid.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            colorTableGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            SetupContextMenu();

            // Initialize menu item states
            UpdateFormatMenuItems();
            UpdateAutoFillMenuItems();

            // Disable built-in button-spec tooltips – the help message is shown on click instead

            // Apply saved font size
            float savedSize = _settingsManager.GetPropertyGridFontSize();
            savedSize = Math.Max(MinFontSize, Math.Min(MaxFontSize, savedSize));
            if (Math.Abs(savedSize - propertyGrid.Font.Size) > 0.1f)
            {
                AdjustGridFont(savedSize - propertyGrid.Font.Size);
            }

            _applyPalettesToBases =
            [
                ..new VisualControlBase[]
                {
                    buttonSpecT1,
                    buttonSpecT2,
                    buttonSpecT3,
                    buttonSpecT4,
                    buttonSpecG1,
                    buttonSpecG2,
                    buttonSpecG3,
                    buttonSpecG4,
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
                    kryptonNavigatorDesign,
                    kryptonNavigatorDesignPanels,
                    kryptonNavigatorDesignHeaders,
                    kryptonNavigatorDesignLabels,
                    kryptonNavigatorDesignNavigator,
                    kryptonNavigatorDesignSeparators,
                    kryptonNavigatorDesignTabs,
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

            ];

            _applyPalettesToPanels =
            [
                ..new[]
                {
                    panel1Disabled,
                    panel1Normal,
                    panelLabelsBackground,
                    borderDesignSeparators,
                    borderDesignLabels,
                    borderDesignHeaders,
                    borderDesignPanels,
                    borderDesignNavigator,
                    borderDesignTabs,
                    panelLabelsBackground,
                    kryptonPanelMainFill
                }

            ];

            if (imageViewerControl != null)
            {
                imageViewerControl.ColorSampled += ImageViewerControl_ColorSampled;
            }

            // Restore last used filter mode
            bool colorFilterMode = _settingsManager.GetFastFilterMode() == "0";
            if (colorFilterMode)
            {
                filterByColorButton.Checked = true;
            }
            else
            {
                filterByNameButton.Checked = true;
            }
        }

        #endregion

        #region Operations

        private void New()
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

            // Generate a fresh palette from scratch
            CreateNewPalette();
            ApplyQuickFilter(false);
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
            try
            {
                if (_settingsManager.GetUpgradeOnImport())
                {
                    using var paletteOpenFileDialog = new KryptonOpenFileDialog()
                    {
                        CheckFileExists = true,
                        CheckPathExists = true,
                        DefaultExt = @"xml",
                        Filter = @"Palette files (*.xml)|*.xml|All files (*.*)|(*.*)",
                        Title = @"Load Custom Palette"
                    };

                    string paletteFileName = (paletteOpenFileDialog.ShowDialog() == DialogResult.OK)
                        ? paletteOpenFileDialog.FileName
                        : string.Empty;

                    palette.ImportWithUpgrade(File.OpenRead(paletteFileName));

                    Cursor = Cursors.Default;

                    if (string.IsNullOrWhiteSpace(paletteFileName))
                    {
                        return;
                    }

                    // Initialise palette and update UI/state
                    InitializeLoadedPalette(palette, paletteFileName);
                }
                else
                {
                    var filename = palette.Import();

                    Cursor = Cursors.Default;

                    // If the load succeeded
                    if (!string.IsNullOrWhiteSpace(filename))
                    {
                        // Initialise palette and update UI/state
                        InitializeLoadedPalette(palette, filename);
                    }
                }
            }
            catch
            {
                // Do not abort due to unsupported xml file
                _filename = string.Empty;
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
                _recentlyUsedDocumentsManager.AddRecentFile(filename!);
            }
        }

        private void Exit()
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

            Close();
        }

        // Tools -> Rebuild Palette Mapping...
        private void rebuildPaletteMappingToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_palette == null)
            {
                KryptonMessageBox.Show(this, @"No palette is currently loaded.", @"Rebuild Palette Mapping", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                return;
            }

            Cursor previous = Cursor;
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // Rebuild mapping for current palette
                PaletteMappingCache.Regenerate(_palette);

                int count = PaletteMappingCache.Map.Count;
                KryptonMessageBox.Show(this, $"Mapping regenerated ({count} entries).", @"Rebuild Palette Mapping", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this, "Failed to regenerate mapping:\r\n" + ex.Message, @"Rebuild Palette Mapping", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
            finally
            {
                Cursor = previous;
            }
        }

        #endregion

        #region Palettes

        /// <summary>
        /// Performs all post-import initialisation for a palette that has just
        /// been loaded from disk (either via Open or Recent-file click).
        /// </summary>
        private void InitializeLoadedPalette(
            KryptonCustomPaletteBase palette,
            string filename,
            bool addToRecentList = true)
        {
            // 1) Unhook old palette
            if (_palette != null)
            {
                _palette.PalettePaint       -= OnPalettePaint;
                _palette.BasePaletteChanged -= OnBaseChanged;
                _palette.SchemeColorChanged -= OnPaletteSchemeColorChanged;
            }

            // 2) Store & wire the new palette
            _palette = palette;
            _chromeTMS.LocalCustomPalette  = palette;
            _chromeTMS2.LocalCustomPalette = palette;
            _chromeRibbon.OverridePalette  = palette;

            _palette.PalettePaint       += OnPalettePaint;
            _palette.BasePaletteChanged += OnBaseChanged;
            _palette.SchemeColorChanged += OnPaletteSchemeColorChanged;

            // 3) Property-grid binding
            propertyGrid.SelectedObject = _palette;
            propertyGrid.Refresh();

            // 3a) Load mapping cache if it exists (no regeneration yet)
            PaletteMappingCache.Initialise(_palette!, regenerateIfMissing: false);

            // 4) Refresh all colors from overrides
            // This compares and reloads colors based on the SchemeBaseColors enum,
            // but some themes have different combinations,
            // and this function causes unexpected changes.
            //RefreshSchemeFromOverrides();

            // 5) Persist cache to disk if it was newly created or updated during path resolution
            PaletteMappingCache.Save();

            // 6) Update state & UI
            _filename = filename;
            _loaded   = true;
            _dirty    = false;
            kryptonManager.GlobalPaletteMode = _palette.BasePaletteMode;

            ApplyPalette();          // push colours to sample controls
            ApplyQuickFilter(false); // re-apply/clear fast filter
            UpdateTitleBar();

            if (addToRecentList)
            {
                _recentlyUsedDocumentsManager.AddRecentFile(filename);
            }
        }

        private readonly List<VisualControlBase> _applyPalettesToBases;
        private readonly List<KryptonPanel> _applyPalettesToPanels;

        private void CreateNewPalette(bool useCurrentGlobalPalette = false)
        {
            // Need to unhook from any existing palette
            _palette?.PalettePaint -= OnPalettePaint;
            _palette?.BasePaletteChanged -= OnBaseChanged;
            _palette?.SchemeColorChanged -= OnPaletteSchemeColorChanged;

            // Create a fresh palette instance
            _palette = new KryptonCustomPaletteBase();

            // Only use the current global palette when explicitly requested (e.g., when loading a default theme)
            if (useCurrentGlobalPalette)
            {
                _palette.BasePalette = KryptonManager.CurrentGlobalPalette;
            }
            else
            {
                kryptonManager.GlobalPaletteMode = ThemeManager.DefaultGlobalPalette;
            }

            _chromeTMS.LocalCustomPalette = _palette;
            _chromeTMS2.LocalCustomPalette = _palette;
            _chromeRibbon.OverridePalette = _palette;

            // We need to know when a change occurs to the palette settings
            _palette.PalettePaint += OnPalettePaint;
            _palette.BasePaletteChanged += OnBaseChanged;
            _palette.SchemeColorChanged += OnPaletteSchemeColorChanged;

            // Hook up the property grid to the palette
            propertyGrid.SelectedObject = _palette;
            propertyGrid.Refresh();

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

        private void OnBaseChanged(object? sender, EventArgs e)
        {
            ApplyPalette();
            // Re-subscribe to the event in case the base palette changed
            _palette?.SchemeColorChanged -= OnPaletteSchemeColorChanged;
            _palette?.SchemeColorChanged += OnPaletteSchemeColorChanged;
        }

        private void ApplyPalette(bool populateTable = true)
        {
            if (_palette == null)
            {
                return;
            }
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => ApplyPalette(populateTable)));
                return;
            }

            _applyPalettesToBases.ForEach(vcb => vcb.LocalCustomPalette = _palette);
            _applyPalettesToPanels.ForEach(pnl => pnl.Palette = _palette);

            kryptonListView1.LocalCustomPalette = _palette;

            // Apply to refactored pages
            inputControls1.ApplyPalette(_palette);
            controlsPage1.ApplyPalette(_palette);
            gridPage1.ApplyPalette(_palette);
            trackBar1.ApplyPalette(_palette);
            menuPage1.ApplyPalette(_palette);
            toolTipsPage1.ApplyPalette(_palette);
            buttonsPage1.ApplyPalette(_palette);

            UpdateChromeTMS();

            // Optionally refresh the color table and property grid
            if (populateTable)
            {
                PopulateColorTableGrid();
                propertyGrid.Refresh();
            }
            else
            {
                // Force immediate repaint of all controls without repopulating grids
                Invalidate(true);
                Update();
            }
        }

        private void PopulateColorTableGrid()
        {
            if (_palette == null)
            {
                return;
            }

            Cursor originalCursor = Cursor;
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                colorTableGrid.SuspendLayout();
                colorTableGrid.Rows.Clear();
                _enumToGridRow.Clear();

                var enumValues = (SchemeBaseColors[])Enum.GetValues(typeof(SchemeBaseColors));
                foreach (var (eVal, idx) in enumValues.Select((v, i) => (v, i)))
                {
                    Color color = _palette.GetSchemeColor(eVal);

                    var colorStr = FormatColorString(color);
                    int row = colorTableGrid.Rows.Add(idx, eVal.ToString(), colorStr, "");
                    _enumToGridRow[eVal] = row;
                    colorTableGrid.Rows[row].Visible = RowPassesFilters(colorTableGrid.Rows[row]);

                    // Set color swatch in the new column
                    var swatchCell = colorTableGrid.Rows[row].Cells[3];
                    if (string.IsNullOrEmpty(colorStr))
                    {
                        color = Color.White;
                    }
                    swatchCell.Style.BackColor = color;
                    swatchCell.Style.ForeColor = color;
                    swatchCell.Style.SelectionBackColor = color;
                    swatchCell.Style.SelectionForeColor = color;
                }

                AutoResizeAndFixSwatchColumn();
                UpdateColorGridRowHeights();
                colorTableGrid.ResumeLayout();
            }
            finally
            {
                Cursor = originalCursor;
            }
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

        #endregion

        #region Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            WindowState = _settingsManager.GetMaximised() ? FormWindowState.Maximized : FormWindowState.Normal;

            // Load form starting position and bounding sizes
            if (!_settingsManager.GetMaximised())
            {
                var savedBounds = _settingsManager.GetWindowBounds();
                if (!savedBounds.IsEmpty)
                {
                    var adjusted = AdjustBoundsToVisibleScreens(savedBounds);
                    StartPosition = FormStartPosition.Manual;
                    Bounds = adjusted;
                }
            }

            // Restore splitter distances
            try
            {
                kryptonSplitContainerMain.SplitterDistance = _settingsManager.GetMainSplitterDistance();
            }
            catch { }
            try
            {
                kryptonSplitContainerProperties.SplitterDistance = _settingsManager.GetPropertiesSplitterDistance();
            }
            catch { }

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
            _chromeTMS2.ToolStripKryptonProgressBar.Enabled = false;
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
            kryptonNavigatorTop.SelectedPage = pageTopRibbon;
            kryptonNavigatorDesign.SelectedPage = pageDesignRibbon;

            // Process Image Viewer options
            if (_settingsManager.GetRestoreLastImageOnStartup())
            {
                string lastImg = _settingsManager.GetLastImagePath();
                if (!string.IsNullOrWhiteSpace(lastImg))
                {
                    if (File.Exists(lastImg))
                    {
                        imageViewerControl?.LoadImageFile(lastImg);
                    }
                    else
                    {
                        // Clear the setting if file no longer exists
                        _settingsManager.SetLastImagePath(string.Empty);
                    }
                }
            }

            if (_settingsManager.GetRestoreLastRegionsOnStartup())
            {
                string lastReg = _settingsManager.GetLastRegionStoragePath();
                if (!string.IsNullOrWhiteSpace(lastReg))
                {
                    if (File.Exists(lastReg))
                    {
                        imageViewerControl?.LoadRegionsFile(lastReg);
                    }
                    else
                    {
                        // Clear the setting if file no longer exists
                        _settingsManager.SetLastRegionStoragePath(string.Empty);
                    }
                }
            }
            // restore folder even if image path invalid
            string lastFolder = _settingsManager.GetLastImageFolder();
            if (imageViewerControl != null)
            {
                imageViewerControl.SetLastImageFolder(lastFolder);
            }

            CreateNewPalette();

            // Load palette mapping cache once at startup (no regeneration)
            PaletteMappingCache.Initialise(_palette!, regenerateIfMissing: false);

            // Restore fast filter text from settings
            string savedFilterText = _settingsManager.GetFastFilterText();
            if (!string.IsNullOrEmpty(savedFilterText))
            {
                fastFilterTextBox.Text = savedFilterText;
            }
            else
            {
                // Initialize filter UI state (without filtering rows during startup)
                UpdateFilterUI(false);
            }

            // Pre-cache enum-to-path mappings for faster color edits
            PrefillEnumToPath();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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
                        // Cancel the form closing
                        e.Cancel = true;
                        return;
                }
            }

            if (imageViewerControl != null && imageViewerControl.HasUnsavedRegionChanges)
            {
                var result = KryptonMessageBox.Show(this,
                    "Save changes to image regions?",
                    "Unsaved Regions",
                    KryptonMessageBoxButtons.YesNoCancel,
                    KryptonMessageBoxIcon.Warning);

                switch (result)
                {
                    case DialogResult.Yes:
                        if (!imageViewerControl.SaveRegions())
                        {
                            e.Cancel = true; // abort close if save failed or cancelled
                            return;
                        }
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }

            _settingsManager.SetMaximised(WindowState == FormWindowState.Maximized);
            var bounds = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;
            _settingsManager.SetWindowBounds(bounds);

            // Save splitter distances
            _settingsManager.SetMainSplitterDistance(kryptonSplitContainerMain.SplitterDistance);
            _settingsManager.SetPropertiesSplitterDistance(kryptonSplitContainerProperties.SplitterDistance);

            // Persist last file paths
            if (imageViewerControl != null)
            {
                _settingsManager.SetLastImagePath(imageViewerControl.LastImagePath ?? string.Empty);
                _settingsManager.SetLastRegionStoragePath(imageViewerControl.LastRegionPath ?? string.Empty);
                _settingsManager.SetLastImageFolder(imageViewerControl.LastImageFolder ?? string.Empty);
            }

            // Save fast filter text
            _settingsManager.SetFastFilterText(fastFilterTextBox.Text ?? string.Empty);

            // Save last used filter mode
            _settingsManager.SetFastFilterMode(filterByColorButton.Checked ? "0" : "1");

            _settingsManager.SaveSettings();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            KryptonManager.GlobalPaletteChanged -= OnGlobalPaletteChanged;
            base.OnFormClosed(e);
        }

        private void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
        {
            // Only interested the first time the palette is changed
            if (!_dirty)
            {
                _dirty = true;
                Task.Run(() => BeginInvoke(UpdateTitleBar));
            }

            // Do we need to setup a new renderer for the ToolMenuStatus page?
            if (e.NeedColorTable)
            {
                UpdateChromeTMS();
            }
        }

        private void MenuNew_Click(object sender, EventArgs e) => New();

        private void MenuNewFromDefault_Click(object? sender, EventArgs e)
        {
            using var dialog = new LoadDefaultPaletteDialog();
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            try
            {
                kryptonManager.GlobalPaletteMode = dialog.SelectedPaletteMode;

                // Start with a fresh palette that inherits from the selected global palette
                CreateNewPalette(useCurrentGlobalPalette: true);
                // For a new-from-default palette, copy all colors directly from the base palette.
                // Avoid grammar/path processing here to prevent transient wrong mappings.
                CopyColorsFromBasePalette();

                // Set state flags and filename
                _dirty = true;
                _loaded = false;
                _filename = "Copy of " + dialog.SelectedThemeName;

                // Define the initial title bar string
                UpdateTitleBar();

                // Colors already copied above

                ApplyPalette();
                ApplyQuickFilter(false);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

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
        // Grids page is now self-contained in Pages/GridPage
        // Removed legacy KryptonNavigatorDesignGrids_SelectedPageChanged handler; logic lives in Pages/GridPage

        private void LaunchPaletteUpgradeToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_paletteUpgradeToolForm == null || _paletteUpgradeToolForm.IsDisposed)
            {
                _paletteUpgradeToolForm = new FormPaletteUpgradeTool();
                _paletteUpgradeToolForm.FormClosed += (_, __) => _paletteUpgradeToolForm = null;
                _paletteUpgradeToolForm.Show(this);
            }
            else
            {
                if (_paletteUpgradeToolForm.Visible)
                {
                    _paletteUpgradeToolForm.Hide();
                }
                else
                {
                    _paletteUpgradeToolForm.Show();
                    _paletteUpgradeToolForm.BringToFront();
                }
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_settingsControlPanel == null || _settingsControlPanel.IsDisposed)
            {
                _settingsControlPanel = new SettingsControlPanel(kryptonManager);
                _settingsControlPanel.FormClosed += (_, __) => _settingsControlPanel = null;
                _settingsControlPanel.Show(this);
            }
            else
            {
                if (_settingsControlPanel.Visible)
                {
                    _settingsControlPanel.Hide();
                }
                else
                {
                    _settingsControlPanel.Show();
                    _settingsControlPanel.BringToFront();
                }
            }
        }

        private void OnGlobalPaletteChanged(object? sender, EventArgs e)
        {
            if (!ReferenceEquals(sender, kryptonManager))
            {
                return;
            }
            if (_palette == null)
            {
                return;
            }

            _palette.BasePalette = KryptonManager.CurrentGlobalPalette;

            ApplyPalette();
        }

        #endregion

        #region Implementation

        private void UpdateTitleBar() =>
            // Mark a changed file with a star
            Text = $@"Palette Designer - {_filename}{(_dirty ? "*" : string.Empty)}";

        private void CopyColorsFromBasePalette()
        {
            if (_palette?.BasePalette == null)
                return;

            try
            {
                _palette.SuspendUpdates();

                // We need to populate each section individually to avoid the HeaderForm null reference
                try { _palette.ButtonStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.CalendarDay.PopulateFromBase(); } catch { }
                try { _palette.ButtonSpecs.PopulateFromBase(); } catch { }
                try { _palette.ControlStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.ContextMenu.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.DragDrop.PopulateFromBase(); } catch { }
                try { _palette.FormStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.GridStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.HeaderStyles.PopulateFromBase(_palette.Common); } catch { } // This one throws
                try { _palette.HeaderGroup.PopulateFromBase(); } catch { }
                try { _palette.Images.PopulateFromBase(); } catch { }
                try { _palette.InputControlStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.LabelStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.Navigator.PopulateFromBase(); } catch { }
                try { _palette.PanelStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.Ribbon.PopulateFromBase(); } catch { }
                try { _palette.SeparatorStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.TabStyles.PopulateFromBase(_palette.Common); } catch { }
                try { _palette.TrackBar.PopulateFromBase(); } catch { }
                try { _palette.ToolMenuStatus.PopulateFromBase(); } catch { }
                try { _palette.CueHintText.PopulateFromBase(PaletteState.Normal); } catch { }
            }
            finally
            {
                _palette.ResumeUpdates();
            }
        }

        #endregion

        #region Recently Used

        private void MyOwnRecentPaletteFileGotClicked_Handler(object sender, EventArgs e)
        {
            var fileName = (sender as ToolStripItem)?.Text;
            if (string.IsNullOrEmpty(fileName) || _palette == null)
            {
                return;
            }

            if (!File.Exists(fileName!))
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
                var newPalette = new KryptonCustomPaletteBase();
                newPalette.Import(fileName!, false);

                InitializeLoadedPalette(newPalette, fileName!, addToRecentList: false);
            }
            catch
            {
                // https://github.com/Krypton-Suite/Theme-Palettes/issues/43
                // Do nothing as the MB will displayed due to silent = `false`
            }
        }

        private void MyOwnRecentPaletteFilesGotCleared_Handler(object sender, EventArgs e)
        {
            // ??
        }

        #endregion

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            // Map edited property back to SchemeBaseColors enum and apply it
            if (e.ChangedItem == null) return;
            string path = GetGridItemFullPath(e.ChangedItem);
            // Strip designer root prefixes that are not part of the palette object graph used by the mapper
            const string rootPrefix = "KryptonCustomPaletteBase.";
            if (path.StartsWith(rootPrefix, StringComparison.Ordinal))
            {
                path = path.Substring(rootPrefix.Length);
            }
            int visualsIdx = path.IndexOf("Visuals.", StringComparison.Ordinal);
            if (visualsIdx >= 0)
            {
                path = path.Substring(visualsIdx + "Visuals.".Length);
            }

            if (path == "BasePaletteMode")
            {
                kryptonManager.GlobalPaletteMode = (PaletteMode)e.ChangedItem.Value!;
            }

            var basePal = _palette?.BasePalette;
            if (basePal != null)
            {
                try
                {
                    SchemeBaseColors enumVal;
                    if (!PaletteMapper.MapPathToSchemeEnum(basePal, path, out enumVal))
                    {
                        // fallback: if we already have mapping stored for this path
                        enumVal = _enumToPath.FirstOrDefault(kvp => kvp.Value == path).Key;
                        if (!_enumToPath.ContainsKey(enumVal))
                        {
                            // Cannot map, abort
                            return;
                        }
                    }

                    // Determine the new color from the property grid change
                    object? valueObj = e.ChangedItem?.Value;
                    Color newColor;
                    if (valueObj is Color col)
                    {
                        newColor = col;
                    }
                    else if (valueObj is string colValue)
                    {
                        newColor = ColorTranslator.FromHtml(colValue);
                    }
                    else
                    {
                        newColor = _palette?.GetSchemeColor(enumVal) ?? Color.Transparent;
                    }

                    _undoStack.Push((enumVal, GetSchemeColorSafe(enumVal)));
                    _palette?.SetSchemeColor(enumVal, newColor);
                }
                catch (Exception exc2)
                {
                    Debug.WriteLine(exc2.Message);
                }
            }
            // Push latest base-palette colors into override properties
            //This populates all the properties; for that, there's already a "New from Default Theme" option
            //CopyColorsFromBasePalette();
            // Rebind grid to pick up override values (single assignment to preserve state)
            propertyGrid.SelectedObject = _palette;
            // Refresh the grid to show updated overrides without collapsing nodes
            propertyGrid.Refresh();
            // Apply the palette to the design controls
            ApplyPalette(populateTable: false);
        }

        private void ColorTableGrid_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBoxBase tb)
            {
                tb.ReadOnly = true;
            }
        }

        private void ColorTableGrid_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                e.Handled = true;
                EditCurrentCellColor();
                return;
            }
        }

        private void ColorTableGrid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
            => EditCurrentCellColor();

        private void EditCurrentCellColor()
        {
            int rowIndex = colorTableGrid.CurrentCell?.RowIndex ?? -1;
            // Only skip column-header clicks: RowIndex == -1 indicates header.
            // RowIndex == 0 is the first data row, so we don't use <1.
            if (rowIndex < 0 || rowIndex >= colorTableGrid.Rows.Count)
            {
                return;
            }

            var enumVal = (SchemeBaseColors)colorTableGrid.Rows[rowIndex].Cells[0].Value!;

            // Get current color
            Color current = GetSchemeColorSafe(enumVal);

            using var dlg = new LiveColorPickerDialog { Color = current };
            dlg.LiveColorChanged += (_, args) =>
            {
                _undoStack.Push((enumVal, current));
                _palette?.SetSchemeColor(enumVal, args.Color);
                // Ensure the grid reflects live updates even if external listeners don't propagate
                UpdateGridRow(enumVal, args.Color);
                propertyGrid.Refresh();
            };

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _undoStack.Push((enumVal, current));
                _palette?.SetSchemeColor(enumVal, dlg.Color);
                // Explicitly refresh the grid in case the palette event was not received
                UpdateGridRow(enumVal, dlg.Color);
            }

            // Ensure the property grid is updated to reflect the new color
            propertyGrid.Refresh();
        }

        private Color GetSchemeColorSafe(SchemeBaseColors val)
        {
            return _palette?.GetSchemeColor(val) ?? Color.Transparent;
        }

        private void UpdateGridRow(int rowIndex, Color color)
        {
            if (rowIndex < 0 || rowIndex >= colorTableGrid.Rows.Count)
            {
                return;
            }
            var row = colorTableGrid.Rows[rowIndex];
            ApplyColorToRow(row, color);
            colorTableGrid.InvalidateRow(rowIndex);
            colorTableGrid.Refresh();
        }

        // Overload that resolves the row index via the enum value look-up table
        private void UpdateGridRow(SchemeBaseColors enumVal, Color color)
        {
            if (_enumToGridRow.TryGetValue(enumVal, out int rowIndex))
            {
                UpdateGridRow(rowIndex, color);
            }
            else
            {
                // Fallback – linear search (should be rare)
                foreach (DataGridViewRow row in colorTableGrid.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells.Count > 1 && row.Cells[1].Value?.ToString() == enumVal.ToString())
                    {
                        ApplyColorToRow(row, color);
                        colorTableGrid.InvalidateRow(row.Index);
                        break;
                    }
                }
            }
        }

        private void ApplyColorToRow(DataGridViewRow row, Color color)
        {
            var colorStr = FormatColorString(color);
            if (string.IsNullOrEmpty(colorStr))
            {
                color = Color.White;
            }
            row.Cells[2].Value = colorStr;
            row.Cells[3].Style.BackColor = color;
            row.Cells[3].Style.ForeColor = color;
            row.Cells[3].Style.SelectionBackColor = color;
            row.Cells[3].Style.SelectionForeColor = color;
        }

        private string FormatColorString(Color c)
        {
            // Treat empty or fully transparent colors as no value
            if (c.IsEmpty || c.A == 0)
                return string.Empty;
            return _displayRgb
                ? $"{c.R};{c.G};{c.B}"
                : $"#{c.R:X2}{c.G:X2}{c.B:X2}";
        }

        private void ToggleColorFormat()
        {
            _displayRgb = !_displayRgb;
            UpdateFormatMenuItems();

            Cursor originalCursor = Cursor;
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                colorTableGrid.SuspendLayout();
                try
                {
                    foreach (DataGridViewRow row in colorTableGrid.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        // Use the swatch cell (column 3) to get the actual color
                        var col = row.Cells[3].Style.BackColor;
                        row.Cells[2].Value = FormatColorString(col);
                    }
                }
                finally
                {
                    colorTableGrid.ResumeLayout();
                    colorTableGrid.Refresh();
                }
            }
            finally
            {
                Cursor = originalCursor;
            }
        }

        private void UpdateFormatMenuItems()
        {
            string newText = _displayRgb ? "Show Hex Values" : "Show RGB Values";
            bool isShowingHex = !_displayRgb;

            if (_toggleFormatMenuItem != null)
            {
                _toggleFormatMenuItem.Text = newText;
                _toggleFormatMenuItem.Checked = isShowingHex;
            }

            if (toggleFormatToolStripMenuItem != null)
            {
                toggleFormatToolStripMenuItem.Text = newText;
                toggleFormatToolStripMenuItem.Checked = isShowingHex;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // If focus is on filter text box, let it handle its own keyboard input (including clipboard)
            if (fastFilterTextBox.Focused)
            {
                if (keyData == (Keys.Control | Keys.F))
                {
                    SearchForColor();
                    return true;
                }

                if (keyData == (Keys.Control | Keys.Shift | Keys.C))
                {
                    FilterRowsByColor();
                    return true;
                }

                if (keyData == (Keys.Control | Keys.Shift | Keys.F))
                {
                    FilterRowsByEnumSubstring();
                    return true;
                }

                if (keyData == (Keys.Control | Keys.Shift | Keys.R))
                {
                    ClearRowFilter();
                    return true;
                }

                // Default to allow the textbox to process other keys (Ctrl+X/C/V, etc.)
                return false;
            }

            if (keyData == (Keys.Control | Keys.Z))
            {
                if (_undoStack.Count > 0)
                {
                    var (enumVal, oldColor) = _undoStack.Pop();
                    _palette?.SetSchemeColor(enumVal, oldColor);
                    // Find row index
                    int idx = (int)enumVal;
                    UpdateGridRow(idx, oldColor);
                    // Ensure the property grid is updated to reflect the new color
                    propertyGrid.Refresh();
                }
                return true;
            }

            // Handle copy in color table grid
            if (keyData == (Keys.Control | Keys.C) && colorTableGrid.ContainsFocus)
            {
                if (colorTableGrid.CurrentCell is { } cell)
                {
                    var text = cell.FormattedValue?.ToString() ?? cell.Value?.ToString();
                    if (!string.IsNullOrEmpty(text))
                    {
                        TryCopyToClipboard(text!);
                        return true;
                    }
                }
            }

            // Handle paste in color table grid
            if (keyData == (Keys.Control | Keys.V) && colorTableGrid.ContainsFocus)
            {
                string? clipText = System.Windows.Forms.Clipboard.GetText();
                if (!string.IsNullOrWhiteSpace(clipText) && TryParseColorString(clipText, out var pastedColor))
                {
                    if (colorTableGrid.CurrentCell != null && colorTableGrid.CurrentCell.RowIndex >= 0)
                    {
                        int rowIndex = colorTableGrid.CurrentCell.RowIndex;
                        var enumVal = (SchemeBaseColors)colorTableGrid.Rows[rowIndex].Cells[0].Value!;

                        // push undo
                        _undoStack.Push((enumVal, GetSchemeColorSafe(enumVal)));

                        _palette?.SetSchemeColor(enumVal, pastedColor);
                        UpdateGridRow(rowIndex, pastedColor);
                        // Ensure the property grid is updated to reflect the new color
                        propertyGrid.Refresh();
                    }
                }
                return true;
            }

            // Handle copy/paste in property grid (color properties)
            bool propertyGridActive = propertyGrid.ContainsFocus;
            if (propertyGridActive)
            {
                var selItem = propertyGrid.SelectedGridItem;
                if (selItem?.PropertyDescriptor != null && selItem.PropertyDescriptor.PropertyType == typeof(Color))
                {
                    if (keyData == (Keys.Control | Keys.C))
                    {
                        if (selItem.Value is Color cVal)
                        {
                            TryCopyToClipboard($"#{cVal.R:X2}{cVal.G:X2}{cVal.B:X2}");
                        }
                        return true;
                    }
                    if (keyData == (Keys.Control | Keys.V))
                    {
                        string clip = System.Windows.Forms.Clipboard.GetText();
                        if (TryParseColorString(clip, out var newCol))
                        {
                            try
                            {
                                selItem.PropertyDescriptor.SetValue((selItem.Parent?.Value ?? propertyGrid.SelectedObject)!, newCol);
                                propertyGrid.Refresh();
                                ApplyPalette();
                            }
                            catch { /* ignore failures */ }
                        }
                        return true;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Context Menu & Filters

        private void SetupContextMenu()
        {
            _contextMenu = new ContextMenuStrip();

            var miSearch = new ToolStripMenuItem("Search Color...")
            {
                ShortcutKeys = Keys.Control | Keys.F
            };
            miSearch.Click += (_, __) => SearchForColor();
            _contextMenu.Items.Add(miSearch);

            var miFilterCol = new ToolStripMenuItem("Filter by Color...")
            {
                ShortcutKeys = Keys.Control | Keys.Shift | Keys.C
            };
            miFilterCol.Click += (_, __) => FilterRowsByColor();
            _contextMenu.Items.Add(miFilterCol);

            var miFilterName = new ToolStripMenuItem("Filter by Name...")
            {
                ShortcutKeys = Keys.Control | Keys.Shift | Keys.F
            };
            miFilterName.Click += (_, __) => FilterRowsByEnumSubstring();
            _contextMenu.Items.Add(miFilterName);

            _contextMenu.Items.Add(new ToolStripSeparator());

            _toggleFormatMenuItem = new ToolStripMenuItem("Show Hex Values") { CheckOnClick = true };
            _toggleFormatMenuItem.Click += (_, __) => ToggleColorFormat();
            _contextMenu.Items.Add(_toggleFormatMenuItem);

            _autoFillMenuItem = new ToolStripMenuItem("Auto-Fill Color in Grid") { CheckOnClick = true, Checked = false };
            _autoFillMenuItem.CheckedChanged += (_, __) =>
            {
                _autoFillFromViewer = _autoFillMenuItem.Checked;
                UpdateAutoFillMenuItems();
            };
            _contextMenu.Items.Add(_autoFillMenuItem);

            _contextMenu.Items.Add(new ToolStripSeparator());

            var miReset = new ToolStripMenuItem("Reset Filters")
            {
                ShortcutKeys = Keys.Control | Keys.Shift | Keys.R
            };
            miReset.Click += (_, __) => ClearRowFilter();
            _contextMenu.Items.Add(miReset);

            _contextMenu.Items.Add(new ToolStripSeparator());

            var miExportCsv = new ToolStripMenuItem("Export as CSV");
            miExportCsv.Click += (_, __) => ExportPaletteAsCsv();
            _contextMenu.Items.Add(miExportCsv);

            var miExportClass = new ToolStripMenuItem("Export as Class");
            miExportClass.Click += (_, __) => ExportPaletteAsClass();
            _contextMenu.Items.Add(miExportClass);

            colorTableGrid.ContextMenuStrip = _contextMenu;
        }

        private void SearchForColor()
        {
            string defaultValue = "#";
            if (_activeColorFilter.HasValue)
            {
                var c = _activeColorFilter.Value;
                defaultValue = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
            }

            string? input = KryptonInputBox.Show(new KryptonInputBoxData
            {
                Prompt = "Enter color to search (e.g. #FF0000 or 255,0,0):",
                Caption = "Search Color",
                DefaultResponse = defaultValue
            });

            if (!TryParseColorString(input, out var target))
            {
                return;
            }

            int startRow = colorTableGrid.CurrentCell?.RowIndex ?? 0;
            int totalRows = colorTableGrid.Rows.Count;

            for (int i = 1; i <= totalRows; i++)
            {
                int idx = (startRow + i) % totalRows;
                var cell = colorTableGrid.Rows[idx].Cells[2];
                if (cell.Style.BackColor.ToArgb() == target.ToArgb())
                {
                    colorTableGrid.CurrentCell = cell;
                    return;
                }
            }
            KryptonMessageBox.Show(this, "Color not found.", "Search",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }

        private void FilterRowsByColor()
        {
            string defaultValue = "#";
            if (_activeColorFilter.HasValue)
            {
                var c = _activeColorFilter.Value;
                defaultValue = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
            }
            else if (!string.IsNullOrWhiteSpace(_lastColorFilterInput))
            {
                defaultValue = _lastColorFilterInput!;
            }

            string? input = KryptonInputBox.Show(new KryptonInputBoxData
            {
                Prompt = "Enter color to filter by (e.g. #FF0000 or 255,0,0):",
                Caption = "Filter Rows – Color",
                DefaultResponse = defaultValue
            });

            if (!TryParseColorString(input, out var target))
            {
                return;
            }

            _activeColorFilter = target;
            _lastColorFilterInput = input;
            UpdateFilterUI();
        }

        private void FilterRowsByEnumSubstring()
        {
            string? keyword = KryptonInputBox.Show(new KryptonInputBoxData
            {
                Prompt = "Enter text to filter Scheme Colors (contains, case-insensitive):",
                Caption = "Filter Rows – SchemeBaseColors",
                DefaultResponse = _activeNameFilter ?? string.Empty
            });

            if (string.IsNullOrWhiteSpace(keyword))
            {
                ClearRowFilter();
                return;
            }

            _activeNameFilter = keyword!.Trim();
            UpdateFilterUI();
        }

        private void ClearRowFilter()
        {
            _activeColorFilter = null;
            _activeNameFilter = null;
            if (fastFilterTextBox != null && fastFilterTextBox.TextLength > 0)
            {
                fastFilterTextBox.Text = string.Empty;
            }
            UpdateFilterUI();
        }

        private void FastFilterTextBox_TextChanged(object? sender, EventArgs e)
        {
            ApplyQuickFilter();
        }

        // Restrict filter textbox input based on format: digits for RGB or hex chars for Hex mode
        private void FastFilterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Always allow control keys
            if (char.IsControl(e.KeyChar))
                return;

            if (filterByNameButton.Checked)
            {
                // Name filter: allow only letters
                if (char.IsLetter(e.KeyChar))
                {
                    return;
                }
                e.Handled = true;
                return;
            }

            // Color filter mode: allow letters (for named colors), digits, '#', ';', ','
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToLowerInvariant(e.KeyChar);
                return;
            }
            if (char.IsDigit(e.KeyChar) || e.KeyChar == ';' || e.KeyChar == ',')
                return;
            if (e.KeyChar == '#')
            {
                // Only allow '#' as first char and only one occurrence
                if (fastFilterTextBox.SelectionStart == 0 && !fastFilterTextBox.Text.Contains('#'))
                    return;
                e.Handled = true;
                return;
            }

            // Disallow everything else
            e.Handled = true;
        }

        private void FilterModeButton_CheckedChanged(object? sender, EventArgs e)
        {
            ApplyQuickFilter();
        }

        private void KryptonCheckSetQuickFilterButtons_CheckedButtonChanged(object sender, EventArgs e)
        {
            ApplyQuickFilter();
        }

        private void ApplyQuickFilter(bool clearOnEmpty = true)
        {
            string input = fastFilterTextBox.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                if (clearOnEmpty)
                {
                    ClearRowFilter();
                }
                return;
            }

            if (filterByColorButton.Checked)
            {
                // Try exact color parse: if succeeds, set active color, otherwise clear it (will fallback to substring match)
                if (TryParseColorString(input, out Color parsed))
                {
                    _activeColorFilter = parsed;
                    _lastColorFilterInput = input;
                }
                else
                {
                    _activeColorFilter = null;
                    _lastColorFilterInput = input;
                }
                _activeNameFilter = null;
            }
            else
            {
                _activeColorFilter = null;
                _activeNameFilter = input.Trim();
            }

            UpdateFilterUI();
        }

        private void UpdateRowVisibility()
        {
            Cursor originalCursor = Cursor;
            try
            {
                Cursor = Cursors.WaitCursor;

                // save the autosize state and disable it.
                // this stops the cells from redrawing over and over again
                colorTableGrid.SuspendLayout();

                var autoSizeRows = colorTableGrid.AutoSizeRowsMode;
                var autoSizeCols = colorTableGrid.AutoSizeColumnsMode;

                colorTableGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                colorTableGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                try
                {
                    // clean all spaces from the textbox before we begin
                    fastFilterTextBox.Text = fastFilterTextBox.Text.Replace(" ", string.Empty);

                    // if the filterbox is empty, all rows will be visible
                    if (fastFilterTextBox.Text.Length == 0
                        && colorTableGrid.Rows.Count != colorTableGrid.DisplayedRowCount(true))
                    {
                        foreach (DataGridViewRow row in colorTableGrid.Rows)
                        {
                            row.Visible = true;
                        }
                    }
                    else
                    {
                        // filter by name or color
                        if (filterByNameButton.Checked || filterByColorButton.Checked)
                        {
                            foreach (DataGridViewRow row in colorTableGrid.Rows)
                            {
                                if (row.IsNewRow)
                                {
                                    continue;
                                }

                                row.Visible = RowPassesFilters(row);
                            }
                        }
                    }
                }
                finally
                {
                    colorTableGrid.ResumeLayout(false);

                    // restore autosizing
                    colorTableGrid.AutoSizeRowsMode = autoSizeRows;
                    colorTableGrid.AutoSizeColumnsMode = autoSizeCols;
                }
            }
            finally
            {
                Cursor = originalCursor;
            }
        }

        // Helper for color-based filtering: tries parse, then exact match on color or substring match on text
        private bool MatchesColorFilter(string input, DataGridViewRow row)
        {
            // Skip rows with no displayed color text (e.g., transparent)
            var cellText = row.Cells[2].Value?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(cellText))
                return false;

            // If input is a full RGB or hex spec, parse and compare against the swatch
            if (_regexFilterBoxFontColor.IsMatch(input) ||
                _regexFilterBoxHexColor.IsMatch(input))
            {
                if (TryParseColorString(input, out Color targetColor))
                {
                    var rowColor = row.Cells[3].Style.BackColor;
                    if (rowColor.ToArgb() == targetColor.ToArgb())
                    {
                        return true;
                    }
                    // Fallback: allow textual "starts with" match when the exact parsed color does not match
                    // This keeps incremental typing (e.g., "30;57;9") behaving intuitively.
                    return cellText.StartsWith(input, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            }

            // Otherwise fallback to textual "starts with" matching on cell text
            return cellText.StartsWith(input, StringComparison.OrdinalIgnoreCase);
        }

        private bool RowPassesFilters(DataGridViewRow row)
        {
            var input = fastFilterTextBox.Text.Trim();
            if (filterByNameButton.Checked)
            {
                var s = row.Cells[1].Value?.ToString() ?? string.Empty;
                return s.Length > 0 &&
                       s.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            // Default to color filter if not name filter
            filterByColorButton.Checked = true;
            return MatchesColorFilter(input, row);
        }

        private void AdjustGridFont(float delta)
        {
            float newSize = Math.Max(MinFontSize, Math.Min(propertyGrid.Font.Size + delta, MaxFontSize));
            if (Math.Abs(newSize - propertyGrid.Font.Size) < 0.1f)
            {
                return; // No visible change required
            }

            // Update fonts for property grids
            var newPropFont = new Font(propertyGrid.Font.FontFamily, newSize, propertyGrid.Font.Style);
            propertyGrid.Font = newPropFont;

            // Update fonts for color table grid
            var newGridFont = new Font(colorTableGrid.Font.FontFamily, newSize, colorTableGrid.Font.Style);
            colorTableGrid.Font = newGridFont;
            colorTableGrid.ColumnHeadersDefaultCellStyle.Font = newGridFont;
            colorTableGrid.DefaultCellStyle.Font = newGridFont;

            UpdateColorGridRowHeights();

            AutoResizeAndFixSwatchColumn();
            colorTableGrid.Refresh();

            _settingsManager.SetPropertyGridFontSize(newSize);
        }

        private void UpdateColorGridRowHeights()
        {
            int newHeight = TextRenderer.MeasureText("Ag", colorTableGrid.Font).Height + 6;

            Cursor originalCursor = Cursor;
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                colorTableGrid.SuspendLayout();
                try
                {
                    colorTableGrid.RowTemplate.Height = newHeight;
                    foreach (DataGridViewRow dgvr in colorTableGrid.Rows)
                    {
                        dgvr.Height = newHeight;
                    }
                }
                finally
                {
                    colorTableGrid.ResumeLayout();
                }
            }
            finally
            {
                Cursor = originalCursor;
            }
        }

        private void ShowHelpHint()
        {
            const string body = "F6: Edit cell color\nCtrl+C / Ctrl+V: Copy / Paste color\nCtrl+F: Search color\nCtrl+Shift+C: Filter by color\nCtrl+Shift+F: Filter by name\nCtrl+Shift+R: Reset filters\nRight-click grid for context menu (toggle Auto-Fill Color)\n+ / –: Adjust font size";
            KryptonMessageBox.Show(this, body, "Palette Designer – Shortcuts",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
        }

        private void ExportPaletteAsCsv()
        {
            string baseName = Path.GetFileNameWithoutExtension(_filename);
            if (string.IsNullOrWhiteSpace(baseName) || baseName.StartsWith("(", StringComparison.Ordinal))
            {
                baseName = "CustomPalette";
            }

            using var dlg = new KryptonSaveFileDialog
            {
                DefaultExt = "csv",
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Export Palette as CSV",
                FileName = IdentifierUtilities.SanitizeIdentifier(baseName) + ".csv"
            };

            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            Cursor originalCursor = Cursor;
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                using var sw = new StreamWriter(dlg.FileName, false, Encoding.UTF8);
                sw.WriteLine("Id,Value,Name");

                foreach (DataGridViewRow row in colorTableGrid.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    var id = row.Cells[0].Value;
                    var name = row.Cells[1].Value;
                    var col = row.Cells[2].Style.BackColor;
                    string value = $"#{col.R:X2}{col.G:X2}{col.B:X2}";
                    sw.WriteLine($"{id},{value},{name}");
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(this, $"Failed to export CSV.\n{ex.Message}", "Error",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
            finally
            {
                Cursor = originalCursor;
            }
        }

        private void ExportPaletteAsClass()
        {
            if (_palette is null)
            {
                return;
            }

            string baseName = Path.GetFileNameWithoutExtension(_filename);
            if (string.IsNullOrWhiteSpace(baseName) || baseName.StartsWith("(", StringComparison.Ordinal))
            {
                baseName = "CustomPalette";
            }

            string className = IdentifierUtilities.SanitizeIdentifier(baseName) + "_BaseScheme";

            using var expDlg = new ExportBaseSchemeClass(_palette, className, _settingsManager);
            expDlg.ShowDialog(this);
        }

        #endregion

        #region ButtonSpec Event Handlers

        private void ButtonSpecIncFont_Click(object? sender, EventArgs e) => AdjustGridFont(1f);

        private void ButtonSpecDecFont_Click(object? sender, EventArgs e) => AdjustGridFont(-1f);

        private void ButtonSpecHelp_Click(object? sender, EventArgs e) => ShowHelpHint();

        #endregion

        private void ImageViewerControl_ColorSampled(object? sender, ColorSampledEventArgs e)
        {
            // Copy to clipboard in #AARRGGBB
            string hex = $"#{e.Color.A:X2}{e.Color.R:X2}{e.Color.G:X2}{e.Color.B:X2}";
            TryCopyToClipboard(hex);

            if (_autoFillFromViewer && colorTableGrid.CurrentCell is { } cell)
            {
                int rowIndex = cell.RowIndex;
                if (rowIndex >= 0 && rowIndex < colorTableGrid.Rows.Count)
                {
                    var enumVal = (SchemeBaseColors)colorTableGrid.Rows[rowIndex].Cells[0].Value!;
                    _undoStack.Push((enumVal, GetSchemeColorSafe(enumVal)));
                    _palette?.SetSchemeColor(enumVal, e.Color);
                    UpdateGridRow(rowIndex, e.Color);
                    // Ensure the property grid is updated to reflect the new color
                    propertyGrid.Refresh();
                }
            }
        }

        #region ToolStrip Event Handlers

        private void SearchColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForColor();
        }

        private void FilterByColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterRowsByColor();
        }

        private void FilterByNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterRowsByEnumSubstring();
        }

        private void ResetFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearRowFilter();
        }

        private void ExportAsCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportPaletteAsCsv();
        }

        private void ExportAsClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportPaletteAsClass();
        }

        private void ToggleFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleColorFormat();
        }

        private void AutoFillToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                _autoFillFromViewer = menuItem.Checked;
                UpdateAutoFillMenuItems();
            }
        }

        private void UpdateAutoFillMenuItems()
        {
            if (_autoFillMenuItem != null)
            {
                _autoFillMenuItem.Checked = _autoFillFromViewer;
            }

            if (autoFillToolStripMenuItem != null)
            {
                autoFillToolStripMenuItem.Checked = _autoFillFromViewer;
            }
        }

        private void ColorFilterBtn_Click(object? sender, EventArgs e)
        {
            Color initial = Color.White;
            if (colorTableGrid.CurrentCell != null && colorTableGrid.CurrentCell.RowIndex >= 0)
            {
                int rowIndex = colorTableGrid.CurrentCell.RowIndex;
                if (rowIndex >= 0 && rowIndex < colorTableGrid.Rows.Count)
                {
                    initial = colorTableGrid.Rows[rowIndex].Cells[3].Style.BackColor;
                }
            }
            else if (_activeColorFilter.HasValue)
            {
                initial = _activeColorFilter.Value;
            }

            using (var dlg = new LiveColorPickerDialog { Color = initial })
            {
                dlg.ShowExtraControls = false;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var c = dlg.Color;
                    filterByColorButton.Checked = true;
                    fastFilterTextBox.Text = string.Format("{0};{1};{2}", c.R, c.G, c.B);
                }
            }
        }

        /// <summary>
        /// Rebuilds the scheme-array by pulling colours from the full palette
        /// object graph using the pre-computed enum→path mapping.
        ///
        /// Performance notes (August 2025 optimisation)
        /// -------------------------------------------
        /// • Uses <see cref="PaletteMapper.GetColorFast"/> – a compiled-delegate
        ///   cache – to fetch each colour. First palette load builds 239
        ///   delegates (≈ 40 ms); subsequent loads are ~30 µs in total.
        /// • No longer calls <c>ApplyScheme</c>; therefore avoids 239
        ///   <c>SchemeColorChanged</c> events and the associated 10-second UI
        ///   stall that occurred previously.
        /// • Still displays a modal progress dialog so the user has visual
        ///   feedback when opening extremely large or remote palettes, but the
        ///   dialog now disappears almost instantly.
        /// </summary>
        private void RefreshSchemeFromOverrides()
        {
            if (_palette == null)
            {
                return;
            }

            // Suspend palette notifications and grid redraw for performance
            _palette.SuspendUpdates();
            SetRedraw(colorTableGrid, false);

            var enumValues = (SchemeBaseColors[])Enum.GetValues(typeof(SchemeBaseColors));
            // Snapshot all file-loaded overrides so we can preserve them
            var loadedOverrides = enumValues.ToDictionary(ev => ev, ev => _palette.GetSchemeColor(ev));

            using (var waitDlg = new ModalWaitDialog(true, 0, enumValues.Length))
            {
                // Manually center the wait dialog over the main form
                waitDlg.StartPosition = FormStartPosition.Manual;
                // Update descriptive message via reflection to avoid modifying toolkit
                var lblField = typeof(ModalWaitDialog).GetField("labelMessage",
                        global::System.Reflection.BindingFlags.Instance
                        | global::System.Reflection.BindingFlags.NonPublic);
                if (lblField?.GetValue(waitDlg) is KryptonLabel lbl)
                {
                    lbl.Text = "Processing color mappings, please wait...";
                }
                waitDlg.Show(this);
                waitDlg.Location = new Point(
                    this.Left + (this.Width - waitDlg.Width) / 2,
                    this.Top + (this.Height - waitDlg.Height) / 2);
                waitDlg.UpdateDialog();

                for (int idx = 0; idx < enumValues.Length; idx++)
                {
                    var enumVal = enumValues[idx];
                    // Determine mapped path, if any
                    string? path = PaletteMapper.ResolvePath(_palette, enumVal);
                    if (path == null && PaletteMapper.TryGetManualPath(enumVal.ToString(), out var manual))
                        path = manual;

                    // File-loaded override for this enum
                    Color fileValue = loadedOverrides[enumVal];
                    Color final;
                    if (path != null)
                    {
                        try
                        {
                            // Normalize for fast lookup
                            string fastPath = path.StartsWith("ButtonStyles.Standalone.", StringComparison.Ordinal)
                                ? "ButtonStyles.ButtonStandalone." + path.Substring("ButtonStyles.Standalone.".Length)
                                : path;
                            // Register mapping in cache
                            PaletteMappingCache.Add(enumVal.ToString(), fastPath);
                            // Preserve file override if it differs from base palette, else fetch from graph or base palette
                            Color baseValue = _palette.BasePalette != null
                                ? _palette.BasePalette.GetSchemeColor(enumVal)
                                : fileValue;
                            if (fileValue != baseValue)
                            {
                                final = fileValue;
                            }
                            else
                            {
                                // Get via compiled delegate
                                Color fast = PaletteMapper.GetColorFast(_palette, fastPath);
                                // If empty, prefer the base palette when available, otherwise keep the file-provided value
                                if (fast.ToArgb() == 0)
                                {
                                    // Preserve the file-provided override when we cannot resolve a fast path
                                    final = fileValue;
                                }
                                else
                                {
                                    final = fast;
                                }
                            }
                        }
                        catch
                        {
                            // On error, keep file override
                            final = fileValue;
                        }
                    }
                    else
                    {
                        // No mapping; keep the file-provided override value
                        final = fileValue;
                    }
                    _palette.SetSchemeColor(enumVal, final);
                    // Update progress bar deterministically
                    waitDlg.UpdateProgressBarValue(idx + 1);
                    waitDlg.UpdateDialog();
                }

                waitDlg.Close();
            }

            // Resume updates and redraw UI
            _palette.ResumeUpdates();
            SetRedraw(colorTableGrid, true);

            // Rebuild the grid so the freshly-set scheme values appear
            PopulateColorTableGrid();
            // colorTableGrid.Invalidate();
            ApplyPalette(populateTable: false);
        }

        #endregion

        #region Static Helpers

        private static Color GetContrastColor(Color c)
        {
            double luminance = (0.299 * c.R + 0.587 * c.G + 0.114 * c.B) / 255;
            return luminance > 0.5 ? Color.Black : Color.White;
        }

        /// <summary>
        /// Ensures window bounds are entirely or partially visible on at least one connected screen.
        /// If not, it re-centers the window within the primary screen and adjusts size if necessary.
        /// </summary>
        private static Rectangle AdjustBoundsToVisibleScreens(Rectangle bounds) =>
            WindowBoundsHelper.AdjustBoundsToVisibleScreens(bounds);

        private static void TryCopyToClipboard(string text)
        {
            const int retries = 5;
            const int delay = 100;
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    Clipboard.SetDataObject(text, true);
                    return;
                }
                catch (ExternalException)
                {
                    System.Threading.Thread.Sleep(delay);
                }
            }
        }

        private static bool TryParseColorString(string? input, out Color color)
        {
            color = Color.Empty;
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            input = input!.Trim();
            input = input.Replace(" ", string.Empty);
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            // Hex with #
            if (input.StartsWith("#", StringComparison.Ordinal))
            {
                try
                {
                    color = ColorTranslator.FromHtml(input);
                    return true;
                }
                catch { }
            }

            // Hex without #
            if (input != null && (input.Length == 6 || input.Length == 8))
            {
                try
                {
                    color = ColorTranslator.FromHtml("#" + input);
                    return true;
                }
                catch { }
            }

            // RGB triplet
            if (input != null)
            {
                var parts = input.Split([',', ';'], StringSplitOptions.None);
                if (parts.Length == 3 &&
                    byte.TryParse(parts[0].Trim(), out byte r) &&
                    byte.TryParse(parts[1].Trim(), out byte g) &&
                    byte.TryParse(parts[2].Trim(), out byte b))
                {
                    color = Color.FromArgb(r, g, b);
                    return true;
                }
            }

            // Named color
            if (input != null)
            {
                var named = Color.FromName(input);
                if (named.IsKnownColor || named.IsNamedColor)
                {
                    color = named;
                    return true;
                }
            }

            return false;
        }

        private static void SetRedraw(Control control, bool redraw)
        {
            const int WM_SETREDRAW = 0x000B;
            SendMessage(control.Handle, WM_SETREDRAW, redraw ? 1 : 0, IntPtr.Zero);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        private void UpdateFilterUI(bool filterRows = true)
        {
            bool hasActiveFilter = _activeColorFilter.HasValue || !string.IsNullOrWhiteSpace(_activeNameFilter);
            filterToolStripDropDownButton.Text = hasActiveFilter ? "Filter*" : "Filter";

            filterByColorToolStripMenuItem.Checked = _activeColorFilter.HasValue;
            filterByNameToolStripMenuItem.Checked = !string.IsNullOrWhiteSpace(_activeNameFilter);

            if (filterRows)
            {
                UpdateRowVisibility();
            }
        }

        /// <summary>
        /// Cache all enum-to-path mappings upfront for faster look-ups.
        /// </summary>
        private void PrefillEnumToPath()
        {
            if (_palette == null)
            {
                return;
            }

            foreach (SchemeBaseColors val in Enum.GetValues(typeof(SchemeBaseColors)))
            {
                if (_enumToPath.ContainsKey(val))
                {
                    continue;
                }

                string? path = PaletteMapper.ResolvePath(_palette, val);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    _enumToPath[val] = path!;
                }
            }
        }
        #endregion

        // Helper to reconstruct full property path from a GridItem
        private string GetGridItemFullPath(GridItem item)
        {
            var labels = new Stack<string>();
            while (item != null)
            {
                if (item.Label != null)
                    labels.Push(item.Label);
                item = item.Parent!;
            }
            return string.Join(".", labels);
        }

        // Helper to resize columns and fix the swatch column width
        private void AutoResizeAndFixSwatchColumn()
        {
            colorTableGrid.AutoResizeColumns();
            var swatchCol = colorTableGrid.Columns[3];
            swatchCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            swatchCol.Width = 60;
        }

        private void OnPaletteSchemeColorChanged(object? sender, SchemeColorChangedEventArgs e)
        {
            // Update the grid row corresponding to the changed color
            UpdateGridRow(e.Index, e.NewColor);

            // Keep override property in sync so the PropertyGrid reflects the change
            if (_palette != null)
            {
                // Try cached mapping first
                if (!_enumToPath.TryGetValue(e.Index, out string? path) || string.IsNullOrWhiteSpace(path))
                {
                    // Resolve and cache the path if not yet known
                    path = PaletteMapper.ResolvePath(_palette, e.Index);
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        _enumToPath[e.Index] = path!;
                    }
                }

                if (!string.IsNullOrWhiteSpace(path))
                {
                    PaletteMapper.SetColorByPath(_palette, path!, e.NewColor);
                }
            }

            // Refresh the property grid so new base value shows through
            propertyGrid.Refresh();

            // Apply the palette to the design controls without repopulating the table
            ApplyPalette(populateTable: false);
        }
    }
}
