/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 10/6/2015
 * Time: 3:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Skynet
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
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
            this.viewportPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.viewportPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // viewportPictureBox
            // 
            this.viewportPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewportPictureBox.Location = new System.Drawing.Point(0, 0);
            this.viewportPictureBox.Name = "viewportPictureBox";
            this.viewportPictureBox.Size = new System.Drawing.Size(772, 472);
            this.viewportPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.viewportPictureBox.TabIndex = 0;
            this.viewportPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 472);
            this.Controls.Add(this.viewportPictureBox);
            this.Name = "MainForm";
            this.Text = "Skynet - View of ";
            ((System.ComponentModel.ISupportInitialize)(this.viewportPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.PictureBox viewportPictureBox;
    }
}
