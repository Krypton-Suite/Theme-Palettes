#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace PaletteDesigner.Utilities;

/// <summary>
/// Wrapper around Cyotek ColorPickerDialog that adds a bottom checkbox for enabling live theme updates and
/// fires <see cref="LiveColorChanged"/> whenever the user changes the colour while the checkbox is checked.
/// </summary>
internal sealed class LiveColorPickerDialog : Cyotek.Windows.Forms.ColorPickerDialog
{
    private readonly CheckBox _chkLive;
    private readonly FlowLayoutPanel _bottomPanel;
    private readonly Button _btnReset;
    private Color _initialColor;
    private bool _panelAdded;

    public LiveColorPickerDialog()
    {
        // Create checkbox – will be added once handle created so we know size of dialog.
        _chkLive = new CheckBox
        {
            AutoSize = true,
            Text = "Live theme updates?",
            Padding = new Padding(0, 0, 20, 0),
            Checked = false
        };

        _btnReset = new Button
        {
            Text = "Reset",
            AutoSize = true,
            Margin = new Padding(0, -3, 0, 0) // up 3px, left shift will be handled by checkbox margin
        };
        _btnReset.Click += (_, __) => { Color = _initialColor; };

        // Reduce right margin of checkbox to pull Reset button leftwards (~40px)
        _chkLive.Margin = new Padding(0, 0, -40, 0);

        _bottomPanel = new FlowLayoutPanel
        {
            AutoSize = true,
            Dock = DockStyle.Bottom,
            FlowDirection = FlowDirection.LeftToRight,
            Padding = new Padding(10, 6, 10, 6)
        };
        _bottomPanel.Controls.Add(_chkLive);
        _bottomPanel.Controls.Add(_btnReset);

        _pollTimer = new Timer { Interval = 150, Enabled = true };
        _pollTimer.Tick += (_, __) => OnColorChangedInternal();
    }

    /// <summary>
    /// Raised when the user changes the colour and live-update is enabled.
    /// </summary>
    public event EventHandler<LiveColorChangedEventArgs>? LiveColorChanged;

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        if (!_panelAdded)
        {
            _panelAdded = true;
            _initialColor = Color;

            Controls.Add(_bottomPanel);
            _bottomPanel.BringToFront();
            // Ensure dialog is tall enough for the new bottom panel
            Height += _bottomPanel.PreferredSize.Height;
        }
    }

    private readonly Timer _pollTimer;

    private void OnColorChangedInternal()
    {
        if (_chkLive.Checked)
        {
            if (Color != _lastReportedColor)
            {
                _lastReportedColor = Color;
                LiveColorChanged?.Invoke(this, new LiveColorChangedEventArgs(Color));
            }
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Attempt to locate the hex textbox inside the dialog
        var hexBox = FindHexTextBox(this);
        if (hexBox != null)
        {
            hexBox.TextChanged -= HexBox_TextChanged;
            hexBox.TextChanged += HexBox_TextChanged;
        }
    }

    private static TextBox? FindHexTextBox(Control root)
    {
        foreach (Control c in root.Controls)
        {
            if (c is TextBox tb && (tb.Name?.ToLower().Contains("hex") == true || tb.AccessibleName?.ToLower().Contains("hex") == true))
            {
                return tb;
            }
            var child = FindHexTextBox(c);
            if (child != null) return child;
        }
        return null;
    }

    private void HexBox_TextChanged(object? sender, EventArgs e)
    {
        if (sender is not TextBox tb) return;

        string txt = tb.Text.Trim();
        if (txt.Count(ch => ch == ';') == 2)
        {
            var parts = txt.Split(';');
            if (parts.Length == 3 &&
                byte.TryParse(parts[0].Trim(), out byte r) &&
                byte.TryParse(parts[1].Trim(), out byte g) &&
                byte.TryParse(parts[2].Trim(), out byte b))
            {
                string hex = $"#{r:X2}{g:X2}{b:X2}";
                tb.Text = hex;
                tb.SelectionStart = tb.Text.Length;
            }
        }
    }

    private Color _lastReportedColor;

    internal sealed class LiveColorChangedEventArgs : EventArgs
    {
        public LiveColorChangedEventArgs(Color color) => Color = color;

        public Color Color { get; }
    }
}