#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

using System.Text.RegularExpressions;
using System.Linq;

namespace PaletteDesigner.Utilities;

public static class PaletteMapper
{
    // Regex for Ribbon Tab mappings to BaseScheme
    private static readonly Regex _ribbonTabSelectedRegex  = new(@"^RibbonTabSelected([1-5])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonTabTrackingRegex  = new(@"^RibbonTabTracking([1-4])$", RegexOptions.Compiled);
    private static readonly Regex _ribbonTabHighlightRegex = new(@"^RibbonTabHighlight([1-5])$", RegexOptions.Compiled);

    // Manual overrides for enum name -> property path when reflection search cannot find a match
    // Currently empty; kept for rare edge cases that cannot be auto-resolved
    private static readonly Dictionary<string, string> _enumToPathOverrides = new(StringComparer.Ordinal);

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
    /// Returns missing enums and also outputs their attempted grammar path (null if none).
    /// </summary>
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

    // Helper to reach the current base color scheme if available
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

    public static SchemeBaseColors MapPathToSchemeEnum(PaletteBase palette, string propertyPath)
    {
        Color target = GetColorByPath(palette, propertyPath);

        foreach (SchemeBaseColors val in Enum.GetValues(typeof(SchemeBaseColors)))
        {
            if (palette.GetSchemeColor(val) == target)
            {
                return val;
            }
        }

        throw new InvalidOperationException("No matching SchemeBaseColors for color " + target);
    }

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
    private static readonly Regex _ribbonGroupExtraRegex = new(@"^RibbonGroup(?:(Collapsed)(Back|Border)([12])|(Dialog)([A-Za-z]+)|(Separator)([1-3]?))$", RegexOptions.Compiled);
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

    private static string? TryGrammarMap(string enumName)
    {
        var m = _buttonNormalRegex.Match(enumName);
        if (m.Success)
        {
            string modifier = m.Groups[1].Value;
            string group = m.Groups[2].Value;
            string index = m.Groups[3].Value;

            string stylePart = modifier == "Navigator" ? "ButtonNavigatorStack" : "ButtonStandalone";
            string statePart = modifier == "Default" ? "NormalDefaultOverride" : "Normal";
            string groupPartRoot = group == "Border" ? "Border" : "Back";
            string indexPart = string.IsNullOrEmpty(index) ? "1" : index;

            return $"ButtonStyles.{stylePart}.State{statePart}.{groupPartRoot}.Color{indexPart}";
        }

        // TextButton* mapping
        m = _textButtonRegex.Match(enumName);
        if (m.Success)
        {
            bool isForm = m.Groups[1].Success;
            string stateToken = m.Groups[2].Value;

            string stylePart = isForm ? "ButtonForm" : "Standalone";
            string statePart = stateToken switch
            {
                "Normal" => "Normal",
                "Tracking" => "Tracking",
                "Pressed" => "Pressed",
                "Checked" => "CheckedNormal",
                _ => "Normal"
            };

            return $"ButtonStyles.{(isForm ? "ButtonForm" : "ButtonStandalone")}.State{statePart}.Text.Color1";
        }

        // FormButton* mapping
        m = _formButtonRegex.Match(enumName);
        if (m.Success)
        {
            string groupToken = m.Groups[1].Value; // Border or Back1/Back2
            string indexToken = m.Groups[2].Success ? m.Groups[2].Value : string.Empty; // 1 or 2 (for Back)
            string stateToken = m.Groups[3].Success ? m.Groups[3].Value : ""; // Track, Pressed, Checked, CheckTrack

            string stylePart = "ButtonForm";
            string statePart = stateToken switch
            {
                "Track" => "Tracking",
                "Pressed" => "Pressed",
                "Checked" => "CheckedNormal",
                "CheckTrack" => "CheckedTracking",
                _ => "Normal"
            };

            string groupRoot = groupToken.StartsWith("Border") ? "Border" : "Back";
            string groupPart = groupRoot + ".Color" + (string.IsNullOrEmpty(indexToken) ? "1" : indexToken);

            return $"ButtonStyles.{stylePart}.State{statePart}.{groupPart}";
        }

        // AppButton* mapping (correct Krypton objects)
        m = _appButtonRegex.Match(enumName);
        if (m.Success)
        {
            string rootPart = "Ribbon.RibbonAppButton";
            if (enumName.StartsWith("AppButtonBorder"))
            {
                // Border is under ControlStyles.ControlRibbonAppMenu -> Border.Color1
                return "ControlStyles.ControlRibbonAppMenu.StateNormal.Border.Color1";
            }
            if (enumName.StartsWith("AppButtonBack"))
            {
                string idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
                return $"{rootPart}.StateNormal.BackColor{idx}";
            }
            if (enumName.StartsWith("AppButtonOuter"))
            {
                string idx = m.Groups[3].Success ? m.Groups[3].Value : "1";
                return $"{rootPart}.StateTracking.BackColor{idx}";
            }
            if (enumName.StartsWith("AppButtonInner"))
            {
                string idx = m.Groups[4].Success ? m.Groups[4].Value : "1";
                return $"{rootPart}.StatePressed.BackColor{idx}";
            }
            // AppButtonMenuDocs mapping
            if (_appMenuDocsBackRegex.IsMatch(enumName))
                return "Ribbon.RibbonAppMenuDocs.BackColor1";
            if (enumName == "AppButtonMenuDocsText")
                return "Ribbon.RibbonAppMenuDocsEntry.TextColor";
        }

        // Generic Button state mapping (Pressed/Checked/Selected Begin/End)
        m = _buttonStateRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value;
            string partToken  = m.Groups[2].Value; // Begin or End

            string stylePart = "ButtonStandalone";
            string statePart = stateToken switch
            {
                "Pressed"  => "Pressed",
                "Checked"  => "CheckedNormal",
                "Selected" => "Tracking",
                _ => "Normal"
            };

            string backPart = "Back.Color" + (partToken == "End" ? "2" : "1");
            return $"ButtonStyles.{stylePart}.State{statePart}.{backPart}";
        }

        // RibbonTab* mapping
        m = _ribbonTabRegex.Match(enumName);
        if (m.Success)
        {
            if (enumName == "RibbonTabSeparatorColor")
            {
                // Defer to reflection; separator can vary across builds.
                return null;
            }

            // Text colors for RibbonTab
            if (enumName == "RibbonTabTextNormal")
            {
                return "Ribbon.RibbonTab.StateNormal.TextColor";
            }
            if (enumName == "RibbonTabTextChecked")
            {
                return "Ribbon.RibbonTab.StateCheckedNormal.TextColor";
            }

            string stateToken = m.Groups[1].Success ? m.Groups[1].Value : string.Empty; // Selected | Tracking | Highlight
            string indexToken = m.Groups[2].Success ? m.Groups[2].Value : string.Empty;

            if (!string.IsNullOrEmpty(stateToken))
            {
                string statePart = stateToken switch
                {
                    "Selected"  => "CheckedNormal",
                    "Tracking"  => "Tracking",
                    // Highlight uses Tracking colors
                    "Highlight" => "Tracking",
                    _           => "Normal"
                };

                string idx = string.IsNullOrEmpty(indexToken) ? "1" : indexToken;

                // Map directly to BackColorN property as shown in Krypton object structure
                return $"Ribbon.RibbonTab.State{statePart}.BackColor{idx}";
            }

            return null;
        }

        // RibbonGroup* mapping
        m = _ribbonGroupRegex.Match(enumName);
        if (m.Success)
        {
            string areaToken  = m.Groups[1].Success ? m.Groups[1].Value : string.Empty; // Area or Border
            string indexToken = m.Groups[2].Success ? m.Groups[2].Value : string.Empty; // 1-5
            bool isTitle      = m.Groups[3].Success;

            string stylePart = "RibbonGroup" + (areaToken == "Border" ? "Border" : areaToken == "Area" ? "Area" : "Title");
            string groupPart;
            string idx = string.IsNullOrEmpty(indexToken) ? (isTitle ? "1" : "1") : indexToken;

            if (isTitle)
            {
                // Title colours map to text
                groupPart = $"TextColor";
            }
            else if (areaToken == "Border")
            {
                groupPart = $"BorderColor{idx}";
            }
            else // Area (Back)
            {
                groupPart = $"BackColor{idx}";
            }

            return $"Ribbon.{stylePart}.StateNormal.{groupPart}";
        }

        // Header* mapping
        m = _headerRegex.Match(enumName);
        if (m.Success)
        {
            string primaryToken = m.Groups[1].Value; // Primary or Secondary
            string groupToken  = m.Groups[2].Value; // Back1/Back2 or Text
            string indexToken  = m.Groups[3].Success ? m.Groups[3].Value : string.Empty; // 1 or 2 for Back

            string stylePart = primaryToken == "Primary" ? "HeaderPrimary" : "HeaderSecondary";
            string groupPart;
            if (groupToken.StartsWith("Back"))
            {
                string idx = string.IsNullOrEmpty(indexToken) ? "1" : indexToken;
                groupPart = $"Back.Color{idx}";
            }
            else // Text
            {
                groupPart = "Content.ShortText.Color1";
            }

            return $"HeaderStyles.{stylePart}.StateNormal.{groupPart}";
        }

        // Form chrome mapping (no Form.StateNormal in Krypton)
        m = _formBorderRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value; // Active / Inactive
            string shadeToken = m.Groups[2].Success ? m.Groups[2].Value : string.Empty; // Dark / Light / empty

            string colorIdx  = shadeToken == "Dark" ? "2" : "1";
            string statePart = stateToken == "Active" ? "StateActive" : "StateInactive";
            // ControlClient or ControlRibbon depending on current chrome; prefer ControlRibbon per guidance
            return $"ControlStyles.ControlRibbon.{statePart}.Border.Color{colorIdx}";
        }

