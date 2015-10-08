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


        #region ugly

        #region Definitions
        private MARGINS margins = new MARGINS();
        private Rectangle topRect = new Rectangle();
        private Rectangle leftRect = new Rectangle();
        private Rectangle rightRect = new Rectangle();
        private Rectangle botRect = new Rectangle();

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();
        #endregion

        #region Overrides
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == VistaApi.WM_NCHITTEST // if this is a click
              && m.Result.ToInt32() == VistaApi.HTCLIENT // ...and it is on the client
              && this.IsOnGlass(m.LParam.ToInt32())) // ...and specifically in the glass area
            {
                m.Result = new IntPtr(VistaApi.HTCAPTION); // lie and say they clicked on the title bar
            }
            //if we were still interested in having glass portions move, sure this would be great
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            //base.WndProc(ref m);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DwmIsCompositionEnabled())
            {
                //Rectangle together = new Rectangle(imageGroupBox.Location.X, imageGroupBox.Location.Y,
                //    imageGroupBox.Width + maskGroupBox.Width, imageGroupBox.Height + resultGroupBox.Height + saveResultButton.Height);
                margins = new MARGINS();
                margins.Top = 38; //(this.Height) / 2 / 2 / 2;
                margins.Left = 0;//this.ClientRectangle.Left; //(this.Width) / 2 / 2 / 2;
                margins.Right = 0;//this.ClientRectangle.Right;
                margins.Bottom = 0;//this.ClientRectangle.Bottom;
                //Define rectnangles, for detection of whether or not the selected area is glass
                topRect = new Rectangle(0, 0, ClientSize.Width, margins.Top);
                //leftRect = new Rectangle(0, 0, (ClientSize.Width) / 2, margins.Top);
                //rightRect = new Rectangle(0, 0, (ClientSize.Width) / 2, margins.Right);
                //botRect = new Rectangle(0, 0, ClientSize.Width, margins.Top);
                //
                DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (DwmIsCompositionEnabled())
            {
                this.Update();
                e.Graphics.Clear(Color.Black);
                Rectangle clientArea = new Rectangle(
                    margins.Left,
                    margins.Top,
                    this.ClientRectangle.Width - margins.Left - margins.Right,
                    this.ClientRectangle.Height - margins.Top - margins.Bottom
                    );
                Brush b = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(b, clientArea);
                this.Update();
            }
        }
        #endregion

        #region other stuffs
        private bool IsGlassEnabled()
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                Debug.WriteLine("Not drawing glass");
                return false;
            }

            //Check if DWM is enabled
            bool isGlassSupported = false;
            VistaApi.DwmIsCompositionEnabled(ref isGlassSupported);
            return isGlassSupported;
        }

        private bool IsOnGlass(int lParam)
        {
            // sanity check
            if (!this.IsGlassEnabled())
            {
                return false;
            }

            // get screen coordinates
            int x = (lParam << 16) >> 16; // lo order word
            int y = lParam >> 16; // hi order word

            // translate screen coordinates to client area
            Point p = this.PointToClient(new Point(x, y));

            // work out if point clicked is on glass
            if (topRect.Contains(p) || leftRect.Contains(p) || rightRect.Contains(p) || botRect.Contains(p))
            {
                return true;
            }

            return false;
        }
        #endregion

        #endregion

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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            viewportPictureBox.Visible = false;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            if (DwmIsCompositionEnabled())
            {
                //Rectangle together = new Rectangle(imageGroupBox.Location.X, imageGroupBox.Location.Y,
                //    imageGroupBox.Width + maskGroupBox.Width, imageGroupBox.Height + resultGroupBox.Height + saveResultButton.Height);
                margins = new MARGINS();
                margins.Top = 60; //(this.Height) / 2 / 2 / 2;
                margins.Left = 0;//this.ClientRectangle.Left; //(this.Width) / 2 / 2 / 2;
                margins.Right = 0;//this.ClientRectangle.Right;
                margins.Bottom = 0;//this.ClientRectangle.Bottom;
                //Define rectnangles, for detection of whether or not the selected area is glass
                topRect = new Rectangle(0, 0, ClientSize.Width, margins.Top);
                //leftRect = new Rectangle(0, 0, (ClientSize.Width) / 2, margins.Top);
                //rightRect = new Rectangle(0, 0, (ClientSize.Width) / 2, margins.Right);
                //botRect = new Rectangle(0, 0, ClientSize.Width, margins.Top);
                //
                DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }

        }
    }
}
