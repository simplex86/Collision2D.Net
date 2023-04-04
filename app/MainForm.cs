using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleX
{
    using SimpleX.Collision2D;

    public partial class MainForm : Form
    {
        private Task task = null;

        public MainForm()
        {
            InitializeComponent();

            showBoundingBox.Checked = Settings.showBoundingBox;
            showVelocity.Checked = Settings.showVelocity;
        }

        // 窗体加载完后，初始化数据
        private void OnLoadHandler(object sender, EventArgs e)
        {
            task = new Task(canvas, detail);
            task.Start();
        }

        // 渲染
        private void OnPaintHandler(object sender, PaintEventArgs e)
        {
            var grap = e.Graphics;
            grap.SmoothingMode = SmoothingMode.HighQuality;

            var world = task.world;
            world.Render(grap);
        }

        private void OnClosingHandler(object sender, FormClosingEventArgs e)
        {
            if (task != null)
            {
                task.Destroy();
                task = null;
            }
        }

        private void OnBoundingBoxVisibleChanged(object sender, EventArgs e)
        {
            var checkbox = sender as CheckBox;
            Settings.showBoundingBox = checkbox.Checked;
        }

        private void OnVelocityVisibleChanged(object sender, EventArgs e)
        {
            var checkbox = sender as CheckBox;
            Settings.showVelocity = checkbox.Checked;
        }
    }
}
