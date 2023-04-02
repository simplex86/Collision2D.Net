namespace SimpleX
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new System.Windows.Forms.PictureBox();
            this.detail = new System.Windows.Forms.Label();
            this.showBoundingBox = new System.Windows.Forms.CheckBox();
            this.showDirection = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Margin = new System.Windows.Forms.Padding(2);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1184, 861);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaintHandler);
            // 
            // detail
            // 
            this.detail.AutoSize = true;
            this.detail.BackColor = System.Drawing.Color.Silver;
            this.detail.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.detail.Location = new System.Drawing.Point(7, 7);
            this.detail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.detail.Name = "detail";
            this.detail.Size = new System.Drawing.Size(105, 80);
            this.detail.TabIndex = 1;
            this.detail.Text = "Collision Count: --\r\nRender\r\n  FPS : -  Cost: - ms\r\nCollide\r\n  FPS : -  Cost: - m" +
    "s";
            this.detail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // showBoundingBox
            // 
            this.showBoundingBox.AutoSize = true;
            this.showBoundingBox.Location = new System.Drawing.Point(8, 93);
            this.showBoundingBox.Margin = new System.Windows.Forms.Padding(2);
            this.showBoundingBox.Name = "showBoundingBox";
            this.showBoundingBox.Size = new System.Drawing.Size(84, 16);
            this.showBoundingBox.TabIndex = 2;
            this.showBoundingBox.Text = "显示包围盒";
            this.showBoundingBox.UseVisualStyleBackColor = true;
            this.showBoundingBox.CheckedChanged += new System.EventHandler(this.OnBoundingBoxVisibleChanged);
            // 
            // showDirection
            // 
            this.showDirection.AutoSize = true;
            this.showDirection.Location = new System.Drawing.Point(8, 113);
            this.showDirection.Margin = new System.Windows.Forms.Padding(2);
            this.showDirection.Name = "showDirection";
            this.showDirection.Size = new System.Drawing.Size(72, 16);
            this.showDirection.TabIndex = 3;
            this.showDirection.Text = "显示方向";
            this.showDirection.UseVisualStyleBackColor = true;
            this.showDirection.CheckedChanged += new System.EventHandler(this.OnDirectionVisibleChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.showDirection);
            this.Controls.Add(this.showBoundingBox);
            this.Controls.Add(this.detail);
            this.Controls.Add(this.canvas);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Collision2D.Net";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosingHandler);
            this.Load += new System.EventHandler(this.OnLoadHandler);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label detail;
        private System.Windows.Forms.CheckBox showBoundingBox;
        private System.Windows.Forms.CheckBox showDirection;
    }
}