        // FormBorderHeader* mapping -> ControlClient/ControlRibbon? Spec says border colors from ControlRibbon
        m = _formBorderHeaderRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value; // Active / Inactive
            string idx        = m.Groups[2].Success ? m.Groups[2].Value : "1";
            string statePart = stateToken == "Active" ? "StateActive" : "StateInactive";
            return $"ControlStyles.ControlRibbon.{statePart}.Border.Color{idx}";
        }

        // FormHeaderShort/Long mapping -> prefer Content.ShortText for broader build compatibility
        m = _formHeaderRegex.Match(enumName);
        if (m.Success)
        {
            // Corrected to match actual Krypton object structure
            string lengthToken = m.Groups[1].Success ? m.Groups[1].Value : string.Empty; // Short / Long / empty
            string stateToken  = m.Groups[2].Value; // Active / Inactive

            string stylePart = string.IsNullOrEmpty(lengthToken) ? "FormHeader" : $"FormHeader{lengthToken}";
            string statePart = stateToken == "Active" ? "Active" : "Inactive";
            return $"HeaderStyles.{stylePart}.State{statePart}.TextColor";
        }

        // FormButtonBorderCheck mapping
        if (_formButtonBorderCheckRegex.IsMatch(enumName))
        {
            return "ButtonStyles.ButtonForm.StateCheckedNormal.Border.Color1";
        }

        // Panel/Control mapping
        if (_panelAlternativeRegex.IsMatch(enumName))
        {
            return "PanelStyles.PanelAlternate.StateNormal.Back.Color1";
        }
        if (_controlBorderRegex.IsMatch(enumName))
        {
            return "PanelStyles.PanelClient.StateNormal.Back.Color1";
        }

        // AlternatePressed* mapping (ButtonAlternate style)
        m = _altPressedRegex.Match(enumName);
        if (m.Success)
        {
            string groupToken = m.Groups[1].Value; // Back or Border
            string idx        = m.Groups[2].Success ? m.Groups[2].Value : "1";
            string groupPart  = (groupToken == "Back" ? "Back" : "Border") + ".Color" + idx;
            return $"ButtonStyles.ButtonAlternate.StatePressed.{groupPart}";
        }

        // SeparatorHigh* mapping
        m = _separatorHighRegex.Match(enumName);
        if (m.Success)
        {
            string internalToken = m.Groups[1].Value; // InternalBorder or Border
            string idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
            string stylePart = internalToken == "InternalBorder" ? "SeparatorHighInternalProfile" : "SeparatorHighProfile";
            return $"SeparatorStyles.{stylePart}.StateNormal.Border.Color{idx}";
        }

        // Ribbon chrome mapping
        m = _ribbonGroupsAreaRegex.Match(enumName);
        if (m.Success)
        {
            // Map GroupsArea to Ribbon.RibbonGroupArea
            string idx = m.Groups[1].Value;
            return $"Ribbon.RibbonGroupArea.StateNormal.Back.Color{idx}";
        }
        if (_ribbonMinimizeBarRegex.IsMatch(enumName))
        {
            // Use Ribbon object graph per screenshot
            return "Ribbon.RibbonMinimizeBar.StateNormal.Back.Color2";
        }
        m = _ribbonGroupFrameRegex.Match(enumName);
        if (m.Success)
        {
            string part = m.Groups[1].Value; // Border or Inside
            string idx  = m.Groups[2].Value;
            string groupPart = (part == "Border" ? "Border" : "Back") + ".Color" + idx;
            return $"Ribbon.RibbonGroupFrame.StateNormal.{groupPart}";
        }
        m = _ribbonGroupSeparatorRegex.Match(enumName);
        if (m.Success)
        {
            string darkLight = m.Groups[1].Value; // Dark or Light
            string idx = darkLight == "Dark" ? "1" : "2";
            return $"Ribbon.RibbonGroupSeparator.StateNormal.Border.Color{idx}";
        }
        if (_ribbonGroupTitleTextRegex.IsMatch(enumName))
        {
            return "Ribbon.RibbonGroupTitle.StateNormal.Text.Color1";
        }
        m = _ribbonQATMiniRegex.Match(enumName);
        if (m.Success)
        {
            string idx = m.Groups[1].Value;
            bool inactive = m.Groups[2].Success;
            string state = inactive ? "StateInactive" : "StateNormal";
            return $"Ribbon.RibbonQATMinibar.{state}.Back.Color{idx}";
        }
        m = _ribbonQATFullRegex.Match(enumName);
        if (m.Success)
        {
            string idx = m.Groups[1].Value;
            return $"Ribbon.RibbonQATFullbar.StateNormal.Back.Color{idx}";
        }
        m = _ribbonQATButtonRegex.Match(enumName);
        if (m.Success)
        {
            string shade = m.Groups[1].Value; // Dark / Light
            string colorIdx = shade == "Dark" ? "1" : "2";
            return $"Ribbon.RibbonQATButton.StateNormal.Border.Color{colorIdx}";
        }
        m = _ribbonQATOverflowRegex.Match(enumName);
        if (m.Success)
        {
            string idx = m.Groups[1].Value;
            return $"Ribbon.RibbonQATOverflow.StateNormal.Back.Color{idx}";
        }
        m = _ribbonDropArrowRegex.Match(enumName);
        if (m.Success)
        {
            // Final correction for RibbonDropArrow mapping
            string shade = m.Groups[1].Value; // Dark or Light
            return $"Ribbon.RibbonGeneral.StateNormal.DropArrow.{(shade == "Dark" ? "Dark" : "Light")}";
        }
        m = _ribbonGalleryRegex.Match(enumName);
        if (m.Success)
        {
            if (enumName == "RibbonGalleryBorder")
            {
                return "Ribbon.RibbonGalleryBorder.StateNormal.Border.Color1";
            }
            if (enumName.StartsWith("RibbonGalleryBackTracking", StringComparison.Ordinal))
            {
                return "Ribbon.RibbonGalleryBack.StateTracking.BackColor1";
            }
            if (enumName.StartsWith("RibbonGalleryBackNormal", StringComparison.Ordinal))
            {
                return "Ribbon.RibbonGalleryBack.StateNormal.BackColor1";
            }
            if (enumName.StartsWith("RibbonGalleryBack", StringComparison.Ordinal))
            {
                string idx = m.Groups[3].Success ? m.Groups[3].Value : "1";
                return $"Ribbon.RibbonGalleryBack.StateNormal.BackColor{idx}";
            }
        }
        // REMOVED: Duplicate RibbonTab BaseScheme mappings - already handled in primary RibbonTab section

        // Remove duplicate Ribbon Tab BaseScheme mapping block; handled earlier with object graph paths

        // Grid & Navigator mapping
        m = _buttonClusterRegex.Match(enumName);
        if (m.Success)
        {
            string groupToken = m.Groups[1].Value; // Back or Border
            string idx = m.Groups[2].Value;
            string groupPart = (groupToken == "Back" ? "Back" : "Border") + ".Color" + idx;
            return $"ButtonStyles.ButtonCluster.StateNormal.{groupPart}";
        }
        m = _gridListRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value; // Normal/Pressed/Selected
            string idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
            if (stateToken == "Pressed")
            {
                // Pressed = HeaderRow/HeaderColumn, not DataCell
                return $"GridStyles.GridList.StatePressed.HeaderRow.Back.Color{idx}";
            }
            if (stateToken == "Selected")
            {
                // Selected colors in GridStyles.GridList.StateSelected.DataCell.*
                return $"GridStyles.GridList.StateSelected.DataCell.Back.Color{idx}";
            }
            return $"GridStyles.GridList.StateNormal.DataCell.Back.Color{idx}";
        }
        m = _gridSheetColRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value;
            string idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
            string statePart = stateToken switch { "Normal"=>"Normal", "Pressed"=>"Pressed", _=>"Selected" };
            return $"GridStyles.GridSheet.State{statePart}.HeaderColumn.Back.Color{idx}";
        }
        m = _gridSheetRowRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value;
            string statePart = stateToken switch { "Normal"=>"Normal", "Pressed"=>"Pressed", _=>"Selected" };
            return $"GridStyles.GridSheet.State{statePart}.HeaderRow.Back.Color1";
        }
        m = _gridDataCellRegex.Match(enumName);
        if (m.Success)
        {
            string part = m.Groups[1].Value; // Border or Selected
            if (part == "Border")
            {
                return "GridStyles.GridList.StateNormal.DataCell.Border.Color1";
            }
            else
            {
                return "GridStyles.GridList.StateSelected.DataCell.Back.Color1";
            }
        }
        if (_navigatorMiniRegex.IsMatch(enumName))
        {
            // Final correction to match actual Krypton object structure
            return "Navigator.NavigatorMini.StateNormal.HeaderGroup.BackColor1";
        }
        m = _buttonNavigatorRegex.Match(enumName);
        if (m.Success)
        {
            string part = m.Groups[1].Value; // Border/Text/Track/Pressed/Checked
            string idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
            string statePart = part switch { "Track"=>"Tracking", "Pressed"=>"Pressed", "Checked"=>"CheckedNormal", _=>"Normal" };
            string groupPart;
            if (part == "Border") groupPart = $"Border.Color{idx}";
            else if (part == "Text") groupPart = "Text.Color1";
            else groupPart = $"Back.Color{idx}";
            return $"ButtonStyles.ButtonNavigatorStack.State{statePart}.{groupPart}";
        }

        // Input Controls mapping
        m = _inputTextRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value; // Normal or Disabled
            string statePart = stateToken == "Normal" ? "Normal" : "Disabled";
            return $"InputControl.InputControlStandalone.State{statePart}.Text.Color1";
        }
        m = _inputBorderRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value;
            string statePart = stateToken == "Normal" ? "Normal" : "Disabled";
            return $"InputControl.InputControlStandalone.State{statePart}.Border.Color1";
        }
        m = _inputBackRegex.Match(enumName);
        if (m.Success)
        {
            string kind = m.Groups[1].Value; // Disabled or Inactive
            string statePart = kind == "Disabled" ? "Disabled" : "Normal";
            return $"InputControl.InputControlStandalone.State{statePart}.Back.Color1";
        }
        m = _inputDropDownRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value;
            string idx = m.Groups[2].Value;
            string statePart = stateToken == "Normal" ? "Normal" : "Disabled";
            return $"InputControl.InputControlStandalone.State{statePart}.Back.Color{idx}";
        }
        if (_toolTipBottomRegex.IsMatch(enumName))
        {
            // ToolTip colors come from ControlStyles.ControlToolTip in this build
            return "ControlStyles.ControlToolTip.StateNormal.Border.Color1";
        }

        // Context menu heading mapping
        if (_contextMenuHeadingRegex.IsMatch(enumName))
        {
            return "ContextMenu.ContextMenuHeading.StateNormal.Back.Color1";
        }

        // HeaderDockInactive mapping
        m = _headerDockInactiveRegex.Match(enumName);
        if (m.Success)
        {
            string idx = m.Groups[1].Value;
            return $"HeaderStyles.HeaderDockInactive.StateNormal.Back.Color{idx}";
        }

        // TrackBar mapping -> KryptonPaletteTrackBar.State*.{Tick|Track|Position}.Color1
        m = _trackBarRegex.Match(enumName);
        if (m.Success)
        {
            string part = m.Groups[1].Value; // TickMarks | TopTrack | BottomTrack | FillTrack | OutsidePosition | BorderPosition
            return part switch
            {
                "TickMarks"       => "TrackBar.StateNormal.Tick.Color1",
                "TopTrack"        => "TrackBar.StateNormal.Track.Color1",
                "BottomTrack"     => "TrackBar.StateNormal.Track.Color1",
                "FillTrack"       => "TrackBar.StateNormal.Track.Color1",
                "OutsidePosition" => "TrackBar.StateNormal.Position.Color1",
                "BorderPosition"  => "TrackBar.StateNormal.Position.Color1",
                _                 => "TrackBar.StateNormal.Position.Color1"
            };
        }

        // TrackBar mapping
        m = _trackBarRegex.Match(enumName);
        if (m.Success)
        {
            // Map directly to top-level TrackBar properties exposed by the base scheme
            string part = m.Groups[1].Value;
            return part switch
            {
                "TickMarks"       => "TrackBar.TickMarks",
                "TopTrack"        => "TrackBar.TopTrack",
                "BottomTrack"     => "TrackBar.BottomTrack",
                "FillTrack"       => "TrackBar.FillTrack",
                "OutsidePosition" => "TrackBar.OutsidePosition",
                "BorderPosition"  => "TrackBar.BorderPosition",
                _                 => "TrackBar.OutsidePosition"
            };
        }

        // RibbonGroup Collapsed/Dialog/Separator mapping
        m = _ribbonGroupExtraRegex.Match(enumName);
        if (m.Success)
        {
            if (m.Groups[1].Success) // Collapsed
            {
                string groupToken = m.Groups[2].Value; // Back or Border
                string idx        = m.Groups[3].Value;
                string groupPart  = (groupToken == "Border" ? "Border" : "Back") + ".Color" + (string.IsNullOrEmpty(idx) ? "1" : idx);
                return $"Ribbon.RibbonGroupCollapsed.StateNormal.{groupPart}";
            }
            if (m.Groups[4].Success) // Dialog
            {
                string dialogPart = m.Groups[5].Value; // e.g., Border, LightShade, DarkShade, Glyph, Back
                string groupPart;
                if (dialogPart.Equals("Border", StringComparison.OrdinalIgnoreCase))
                {
                    groupPart = "Border.Color1";
                }
                else if (dialogPart.Equals("Back", StringComparison.OrdinalIgnoreCase))
                {
                    groupPart = "Back.Color1";
                }
                else // Glyph / LightShade / DarkShade treated as Text
                {
                    groupPart = "Text.Color1";
                }
                return $"Ribbon.RibbonGroupDialog.StateNormal.{groupPart}";
            }
            if (m.Groups[6].Success) // Separator
            {
                string idx = m.Groups[7].Value;
                string groupPart = "Border.Color" + (string.IsNullOrEmpty(idx) ? "1" : idx);
                return $"Ribbon.RibbonGroupSeparator.StateNormal.{groupPart}";
            }
        }

        // TODO: extend grammar rules for other control families
        return null;
    }

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

        // 3) reflection search fallback
        return FindPathsForEnum(palette, palette, enumVal, doRecursive: true).FirstOrDefault();
    }

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

    private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new();
        public new bool Equals(object? x, object? y) => ReferenceEquals(x, y);
        public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
    }
}
