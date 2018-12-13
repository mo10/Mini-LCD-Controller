namespace MiniLCDController
{
    partial class Form1
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
            this.btn_capture = new System.Windows.Forms.Button();
            this.preview_box = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.device_list = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.pl_conn = new System.Windows.Forms.Panel();
            this.pl_main = new System.Windows.Forms.Panel();
            this.btn_toBl = new System.Windows.Forms.Button();
            this.lb_fps = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.preview_box)).BeginInit();
            this.pl_conn.SuspendLayout();
            this.pl_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_capture
            // 
            this.btn_capture.Location = new System.Drawing.Point(169, 74);
            this.btn_capture.Name = "btn_capture";
            this.btn_capture.Size = new System.Drawing.Size(164, 23);
            this.btn_capture.TabIndex = 0;
            this.btn_capture.Text = "开始窗口捕捉";
            this.btn_capture.UseVisualStyleBackColor = true;
            this.btn_capture.Click += new System.EventHandler(this.btn_capture_Click);
            // 
            // preview_box
            // 
            this.preview_box.Location = new System.Drawing.Point(3, 17);
            this.preview_box.Name = "preview_box";
            this.preview_box.Size = new System.Drawing.Size(160, 80);
            this.preview_box.TabIndex = 1;
            this.preview_box.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "实际显示预览:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(169, 23);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "捕捉鼠标";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(247, 23);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "鼠标穿透";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // device_list
            // 
            this.device_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.device_list.FormattingEnabled = true;
            this.device_list.Location = new System.Drawing.Point(68, 3);
            this.device_list.Name = "device_list";
            this.device_list.Size = new System.Drawing.Size(190, 20);
            this.device_list.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "设备列表:";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(276, 13);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_connect.TabIndex = 7;
            this.btn_connect.Text = "打开设备";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // pl_conn
            // 
            this.pl_conn.Controls.Add(this.label2);
            this.pl_conn.Controls.Add(this.device_list);
            this.pl_conn.Location = new System.Drawing.Point(12, 12);
            this.pl_conn.Name = "pl_conn";
            this.pl_conn.Size = new System.Drawing.Size(261, 27);
            this.pl_conn.TabIndex = 8;
            // 
            // pl_main
            // 
            this.pl_main.Controls.Add(this.lb_fps);
            this.pl_main.Controls.Add(this.btn_toBl);
            this.pl_main.Controls.Add(this.label1);
            this.pl_main.Controls.Add(this.preview_box);
            this.pl_main.Controls.Add(this.btn_capture);
            this.pl_main.Controls.Add(this.checkBox2);
            this.pl_main.Controls.Add(this.checkBox1);
            this.pl_main.Enabled = false;
            this.pl_main.Location = new System.Drawing.Point(12, 45);
            this.pl_main.Name = "pl_main";
            this.pl_main.Size = new System.Drawing.Size(336, 101);
            this.pl_main.TabIndex = 9;
            // 
            // btn_toBl
            // 
            this.btn_toBl.Location = new System.Drawing.Point(169, 45);
            this.btn_toBl.Name = "btn_toBl";
            this.btn_toBl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_toBl.Size = new System.Drawing.Size(164, 23);
            this.btn_toBl.TabIndex = 5;
            this.btn_toBl.Text = "启动到Bootloader";
            this.btn_toBl.UseVisualStyleBackColor = true;
            this.btn_toBl.Click += new System.EventHandler(this.btn_toBlDfu_Click);
            // 
            // lb_fps
            // 
            this.lb_fps.AutoSize = true;
            this.lb_fps.Location = new System.Drawing.Point(169, 8);
            this.lb_fps.Name = "lb_fps";
            this.lb_fps.Size = new System.Drawing.Size(59, 12);
            this.lb_fps.TabIndex = 6;
            this.lb_fps.Text = "当前帧数:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 157);
            this.Controls.Add(this.pl_main);
            this.Controls.Add(this.pl_conn);
            this.Controls.Add(this.btn_connect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mini LCD Controller";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.preview_box)).EndInit();
            this.pl_conn.ResumeLayout(false);
            this.pl_conn.PerformLayout();
            this.pl_main.ResumeLayout(false);
            this.pl_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_capture;
        private System.Windows.Forms.PictureBox preview_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox device_list;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Panel pl_conn;
        private System.Windows.Forms.Panel pl_main;
        private System.Windows.Forms.Button btn_toBl;
        private System.Windows.Forms.Label lb_fps;
    }
}

