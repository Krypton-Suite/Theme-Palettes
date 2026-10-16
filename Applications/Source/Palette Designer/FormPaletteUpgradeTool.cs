#region BSD License
/*
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner;

public partial class FormPaletteUpgradeTool : KryptonForm
{
    #region Static Fields

    private const int MINIMUM_PALETTE_FILE_VERSION = 2;

    private const int MAXIMUM_PALETTE_FILE_VERSION = SharedStaticConstants.CURRENT_SUPPORTED_PALETTE_VERSION;

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
    private int GetPaletteFileNumber(string? fileName)
    {
        if (fileName != null)
        {
            XPathNavigator? xPathNavigator = new XPathDocument(fileName).CreateNavigator().SelectSingleNode("KryptonPalette");
            try
            {
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
        }

        return -1;
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
            if (string.IsNullOrWhiteSpace(filename))
            {
                return false;
            }

            var dirName = new FileInfo(filename).DirectoryName;
            return !string.IsNullOrWhiteSpace(dirName) && new DirectoryInfo(dirName).Exists;
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
            PaletteUpgradeUtilities.UpgradeFile(krtbInput.Text, krtbOutput.Text);

            string message = $"Input file: {krtbInput.Text}\nOutput file: {krtbOutput.Text}\n\nUpgrade from version '{_inputVersionNumber}' to version '{MAXIMUM_PALETTE_FILE_VERSION}' has succeeded.";

            KryptonMessageBox.Show(this, message, "Upgrade Success",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);

            kbtnUpgrade.Enabled = false;
        }
        catch (Exception exc)
        {
            KryptonMessageBox.Show(this, $"Error: {exc.Message}", "Upgrade Error",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
        }
    }

    private void kcmdBrowseForOriginalFile_Execute(object sender, EventArgs e)
    {
        KryptonOpenFileDialog openFileDialog = new()
        {
            Title = @"Open an existing Krypton palette file:",
            Filter = @"Krypton palette XML files (*.kthemex;*.xml)|*.kthemex;*.xml",
            DefaultExt = KryptonPaletteFile.Extension
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
                        // who came up with this idea?!
                        string[] fileName = ["File '", openFileDialog.FileName,
                            "' contains palette format version '",
                            paletteFileVersionNumber.ToString(),
                            "'.\nPalette upgrade tool can only upgrade version '",
                            MINIMUM_PALETTE_FILE_VERSION.ToString(),
                            "' and upwards."
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
                        string[] fileName1 = ["File '",
                            openFileDialog.FileName,
                            "' contains palette format version '",
                            paletteFileVersionNumber.ToString(),
                            "'.\nPalette upgrade tool can only upgrade version '",
                            17.ToString(),
                            "' and below."
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
            Filter = @"Krypton palette XML files (*.kthemex;*.xml)|*.kthemex;*.xml",
            DefaultExt = KryptonPaletteFile.Extension,
            InitialDirectory = Path.GetDirectoryName(krtbOutput.Text),
            FileName = Path.GetFileName(krtbOutput.Text)
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            krtbOutput.Text = Path.GetFullPath(saveFileDialog.FileName);
        }
    }
}