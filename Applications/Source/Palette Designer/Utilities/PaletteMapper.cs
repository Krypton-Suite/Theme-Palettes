#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

using System.Text.RegularExpressions;
using System.Linq;

namespace PaletteDesigner.Utilities;

public static partial class PaletteMapper
{
    #region Section Regexes

    // Regex for Ribbon Tab mappings to BaseScheme
    private static readonly Regex _ribbonTabSelectedRegex  = new(@"^RibbonTabSelected([1-5])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonTabTrackingRegex  = new(@"^RibbonTabTracking([1-4])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonTabHighlightRegex = new(@"^RibbonTabHighlight([1-5])$", RegexOptions.Compiled);
    // === Automatic enum->property path resolver ===
    private static readonly Regex _buttonNormalRegex = new(@"^ButtonNormal(?:(Default|Navigator))?(Border|Back)([0-9]?)$", RegexOptions.Compiled);
    private static readonly Regex _textButtonRegex   = new(@"^TextButton(Form)?(Normal|Tracking|Pressed|Checked)$", RegexOptions.Compiled);
    private static readonly Regex _formButtonRegex  = new(@"^FormButton(Border|Back([12]))(Track|Pressed|Checked|CheckTrack)?$", RegexOptions.Compiled);
    private static readonly Regex _buttonStateRegex = new(@"^Button(Pressed|Checked|Selected)(Begin|End)$", RegexOptions.Compiled);
    // AppButton: Back/Outer/Inner/Border/MenuDocs*
    private static readonly Regex _appButtonRegex   = new(@"^AppButton(Back([123])|Border|Outer([123])|Inner([12])|MenuDocs(Back|Text))$", RegexOptions.Compiled);
    // RibbonTab: do not use Highlight state (use Tracking colors instead)
    private static readonly Regex _ribbonTabRegex   = new(@"^RibbonTab(?:(Selected|Tracking|Highlight)([1-5])|SeparatorColor|Text(Normal|Checked))$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGroupRegex = new(@"^RibbonGroup(?:(Area|Border)([1-5])|Title([12]))$", RegexOptions.Compiled);
    private static readonly Regex _headerRegex      = new(@"^Header(Primary|Secondary)(Back([12])|Text)$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGroupExtraRegex = new(@"^RibbonGroup(?:(Collapsed)(Back|Border)([1-4])|(Dialog)([A-Za-z]+)|(Separator)([1-3]?))$", RegexOptions.Compiled);
    // Form chrome families (no Form.StateNormal in Krypton)
    private static readonly Regex _formBorderRegex        = new(@"^FormBorder(Active|Inactive)(Dark|Light)?$", RegexOptions.Compiled);
    private static readonly Regex _formBorderHeaderRegex  = new(@"^FormBorderHeader(Active|Inactive)([12])?$", RegexOptions.Compiled);
    private static readonly Regex _formHeaderRegex        = new(@"^FormHeader(Short|Long)?(Active|Inactive)$", RegexOptions.Compiled);
    private static readonly Regex _formButtonBorderCheckRegex = new(@"^FormButtonBorderCheck$", RegexOptions.Compiled);
    private static readonly Regex _panelAlternativeRegex = new(@"^PanelAlternative$", RegexOptions.Compiled);
    private static readonly Regex _controlBorderRegex   = new(@"^ControlBorder$", RegexOptions.Compiled);
    private static readonly Regex _altPressedRegex      = new(@"^AlternatePressed(Back|Border)([12])$", RegexOptions.Compiled);
    private static readonly Regex _separatorHighRegex   = new(@"^SeparatorHigh(InternalBorder|Border)([12])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGroupsAreaRegex = new(@"^RibbonGroupsArea([1-5])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonMinimizeBarRegex = new(@"^RibbonMinimizeBarDark$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGroupFrameRegex = new(@"^RibbonGroupFrame(Border|Inside)([12])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGroupSeparatorRegex = new(@"^RibbonGroupSeparator(Dark|Light)$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGroupTitleTextRegex = new(@"^RibbonGroupTitleText$", RegexOptions.Compiled);
    private static readonly Regex _ribbonQATMiniRegex = new(@"^RibbonQATMini([1-5])(I?)$", RegexOptions.Compiled);
    private static readonly Regex _ribbonQATFullRegex = new(@"^RibbonQATFullbar([1-3])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonQATButtonRegex = new(@"^RibbonQATButton(Dark|Light)$", RegexOptions.Compiled);
    private static readonly Regex _ribbonQATOverflowRegex = new(@"^RibbonQATOverflow([1-2])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonDropArrowRegex = new(@"^RibbonDropArrow(Dark|Light)$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGalleryRegex = new(@"^RibbonGallery(Back(Normal|Tracking)|Back([12])|Border)$", RegexOptions.Compiled);
    private static readonly Regex _appMenuDocsBackRegex = new(@"^AppButtonMenuDocsBack$", RegexOptions.Compiled);
    private static readonly Regex _formHeaderShortLongRegex = new(@"^FormHeader(Short|Long)(Active|Inactive)$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGalleryBackRegex = new(@"^RibbonGalleryBack(?:(Tracking|Normal)|([12]))?$", RegexOptions.Compiled);
    private static readonly Regex _ribbonGroupsAreaAnyRegex = new(@"^RibbonGroupsArea([1-5])$", RegexOptions.Compiled); // keep but map to Ribbon.Tab/Group styles below
    private static readonly Regex _buttonClusterRegex = new(@"^ButtonClusterButton(Back|Border)([12])$", RegexOptions.Compiled);
    private static readonly Regex _gridListRegex = new(@"^GridList(Normal|Pressed|Selected)([12])?$", RegexOptions.Compiled);
    private static readonly Regex _gridSheetColRegex = new(@"^GridSheetCol(Normal|Pressed|Selected)([12])?$", RegexOptions.Compiled);
    private static readonly Regex _gridSheetRowRegex = new(@"^GridSheetRow(Normal|Pressed|Selected)$", RegexOptions.Compiled);
    private static readonly Regex _gridDataCellRegex = new(@"^GridDataCell(Border|Selected)$", RegexOptions.Compiled);
    private static readonly Regex _navigatorMiniRegex = new(@"^NavigatorMiniBackColor$", RegexOptions.Compiled);
    private static readonly Regex _buttonNavigatorRegex = new(@"^ButtonNavigator(Border|Text|Track|Pressed|Checked)([12])?$", RegexOptions.Compiled);
    private static readonly Regex _inputTextRegex     = new(@"^InputControlText(Normal|Disabled)$", RegexOptions.Compiled);
    private static readonly Regex _inputBorderRegex   = new(@"^InputControlBorder(Normal|Disabled)$", RegexOptions.Compiled);
    private static readonly Regex _inputBackRegex     = new(@"^InputControlBack(Disabled|Inactive)$", RegexOptions.Compiled);
    private static readonly Regex _inputDropDownRegex = new(@"^InputDropDown(Normal|Disabled)([12])$", RegexOptions.Compiled);
    private static readonly Regex _toolTipBottomRegex = new(@"^ToolTipBottom$", RegexOptions.Compiled);
    private static readonly Regex _trackBarRegex      = new(@"^TrackBar(TopTrack|BottomTrack|FillTrack|OutsidePosition|BorderPosition|TickMarks)$", RegexOptions.Compiled);
    private static readonly Regex _contextMenuHeadingRegex = new(@"^ContextMenuHeadingBack$", RegexOptions.Compiled);
    private static readonly Regex _headerDockInactiveRegex = new(@"^HeaderDockInactiveBack([12])$", RegexOptions.Compiled);

    #endregion Section Regexes

    // Manual overrides for enum name -> property path when reflection search cannot find a (valid) match
    private static readonly Dictionary<string, string> _enumToPathOverrides = new(StringComparer.Ordinal)
    {
        // Tool/StatusStrip & related menu colors
        ["StatusStripText"]   = "ToolMenuStatus.StatusStrip.ToolStripText",
        ["ButtonBorder"]      = "ToolMenuStatus.Button.ButtonBorder",
        ["SeparatorLight"]    = "ToolMenuStatus.Separator.SeparatorLight",
        ["SeparatorDark"]     = "ToolMenuStatus.Separator.SeparatorDark",
        ["GripLight"]         = "ToolMenuStatus.Grip.GripLight",
        ["GripDark"]          = "ToolMenuStatus.Grip.GripDark",
        ["ToolStripBack"]     = "ToolMenuStatus.ToolStrip.ToolStripGradientMiddle",
        ["StatusStripLight"]  = "ToolMenuStatus.StatusStrip.ToolStripGradientBegin",
        ["StatusStripDark"]   = "ToolMenuStatus.StatusStrip.ToolStripGradientEnd",
        ["ImageMargin"]       = "ToolMenuStatus.MenuStrip.ToolStripDropDownBackground",
        ["ToolStripBegin"]    = "ToolMenuStatus.ToolStrip.ToolStripGradientBegin",
        ["ToolStripMiddle"]   = "ToolMenuStatus.ToolStrip.ToolStripGradientMiddle",
        ["ToolStripEnd"]      = "ToolMenuStatus.ToolStrip.ToolStripGradientEnd",
        ["OverflowBegin"]     = "ToolMenuStatus.ToolStrip.ToolStripPanelGradientBegin",
        ["OverflowMiddle"]    = "ToolMenuStatus.ToolStrip.ToolStripPanelGradientMiddle",
        ["OverflowEnd"]       = "ToolMenuStatus.ToolStrip.ToolStripPanelGradientEnd",
        ["ToolStripBorder"]   = "ToolMenuStatus.ToolStrip.ToolStripBorder"
    };

    /// <summary>
    /// Attempts to fetch a manually-defined property path for the supplied
    /// <paramref name="enumName"/> from <see cref="_enumToPathOverrides"/>.
    /// </summary>
    /// <param name="enumName">
    /// Name of the <see cref="SchemeBaseColors"/> enum value to resolve.
    /// </param>
    /// <param name="path">
    /// When this method returns, contains the resolved property path if one is
    /// registered; otherwise <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if a manual mapping was found; otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryGetManualPath(string enumName, out string path) => _enumToPathOverrides.TryGetValue(enumName, out path);

    /// <summary>
    /// Returns enum names that cannot be resolved either via reflection search or the manual override table.
    /// Useful in DEBUG to discover new mappings that need to be added.
    /// </summary>
    public static IReadOnlyCollection<string> GetMissingEnums(PaletteBase palette)
    {
        return GetMissingEnums(palette, out _);
    }

    /// <summary>
    /// Same as <see cref="GetMissingEnums(PaletteBase)"/>, but also outputs the
    /// grammar path that was attempted for each enum (if any).
    /// </summary>
    /// <param name="palette">Palette instance to inspect.</param>
    /// <param name="attemptedGrammar">
    /// A dictionary mapping enum names to the grammar-derived path that was
    /// tried (or <see langword="null"/> if grammar mapping failed).
    /// </param>
    /// <returns>
    /// Collection of enum names that remain unresolved.
    /// </returns>
    public static IReadOnlyCollection<string> GetMissingEnums(PaletteBase palette, out Dictionary<string, string?> attemptedGrammar)
    {
        if (palette == null) throw new ArgumentNullException(nameof(palette));
        attemptedGrammar = new Dictionary<string, string?>();
        var missing = new List<string>();
        foreach (SchemeBaseColors val in Enum.GetValues(typeof(SchemeBaseColors)))
        {
            string name = val.ToString();
            if (_enumToPathOverrides.ContainsKey(name))
            {
                continue; // manual mapping exists
            }

            // Try grammar first
            string? grammarPath = TryGrammarMap(name);
            if (!string.IsNullOrEmpty(grammarPath))
            {
                attemptedGrammar[name] = grammarPath;
            }

            if (FindPathsForEnum(palette, palette, val, doRecursive: true).Count == 0 && string.IsNullOrEmpty(grammarPath))
            {
                missing.Add(name);
            }
        }
        return missing;
    }

    /// <summary>
    /// Retrieves the <see cref="Color"/> located at the specified dot-separated
    /// <paramref name="propertyPath"/>.
    /// The method understands a variety of special-case aliases (TrackBar,
    /// ToolTip, ButtonStyles, etc.) before falling back to reflection.
    /// </summary>
    /// <param name="palette">Palette that supplies colors and style helpers.</param>
    /// <param name="propertyPath">
    /// Dot-notation path (e.g. <c>"ButtonStyles.Standalone.StateNormal.Back.Color1"</c>).
    /// </param>
    /// <returns>The color stored at the indicated location.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the supplied path is syntactically invalid or cannot be resolved.
    /// </exception>
    public static Color GetColorByPath(PaletteBase palette, string propertyPath)
    {
        string[] parts = propertyPath.Split('.');
        int stateIndex = Array.FindIndex(parts, p => p.StartsWith("State", StringComparison.Ordinal));

        // TrackBar and Base-scheme aliases: resolve via KryptonColorSchemeBase when given as flat tokens
        // e.g., "TrackBar.TickMarks" -> palette.BaseScheme.TrackBarTickMarks
        if (stateIndex == -1 && parts.Length == 2 && parts[0] == "TrackBar")
        {
            // Map TrackBar.<Part> -> TrackBar<Part> on BaseScheme
            string baseProp = "TrackBar" + parts[1];
            var scheme = GetBaseScheme(palette);
            if (scheme != null)
            {
                var piScheme = scheme.GetType().GetProperty(baseProp, BindingFlags.Public | BindingFlags.Instance);
                if (piScheme != null && piScheme.PropertyType == typeof(Color))
                {
                    return (Color)piScheme.GetValue(scheme)!;
                }
            }
            // fall through to reflection chain if not found
        }

        // ToolTip bottom alias: allow "ToolTip.StateNormal.Border.Color1" to fetch from BaseScheme.ToolTipBottom
        if (parts.Length == 4 && parts[0] == "ToolTip" && parts[1] == "StateNormal" && parts[2] == "Border")
        {
            var scheme = GetBaseScheme(palette);
            if (scheme != null)
            {
                var piScheme = scheme.GetType().GetProperty("ToolTipBottom", BindingFlags.Public | BindingFlags.Instance);
                if (piScheme != null && piScheme.PropertyType == typeof(Color))
                {
                    return (Color)piScheme.GetValue(scheme)!;
                }
            }
            // fall through if not present
        }

        // Fast generic fallback when no State segment present (direct property chain)
        if (stateIndex == -1)
        {
            object? current = palette;
            for (int i = 0; i < parts.Length; i++)
            {
                PropertyInfo? pi = current?.GetType().GetProperty(parts[i], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (pi == null)
                {
                    break; // invalid
                }
                current = pi.GetValue(current!);
                if (current == null)
                {
                    break;
                }

                if (i == parts.Length - 1 && current is Color c)
                {
                    return c;
                }
            }
            throw new ArgumentException("Invalid property path: " + propertyPath);
        }

        if (stateIndex < 1 || stateIndex + 1 >= parts.Length)
        {
            throw new ArgumentException("Invalid property path: " + propertyPath);
        }

        string styleKey = parts[stateIndex - 1];
        string stateKeyRaw = parts[stateIndex].Substring("State".Length);
        PaletteState state;
        if (!Enum.TryParse(stateKeyRaw, out state))
        {
            // handle aliases that are not present in the PaletteState enum
            state = stateKeyRaw switch
            {
                "Common" => PaletteState.Normal,
                "Selected" => PaletteState.CheckedNormal, // grid row/element selected
                "Active"   => PaletteState.Normal,
                "Inactive" => PaletteState.Disabled,
                _ => throw new ArgumentException("Invalid state segment: " + stateKeyRaw)
            };
        }
        string groupKey = parts[stateIndex + 1];
        string indexKey = (stateIndex + 2 < parts.Length && parts[stateIndex + 2]
            .StartsWith("Color", StringComparison.Ordinal))
                ? parts[stateIndex + 2].Substring("Color".Length)
                : "1";

        // Handle Ribbon BackColorN direct properties (no Back group)
        if (parts.Length > stateIndex + 1 && parts[stateIndex - 1] == "Ribbon" && groupKey.StartsWith("BackColor", StringComparison.Ordinal))
        {
            string idx = groupKey.Substring("BackColor".Length);
            if (Enum.TryParse(parts[stateIndex - 1], out PaletteBackStyle _))
            {
                // Not used; fall-through to reflection works better for these
            }
        }

        // Special case for ButtonStyles.Standalone which maps to ButtonStandalone
        if (parts.Length > 1 && parts[0] == "ButtonStyles" && styleKey == "Standalone")
        {
            styleKey = "ButtonStandalone";
        }

        if (groupKey == "Back")
        {
            if (Enum.TryParse(styleKey, out PaletteBackStyle style))
            {
                if (indexKey == "2")
                {
                    return palette.GetBackColor2(style, state);
                }
                return palette.GetBackColor1(style, state);
            }
        }
        else if (groupKey == "Border")
        {
            if (Enum.TryParse(styleKey, out PaletteBorderStyle style))
            {
            if (indexKey == "2")
            {
                return palette.GetBorderColor2(style, state);
            }
            return palette.GetBorderColor1(style, state);
            }
        }
        else if (groupKey == "Text")
        {
            if (Enum.TryParse(styleKey, out PaletteContentStyle style))
            {
            if (indexKey == "2")
            {
                return palette.GetContentShortTextColor2(style, state);
            }
            return palette.GetContentShortTextColor1(style, state);
            }
        }

        return GetColorByReflection(palette, parts);
    }

    /// <summary>
    /// Attempts to obtain the current <see cref="KryptonColorSchemeBase"/>
    /// instance that underpins the supplied <paramref name="palette"/>.
    /// </summary>
    /// <param name="palette">Palette to interrogate.</param>
    /// <returns>
    /// The backing color scheme if discoverable; otherwise <see langword="null"/>.
    /// </returns>
    private static KryptonColorSchemeBase? GetBaseScheme(PaletteBase palette)
    {
        // KryptonCustomPaletteBase stores a private _basePalette and exposes SchemeColors; not directly the scheme.
        // However, many PaletteBase implementations expose a BaseScheme or equivalent. Try common names via reflection.
        var pi = palette.GetType().GetProperty("BaseScheme", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                 ?? palette.GetType().GetProperty("ColorScheme", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                 ?? palette.GetType().GetProperty("Scheme", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (pi != null)
        {
            var scheme = pi.GetValue(palette) as KryptonColorSchemeBase;
            if (scheme != null) return scheme;
        }

        // Some palettes keep a reference to a base palette that can expose the scheme
        var basePalettePi = palette.GetType().GetProperty("BasePalette", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        var basePalette = basePalettePi?.GetValue(palette);
        if (basePalette != null)
        {
            var piScheme = basePalette.GetType().GetProperty("BaseScheme", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                          ?? basePalette.GetType().GetProperty("ColorScheme", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                          ?? basePalette.GetType().GetProperty("Scheme", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (piScheme != null)
            {
                return piScheme.GetValue(basePalette) as KryptonColorSchemeBase;
            }
        }

        return null;
    }

    /// <summary>
    /// Performs a simple reflection walk across <paramref name="root"/> using
    /// <paramref name="parts"/> as the sequence of property names to traverse.
    /// </summary>
    /// <param name="root">Root object from which to start reflection.</param>
    /// <param name="parts">Individual path segments.</param>
    /// <returns>The <see cref="Color"/> found at the end of the chain.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when traversal fails to locate a <see cref="Color"/> property.
    /// </exception>
    private static Color GetColorByReflection(object root, string[] parts)
    {
        object? current = root;
        for (int i = 0; i < parts.Length; i++)
        {
            var pi = current?.GetType().GetProperty(parts[i], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null)
            {
                break;
            }
            current = pi.GetValue(current!);
            if (current == null)
            {
                break;
            }
            if (i == parts.Length - 1 && current is Color c)
            {
                return c;
            }
        }
        throw new ArgumentException("Invalid property path: " + string.Join(".", parts));
    }

    /// <summary>
    /// Maps a palette property path back to the corresponding
    /// <see cref="SchemeBaseColors"/> enumeration value by comparing the color
    /// stored at the path with all colors exposed by the palette’s base scheme.
    /// </summary>
    /// <param name="palette">Palette supplying both path and scheme colors.</param>
    /// <param name="propertyPath">Dot-notation property path.</param>
    /// <returns>The enum value whose color matches <paramref name="propertyPath"/>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no enum value matches the color found at the path.
    /// </exception>
    public static SchemeBaseColors MapPathToSchemeEnum(PaletteBase palette, string propertyPath)
    {
        if (palette == null) throw new ArgumentNullException(nameof(palette));
        if (string.IsNullOrWhiteSpace(propertyPath)) throw new ArgumentNullException(nameof(propertyPath));

        // 1) Fast path – try to invert the grammar mapping directly
        // Adjust legacy alias where Standalone appears without the "Button" prefix
        string adjustedPath = propertyPath.StartsWith("ButtonStyles.Standalone.", StringComparison.Ordinal)
            ? "ButtonStyles.ButtonStandalone." + propertyPath.Substring("ButtonStyles.Standalone.".Length)
            : propertyPath;

        foreach (SchemeBaseColors val in Enum.GetValues(typeof(SchemeBaseColors)))
        {
            string? grammarPath = TryGrammarMap(val.ToString());
            if (!string.IsNullOrEmpty(grammarPath) && string.Equals(grammarPath, adjustedPath, StringComparison.Ordinal))
            {
                return val; // grammar provided an exact structural match
            }
        }

        // 2) Fallback – compare actual colours (existing behaviour)
        Color target = GetColorByPath(palette, adjustedPath);

        foreach (SchemeBaseColors val in Enum.GetValues(typeof(SchemeBaseColors)))
        {
            if (palette.GetSchemeColor(val) == target)
            {
                return val;
            }
        }

        throw new InvalidOperationException("No matching SchemeBaseColors for color " + target);
    }

    /// <summary>
    /// Safe variant of <see cref="MapPathToSchemeEnum"/> that returns a boolean
    /// instead of throwing.
    /// </summary>
    /// <param name="palette">Palette to search.</param>
    /// <param name="propertyPath">Dot-notation path.</param>
    /// <param name="result">
    /// When this method returns, contains the resolved enum value if mapping
    /// succeeded.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when mapping succeeds; otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryMapPathToSchemeEnum(PaletteBase palette, string propertyPath, out SchemeBaseColors result)
    {
        try
        {
            result = MapPathToSchemeEnum(palette, propertyPath);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// Attempts to translate <paramref name="enumName"/> into a palette property
    /// path by applying a set of regular-expression grammar rules implemented in
    /// the dedicated TryMap* helper methods.
    /// </summary>
    /// <param name="enumName">Name of a <see cref="SchemeBaseColors"/> value.</param>
    /// <returns>
    /// The calculated path if a grammar rule matches; otherwise
    /// <see langword="null"/>.
    /// </returns>
    private static readonly Dictionary<string, Func<string,string?>> _prefixMap = new(StringComparer.Ordinal)
    {
        ["TextButton"]       = TryMapTextButton,
        ["FormButton"]       = TryMapFormButton,
        ["AppButton"]        = TryMapAppButton,
        ["Button"]           = TryMapButton,
        ["Ribbon"]           = TryMapRibbon,
        ["Grid"]             = TryMapGrid,
        ["InputControl"]     = TryMapInputControl,
        ["InputDropDown"]    = TryMapInputControl,
        ["ControlBorder"]    = TryMapPanel,
        ["Panel"]            = TryMapPanel,
        ["Form"]             = TryMapForm,
        ["AlternatePressed"] = TryMapAlternatePressed,
        ["Separator"]        = TryMapSeparator,
        ["NavigatorMini"]    = TryMapNavigatorMini,
        ["ToolTip"]          = TryMapToolTip,
        ["TrackBar"]         = TryMapTrackBar,
        ["ContextMenu"]      = TryMapContextMenu,
        ["HeaderDockInactive"] = TryMapHeaderDockInactive,
        ["Header"]           = TryMapHeader,
        ["TextLabel"]        = TryMapLabel
    };

    private static string? TryGrammarMap(string enumName)
    {
        foreach (var kvp in _prefixMap)
        {
            if (enumName.StartsWith(kvp.Key, StringComparison.Ordinal))
                return kvp.Value(enumName);
        }
        return null;
    }

    /// <summary>
    /// Resolves a <see cref="SchemeBaseColors"/> value to its property path
    /// using the following strategy:
    /// 1. Manual overrides
    /// 2. Grammar-based mapping
    /// 3. Recursive reflection search (slow)
    /// </summary>
    /// <param name="palette">Palette whose structure is inspected.</param>
    /// <param name="enumVal">Enum value to resolve.</param>
    /// <returns>
    /// Matching property path if found; otherwise <see langword="null"/>.
    /// </returns>
    public static string? ResolvePath(PaletteBase palette, SchemeBaseColors enumVal)
    {
        string name = enumVal.ToString();

        // 1) manual overrides
        if (_enumToPathOverrides.TryGetValue(name, out var manualPath))
        {
            return manualPath;
        }

        // 2) grammar-based mapping
        string? grammarPath = TryGrammarMap(name);
        if (!string.IsNullOrEmpty(grammarPath))
        {
            return grammarPath;
        }

        // 3) reflection search fallback – may return multiple candidates
        var candidates = FindPathsForEnum(palette, palette, enumVal, doRecursive: true);
        if (candidates.Count == 0)
        {
            return null;
        }

        // Prefer paths that do not traverse Redirectors or ButtonSpecs, which are
        // internal indirections we cannot reliably write back to.
        foreach (var p in candidates)
        {
            if (!p.Contains(".Redirector.", StringComparison.Ordinal) &&
                !p.Contains(".Target.", StringComparison.Ordinal) &&
                !p.Contains(".InternalKCT.", StringComparison.Ordinal) &&
                !p.StartsWith("ToolMenuStatus", StringComparison.Ordinal) &&
                !p.Contains(".ColorMap", StringComparison.Ordinal))
            {
                return p;
            }
        }

        // Fallback to the first result if all contain redirectors
        return candidates[0];
    }

    /// <summary>
    /// Sets a palette color located at <paramref name="propertyPath"/> to the
    /// supplied <paramref name="newColor"/>.
    /// The method understands the same alias rules as
    /// <see cref="GetColorByPath"/>.
    /// </summary>
    /// <param name="root">Root palette or object graph.</param>
    /// <param name="propertyPath">Dot-notation path to the color property.</param>
    /// <param name="newColor">Color to apply.</param>
    public static void SetColorByPath(object root, string propertyPath, Color newColor)
    {
        if (root == null)
        {
            throw new ArgumentNullException(nameof(root));
        }
        if (string.IsNullOrWhiteSpace(propertyPath))
        {
            throw new ArgumentNullException(nameof(propertyPath));
        }

        // Special case for ButtonStyles.Standalone which maps to ButtonStandalone
        string adjustedPath = propertyPath;
        if (propertyPath.StartsWith("ButtonStyles.Standalone.", StringComparison.Ordinal))
        {
            adjustedPath = "ButtonStyles.ButtonStandalone." + propertyPath.Substring("ButtonStyles.Standalone.".Length);
        }

        string[] parts = adjustedPath.Split('.');

        // Handle direct SchemeBaseColors property mapping (e.g. "TextLabelControl" or "BaseColors.TextLabelControl")
        if (parts.Length == 1 || (parts.Length == 2 && parts[0] == "BaseColors"))
        {
            string schemeName = parts[parts.Length - 1];
            if (Enum.TryParse<SchemeBaseColors>(schemeName, out var enumVal) && root is PaletteBase paletteBase)
            {
                paletteBase.SetSchemeColor(enumVal, newColor);
                return;
            }
        }

        object current = root;
        for (int i = 0; i < parts.Length; i++)
        {
            string part = parts[i];
            PropertyInfo? pi = current.GetType().GetProperty(part, BindingFlags.Public | BindingFlags.Instance);
            if (pi == null)
            {
                return; // path invalid
            }
            if (i == parts.Length - 1)
            {
                // Final segment – expecting Color property
                if (pi.PropertyType == typeof(Color) && pi.CanWrite)
                {
                    pi.SetValue(current, newColor);
                }
                return;
            }
            current = pi.GetValue(current)!;
            if (current == null)
            {
                return; // path broken
            }
        }
    }

    /// <summary>
    /// Performs a depth-limited recursive reflection search to locate every
    /// property path within <paramref name="root"/> whose value equals the
    /// color associated with <paramref name="enumVal"/> in
    /// <paramref name="palette"/>.
    /// </summary>
    /// <param name="root">Starting object for the search.</param>
    /// <param name="palette">
    /// Palette whose base scheme provides the target color.
    /// </param>
    /// <param name="enumVal">Enum value whose color is matched.</param>
    /// <param name="doRecursive">
    /// If <see langword="true"/>, the object graph is walked recursively.
    /// </param>
    /// <returns>
    /// List of dot-notation property paths that match the enum color.
    /// </returns>
    public static List<string> FindPathsForEnum(object root, PaletteBase palette, SchemeBaseColors enumVal, bool doRecursive = false)
    {
        var result = new List<string>();
        if (root == null || palette == null)
        {
            return result;
        }

        // Obtain the expected Color for this enum from the palette's BaseScheme
        Color expectedColor = palette.GetSchemeColor(enumVal);

        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);

        void Recurse(object? obj, string prefix, int depth)
        {
            if (obj == null || depth > 6)
            {
                return;
            }
            if (!visited.Add(obj))
            {
                return; // cycle
            }

            Type type = obj.GetType();
            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead || prop.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                string currentPath = string.IsNullOrEmpty(prefix) ? prop.Name : prefix + "." + prop.Name;

                try
                {
                    if (prop.PropertyType == typeof(Color))
                    {
                        var valueObj = prop.GetValue(obj);
                        if (valueObj is Color col && col.ToArgb() == expectedColor.ToArgb())
                        {
                            result.Add(currentPath);
                        }
                    }
                    else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string) &&
                             !prop.PropertyType.IsEnum && !prop.PropertyType.FullName!.StartsWith("System."))
                    {
                        var child = prop.GetValue(obj);
                        if (child != null)
                        {
                            Recurse(child, currentPath, depth + 1);
                        }
                    }
                }
                catch
                {
                    // Ignore reflection/access exceptions and continue walking
                }
            }
        }

        if (doRecursive)
        {
            Recurse(root, string.Empty, 0);
        }
        return result;
    }

    /// <summary>
    /// Equality comparer that enforces reference equality semantics, allowing
    /// the search algorithms to maintain a visited-object set without being
    /// confused by value equality implementations.
    /// </summary>
    private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new();
        public new bool Equals(object? x, object? y) => ReferenceEquals(x, y);
        public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
    }
}