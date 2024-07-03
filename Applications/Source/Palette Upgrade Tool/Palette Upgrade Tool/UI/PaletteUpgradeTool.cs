using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace PaletteUpgradeTool
{
    public partial class PaletteUpgradeTool : KryptonForm
    {
        public PaletteUpgradeTool()
        {
            InitializeComponent();

            bsaInputDirectory.Click += InputDirectory_Click;

            bsaOutputDirectory.Click += OutputDirectory_Click;
        }

        private void OutputDirectory_Click(object sender, EventArgs e)
        {
            kcmdOutputDirectory.PerformExecute();
        }

        private void InputDirectory_Click(object sender, EventArgs e)
        {
            kcmdInputDirectory.PerformExecute();
        }

        private void kcmdInputDirectory_Execute(object sender, EventArgs e)
        {
            if (kmMain.UseKryptonFileDialogs)
            {
                KryptonFolderBrowserDialog dialog = new KryptonFolderBrowserDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ktxtInputDirectory.Text = Path.GetFullPath(dialog.SelectedPath);
                }
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ktxtInputDirectory.Text = Path.GetFullPath(dialog.SelectedPath);
                }
            }
        }

        private void kcmdOutputDirectory_Execute(object sender, EventArgs e)
        {

        }

        private void kbtnOptions_Click(object sender, EventArgs e)
        {

        }

        private void kbtnUpgrade_Click(object sender, EventArgs e)
        {
            // Validate output path to check if it exists
            ValidateOutputPath(ktxtOutputDirectory.Text);

            // Loop through the file array
            foreach (var paletteFile in GetPaletteFiles(ktxtInputDirectory.Text))
            {
                // Upgrade the file and store it into the newly created directory
                kcpbUpgrader.ImportWithUpgrade(File.OpenRead(Path.GetFullPath(paletteFile)));

                // Export the file
                kcpbUpgrader.Export(File.OpenWrite($"{ktxtOutputDirectory}\\{paletteFile}.xml"), false);
            }
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
            if (ktxtInputDirectory.Text.EndsWith("\\"))
            {
                // Fill listbox
                foreach (var paletteFile in GetPaletteFiles(ktxtInputDirectory.Text))
                {
                    klbFiles.Items.Add(Path.GetFullPath(paletteFile));
                }
            }
        }
    }
}
