using System;
using System.IO;
using System.Windows.Forms;
using Krypton.Toolkit;
using PaletteUpgradeTool.Properties;

namespace PaletteUpgradeTool
{
    public partial class PaletteUpgradeTool : KryptonForm
    {
        public PaletteUpgradeTool()
        {
            InitializeComponent();

            Icon = Resources.Krypton;
        }

        private void kbtnOptions_Click(object sender, EventArgs e)
        {
            var options = new PaletteUpgradeToolOptions();

            options.ShowDialog();
        }

        private void bsaBrowseInputDirectory_Click(object sender, EventArgs e)
        {
            if (kryptonManager1.UseKryptonFileDialogs)
            {
                KryptonFolderBrowserDialog dialog = new KryptonFolderBrowserDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ktxtInputDirectory.Text = $@"{Path.GetFullPath(dialog.SelectedPath)}\";
                }
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ktxtInputDirectory.Text = $@"{Path.GetFullPath(dialog.SelectedPath)}\";
                }
            }

            FillListBox();
        }

        private void bsaBrowseOutputDirectory_Click(object sender, EventArgs e)
        {
            if (kryptonManager1.UseKryptonFileDialogs)
            {
                KryptonFolderBrowserDialog dialog = new KryptonFolderBrowserDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ktxtOutputDirectory.Text = $"{Path.GetFullPath(dialog.SelectedPath)}\\Palette Version {GlobalStaticValues.CURRENT_SUPPORTED_PALETTE_VERSION}\\";
                }
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ktxtOutputDirectory.Text = $@"{Path.GetFullPath(dialog.SelectedPath)}\Palette Version {GlobalStaticValues.CURRENT_SUPPORTED_PALETTE_VERSION}\";
                }
            }

            kbtnUpgrade.Enabled = true;
        }

        private void kbtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void kbtnUpgrade_Click(object sender, EventArgs e)
        {
            //try
            //{
                UpgradePalettes(ktxtInputDirectory.Text, ktxtOutputDirectory.Text);
            //}
            //catch (Exception exception)
            //{
            //    KryptonMessageBox.Show($"{exception}");
            //}
        }

        private void ValidateOutputPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string[] GetPaletteFiles(string path) => Directory.GetFiles(path);

        private void ktxtInputDirectory_TextChanged(object sender, EventArgs e)
        {
            FillListBox();
        }

        private void FillListBox()
        {
            // Clear list box first
            if (klbFiles.Items.Count > 0)
            {
                klbFiles.Items.Clear();
            }

            if (ktxtInputDirectory.Text.EndsWith("\\"))
            {
                // Fill listbox
                foreach (var paletteFile in GetPaletteFiles(ktxtInputDirectory.Text))
                {
                    klbFiles.Items.Add(Path.GetFullPath(paletteFile));
                }
            }
        }

        private void UpgradePalettes(string inputPath, string outputPath)
        {
            // Validate output path to check if it exists
            ValidateOutputPath(outputPath);

            // Loop through the file array
            foreach (var paletteFile in GetPaletteFiles(inputPath))
            {
                // Set a temporary file name
                string tempFileName = Path.GetFileName(paletteFile);

                // Upgrade the file and store it into the newly created directory
                kcpbUpgrader.ImportWithUpgrade(File.OpenRead(Path.GetFullPath(paletteFile)));

                // Export the file
                kcpbUpgrader.Export(File.OpenWrite($"{outputPath}\\{tempFileName}"), false);

                tempFileName = string.Empty;
            }
        }
    }
}
