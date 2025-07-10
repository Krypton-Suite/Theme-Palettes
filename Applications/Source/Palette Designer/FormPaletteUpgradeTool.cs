#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 */
#endregion

using System.Drawing;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace PaletteDesigner
{
    public partial class FormPaletteUpgradeTool : KryptonForm
    {
        #region Static Fields

        private const int MINIMUM_PALETTE_FILE_VERSION = 2;

        private const int MAXIMUM_PALETTE_FILE_VERSION = GlobalStaticValues.CURRENT_SUPPORTED_PALETTE_VERSION;

        #endregion

        #region Public

        private int _inputVersionNumber;

        #endregion

        #region Identity

        public FormPaletteUpgradeTool()
        {
            InitializeComponent();

            SetInputVersionNumber(-1);

            UpdateState();
        }

        #endregion

        #region Setters and Getters
        /// <summary>Sets the InputVersionNumber to the value of value.</summary>
        /// <param name="value">The desired value of InputVersionNumber.</param>
        private void SetInputVersionNumber(int value) => _inputVersionNumber = value;

        /// <summary>Returns the value of the InputVersionNumber.</summary>
        /// <returns>The value of the InputVersionNumber.</returns>
        private int GetInputVersionNumber() => _inputVersionNumber;

        #endregion

        #region Implementation

        /// <summary>
        /// Gets the palette file number.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private int GetPaletteFileNumber(string fileName)
        {
            try
            {
                XPathNavigator xPathNavigator = (new XPathDocument(fileName)).CreateNavigator().SelectSingleNode("KryptonPalette");

                if (xPathNavigator != null)
                {
                    string attribute = xPathNavigator.GetAttribute("Version", string.Empty);

                    if (!string.IsNullOrEmpty(attribute))
                    {
                        return int.Parse(attribute);
                    }
                }
            }
            catch (Exception exc)
            {
                KryptonMessageBox.Show(this,
                    $"Error: {exc.Message}",
                    "Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }

            return -1;
        }

        /// <summary>
        /// Transforms the XML.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        private static string TransformXml(XslCompiledTransform transform, string xml)
        {
            using (var reader = new StringReader(xml))

            using (var writer = new StringWriter())

            using (var xmlReader = new XmlTextReader(reader))

            using (var xmlWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented, Indentation = 4 })
            {
                transform.Transform(xmlReader, xmlWriter);

                return writer.ToString();
            }
        }

        /// <summary>
        /// Updates the UI state.
        /// </summary>
        private void UpdateState()
        {
            bool length = krtbInput.Text.Length > 0;
            bool flag0 = ValidOutputDirectory(krtbOutput.Text);
            bool flag1 = ValidOutputFilename(krtbOutput.Text);
            bool flag2 = (GetInputVersionNumber() >= MINIMUM_PALETTE_FILE_VERSION && GetInputVersionNumber() < MAXIMUM_PALETTE_FILE_VERSION);

            kbtnUpgrade.Enabled = (length && flag0 && flag1 && flag2);

            if (kbtnUpgrade.Enabled)
            {
                klblStatus.ForeColor = Color.Green;

                klblStatus.Text = $"Convert to output version ' {MAXIMUM_PALETTE_FILE_VERSION}'.";

                return;
            }

            if (!length)
            {
                klblStatus.Text = "You must select a valid input file.";
            }
            else if (!flag0)
            {
                klblStatus.Text = "Must select a valid output directory.";
            }
            else if (!flag1)
            {
                klblStatus.Text = "Must select a valid output filename.";
            }
            else if (flag2)
            {
                klblStatus.Text = "Must select valid input and output files.";
            }
            else
            {
                klblStatus.Text = "Input file format version cannot be upgraded.";
            }

            klblStatus.ForeColor = Color.Red;
        }

        /// <summary>
        /// Validates the output directory.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private bool ValidOutputDirectory(string filename)
        {
            try
            {
                return (!string.IsNullOrWhiteSpace(filename)
                    && (new DirectoryInfo((new FileInfo(filename)).DirectoryName)).Exists
                    );
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the output filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private bool ValidOutputFilename(string filename)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filename))
                {
                    return false;
                }
                var fileInfo = new FileInfo(filename);
                return (!fileInfo.Exists || !fileInfo.IsReadOnly);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        private void kbtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void kbtnUpgrade_Click(object sender, EventArgs e)
        {
            try
            {
                // Read the original palette XML
                string xml;
                using (var reader = new StreamReader(krtbInput.Text))
                {
                    xml = reader.ReadToEnd();
                }

                // Apply the required transformation(s)
                if (GetInputVersionNumber() < 6)
                {
                    var transform = new XslCompiledTransform();
                    transform.Load(new XmlTextReader(new StringReader(Resources.v2to6)));
                    xml = TransformXml(transform, xml);
                }
                else if (GetInputVersionNumber() < MAXIMUM_PALETTE_FILE_VERSION)
                {
                    var transform = new XslCompiledTransform();

                    using (var sr = new StringReader(Resources.v6to20))

                    using (var xr = XmlReader.Create(sr))
                    {
                        transform.Load(xr);
                    }
                    xml = TransformXml(transform, xml);
                }

                // Write the upgraded XML to the chosen output file
                using (var writer = new StreamWriter(krtbOutput.Text, false))
                {
                    writer.WriteLine("<?xml version=\"1.0\"?>");
                    writer.Write(xml);
                }

                string message = $"Input file: {krtbInput.Text}\nOutput file: {krtbOutput.Text}\n\nUpgrade from version '{_inputVersionNumber}' to version '{MAXIMUM_PALETTE_FILE_VERSION}' has succeeded.";

                KryptonMessageBox.Show(this, message, "Upgrade Success", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);

                kbtnUpgrade.Enabled = false;
            }
            catch (Exception exc)
            {
                KryptonMessageBox.Show(this, $"Error: {exc.Message}", "Upgrade Error", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private void kcmdBrowseForOriginalFile_Execute(object sender, EventArgs e)
        {
            KryptonOpenFileDialog openFileDialog = new()
            {
                Title = @"Open an existing Krypton palette file:",
                Filter = @"Krypton palette XML files (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                int paletteFileVersionNumber = GetPaletteFileNumber(openFileDialog.FileName);

                switch (paletteFileVersionNumber)
                {
                    case -1:
                        KryptonMessageBox.Show(this,
                           $"File: '{openFileDialog.FileName}' does not contain a valid palette definition.",
                           "Select Palette",
                           KryptonMessageBoxButtons.OK,
                           KryptonMessageBoxIcon.Warning);
                        break;
                    case < MINIMUM_PALETTE_FILE_VERSION:
                        {
                            string[] fileName = ["File '", openFileDialog.FileName, "' contains palette format version '", paletteFileVersionNumber.ToString(), "'.\nPalette upgrade tool can only upgrade version '", MINIMUM_PALETTE_FILE_VERSION.ToString(), "' and upwards."
                            ];

                            KryptonMessageBox.Show(this,
                                string.Concat(fileName),
                                "Incompatible Version",
                                KryptonMessageBoxButtons.OK,
                                KryptonMessageBoxIcon.Warning);
                            break;
                        }
                    case < MAXIMUM_PALETTE_FILE_VERSION:
                        {
                            krtbInput.Text = openFileDialog.FileName;

                            SetInputVersionNumber(paletteFileVersionNumber);

                            string directoryName = Path.GetDirectoryName(openFileDialog.FileName) ?? string.Empty;
                            string baseName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                            string extension = Path.GetExtension(openFileDialog.FileName);

                            krtbOutput.Text = Path.Combine(directoryName, $"{baseName}_v{MAXIMUM_PALETTE_FILE_VERSION}{extension}");
                            break;
                        }
                    default:
                        {
                            string[] fileName1 = ["File '", openFileDialog.FileName, "' contains palette format version '", paletteFileVersionNumber.ToString(), "'.\nPalette upgrade tool can only upgrade version '", 17.ToString(), "' and below."
                            ];

                            KryptonMessageBox.Show(this,
                                string.Concat(fileName1),
                                "Incompatible Version",
                                KryptonMessageBoxButtons.OK,
                                KryptonMessageBoxIcon.Warning);
                            break;
                        }
                }

                UpdateState();
            }
        }

        private void krtbOutput_TextChanged(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void bsaBrowseForOriginal_Click(object sender, EventArgs e)
        {
            kcmdBrowseForOriginalFile.PerformExecute();
        }

        private void kcmdBrowseForUpgradedFile_Execute(object sender, EventArgs e)
        {
            KryptonSaveFileDialog saveFileDialog = new()
            {
                Title = @"Save Krypton palette file as:",
                Filter = @"Krypton palette XML files (*.xml)|*.xml",
                InitialDirectory = Path.GetDirectoryName(krtbOutput.Text),
                FileName = Path.GetFileName(krtbOutput.Text)
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                krtbOutput.Text = Path.GetFullPath(saveFileDialog.FileName);
            }
        }
    }
}
