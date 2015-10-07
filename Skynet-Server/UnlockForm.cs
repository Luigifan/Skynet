using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Skynet_Server
{
    public partial class UnlockForm : Form
    {
        private MainForm _parent;

        public UnlockForm(MainForm parent)
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();

            _parent = parent;
            Shown += (sender, e) => _parent.notifyIcon1.Visible = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Color defaultForeColor = textBox1.ForeColor;
            if(textBox1.Focused)
            {
                textBox1.Text = e.KeyData.ToString();
                if(e.KeyData == _parent.serverSettings.Keydata)
                {
                    textBox1.Enabled = false;
                    textBox1.Text = "Approved!";
                    textBox1.ForeColor = Color.Green;

                    Thread.Sleep(2500);
                    this.Hide();
                    _parent.Show();
                    _parent.notifyIcon1.Visible = false;
                }
                else
                {
                    textBox1.Text = e.KeyData.ToString();
                }
            }
        }

        private void UnlockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parent.notifyIcon1.Visible = true;
        }
    }
}
