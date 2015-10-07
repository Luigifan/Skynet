namespace Skynet_Server
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox allowKeylogging;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown portTextBox;
        private System.Windows.Forms.CheckBox allowScreenshots;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label serverStatusLabel;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button viewLogButton;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.allowKeylogging = new System.Windows.Forms.CheckBox();
            this.unlockShortcutTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.NumericUpDown();
            this.allowScreenshots = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serverStatusLabel = new System.Windows.Forms.Label();
            this.startStopButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.viewLogButton = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.allowKeylogging);
            this.groupBox1.Controls.Add(this.unlockShortcutTextbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.portTextBox);
            this.groupBox1.Controls.Add(this.allowScreenshots);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Unlock Shortcut";
            this.toolTip1.SetToolTip(this.label3, "The keyboard shortcut used to unlock the server when activated via the notify ico" +
        "n.");
            // 
            // allowKeylogging
            // 
            this.allowKeylogging.Enabled = false;
            this.allowKeylogging.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.allowKeylogging.Location = new System.Drawing.Point(15, 49);
            this.allowKeylogging.Name = "allowKeylogging";
            this.allowKeylogging.Size = new System.Drawing.Size(160, 24);
            this.allowKeylogging.TabIndex = 3;
            this.allowKeylogging.Text = "Enable Keylogging";
            this.allowKeylogging.UseVisualStyleBackColor = true;
            // 
            // unlockShortcutTextbox
            // 
            this.unlockShortcutTextbox.Location = new System.Drawing.Point(291, 49);
            this.unlockShortcutTextbox.Name = "unlockShortcutTextbox";
            this.unlockShortcutTextbox.Size = new System.Drawing.Size(100, 20);
            this.unlockShortcutTextbox.TabIndex = 6;
            this.toolTip1.SetToolTip(this.unlockShortcutTextbox, "The keyboard shortcut used to unlock the server when activated via the notify ico" +
        "n.");
            this.unlockShortcutTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.unlockShortcutTextbox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Open Port On: ";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(99, 79);
            this.portTextBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.portTextBox.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(76, 20);
            this.portTextBox.TabIndex = 1;
            this.portTextBox.Value = new decimal(new int[] {
            4649,
            0,
            0,
            0});
            this.portTextBox.ValueChanged += new System.EventHandler(this.PortTextBoxValueChanged);
            // 
            // allowScreenshots
            // 
            this.allowScreenshots.Checked = true;
            this.allowScreenshots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allowScreenshots.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.allowScreenshots.Location = new System.Drawing.Point(15, 19);
            this.allowScreenshots.Name = "allowScreenshots";
            this.allowScreenshots.Size = new System.Drawing.Size(334, 24);
            this.allowScreenshots.TabIndex = 0;
            this.allowScreenshots.Text = "Enable Screenshots";
            this.allowScreenshots.UseVisualStyleBackColor = true;
            this.allowScreenshots.CheckedChanged += new System.EventHandler(this.AllowScreenshotsCheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Server Status: ";
            // 
            // serverStatusLabel
            // 
            this.serverStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.serverStatusLabel.AutoSize = true;
            this.serverStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.serverStatusLabel.Location = new System.Drawing.Point(97, 136);
            this.serverStatusLabel.Name = "serverStatusLabel";
            this.serverStatusLabel.Size = new System.Drawing.Size(50, 13);
            this.serverStatusLabel.TabIndex = 2;
            this.serverStatusLabel.Text = "Stopped!";
            // 
            // startStopButton
            // 
            this.startStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startStopButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.startStopButton.Location = new System.Drawing.Point(13, 153);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(75, 23);
            this.startStopButton.TabIndex = 3;
            this.startStopButton.Text = "Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.StartStopButtonClick);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.exitButton.Location = new System.Drawing.Point(347, 153);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // viewLogButton
            // 
            this.viewLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.viewLogButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.viewLogButton.Location = new System.Drawing.Point(94, 153);
            this.viewLogButton.Name = "viewLogButton";
            this.viewLogButton.Size = new System.Drawing.Size(75, 23);
            this.viewLogButton.TabIndex = 5;
            this.viewLogButton.Text = "View Log";
            this.viewLogButton.UseVisualStyleBackColor = true;
            this.viewLogButton.Click += new System.EventHandler(this.ViewLogButtonClick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "taskbarIcon";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 183);
            this.Controls.Add(this.viewLogButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startStopButton);
            this.Controls.Add(this.serverStatusLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skynet Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox unlockShortcutTextbox;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
