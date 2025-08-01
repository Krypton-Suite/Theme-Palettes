#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PaletteDesigner.Utilities;

public static partial class PaletteMapper
{
    /// <summary>
    /// Maps button-related SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapButton(string enumName)
    {
        var m = _buttonNormalRegex.Match(enumName);
        if (m.Success)
        {
            var modifier = m.Groups[1].Value;
            var group = m.Groups[2].Value;
            var index = m.Groups[3].Value;

            var stylePart = modifier == "Navigator" ? "ButtonNavigatorStack" : "ButtonStandalone";
            var statePart = modifier == "Default" ? "NormalDefaultOverride" : "Normal";
            var groupPartRoot = group == "Border" ? "Border" : "Back";
            var indexPart = string.IsNullOrEmpty(index) ? "1" : index;

            return $"ButtonStyles.{stylePart}.State{statePart}.{groupPartRoot}.Color{indexPart}";
        }

        m = _buttonStateRegex.Match(enumName);
        if (m.Success)
        {
            var stateToken = m.Groups[1].Value;
            var partToken = m.Groups[2].Value;

            var stylePart = "ButtonStandalone";
            var statePart = stateToken switch
            {
                "Pressed" => "Pressed",
                "Checked" => "CheckedNormal",
                "Selected" => "Tracking",
                _ => "Normal"
            };

            var backPart = "Back.Color" + (partToken == "End" ? "2" : "1");
            return $"ButtonStyles.{stylePart}.State{statePart}.{backPart}";
        }

        m = _buttonClusterRegex.Match(enumName);
        if (m.Success)
        {
            var groupToken = m.Groups[1].Value;
            var idx = m.Groups[2].Value;
            var groupPart = (groupToken == "Back" ? "Back" : "Border") + ".Color" + idx;
            return $"ButtonStyles.ButtonCluster.StateNormal.{groupPart}";
        }

        m = _buttonNavigatorRegex.Match(enumName);
        if (m.Success)
        {
            var part = m.Groups[1].Value;
            var idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
            var statePart = part switch { "Track" => "Tracking", "Pressed" => "Pressed", "Checked" => "CheckedNormal", _ => "Normal" };
            var groupPart = part == "Border" ? $"Border.Color{idx}" : part == "Text" ? "Text.Color1" : $"Back.Color{idx}";
            return $"ButtonStyles.ButtonNavigatorStack.State{statePart}.{groupPart}";
        }

        return null;
    }

    /// <summary>
    /// Maps text-button SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapTextButton(string enumName)
    {
        var m = _textButtonRegex.Match(enumName);
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
        return null;
    }

    /// <summary>
    /// Maps form-button SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapFormButton(string enumName)
    {
        if (_formButtonBorderCheckRegex.IsMatch(enumName))
        {
            return "ButtonStyles.ButtonForm.StateCheckedNormal.Border.Color1";
        }
        var m = _formButtonRegex.Match(enumName);
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
        return null;
    }

