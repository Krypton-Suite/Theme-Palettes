#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved.
 *
 */
#endregion


using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace PaletteDesigner
{
    internal class PaletteUpgradeUtilities
    {
        #region Identity

        public PaletteUpgradeUtilities()
        {

        }

        #endregion

        #region Implementation

        public static void PerformUpgrade(Stream inputStream)
        {
            try
            {
                using var reader = new StreamReader(inputStream);

                string end = reader.ReadToEnd();

                reader.Close();

                using (var streamReader = new StreamReader(Resources.v6to19))
                {
                    using (var xmlTextReader = XmlReader.Create(streamReader))
                    {
                        var xslCompiledTransform = new XslCompiledTransform();

                        xslCompiledTransform.Load(xmlTextReader);

                        end = TransformXml(xslCompiledTransform, end);
                    }
                }

                using var ms = new MemoryStream();

                using (var writer = new StreamWriter(ms, new UTF8Encoding(false, true), 1024, true))
                {
                    writer.WriteLine("<?xml version=\"1.0\"?>");
                    writer.Write(end);
                    writer.Flush();
                    writer.Close();
                }

                ms.Position = 0;
            }
            catch (Exception e)
            {

            }
        }

        private static string TransformXml(XslCompiledTransform transform, string xml)
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

        #endregion
    }
}