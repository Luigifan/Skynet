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
    public partial class Progress : Form
    {
        public Progress(string address)
        {
            InitializeComponent();

            MainForm m = new MainForm(address);
            m.Show();
            this.Hide();
        }
    }
}
