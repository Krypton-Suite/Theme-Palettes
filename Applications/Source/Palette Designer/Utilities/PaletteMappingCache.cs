#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Utilities;

public static class PaletteMappingCache
{
    // Store the cache alongside the executable so the default file can be part of the repository
    private static readonly string _cachePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PaletteMapperCache.xml");

    private static readonly Dictionary<string, string> _map = new(StringComparer.Ordinal);

    public static IReadOnlyDictionary<string, string> Map => _map;

    public static void Initialise(PaletteBase palette, bool regenerateIfMissing = true)
    {
        if (!TryLoad())
        {
            if (regenerateIfMissing && palette is not null)
            {
                Regenerate(palette);
            }
        }
    }

    public static void Regenerate(PaletteBase palette)
    {
        if (palette is null) throw new ArgumentNullException(nameof(palette));

        _map.Clear();
        foreach (SchemeBaseColors e in Enum.GetValues(typeof(SchemeBaseColors)))
        {
            string? path = PaletteMapper.ResolvePath(palette, e);
            if (!string.IsNullOrEmpty(path))
            {
                _map[e.ToString()] = path!;
            }
        }
        Save();
    }

    public static bool TryGetPath(string enumName, out string path)
        => _map.TryGetValue(enumName, out path!);

    /// <summary>
    /// Registers a new mapping of enum name to property path in the cache.
    /// </summary>
    public static void Add(string enumName, string path)
        => _map[enumName] = path;

    private static bool TryLoad()
    {
        try
        {
            if (!File.Exists(_cachePath)) return false;
            var doc = XDocument.Load(_cachePath);
            if (doc.Root?.Attribute("version")?.Value != "1") return false;

            _map.Clear();
            foreach (var el in doc.Root!.Elements("Entry"))
            {
                string en = el.Attribute("enum")?.Value ?? string.Empty;
                string pa = el.Attribute("path")?.Value ?? string.Empty;
                if (en.Length > 0 && pa.Length > 0)
                {
                    _map[en] = pa;
                }
            }
            return _map.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static void Save()
    {
        var doc = new XDocument(
            new XElement("PaletteMapper",
                new XAttribute("version", "1"),
                _map.Select(kv => new XElement("Entry",
                    new XAttribute("enum", kv.Key),
                    new XAttribute("path", kv.Value)))));

        Directory.CreateDirectory(Path.GetDirectoryName(_cachePath)!);
        doc.Save(_cachePath);
    }
}