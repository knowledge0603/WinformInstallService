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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.frpStatusLabel = new System.Windows.Forms.Label();
            this.zookeeperStatusLabel = new System.Windows.Forms.Label();
            this.kafkaStatusLabel = new System.Windows.Forms.Label();
            this.mySqlStatusLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // frpLabel
            // 
            this.frpLabel.AutoSize = true;
            this.frpLabel.Location = new System.Drawing.Point(109, 42);
            this.frpLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.frpLabel.Name = "frpLabel";
            this.frpLabel.Size = new System.Drawing.Size(23, 12);
            this.frpLabel.TabIndex = 1;
            this.frpLabel.Text = "...";
            // 
            // zookeeperLabel
            // 
            this.zookeeperLabel.AutoSize = true;
            this.zookeeperLabel.Location = new System.Drawing.Point(111, 38);
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
            this.kafkaLabel.Location = new System.Drawing.Point(109, 73);
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
            this.mysqlLabel.Location = new System.Drawing.Point(111, 78);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(193, 114);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem1.Text = "exit";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem3.Text = "toolStripMenuItem3";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem4.Text = "toolStripMenuItem4";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem5.Text = "toolStripMenuItem5";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "frp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "zookeeper";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "kafka";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "mysql";
            // 
            // frpStatusLabel
            // 
            this.frpStatusLabel.AutoSize = true;
            this.frpStatusLabel.Location = new System.Drawing.Point(166, 42);
            this.frpStatusLabel.Name = "frpStatusLabel";
            this.frpStatusLabel.Size = new System.Drawing.Size(41, 12);
            this.frpStatusLabel.TabIndex = 6;
            this.frpStatusLabel.Text = "......";
            // 
            // zookeeperStatusLabel
            // 
            this.zookeeperStatusLabel.AutoSize = true;
            this.zookeeperStatusLabel.Location = new System.Drawing.Point(168, 38);
            this.zookeeperStatusLabel.Name = "zookeeperStatusLabel";
            this.zookeeperStatusLabel.Size = new System.Drawing.Size(41, 12);
            this.zookeeperStatusLabel.TabIndex = 7;
            this.zookeeperStatusLabel.Text = "......";
            // 
            // kafkaStatusLabel
            // 
            this.kafkaStatusLabel.AutoSize = true;
            this.kafkaStatusLabel.Location = new System.Drawing.Point(168, 73);
            this.kafkaStatusLabel.Name = "kafkaStatusLabel";
            this.kafkaStatusLabel.Size = new System.Drawing.Size(41, 12);
            this.kafkaStatusLabel.TabIndex = 8;
            this.kafkaStatusLabel.Text = "......";
            // 
            // mySqlStatusLabel
            // 
            this.mySqlStatusLabel.AutoSize = true;
            this.mySqlStatusLabel.Location = new System.Drawing.Point(170, 78);
            this.mySqlStatusLabel.Name = "mySqlStatusLabel";
            this.mySqlStatusLabel.Size = new System.Drawing.Size(41, 12);
            this.mySqlStatusLabel.TabIndex = 9;
            this.mySqlStatusLabel.Text = "......";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kafkaStatusLabel);
            this.groupBox1.Controls.Add(this.zookeeperLabel);
            this.groupBox1.Controls.Add(this.kafkaLabel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.zookeeperStatusLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(612, 320);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "server";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mySqlStatusLabel);
            this.groupBox2.Controls.Add(this.mysqlLabel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.frpStatusLabel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.frpLabel);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(612, 320);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "client";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(632, 358);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(624, 332);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "server";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(624, 332);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "client";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 378);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "menu";
            this.Text = "menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.menu_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label frpStatusLabel;
        private System.Windows.Forms.Label zookeeperStatusLabel;
        private System.Windows.Forms.Label kafkaStatusLabel;
        private System.Windows.Forms.Label mySqlStatusLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

