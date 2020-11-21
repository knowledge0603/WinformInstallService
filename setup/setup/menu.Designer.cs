namespace setup
{
    partial class menu
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(menu));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.javaLabel = new System.Windows.Forms.Label();
            this.frpLabel = new System.Windows.Forms.Label();
            this.zookeeperLabel = new System.Windows.Forms.Label();
            this.kafkaLabel = new System.Windows.Forms.Label();
            this.mysqlLabel = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(96, 52);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 18);
            this.button1.TabIndex = 0;
            this.button1.Text = "java";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 151);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 18);
            this.button2.TabIndex = 0;
            this.button2.Text = "zookeeper";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(96, 105);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 18);
            this.button3.TabIndex = 0;
            this.button3.Text = "frp";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(96, 201);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 18);
            this.button4.TabIndex = 0;
            this.button4.Text = "kafka";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(96, 254);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(56, 18);
            this.button5.TabIndex = 0;
            this.button5.Text = "mysql";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // javaLabel
            // 
            this.javaLabel.AutoSize = true;
            this.javaLabel.Location = new System.Drawing.Point(340, 58);
            this.javaLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.javaLabel.Name = "javaLabel";
            this.javaLabel.Size = new System.Drawing.Size(23, 12);
            this.javaLabel.TabIndex = 1;
            this.javaLabel.Text = "...";
            // 
            // frpLabel
            // 
            this.frpLabel.AutoSize = true;
            this.frpLabel.Location = new System.Drawing.Point(340, 111);
            this.frpLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.frpLabel.Name = "frpLabel";
            this.frpLabel.Size = new System.Drawing.Size(23, 12);
            this.frpLabel.TabIndex = 1;
            this.frpLabel.Text = "...";
            // 
            // zookeeperLabel
            // 
            this.zookeeperLabel.AutoSize = true;
            this.zookeeperLabel.Location = new System.Drawing.Point(340, 154);
            this.zookeeperLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.zookeeperLabel.Name = "zookeeperLabel";
            this.zookeeperLabel.Size = new System.Drawing.Size(23, 12);
            this.zookeeperLabel.TabIndex = 1;
            this.zookeeperLabel.Text = "...";
            // 
            // kafkaLabel
            // 
            this.kafkaLabel.AutoSize = true;
            this.kafkaLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.kafkaLabel.Location = new System.Drawing.Point(340, 204);
            this.kafkaLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kafkaLabel.Name = "kafkaLabel";
            this.kafkaLabel.Size = new System.Drawing.Size(25, 14);
            this.kafkaLabel.TabIndex = 1;
            this.kafkaLabel.Text = "...";
            // 
            // mysqlLabel
            // 
            this.mysqlLabel.AutoSize = true;
            this.mysqlLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mysqlLabel.Location = new System.Drawing.Point(340, 258);
            this.mysqlLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mysqlLabel.Name = "mysqlLabel";
            this.mysqlLabel.Size = new System.Drawing.Size(25, 14);
            this.mysqlLabel.TabIndex = 1;
            this.mysqlLabel.Text = "...";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 114);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem1.Text = "exit";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem3.Text = "toolStripMenuItem3";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem4.Text = "toolStripMenuItem4";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem5.Text = "toolStripMenuItem5";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(811, 347);
            this.Controls.Add(this.mysqlLabel);
            this.Controls.Add(this.kafkaLabel);
            this.Controls.Add(this.zookeeperLabel);
            this.Controls.Add(this.frpLabel);
            this.Controls.Add(this.javaLabel);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "menu";
            this.Text = "menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.menu_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label javaLabel;
        private System.Windows.Forms.Label frpLabel;
        private System.Windows.Forms.Label zookeeperLabel;
        private System.Windows.Forms.Label kafkaLabel;
        private System.Windows.Forms.Label mysqlLabel;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.Timer timer1;
    }
}

