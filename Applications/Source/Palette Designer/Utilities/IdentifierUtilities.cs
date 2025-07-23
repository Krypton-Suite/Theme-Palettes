#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Utilities;

public static class IdentifierUtilities
{
    /// <summary>
    /// Returns a C# identifier-safe version of the supplied string.
    /// Falls back to "CustomPalette" if the result would be empty.
    /// </summary>
    public static string SanitizeIdentifier(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "CustomPalette";
        }

        var sb = new StringBuilder();
        foreach (char ch in input)
        {
            if (char.IsLetterOrDigit(ch) || ch == '_')
            {
                sb.Append(ch);
            }
        }

        if (sb.Length == 0)
        {
            sb.Append("CustomPalette");
        }

        // Ensure first character is a letter or underscore
        if (!char.IsLetter(sb[0]) && sb[0] != '_')
        {
            sb.Insert(0, '_');
        }

        return sb.ToString();
    }
}