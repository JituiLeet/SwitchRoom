using System;
using System.Drawing;
using System.Windows.Forms;

namespace SwitchMagnifier
{
    public class MainForm : Form
    {
        private Button startButton, zoomInButton, zoomOutButton, languageButton;
        private Label titleLabel, descriptionLabel, zoomLabel, statusLabel, shortcutLabel;
        private MagnifierForm magnifier;
        private bool english;
        private float zoom = 2.0f;

        public MainForm()
        {
            english = false;
            Text = "Switch 放大镜";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(440, 330);
            MinimumSize = new Size(440, 330);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9F);
            BuildUi();
            UpdateLanguage();
        }

        private void BuildUi()
        {
            titleLabel = new Label { Left = 24, Top = 20, Width = 330, Height = 34, Font = new Font("Segoe UI", 19F, FontStyle.Bold), ForeColor = Color.FromArgb(28, 42, 62) };
            Controls.Add(titleLabel);

            languageButton = new Button { Left = 350, Top = 23, Width = 66, Height = 28 };
            languageButton.Click += delegate { english = !english; UpdateLanguage(); };
            Controls.Add(languageButton);

            descriptionLabel = new Label { Left = 26, Top = 65, Width = 390, Height = 42, ForeColor = Color.FromArgb(80, 91, 107) };
            Controls.Add(descriptionLabel);

            startButton = new Button { Left = 26, Top = 119, Width = 390, Height = 43, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(0, 120, 215), ForeColor = Color.White, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
            startButton.FlatAppearance.BorderSize = 0;
            startButton.Click += delegate { ToggleMagnifier(); };
            Controls.Add(startButton);

            zoomOutButton = new Button { Left = 26, Top = 180, Width = 54, Height = 32 };
            zoomOutButton.Click += delegate { SetZoom(zoom - 0.5f); };
            Controls.Add(zoomOutButton);

            zoomLabel = new Label { Left = 90, Top = 185, Width = 240, Height = 24, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            Controls.Add(zoomLabel);

            zoomInButton = new Button { Left = 362, Top = 180, Width = 54, Height = 32 };
            zoomInButton.Click += delegate { SetZoom(zoom + 0.5f); };
            Controls.Add(zoomInButton);

            shortcutLabel = new Label { Left = 26, Top = 229, Width = 390, Height = 25, ForeColor = Color.FromArgb(65, 76, 91) };
            Controls.Add(shortcutLabel);

            statusLabel = new Label { Left = 26, Top = 265, Width = 390, Height = 25, ForeColor = Color.FromArgb(0, 115, 75) };
            Controls.Add(statusLabel);
        }

        private void UpdateLanguage()
        {
            Text = english ? "Switch Magnifier" : "Switch 放大镜";
            titleLabel.Text = english ? "Switch Magnifier" : "Switch 放大镜";
            languageButton.Text = english ? "中文" : "English";
            descriptionLabel.Text = english
                ? "Magnify a movable area of your desktop, inspired by the Nintendo Switch magnifier."
                : "放大桌面上的局部区域，体验类似 Nintendo Switch 的放大镜功能。";
            startButton.Text = magnifier != null && !magnifier.IsDisposed && magnifier.Visible
                ? (english ? "Close magnifier" : "关闭放大镜")
                : (english ? "Start magnifier" : "开启放大镜");
            zoomOutButton.Text = "−";
            zoomInButton.Text = "+";
            zoomLabel.Text = (english ? "Zoom: " : "放大倍率：") + zoom.ToString("0.0") + "×";
            shortcutLabel.Text = english ? "Tip: drag the lens to move it; use the mouse wheel to zoom." : "提示：拖动放大镜窗口可移动位置，滚动鼠标滚轮可调整倍率。";
            statusLabel.Text = english ? "Windows 7 or later · .NET Framework 4.0" : "支持 Windows 7 及以上 · .NET Framework 4.0";
        }

        private void SetZoom(float value)
        {
            zoom = Math.Max(1.5f, Math.Min(5.0f, value));
            if (magnifier != null && !magnifier.IsDisposed) magnifier.Zoom = zoom;
            UpdateLanguage();
        }

        private void ToggleMagnifier()
        {
            if (magnifier != null && !magnifier.IsDisposed && magnifier.Visible)
            {
                magnifier.Close();
                magnifier = null;
            }
            else
            {
                magnifier = new MagnifierForm(zoom, english);
                magnifier.FormClosed += delegate { UpdateLanguage(); };
                magnifier.Show();
            }
            UpdateLanguage();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (magnifier != null && !magnifier.IsDisposed) magnifier.Close();
            base.OnFormClosed(e);
        }
    }
}
