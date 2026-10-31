using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SwitchMagnifier
{
    public class MagnifierForm : Form
    {
        private readonly Timer timer;
        private readonly Label header;
        private readonly Panel view;
        private float zoom;
        private bool english;
        private Point sourceCenter;
        private bool capturing;

        public float Zoom
        {
            get { return zoom; }
            set { zoom = Math.Max(1.5f, Math.Min(5.0f, value)); view.Invalidate(); }
        }

        public MagnifierForm(float initialZoom, bool useEnglish)
        {
            zoom = initialZoom;
            english = useEnglish;
            sourceCenter = Cursor.Position;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            StartPosition = FormStartPosition.Manual;
            Bounds = new Rectangle(Math.Max(0, Screen.PrimaryScreen.WorkingArea.Right - 390), 100, 360, 260);
            BackColor = Color.FromArgb(20, 27, 39);
            Padding = new Padding(3);

            header = new Label { Dock = DockStyle.Top, Height = 30, BackColor = Color.FromArgb(20, 27, 39), ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0), Cursor = Cursors.SizeAll };
            header.Text = english ? "  Switch Magnifier  ·  drag to move  ·  Esc to close" : "  Switch 放大镜  ·  拖动移动  ·  Esc 关闭";
            header.MouseDown += DragStart;
            header.MouseMove += DragMove;
            header.MouseUp += DragEnd;
            Controls.Add(header);

            view = new Panel { Dock = DockStyle.Fill, BackColor = Color.Black };
            view.Paint += PaintMagnified;
            view.MouseWheel += delegate(object s, MouseEventArgs e) { Zoom += e.Delta > 0 ? 0.25f : -0.25f; };
            Controls.Add(view);
            header.BringToFront();

            timer = new Timer { Interval = 80 };
            timer.Tick += delegate { sourceCenter = Cursor.Position; view.Invalidate(); };
            timer.Start();
            KeyPreview = true;
            KeyDown += delegate(object s, KeyEventArgs e) { if (e.KeyCode == Keys.Escape) Close(); };
            MouseWheel += delegate(object s, MouseEventArgs e) { Zoom += e.Delta > 0 ? 0.25f : -0.25f; };
        }

        private Point dragStart;
        private Point formStart;
        private bool dragging;
        private void DragStart(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            dragging = true; dragStart = Cursor.Position; formStart = Location;
        }
        private void DragMove(object sender, MouseEventArgs e)
        {
            if (dragging) Location = new Point(formStart.X + Cursor.Position.X - dragStart.X, formStart.Y + Cursor.Position.Y - dragStart.Y);
        }
        private void DragEnd(object sender, MouseEventArgs e) { dragging = false; }

        private void PaintMagnified(object sender, PaintEventArgs e)
        {
            if (capturing || view.ClientSize.Width <= 0 || view.ClientSize.Height <= 0) return;
            capturing = true;
            Bitmap shot = null;
            try
            {
                int sourceWidth = Math.Max(1, (int)(view.ClientSize.Width / zoom));
                int sourceHeight = Math.Max(1, (int)(view.ClientSize.Height / zoom));
                int sx = sourceCenter.X - sourceWidth / 2;
                int sy = sourceCenter.Y - sourceHeight / 2;
                Rectangle virtualBounds = SystemInformation.VirtualScreen;
                sx = Math.Max(virtualBounds.Left, Math.Min(virtualBounds.Right - sourceWidth, sx));
                sy = Math.Max(virtualBounds.Top, Math.Min(virtualBounds.Bottom - sourceHeight, sy));
                shot = new Bitmap(sourceWidth, sourceHeight);
                using (Graphics g = Graphics.FromImage(shot))
                    g.CopyFromScreen(sx, sy, 0, 0, new Size(sourceWidth, sourceHeight), CopyPixelOperation.SourceCopy);
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                e.Graphics.DrawImage(shot, new Rectangle(0, 0, view.ClientSize.Width, view.ClientSize.Height));
                using (Pen p = new Pen(Color.FromArgb(235, 255, 255, 255), 1))
                {
                    int cx = view.ClientSize.Width / 2, cy = view.ClientSize.Height / 2;
                    e.Graphics.DrawLine(p, cx - 10, cy, cx + 10, cy);
                    e.Graphics.DrawLine(p, cx, cy - 10, cx, cy + 10);
                }
            }
            catch { }
            finally
            {
                if (shot != null) shot.Dispose();
                capturing = false;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            base.OnFormClosed(e);
        }
    }
}
