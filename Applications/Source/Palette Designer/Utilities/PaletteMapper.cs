#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Utilities;

public static class PaletteMapper
{
    public static Color GetColorByPath(PaletteBase palette, string propertyPath)
    {
        string[] parts = propertyPath.Split('.');
        int stateIndex = Array.FindIndex(parts, p => p.StartsWith("State", StringComparison.Ordinal));
        if (stateIndex < 1 || stateIndex + 1 >= parts.Length)
        {
            throw new ArgumentException("Invalid property path: " + propertyPath);
        }

        string styleKey = parts[stateIndex - 1];
        string stateKey = parts[stateIndex].Substring("State".Length);
        string groupKey = parts[stateIndex + 1];
        string indexKey = (stateIndex + 2 < parts.Length && parts[stateIndex + 2]
            .StartsWith("Color", StringComparison.Ordinal))
                ? parts[stateIndex + 2].Substring("Color".Length)
                : "1";

        PaletteState state = (PaletteState)Enum.Parse(typeof(PaletteState), stateKey);

        if (groupKey == "Back")
        {
            PaletteBackStyle style = (PaletteBackStyle)Enum.Parse(typeof(PaletteBackStyle), styleKey);
            if (indexKey == "2")
            {
                return palette.GetBackColor2(style, state);
            }
            return palette.GetBackColor1(style, state);
        }
        else if (groupKey == "Border")
        {
            PaletteBorderStyle style = (PaletteBorderStyle)Enum.Parse(typeof(PaletteBorderStyle), styleKey);
            if (indexKey == "2")
            {
                return palette.GetBorderColor2(style, state);
            }
            return palette.GetBorderColor1(style, state);
        }
        else if (groupKey == "Text")
        {
            PaletteContentStyle style = (PaletteContentStyle)Enum.Parse(typeof(PaletteContentStyle), styleKey);
            if (indexKey == "2")
            {
                return palette.GetContentShortTextColor2(style, state);
            }
            return palette.GetContentShortTextColor1(style, state);
        }

        throw new NotSupportedException("Group " + groupKey);
    }

    public static SchemeBaseColors MapPathToSchemeEnum(PaletteBase palette, string propertyPath)
    {
        Color target = GetColorByPath(palette, propertyPath);

        FieldInfo? field = palette.GetType().GetField("BaseColors", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (field == null)
        {
            throw new InvalidOperationException("BaseColors field not found for palette " + palette.GetType().Name);
        }

        var baseScheme = field.GetValue(palette) as KryptonColorSchemeBase;
        if (baseScheme == null)
        {
            throw new InvalidOperationException("BaseColors field is null or not a KryptonColorSchemeBase for palette " + palette.GetType().Name);
        }

        foreach (SchemeBaseColors val in Enum.GetValues(typeof(SchemeBaseColors)))
        {
            PropertyInfo? prop = baseScheme.GetType().GetProperty(val.ToString());
            if (prop == null)
            {
                continue;
            }
            object? colorValue = prop.GetValue(baseScheme);
            if (colorValue is Color c && c.ToArgb() == target.ToArgb())
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

        string[] parts = propertyPath.Split('.');
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

    public static List<string> FindPathsForEnum(object root, PaletteBase palette, SchemeBaseColors enumVal)
    {
        var result = new List<string>();
        if (root == null || palette == null)
        {
            return result;
        }

        // Obtain the expected Color for this enum from the palette's BaseScheme
        Color expectedColor;
        try
        {
            FieldInfo? fi = palette.GetType().GetField("BaseColors", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (fi == null)
            {
                return result;
            }
            var baseScheme = fi.GetValue(palette);
            if (baseScheme == null)
            {
                return result;
            }
            PropertyInfo? piColor = baseScheme.GetType().GetProperty(enumVal.ToString(), BindingFlags.Public | BindingFlags.Instance);
            if (piColor == null || piColor.PropertyType != typeof(Color))
            {
                return result;
            }
            object? colorValue = piColor.GetValue(baseScheme);
            if (colorValue is not Color color)
            {
                return result;
            }
            expectedColor = color;
        }
        catch
        {
            return result;
        }

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

        Recurse(root, string.Empty, 0);
        return result;
    }

    private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new();
        public new bool Equals(object? x, object? y) => ReferenceEquals(x, y);
        public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
    }
}
