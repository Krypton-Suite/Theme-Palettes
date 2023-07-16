#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

using System.Drawing;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

namespace PaletteDesigner
{
    public partial class FormPaletteUpgradeTool : KryptonForm
    {
        #region Static Fields

        private const int MINIMUM_PALETTE_FILE_VERSION = 2;

        private const int MAXIMUM_PALETTE_FILE_VERSION = 20;

        #endregion

        #region Public

        public int InputVersionNumber { get; set; }

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
        private void SetInputVersionNumber(int value) => InputVersionNumber = value;

        /// <summary>Returns the value of the InputVersionNumber.</summary>
        /// <returns>The value of the InputVersionNumber.</returns>
        private int GetInputVersionNumber() => InputVersionNumber;

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
        private string TransformXml(XslCompiledTransform transform, string xml)
        {
            StringReader reader = new StringReader(xml);

            StringWriter writer = new StringWriter();

            XmlTextReader xmlTextReader = new XmlTextReader(reader);

            XmlTextWriter xmlTextWriter = new XmlTextWriter(writer)
            {
                Formatting = Formatting.Indented,
                Indentation = 4
            };

            transform.Transform(xmlTextReader, xmlTextWriter);

            return writer.ToString();
        }

        /// <summary>
        /// Updates the UI state.
        /// </summary>
        private void UpdateState()
        {
            bool length = krtbInput.Text.Length > 0;
            bool flag0 = ValidOutputDirectory(krtbOutput.Text);
            bool flag1 = ValidOutputFilename(krtbOutput.Text);
            bool flag2 = (GetInputVersionNumber() >= MINIMUM_PALETTE_FILE_VERSION && GetInputVersionNumber() <= MAXIMUM_PALETTE_FILE_VERSION);

            kbtnUpgrade.Enabled = (length && flag0 && flag1 && flag2);

            if (kbtnUpgrade.Enabled)
            {
                klblStatus.ForeColor = Color.Green;

                int num = MAXIMUM_PALETTE_FILE_VERSION + 1;

                klblStatus.Text = $"Convert to output version ' {num}'.";

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
                StreamReader reader = new StreamReader(krtbInput.Text);

                string end = reader.ReadToEnd();

                reader.Close();

                if (GetInputVersionNumber() < 6)
                {
                    var xslCompiledTransform = new XslCompiledTransform();

                    xslCompiledTransform.Load(new XmlTextReader(new StringReader(Resources.v2to6)));
                    end = TransformXml(xslCompiledTransform, end);
                }
                else if (GetInputVersionNumber() <= MAXIMUM_PALETTE_FILE_VERSION)
                {
                    var streamReader = new StringReader(Resources.v6to19);
                    var xmlTextReader = XmlReader.Create(streamReader);
                    var xslCompiledTransform1 = new XslCompiledTransform();
                    xslCompiledTransform1.Load(xmlTextReader);
                    end = TransformXml(xslCompiledTransform1, end);
                }

                var writer = new StreamWriter(krtbOutput.Text);

                writer.WriteLine("<?xml version=\"1.0\"?>");

                writer.Write(end);

                writer.Flush();

                writer.Close();

                object[] text = new object[] { "Input file: ", krtbInput.Text, "\nOutput file: ", krtbOutput.Text, "\n\nUpgrade from version '", InputVersionNumber, "' to version '", 19.ToString(), "' has succeeded." };

                KryptonMessageBox.Show(this, string.Concat(text), "Upgrade Success", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);

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
                Title = @"Open a Existing Krypton Palette File:",
                Filter = @"Krypton Palette XML Files (*.xml)|*.xml"
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
                            string[] fileName = new string[] { "File '", openFileDialog.FileName, "' contains palette format version '", paletteFileVersionNumber.ToString(), "'.\nPalette upgrade tool can only upgrade version '", MINIMUM_PALETTE_FILE_VERSION.ToString(), "' and upwards." };

                            KryptonMessageBox.Show(this,
                                string.Concat(fileName),
                                "Incompatible Version",
                                KryptonMessageBoxButtons.OK,
                                KryptonMessageBoxIcon.Warning);
                            break;
                        }
                    case <= MAXIMUM_PALETTE_FILE_VERSION:
                        {
                            krtbInput.Text = openFileDialog.FileName;

                            SetInputVersionNumber(paletteFileVersionNumber);

                            FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                            if (fileInfo != null)
                            {
                                string str = (fileInfo.Name.IndexOf(fileInfo.Extension) <= 0 ? fileInfo.Name : fileInfo.Name.Substring(0, fileInfo.Name.IndexOf(fileInfo.Extension)));

                                string directoryName = fileInfo.DirectoryName;

                                if (!directoryName.EndsWith("\\"))
                                {
                                    directoryName = string.Concat(directoryName, "\\");
                                }

                                KryptonRichTextBox richTextBox = krtbOutput;

                                string[] strArrays = new string[] { directoryName, str, "_v", (MAXIMUM_PALETTE_FILE_VERSION + 1).ToString(), fileInfo.Extension };

                                richTextBox.Text = string.Concat(strArrays);
                            }

                            break;
                        }
                    default:
                        {
                            string[] fileName1 = new string[] { "File '", openFileDialog.FileName, "' contains palette format version '", paletteFileVersionNumber.ToString(), "'.\nPalette upgrade tool can only upgrade version '", 17.ToString(), "' and below." };

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
                Title = @"Save Krypton Palette File:",
                Filter = @"Krypton Palette XML Files (*.xml)|*.xml"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(Path.GetFullPath(saveFileDialog.FileName));

                writer.Write(krtbOutput.Text);

                writer.Close();

                writer.Dispose();
            }
        }
    }
}
