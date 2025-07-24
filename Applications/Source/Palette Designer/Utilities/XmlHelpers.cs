#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Utilities;

internal static class XmlHelpers
{
    /// <summary>
    /// Transforms XML using the specified XSL transform.
    /// </summary>
    /// <param name="transform">The XSL compiled transform to apply.</param>
    /// <param name="xml">The XML string to transform.</param>
    /// <returns>The transformed XML string.</returns>
    public static string TransformXml(XslCompiledTransform transform, string xml)
    {
        using var reader = new StringReader(xml);
        using var writer = new StringWriter();
        using var xmlTextReader = new XmlTextReader(reader);
        using var xmlTextWriter = new XmlTextWriter(writer)
        {
            Formatting = Formatting.Indented,
            Indentation = 4
        };

        transform.Transform(xmlTextReader, xmlTextWriter);

        return writer.ToString();
    }

    /// <summary>
    /// Transforms XML using an XSL resource string.
    /// </summary>
    /// <param name="xslResource">The XSL transformation resource string.</param>
    /// <param name="xml">The XML string to transform.</param>
    /// <returns>The transformed XML string.</returns>
    public static string TransformXmlWithResource(string xslResource, string xml)
    {
        var transform = new XslCompiledTransform();
        using var stringReader = new StringReader(xslResource);
        using var xmlReader = XmlReader.Create(stringReader);

        transform.Load(xmlReader);

        return TransformXml(transform, xml);
    }
}