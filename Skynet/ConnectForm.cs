using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skynet
{
    public partial class ConnectForm : Form
    {
        public ConnectForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if(addressTextBox.Text.Trim() != string.Empty)
            {
                Progress p = new Progress(addressTextBox.Text);
                p.Show();
                this.Hide();
            }
        }
    }
}
