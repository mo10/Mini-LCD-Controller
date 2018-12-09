namespace MiniLCDController
{
    partial class CaptureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.title_bar = new System.Windows.Forms.Panel();
            this.settingMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAllowEvent = new System.Windows.Forms.ToolStripMenuItem();
            this.capture_box = new System.Windows.Forms.Panel();
            this.settingMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // title_bar
            // 
            this.title_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.title_bar.ContextMenuStrip = this.settingMenu;
            this.title_bar.Location = new System.Drawing.Point(1, 1);
            this.title_bar.Name = "title_bar";
            this.title_bar.Size = new System.Drawing.Size(168, 30);
            this.title_bar.TabIndex = 1;
            this.title_bar.Paint += new System.Windows.Forms.PaintEventHandler(this.title_bar_Paint);
            this.title_bar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CaptureForm_MouseDown);
            this.title_bar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CaptureForm_MouseMove);
            this.title_bar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CaptureForm_MouseUp);
            // 
            // settingMenu
            // 
            this.settingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAllowEvent});
            this.settingMenu.Name = "contextMenuStrip1";
            this.settingMenu.Size = new System.Drawing.Size(181, 48);
            // 
            // setAllowEvent
            // 
            this.setAllowEvent.Name = "setAllowEvent";
            this.setAllowEvent.Size = new System.Drawing.Size(180, 22);
            this.setAllowEvent.Text = "允许鼠标穿透";
            this.setAllowEvent.Click += new System.EventHandler(this.setAllowEvent_Click);
            // 
            // capture_box
            // 
            this.capture_box.BackColor = System.Drawing.Color.Red;
            this.capture_box.ContextMenuStrip = this.settingMenu;
            this.capture_box.Location = new System.Drawing.Point(5, 36);
            this.capture_box.Name = "capture_box";
            this.capture_box.Size = new System.Drawing.Size(160, 80);
            this.capture_box.TabIndex = 2;
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(170, 121);
            this.ContextMenuStrip = this.settingMenu;
            this.Controls.Add(this.capture_box);
            this.Controls.Add(this.title_bar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CaptureForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "捕捉窗口";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.CaptureForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CaptureForm_Paint);
            this.settingMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel title_bar;
        public System.Windows.Forms.Panel capture_box;
        private System.Windows.Forms.ContextMenuStrip settingMenu;
        private System.Windows.Forms.ToolStripMenuItem setAllowEvent;
    }
}