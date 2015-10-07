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
    public partial class SendNoticeForm : Form
    {
        public string Message { get; set; }
        public int Timeout { get; set; }

        public SendNoticeForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Message = textBox1.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Timeout = (int)numericUpDown1.Value;
        }
    }
}
