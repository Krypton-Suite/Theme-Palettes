#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

using Microsoft.Win32;

namespace PaletteDesigner
{
    /// <summary>
    /// Deals with the back-end logic of a most recently used file <see cref="ToolStripMenuItem"/>.
    /// Adapted from (https://www.codeproject.com/Articles/407513/Add-Most-Recently-Used-Files-MRU-List-to-Windows).
    /// </summary>
    public class MostRecentlyUsedDocumentsManager
    {
        #region Private members

        private string _nameOfProgram;

        private string _subKeyName;

        private string _filePath;

        private ToolStripMenuItem _parentMenuItem;

        private Action<object, EventArgs>? _onRecentFileClick;

        private Action<object, EventArgs>? _onClearRecentFilesClick;

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath
        {
            get => _filePath;

            set => _filePath = value;
        }

        /// <summary>
        /// Called when [clear recent files click].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="evt">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnClearRecentFiles_Click(object obj, EventArgs evt)
        {
            try
            {
                DialogResult result = KryptonMessageBox.Show(
                    "You are about to clear your recent files list. Do you want to continue?",
                    "Clear Recent Files",
                    KryptonMessageBoxButtons.YesNo,
                    KryptonMessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ClearRecentFiles();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            _onClearRecentFilesClick?.Invoke(obj, evt);
        }

        /// <summary>
        /// Clears the recent files.
        /// </summary>
        private void ClearRecentFiles()
        {
            try
            {
                RegistryKey rK = Registry.CurrentUser.OpenSubKey(_subKeyName, true);

                if (rK == null)
                {
                    return;
                }

                var values = rK.GetValueNames();

                foreach (var valueName in values)
                {
                    rK.DeleteValue(valueName, true);
                }

                rK.Close();

                _parentMenuItem.DropDownItems.Clear();

                _parentMenuItem.Enabled = false;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Error: {ex.Message}",
                    "Unexpected Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refreshes the recent files menu.
        /// </summary>
        private void RefreshRecentFilesMenu()
        {
            RegistryKey rK;

            string s;

            ToolStripItem tSi;

            try
            {
                rK = Registry.CurrentUser.OpenSubKey(this._subKeyName, false);

                if (rK == null)
                {
                    _parentMenuItem.Enabled = false;

                    return;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Error: {ex.Message}",
                    "Unexpected Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);

                return;
            }

            _parentMenuItem.DropDownItems.Clear();

            var valueNames = rK.GetValueNames();

            foreach (var valueName in valueNames)
            {
                s = rK.GetValue(valueName, null) as string;

                if (s == null)
                {
                    continue;
                }

                tSi = _parentMenuItem.DropDownItems.Add(s);

                tSi.Click += new EventHandler(_onRecentFileClick);
            }

            if (_parentMenuItem.DropDownItems.Count == 0)
            {
                _parentMenuItem.Enabled = false;

                return;
            }

            _parentMenuItem.DropDownItems.Add("-");

            tSi = _parentMenuItem.DropDownItems.Add("&Clear list");

            tSi.Click += OnClearRecentFiles_Click;

            _parentMenuItem.Enabled = true;
        }
        #endregion

        #region Public members
        /// <summary>
        /// Adds the recent file.
        /// </summary>
        /// <param name="fileNameWithFullPath">The file name with full path.</param>
        public void AddRecentFile(string fileNameWithFullPath)
        {
            string s;

            try
            {
                RegistryKey rK = Registry.CurrentUser.CreateSubKey(_subKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);

                for (var i = 0; true; i++)
                {
                    s = rK.GetValue(i.ToString(), null) as string;

                    if (s == null)
                    {
                        rK.SetValue(i.ToString(), fileNameWithFullPath);

                        rK.Close();

                        break;
                    }
                    else if (s == fileNameWithFullPath)
                    {
                        rK.Close();

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Error: {ex.Message}",
                    "Unexpected Error",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }

            RefreshRecentFilesMenu();
        }


        /// <summary>
        /// Removes the recent file.
        /// </summary>
        /// <param name="fileNameWithFullPath">The file name with full path.</param>
        public void RemoveRecentFile(string fileNameWithFullPath)
        {
            try
            {
                RegistryKey rK = Registry.CurrentUser.OpenSubKey(_subKeyName, true);

                var valuesNames = rK.GetValueNames();

                foreach (var valueName in valuesNames)
                {
                    if ((rK.GetValue(valueName, null) as string) == fileNameWithFullPath)
                    {
                        rK.DeleteValue(valueName, true);

                        RefreshRecentFilesMenu();

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            RefreshRecentFilesMenu();
        }
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="MostRecentlyUsedDocumentsManager"/> class.
        /// </summary>
        /// <param name="parentMenuItem">The parent menu item.</param>
        /// <param name="nameOfProgram">The name of program.</param>
        /// <param name="onRecentFileClick">The on recent file click.</param>
        /// <param name="onClearRecentFilesClick">The on clear recent files click.</param>
        /// <param name="useConfirmClearListDialogue">if set to <c>true</c> [use confirm clear list dialogue].</param>
        /// <exception cref="ArgumentException">Bad argument.</exception>
        public MostRecentlyUsedDocumentsManager(ToolStripMenuItem parentMenuItem, string nameOfProgram, Action<object, EventArgs> onRecentFileClick, Action<object, EventArgs> onClearRecentFilesClick = null, bool useConfirmClearListDialogue = true)
        {
            if (parentMenuItem == null || onRecentFileClick == null || nameOfProgram == null || nameOfProgram.Length == 0 || nameOfProgram.Contains("\\"))
            {
                throw new ArgumentException("Bad argument.");
            }

            _parentMenuItem = parentMenuItem;

            _nameOfProgram = nameOfProgram;

            _onRecentFileClick = onRecentFileClick;

            _onClearRecentFilesClick = onClearRecentFilesClick;

            _subKeyName = string.Format($"Software\\{_nameOfProgram}\\MRU");

            RefreshRecentFilesMenu();
        }
        #endregion
    }
}