    /// <summary>
    /// Maps application-button SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapAppButton(string enumName)
    {
        var m = _appButtonRegex.Match(enumName);
        if (m.Success)
        {
            string rootPart = "Ribbon.RibbonAppButton";
            if (enumName.StartsWith("AppButtonBorder"))
            {
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
            if (_appMenuDocsBackRegex.IsMatch(enumName))
                return "Ribbon.RibbonAppMenuDocs.BackColor1";
            if (enumName == "AppButtonMenuDocsText")
                return "Ribbon.RibbonAppMenuDocsEntry.TextColor";
        }
        return null;
    }

    /// <summary>
    /// Maps ribbon-related SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapRibbon(string enumName)
    {
        var m = _ribbonTabRegex.Match(enumName);
        if (m.Success)
        {
            if (enumName == "RibbonTabSeparatorColor")
            {
                return "Ribbon.RibbonGeneral.StateNormal.SeparatorColor";
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

            string stateToken = m.Groups[1].Success ? m.Groups[1].Value : string.Empty;
            string indexToken = m.Groups[2].Success ? m.Groups[2].Value : string.Empty;

            if (!string.IsNullOrEmpty(stateToken))
            {
                string statePart = stateToken switch
                {
                    "Selected" => "CheckedNormal",
                    "Tracking" => "Tracking",
                    "Highlight" => "Tracking",
                    _ => "Normal"
                };

                string idx = string.IsNullOrEmpty(indexToken) ? "1" : indexToken;

                // Map directly to BackColorN property as shown in Krypton object structure
                return $"Ribbon.RibbonTab.State{statePart}.BackColor{idx}";
            }
            return null;
        }

        m = _ribbonGroupRegex.Match(enumName);
        if (m.Success)
        {
            string areaToken = m.Groups[1].Success ? m.Groups[1].Value : string.Empty;
            string indexToken = m.Groups[2].Success ? m.Groups[2].Value : string.Empty;
            bool isTitle = m.Groups[3].Success;

            string stylePart = "RibbonGroup" + (areaToken == "Border" ? "Border" : areaToken == "Area" ? "Area" : "Title");
            string idx = string.IsNullOrEmpty(indexToken) ? "1" : indexToken;
            string groupPart = isTitle ? "TextColor" : areaToken == "Border" ? $"BorderColor{idx}" : $"BackColor{idx}";
            return $"Ribbon.{stylePart}.StateNormal.{groupPart}";
        }

        m = _headerRegex.Match(enumName);
        if (m.Success)
        {
            string primaryToken = m.Groups[1].Value;
            string groupToken = m.Groups[2].Value;
            string indexToken = m.Groups[3].Success ? m.Groups[3].Value : string.Empty;
            string stylePart = primaryToken == "Primary" ? "HeaderPrimary" : "HeaderSecondary";
            string groupPart;
            if (groupToken.StartsWith("Back"))
            {
                string idx = string.IsNullOrEmpty(indexToken) ? "1" : indexToken;
                groupPart = $"Back.Color{idx}";
            }
            else
            {
                groupPart = "Content.ShortText.Color1";
            }
            return $"HeaderStyles.{stylePart}.StateNormal.{groupPart}";
        }


        // Ribbon chrome mapping
        m = _ribbonGroupsAreaRegex.Match(enumName);
        if (m.Success)
        {
            // Map GroupsArea to Ribbon.RibbonGroupArea
            string idx = m.Groups[1].Value;
            return $"Ribbon.RibbonGroupArea.StateNormal.Back.Color{idx}";
        }
        if (enumName == "RibbonMinimizeBarLight")
        {
            return "Ribbon.RibbonMinimizeBar.StateNormal.Back.Color1";
        }
        if (_ribbonMinimizeBarRegex.IsMatch(enumName))
        {
            // Dark variant uses Color2, light uses Color1
            return "Ribbon.RibbonMinimizeBar.StateNormal.Back.Color2";
        }
        // RibbonGroup Border/Title context tracking
        if (enumName.StartsWith("RibbonGroupBorderContext", StringComparison.Ordinal))
        {
            string idx = enumName.Substring("RibbonGroupBorderContext".Length);
            idx = string.IsNullOrEmpty(idx) ? "1" : idx;
            return $"Ribbon.RibbonGroupBorder.StateContextChecked.BorderColor{idx}";
        }
        if (enumName.StartsWith("RibbonGroupTitleContext", StringComparison.Ordinal))
        {
            // Title context uses TextColor (no index differentiation in object model)
            return "Ribbon.RibbonGroupTitle.StateContextChecked.TextColor";
        }
        if (enumName.StartsWith("RibbonGroupTitleTracking", StringComparison.Ordinal))
        {
            return "Ribbon.RibbonGroupTitle.StateTracking.TextColor";
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
            string shade = m.Groups[1].Value;
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

        return null;
    }

    /// <summary>
    /// Maps alternate-pressed SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapAlternatePressed(string enumName)
    {
        var m = _altPressedRegex.Match(enumName);
        if (m.Success)
        {
            string groupToken = m.Groups[1].Value;
            string idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
            string groupPart = (groupToken == "Back" ? "Back" : "Border") + ".Color" + idx;
            return $"ButtonStyles.ButtonAlternate.StatePressed.{groupPart}";
        }
        return null;
    }

    /// <summary>
    /// Maps separator-related SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapSeparator(string enumName)
    {
        var m = _separatorHighRegex.Match(enumName);
        if (m.Success)
        {
            string internalToken = m.Groups[1].Value;
            string idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
            string stylePart = internalToken == "InternalBorder" ? "SeparatorHighInternalProfile" : "SeparatorHighProfile";
            return $"SeparatorStyles.{stylePart}.StateNormal.Border.Color{idx}";
        }
        return null;
    }

    /// <summary>
    /// Maps navigator-mini SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapNavigatorMini(string enumName)
    {
        if (_navigatorMiniRegex.IsMatch(enumName))
        {
            // Final correction to match actual Krypton object structure
            return "Navigator.NavigatorMini.StateNormal.HeaderGroup.BackColor1";
        }
        return null;
    }

    /// <summary>
    /// Maps tooltip SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapToolTip(string enumName)
    {
        if (_toolTipBottomRegex.IsMatch(enumName))
        {
            // ToolTip colors come from ControlStyles.ControlToolTip in this build
            return "ControlStyles.ControlToolTip.StateNormal.Border.Color1";
        }
        return null;
    }

    /// <summary>
    /// Maps trackbar SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapTrackBar(string enumName)
    {
        var m = _trackBarRegex.Match(enumName);
        if (m.Success)
        {
            string part = m.Groups[1].Value;
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

        m = _trackBarRegex.Match(enumName);
        if (m.Success)
        {
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
        return null;
    }

    /// <summary>
    /// Maps context-menu SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapContextMenu(string enumName)
    {
        if (_contextMenuHeadingRegex.IsMatch(enumName))
        {
            return "ContextMenu.ContextMenuHeading.StateNormal.Back.Color1";
        }
        return null;
    }

    /// <summary>
    /// Maps header-dock-inactive SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapHeaderDockInactive(string enumName)
    {
        var m = _headerDockInactiveRegex.Match(enumName);
        if (m.Success)
        {
            string idx = m.Groups[1].Value;
            return $"HeaderStyles.HeaderDockInactive.StateNormal.Back.Color{idx}";
        }
        return null;
    }

    /// <summary>
    /// Maps header SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapHeader(string enumName)
    {
        if (enumName == "HeaderText")
        {
            return "HeaderStyles.HeaderPrimary.StateNormal.Content.ShortText.Color1";
        }
        var m = _headerRegex.Match(enumName);
        if (m.Success)
        {
            string primaryToken = m.Groups[1].Value;
            string groupToken = m.Groups[2].Value;
            string indexToken = m.Groups[3].Success ? m.Groups[3].Value : string.Empty;
            string stylePart = primaryToken == "Primary" ? "HeaderPrimary" : "HeaderSecondary";
            string groupPart;
            if (groupToken.StartsWith("Back"))
            {
                string idx = string.IsNullOrEmpty(indexToken) ? "1" : indexToken;
                groupPart = $"Back.Color{idx}";
            }
            else
                groupPart = "Content.ShortText.Color1";
            return $"HeaderStyles.{stylePart}.StateNormal.{groupPart}";
        }
        return null;
    }

    /// <summary>
    /// Maps grid-related SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapGrid(string enumName)
    {
        var m = _gridListRegex.Match(enumName);
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
            string statePart = stateToken switch { "Normal" => "Normal", "Pressed" => "Pressed", _ => "Selected" };
            return $"GridStyles.GridSheet.State{statePart}.HeaderColumn.Back.Color{idx}";
        }
        m = _gridSheetRowRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value;
            string statePart = stateToken switch { "Normal" => "Normal", "Pressed" => "Pressed", _ => "Selected" };
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
        return null;
    }

    /// <summary>
    /// Maps input-control SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapInputControl(string enumName)
    {
        var m = _inputTextRegex.Match(enumName);
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
        return null;
    }

    /// <summary>
    /// Maps panel-related SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapPanel(string enumName)
    {
        if (enumName == "PanelClient")
            return "PanelStyles.PanelClient.StateNormal.Back.Color1";
        if (_panelAlternativeRegex.IsMatch(enumName))
            return "PanelStyles.PanelAlternate.StateNormal.Back.Color1";
        if (_controlBorderRegex.IsMatch(enumName))
            return "ControlStyles.ControlClient.StateNormal.Border.Color1";
        return null;
    }

    /// <summary>
    /// Maps TextLabel* SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapLabel(string enumName)
    {
        return enumName switch
        {
            "TextLabelControl" => "LabelStyles.LabelCommon.StateNormal.ShortText.Color1",
            "TextLabelPanel"   => "LabelStyles.LabelCaptionPanel.StateNormal.ShortText.Color1",
            _ => null
        };
    }

    /// <summary>
    /// Maps form-related SchemeBaseColors names to palette property paths.
    /// </summary>
    /// <param name="enumName">Enum name to resolve.</param>
    /// <returns>The mapped property path, or <c>null</c> if not recognized.</returns>
    private static string? TryMapForm(string enumName)
    {
        var m = _formBorderRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value; // Active / Inactive
            string shadeToken = m.Groups[2].Success ? m.Groups[2].Value : string.Empty; // Dark / Light / empty
            string colorIdx = shadeToken == "Dark" ? "2" : "1";
            string statePart = stateToken == "Active" ? "StateActive" : "StateInactive";
            // Use FormMain style (Main application window) instead of generic FormCommon
            return $"FormStyles.FormMain.{statePart}.Border.Color{colorIdx}";
        }

        m = _formBorderHeaderRegex.Match(enumName);
        if (m.Success)
        {
            string stateToken = m.Groups[1].Value;
            string idx = m.Groups[2].Success ? m.Groups[2].Value : "1";
            string statePart = stateToken == "Active" ? "StateActive" : "StateInactive";
            // Map to HeaderForm style rather than generic FormCommon border
            return $"HeaderStyles.HeaderForm.{statePart}.Border.Color{idx}";
        }

        // FormMain background colors (Back.Color1/2) mapping
        if (enumName.StartsWith("FormMainBack", StringComparison.Ordinal))
        {
            var mBack = System.Text.RegularExpressions.Regex.Match(enumName, @"^FormMainBack(?:(Active|Inactive))?([12])$");
            if (mBack.Success)
            {
                string stateToken = mBack.Groups[1].Success ? mBack.Groups[1].Value : "Active"; // default Active if omitted
                string idx = string.IsNullOrEmpty(mBack.Groups[2].Value) ? "1" : mBack.Groups[2].Value;
                string statePart = stateToken == "Inactive" ? "StateInactive" : "StateActive";
                return $"FormStyles.FormMain.{statePart}.Back.Color{idx}";
            }
        }

        m = _formHeaderRegex.Match(enumName);
        if (m.Success)
        {
            string lengthToken = m.Groups[1].Success ? m.Groups[1].Value : string.Empty; // Short / Long / empty
            string stateToken  = m.Groups[2].Value; // Active / Inactive
            string statePart   = stateToken == "Active" ? "Active" : "Inactive";

            if (lengthToken == "Short")
            {
                return $"HeaderStyles.HeaderForm.State{statePart}.Content.ShortText.Color1";
            }
            if (lengthToken == "Long")
            {
                return $"HeaderStyles.HeaderForm.State{statePart}.Content.LongText.Color1";
            }
            // Fallback for unspecified length – default to short text
            return $"HeaderStyles.HeaderForm.State{statePart}.Content.ShortText.Color1";
        }
        if (_formButtonBorderCheckRegex.IsMatch(enumName))
            return "ButtonStyles.ButtonForm.StateCheckedNormal.Border.Color1";
        return null;
    }
}
