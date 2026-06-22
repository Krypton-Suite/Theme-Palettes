#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2023 - 2025. All rights reserved.
 *
 */
#endregion

namespace PaletteDesigner.Utilities;

public static class PaletteUpgradeUtilities
{
    #region Implementation

    public static MemoryStream PerformUpgrade(Stream inputStream)
    {
        using var reader = new StreamReader(inputStream);
        string xmlContent = reader.ReadToEnd();

        using var streamReader = new StreamReader(Resources.v6to20);
        using var xmlTextReader = XmlReader.Create(streamReader);

        var xslCompiledTransform = new XslCompiledTransform();
        xslCompiledTransform.Load(xmlTextReader);

        string transformedXml = XmlHelpers.TransformXml(xslCompiledTransform, xmlContent);

        var outputStream = new MemoryStream();
        using var writer = new StreamWriter(outputStream, new UTF8Encoding(false), 1024, true);

        writer.WriteLine("<?xml version=\"1.0\"?>");
        writer.Write(transformedXml);
        writer.Flush();

        outputStream.Position = 0;
        return outputStream;
    }

    public static void UpgradeFile(string inputFilePath, string outputFilePath)
    {
        // Read input file
        string xmlContent = File.ReadAllText(inputFilePath);

        // Detect version and apply appropriate transformation
        int inputVersion = DetectPaletteVersion(xmlContent);
        string transformedXml = ApplyUpgradeTransformation(xmlContent, inputVersion);

        // Write output file
        using var writer = new StreamWriter(outputFilePath, false, new UTF8Encoding(false));
        writer.WriteLine("<?xml version=\"1.0\"?>");
        writer.Write(transformedXml);
    }

    public static int DetectPaletteVersion(string xmlContent)
    {
        try
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlContent);

            var versionNode = doc.SelectSingleNode("//Version");
            if (versionNode != null && int.TryParse(versionNode.InnerText, out int version))
            {
                return version;
            }
        }
        catch
        {
            // Fallback to default if detection fails
        }

        return 1; // Default to oldest version if detection fails
    }

    public static string ApplyUpgradeTransformation(string xmlContent, int inputVersion)
    {
        const int MAXIMUM_PALETTE_FILE_VERSION = GlobalStaticConstants.CURRENT_SUPPORTED_PALETTE_VERSION;

        string result = xmlContent;

        // Apply v2to6 transformation if needed
        if (inputVersion < 6)
        {
            result = XmlHelpers.TransformXmlWithResource(Resources.v2to6, result);
        }

        // Apply v6to20 transformation if needed
        if (inputVersion < MAXIMUM_PALETTE_FILE_VERSION)
        {
            result = XmlHelpers.TransformXmlWithResource(Resources.v6to20, result);
        }

        return result;
    }

    #endregion
}