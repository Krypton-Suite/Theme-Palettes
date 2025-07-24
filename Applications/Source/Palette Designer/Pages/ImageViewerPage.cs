#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace PaletteDesigner.Pages;

public partial class ImageViewerPage : UserControl
{
    #region Variables

    private Image? _currentImage;
    private Point _lastMousePos;
    private bool _mouseInside;
    private readonly System.Windows.Forms.Timer _uiTimer = new() { Interval = 50 }; // 100ms refresh
    private bool _showCrosshair = true;
    private Cursor? _prevCursor;

    // Persistence fields
    private string? _lastImagePath;
    private string? _lastRegionPath;
    private string? _lastImageFolder;

    // Region handling
    private readonly List<RegionInfo> _regions = [];
    private bool _regionsDirty;
    private bool _regionMode;
    private bool _isDrawing;
    private Point _startPt;
    private PointF _startImgPt;
    private RegionInfo? _currentRegionDraft;
    private RegionInfo? _selectedRegion;
    private bool _spacePanMode;

    public event EventHandler<ColorSampledEventArgs>? ColorSampled;

    public string? LastImagePath => _lastImagePath;
    public string? LastRegionPath => _lastRegionPath;
    public string? LastImageFolder => _lastImageFolder;

    #endregion Variables

    /// <summary>
    /// Indicates unsaved changes in the regions collection.
    /// </summary>
    public bool HasUnsavedRegionChanges => _regionsDirty;

    #region Identity

    public ImageViewerPage()
    {
        InitializeComponent();

        InitialiseTimer();

        try
        {
            // Ensure 10% granularity for zoom levels up to 800%
            if (imageBox.ZoomLevels != null)
            {
                imageBox.ZoomLevels.Clear();
                for (int i = 1; i <= 80; i++)
                {
                    imageBox.ZoomLevels.Add(i * 10);
                }
            }
        }
        catch { /* ignore if ZoomLevels collection not available */ }
    }

    #endregion Identity

    #region ImageBox Event Handlers

    private void ImageBox_MouseLeave(object? sender, EventArgs e)
    {
        _mouseInside = false;
        _uiTimer.Stop();
        imageBox.Invalidate();
    }

    private void ImageBox_MouseEnter(object? sender, EventArgs e)
    {
        _mouseInside = true;
        if (!_uiTimer.Enabled)
        {
            _uiTimer.Start();
        }
    }

    private void ImageBox_ZoomChanged(object? sender, EventArgs e)
    {
        statusZoomLabel.Text = $"Zoom: {imageBox.Zoom}%";
    }

    private void ImageBox_Paint(object? sender, PaintEventArgs e)
    {
        if (_currentImage == null)
        {
            return;
        }

        // Draw crosshair if enabled
        if (_mouseInside && _showCrosshair)
        {
            using var crosshairPen = new Pen(Color.Red, 1);
            // vertical line
            e.Graphics.DrawLine(crosshairPen, new Point(_lastMousePos.X, 0), new Point(_lastMousePos.X, imageBox.Height));
            // horizontal line
            e.Graphics.DrawLine(crosshairPen, new Point(0, _lastMousePos.Y), new Point(imageBox.Width, _lastMousePos.Y));
        }

        // Draw regions
        using var regionPen = new Pen(Color.LimeGreen, 2);
        using var selPen = new Pen(Color.Red, 2);
        foreach (var r in _regions)
        {
            var dispRect = ImageRectToDisplay(r.Rect);
            var pen = ReferenceEquals(r, _selectedRegion) ? selPen : regionPen;
            e.Graphics.DrawRectangle(pen, dispRect);
            e.Graphics.DrawString(r.Label, Font, Brushes.LimeGreen, dispRect.Location);
        }

        if (_currentRegionDraft != null)
        {
            var dispRect = ImageRectToDisplay(_currentRegionDraft.Rect);
            e.Graphics.DrawRectangle(selPen, dispRect);
        }
    }

