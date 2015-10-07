using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skynet_Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WebSocketSharp;

namespace Skynet
{
    public delegate void OnConnectionEstablished();
	public partial class MainForm : Form
	{
        WebSocket ws;
        public event OnConnectionEstablished Connected;
        Thread t;
        System.Windows.Forms.Timer timer;

        public MainForm(string address)
		{
            Font = SystemFonts.MessageBoxFont;
            CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
            ws = new WebSocket(address);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1500;
            timer.Tick += (sender, e) =>
            {
                ws.Send(JsonConvert.SerializeObject(new { type = "getframe" }));
            };

            ws.OnMessage += Ws_OnMessage;
            ws.OnOpen += (sender, e) =>
            {
                //ws.Send(JsonConvert.SerializeObject(new { type = "getinfo" }));
                string msg = "{\"type\":\"getinfo\"}";
                ws.Send(msg);
            };
            ws.Connect();
            if (Connected != null)
                Connected();
            timer.Start();
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
                if(asJson["type"].ToString() == "sysinfo")
                {
                    Text = "Skynet - View of " + asJson["name"].ToString();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SendNoticeForm f = new SendNoticeForm();
            DialogResult dr = f.ShowDialog();
            if(dr == DialogResult.OK)
            {
                string msg = JsonConvert.SerializeObject(new { type = "notice", message = f.Message, timeout = f.Timeout});
                ws.Send(msg);
            }
        }
    }
}
