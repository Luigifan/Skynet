using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skynet_Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WebSocketSharp;
using System.Runtime.InteropServices;
using System.Diagnostics;
using GlassMoth;

namespace Skynet
{
    public delegate void OnConnectionEstablished();
    public partial class MainForm : Form
    {
        private WebSocket ws;
        private Thread t;
        private System.Windows.Forms.Timer timer;
        private string _Address;

        public event OnConnectionEstablished Connected;


        public MainForm(string address)
        {

            Font = SystemFonts.MessageBoxFont;
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            _Address = address;
        }

        public int ConnectToWebSocket()
        {
            ws = new WebSocket(_Address);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1500;
            timer.Tick += (sender, e) =>
            {
                if (ws != null)
                    ws.Send(JsonConvert.SerializeObject(new { type = "getframe" }));
            };

            ws.OnMessage += Ws_OnMessage;
            ws.OnOpen += (sender, e) =>
            {
                //ws.Send(JsonConvert.SerializeObject(new { type = "getinfo" }));
                string msg = "{\"type\":\"getinfo\"}";
                ws.Send(msg);
            };
            ws.OnError += (sender, e) =>
                {
                    MessageBox.Show("Error during Websocket Connection: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ws.Close();
                    ws = null;
                    this.Dispose();
                };

            ws.Connect();
            if (Connected != null)
                Connected();
            timer.Start();

            return 0;
        }

        private void UpdateImage(string base64Image)
        {
            viewportPictureBox.Image = Conversions.FromBase64(base64Image);
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Type == Opcode.Text)
            {
                JObject asJson = JObject.Parse(e.Data);
                if (asJson["type"].ToString() == "image")
                {
                    t = new Thread(() => UpdateImage(asJson["image"].ToString()));
                    t.Start();
                }
                if (asJson["type"].ToString() == "sysinfo")
                {
                    Text = "Skynet - View of " + asJson["name"].ToString();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SendNoticeForm f = new SendNoticeForm();
            DialogResult dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string msg = JsonConvert.SerializeObject(new { type = "notice", message = f.Message, timeout = f.Timeout });
                ws.Send(msg);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ws == null)
                Environment.Exit(0);
            if (ws.IsAlive)
            {
                DialogResult dr = MessageBox.Show("A connection is still active, are you sure you want to quit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    ws.Close();
                    ws = null;
                    Environment.Exit(0);
                }
            }
            else
                Environment.Exit(0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            viewportPictureBox.Visible = false;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            
        }

        FormWindowState LastWindowState = FormWindowState.Normal;
        private void MainForm_Resize(object sender, EventArgs e)
        {
            
        }
    }
}