    private void ImageBox_MouseMove(object? sender, MouseEventArgs e)
    {
        // Always track mouse position for crosshair and status
        _lastMousePos = e.Location;
        _mouseInside = true;

        // If currently drawing a region, update the draft rectangle
        if (_isDrawing && _currentRegionDraft != null)
        {
            // Use high-precision conversion to image coordinates
            var currImg = DisplayPointToImage(e.Location);
            float left = Math.Min(_startImgPt.X, currImg.X);
            float top = Math.Min(_startImgPt.Y, currImg.Y);
            float right = Math.Max(_startImgPt.X, currImg.X);
            float bottom = Math.Max(_startImgPt.Y, currImg.Y);
            _currentRegionDraft.Rect = RectangleF.FromLTRB(left, top, right, bottom);
        }

        // Request repaint so crosshair/region updates render
        imageBox.Invalidate();
    }

    private void ImageBox_MouseClick(object? sender, MouseEventArgs e)
    {
        if (_currentImage == null)
        {
            return;
        }

        // If region mode is active and not currently drawing, handle region selection
        if (_regionMode && !_isDrawing)
        {
            var imgPtSel = DisplayPointToImage(e.Location);
            var hit = _regions.FirstOrDefault(r => r.Rect.Contains(imgPtSel));
            _selectedRegion = hit;
            imageBox.Invalidate();

            // If a region was clicked, consume click (do not sample color)
            if (hit != null)
            {
                return;
            }
        }

        var imgPoint = imageBox.PointToImage(e.Location);
        if (imgPoint.X < 0 || imgPoint.Y < 0 ||
            imgPoint.X >= _currentImage.Width ||
            imgPoint.Y >= _currentImage.Height)
        {
            return;
        }

        Color c = ((Bitmap)_currentImage).GetPixel(imgPoint.X, imgPoint.Y);
        colorPreviewLabel.BackColor = c;
        OnColorSampled(imgPoint, c);
    }

    private void ImageBox_MouseWheel(object? sender, MouseEventArgs e)
    {
        int stepPerTick = (ModifierKeys & Keys.Control) == Keys.Control ? 50 : 10;
        int ticks = e.Delta / System.Windows.Forms.SystemInformation.MouseWheelScrollDelta;
        if (ticks == 0)
        {
            ticks = Math.Sign(e.Delta);
        }
        AdjustZoom(stepPerTick * ticks);

        // Prevent ImageBox or parent controls from applying additional zoom
        if (e is HandledMouseEventArgs hme)
        {
            hme.Handled = true;
        }
    }

    private void ImageBox_KeyDown(object? sender, KeyEventArgs e)
    {
        // Zoom shortcuts
        if (e.Control && (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus))
        {
            AdjustZoom(10);
            e.Handled = true;
            return;
        }

        if (e.Control && (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus))
        {
            AdjustZoom(-10);
            e.Handled = true;
            return;
        }

        // Temporary pan-hand (space)
        if (e.KeyCode == Keys.Space && !_spacePanMode)
        {
            _spacePanMode = true;
            _prevCursor = imageBox.Cursor;
            imageBox.Cursor = Cursors.Hand;
            e.Handled = true;
            return;
        }

        // Delete selected region
        if (e.KeyCode == Keys.Delete)
        {
            if (_selectedRegion != null && _regions.Contains(_selectedRegion))
            {
                _regions.Remove(_selectedRegion);
                _selectedRegion = null;
                _regionsDirty = true;
                imageBox.Invalidate();
            }
            e.Handled = true;
            return;
        }
    }

