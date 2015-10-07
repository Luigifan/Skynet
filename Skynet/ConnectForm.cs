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
        private Properties.Settings settings = new Properties.Settings();

        public ConnectForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            addressTextBox.Text = settings.LastConnected;
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if(addressTextBox.Text.Trim() != string.Empty)
            {
                Progress p = new Progress(addressTextBox.Text);
                settings.LastConnected = addressTextBox.Text;
                settings.Save();
                this.Hide();
            }
        }
    }
}