    private void ImageBox_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space && _spacePanMode)
        {
            _spacePanMode = false;
            imageBox.Cursor = _prevCursor ?? Cursors.Default;
            e.Handled = true;
        }
    }

    private void ImageBox_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            e.IsInputKey = true;
        }
    }

    #endregion

    #region ImageBox Region Drawing

    private void ImageBox_MouseDown(object? sender, MouseEventArgs e)
    {
        if (_spacePanMode)
        {
            return; // allow panning while holding space
        }

        if (!_regionMode || _currentImage == null)
        {
            return;
        }

        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        _isDrawing = true;
        _startPt = e.Location;

        // Use high-precision conversion to image coordinates (avoids integer rounding errors)
        _startImgPt = DisplayPointToImage(_startPt);
        _currentRegionDraft = new RegionInfo { Label = "", Rect = new RectangleF() };
    }

    private void ImageBox_MouseUp(object? sender, MouseEventArgs e)
    {
        if (!_isDrawing || _currentRegionDraft == null)
        {
            return;
        }

        _isDrawing = false;
        if (_currentRegionDraft.Rect.Width < 5 || _currentRegionDraft.Rect.Height < 5)
        {
            _currentRegionDraft = null;
            imageBox.Invalidate();
            return;
        }

        string? label = KryptonInputBox.Show(new KryptonInputBoxData
        {
            Prompt = "Enter label for region:",
            Caption = "Label Region",
            DefaultResponse = "Region"
        });

        _currentRegionDraft.Label = string.IsNullOrWhiteSpace(label) ? "Region" : label!.Trim();
        _regions.Add(_currentRegionDraft);
        _regionsDirty = true;
        _currentRegionDraft = null;
        imageBox.Invalidate();
    }

    private void ImageBox_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
        if (_currentImage == null)
        {
            return;
        }

        var imgPt = DisplayPointToImage(e.Location);
        var region = _regions.FirstOrDefault(r => r.Rect.Contains(imgPt));
        if (region == null)
        {
            return;
        }

        string? label = KryptonInputBox.Show(new KryptonInputBoxData
        {
            Prompt = "Edit label:",
            Caption = "Edit Region Label",
            DefaultResponse = region.Label
        });

        if (label != null)
        {
            region.Label = label.Trim();
            _regionsDirty = true;
            imageBox.Invalidate();
        }
    }

    #endregion

    #region Events

    private void ToolStripButtonZoomIn_Click(object? sender, EventArgs e)
    {
        AdjustZoom(10);
    }

    private void ToolStripButtonZoomOut_Click(object? sender, EventArgs e)
    {
        AdjustZoom(-10);
    }

    private void OnColorSampled(Point location, Color color) =>
        ColorSampled?.Invoke(this, new ColorSampledEventArgs(location, color));

    private void ToolStripButtonOpen_Click(object? sender, EventArgs e)
    {
        if (!ConfirmImageReplacement("Confirm Open"))
        {
            return;
        }

        using var ofd = new System.Windows.Forms.OpenFileDialog
        {
            Title = "Open Image",
            Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif|All Files|*.*",
            InitialDirectory = _lastImageFolder
                ?? (_lastImagePath != null ? Path.GetDirectoryName(_lastImagePath)
                : null)
        };

        if (ofd.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                using var temp = Image.FromFile(ofd.FileName);
                LoadImageWithRegionClear(new Bitmap(temp));
                _lastImagePath = ofd.FileName;
                _lastImageFolder = Path.GetDirectoryName(ofd.FileName);
            }
            catch (Exception ex)
            {
                ShowImageLoadError(ex, "load");
            }
        }
    }

    private void ToolStripButtonPaste_Click(object? sender, EventArgs e)
    {
        if (!Clipboard.ContainsImage())
        {
            KryptonMessageBox.Show(this, "Clipboard does not contain an image.", "Paste",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            return;
        }

        if (!ConfirmImageReplacement("Confirm Paste"))
        {
            return;
        }

        try
        {
            using var img = Clipboard.GetImage();
            if (img != null)
            {
                LoadImageWithRegionClear(new Bitmap(img));
            }
        }
        catch (Exception ex)
        {
            ShowImageLoadError(ex, "paste");
        }
    }

    private void ZoomMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem mi)
        {
            if (mi.Tag is int factor)
            {
                imageBox.Zoom = factor;
            }
            else if (mi.Tag?.ToString() == "Fit")
            {
                FitToWindow();
            }
        }
    }

    private void ToolStripButtonCrosshair_Click(object? sender, EventArgs e)
    {
        _showCrosshair = toolStripButtonCrosshair.Checked;
        imageBox.Invalidate();
    }

    private void ToolStripButtonRegionMode_CheckedChanged(object? sender, EventArgs e)
    {
        _regionMode = toolStripButtonRegionMode.Checked;
    }

    private void ToolStripButtonSaveRegions_Click(object? sender, EventArgs e)
    {
        SaveRegions();
    }

    private void ToolStripButtonLoadRegions_Click(object? sender, EventArgs e)
    {
        LoadRegions();
    }

    #endregion

    #region Implementation

    private void AdjustZoom(int delta)
    {
        int newZoom = imageBox.Zoom + delta;
        newZoom = Math.Max(1, Math.Min(800, newZoom));
        imageBox.Zoom = newZoom;
    }

    private void FitToWindow()
    {
        if (_currentImage == null)
        {
            return;
        }

        if (imageBox.ClientSize.Width == 0 || imageBox.ClientSize.Height == 0)
        {
            return;
        }

        double scaleX = imageBox.ClientSize.Width / (double)_currentImage.Width;
        double scaleY = imageBox.ClientSize.Height / (double)_currentImage.Height;
        int percent = (int)(Math.Min(scaleX, scaleY) * 100);
        percent = Math.Max(1, Math.Min(800, percent));
        imageBox.Zoom = percent;
    }

    private void UpdateStatus()
    {
        if (_currentImage == null)
        {
            statusLabel.Text = string.Empty;
            toolStripHex.Text = string.Empty;
            toolStripR.Text = string.Empty;
            toolStripG.Text = string.Empty;
            toolStripB.Text = string.Empty;
            return;
        }

        if (!_mouseInside)
        {
            return; // Don't update if mouse is not inside
        }

        var imgPoint = imageBox.PointToImage(_lastMousePos);
        if (imgPoint.X < 0 || imgPoint.Y < 0 || imgPoint.X >= _currentImage.Width || imgPoint.Y >= _currentImage.Height)
        {
            statusLabel.Text = string.Empty;
            return;
        }

        Color c = ((Bitmap)_currentImage).GetPixel(imgPoint.X, imgPoint.Y);
        statusLabel.Text = $"X: {imgPoint.X} / Y: {imgPoint.Y}  ||  {_currentImage.Width}px x {_currentImage.Height}px  ||  ";
        toolStripHex.Text = $"HEX: #{c.R:X2}{c.G:X2}{c.B:X2}";
        toolStripR.Text = c.R.ToString();
        toolStripG.Text = c.G.ToString();
        toolStripB.Text = c.B.ToString();
    }

    private void InitialiseTimer()
    {
        _uiTimer.Tick += (_, __) => UpdateStatus();
    }

    public void ApplyPalette(KryptonCustomPaletteBase palette)
    {
        kryptonPanel1.Palette = palette;
    }

    private bool ConfirmImageReplacement(string dialogTitle)
    {
        if (!_regionsDirty)
        {
            return true;
        }

        const string message = "This will replace the current image and clear all unsaved regions. Continue?";
        var result = KryptonMessageBox.Show(this, message, dialogTitle,
                        KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Question);
        return result == DialogResult.Yes;
    }

    private void ShowImageLoadError(Exception ex, string operation)
    {
        KryptonMessageBox.Show(this, $"Failed to {operation} image.\n{ex.Message}", "Error",
            KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
    }

    public void SetLastImageFolder(string? folder)
    {
        _lastImageFolder = string.IsNullOrWhiteSpace(folder) ? null : folder;
    }

    #endregion

    #region Helpers – Coordinate Transforms

    private RectangleF DisplayRectToImage(Rectangle disp)
    {
        if (_currentImage == null)
        {
            return RectangleF.Empty;
        }

        // Use high-precision conversion to minimise rounding artefacts
        var tl = DisplayPointToImage(new Point(disp.Left, disp.Top));
        var br = DisplayPointToImage(new Point(disp.Right, disp.Bottom));

        return RectangleF.FromLTRB(tl.X, tl.Y, br.X, br.Y);
    }

    private Rectangle ImageRectToDisplay(RectangleF imgRect)
    {
        if (_currentImage == null)
        {
            return Rectangle.Empty;
        }

        // Obtain viewport of image within control
        Rectangle vp = imageBox.GetImageViewPort();
        float scale = imageBox.Zoom / 100f;
        int x = (int)(vp.Left + imgRect.Left * scale);
        int y = (int)(vp.Top + imgRect.Top * scale);
        int w = (int)(imgRect.Width * scale);
        int h = (int)(imgRect.Height * scale);

        return new Rectangle(x, y, w, h);
    }

    private PointF DisplayPointToImage(Point dispPt)
    {
        if (_currentImage == null)
        {
            return PointF.Empty;
        }

        // Obtain the viewport (location of the _scaled_ image within the control)
        Rectangle vp = imageBox.GetImageViewPort();
        float scale = imageBox.Zoom / 100f;

        // Translate control coordinates into unscaled image coordinates with sub-pixel accuracy
        float x = (dispPt.X - vp.Left) / scale;
        float y = (dispPt.Y - vp.Top) / scale;

        return new PointF(x, y);
    }

    #endregion

    #region Load / Save Image and Regions

    private void LoadImageWithRegionClear(Image image)
    {
        _regions.Clear();
        _regionsDirty = false;

        LoadImage(image);
    }

    public bool LoadImageFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)
            || !File.Exists(filePath))
        {
            return false;
        }

        try
        {
            using var img = Image.FromFile(filePath);
            _regions.Clear();
            _regionsDirty = false;
            LoadImage(new Bitmap(img));
            _lastImagePath = filePath;
            _lastImageFolder = Path.GetDirectoryName(filePath);
            return true;
        }
        catch { }
        return false;
    }

    private void LoadImage(Image img)
    {
        _currentImage?.Dispose();
        _currentImage = img;
        imageBox.Image = _currentImage;
        imageBox.Zoom = 100;
        toolStripButtonCrosshair.Enabled = true;
        toolStripHex.Text = string.Empty;
        toolStripR.Text = string.Empty;
        toolStripG.Text = string.Empty;
        toolStripB.Text = string.Empty;
        statusLabel.Text = string.Empty;
        statusZoomLabel.Text = "Zoom: 100%";
        colorPreviewLabel.BackColor = Color.Transparent;

        UpdateStatus();
    }

    public bool LoadRegionsFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)
            || !File.Exists(filePath))
        {
            return false;
        }

        return ApplyRegionsFromFile(filePath, showError: false);
    }

    private void LoadRegions()
    {
        using var ofd = new System.Windows.Forms.OpenFileDialog {
            Filter = "XML files|*.xml",
            Title = "Load Regions"
        };

        if (ofd.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        ApplyRegionsFromFile(ofd.FileName, showError: true);
    }

    private bool ApplyRegionsFromFile(string filePath, bool showError = false)
    {
        try
        {
            var xs = new XmlSerializer(typeof(List<RegionInfo>));
            using var fs = File.OpenRead(filePath);
            if (xs.Deserialize(fs) is List<RegionInfo> list)
            {
                _regions.Clear();
                _regions.AddRange(list);
                _regionsDirty = false;
                imageBox.Invalidate();
                _lastRegionPath = filePath;
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            if (showError)
            {
                KryptonMessageBox.Show(this, ex.Message, "Error",
                    KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
            return false;
        }
    }

    public bool SaveRegions()
    {
        if (_regions.Count == 0)
        {
            KryptonMessageBox.Show(this, "No regions to save.", "Save Regions",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
            return false;
        }
        string initDir = _lastRegionPath != null
            ? Path.GetDirectoryName(_lastRegionPath)!
            : _lastImageFolder ??
               (_lastImagePath != null
                ? Path.GetDirectoryName(_lastImagePath)!
                : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        using var sfd = new System.Windows.Forms.SaveFileDialog
        {
            Filter = "XML files|*.xml",
            Title = "Save Regions",
            InitialDirectory = initDir
        };

        if (sfd.ShowDialog(this) != DialogResult.OK)
        {
            return false;
        }

        try
        {
            var xs = new XmlSerializer(typeof(List<RegionInfo>));
            using var fs = File.Create(sfd.FileName);
            xs.Serialize(fs, _regions);
            _regionsDirty = false;
            _lastRegionPath = sfd.FileName; // track last saved path
            return true;
        }
        catch (Exception ex)
        {
            KryptonMessageBox.Show(this, ex.Message, "Error",
                KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            return false;
        }
    }

    #endregion

}

internal sealed class RegionInfo
{
    public string Label { get; set; } = string.Empty;
    public RectangleF Rect { get; set; }
}

public sealed class ColorSampledEventArgs(Point location, Color color) : EventArgs
{
    public Point Location { get; } = location;
    public Color Color { get; } = color;
}
