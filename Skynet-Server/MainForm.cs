using System;
using System.Drawing;
using System.Windows.Forms;

namespace Skynet_Server
{
    public partial class MainForm : Form
    {
        private SkynetServer SkynetServer = new SkynetServer();
        private SkynetServerSettings serverSettings = new SkynetServerSettings();
        public MainForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }
        void StartStopButtonClick(object sender, EventArgs e)
        {
            if (!SkynetServer.ServerRunning)
            {
                Program.Log.Insert(DateTime.Now.ToString(), "Starting server...");

                SkynetServer = new SkynetServer(serverSettings);

                SkynetServer.StoppedServer += (ee) =>
                {
                    serverStatusLabel.ForeColor = Color.Red;
                    serverStatusLabel.Text = "Server stopped, check log";
                    Program.Log.Insert(DateTime.Now.ToString(), "Server stopped. Code: " + ee.Code + "; Reason: " + ee.Reason);
                };

                SkynetServer.StartedServer += () =>
                {
                    serverStatusLabel.ForeColor = Color.Green;
                    serverStatusLabel.Text = "Server started! Point Skynet Client to: ws://localhost:" + SkynetServer.SettingsCopy.LocalServerPort + "/Skynet";
                    Program.Log.Insert(DateTime.Now.ToString(), "Server started; running on: ws://localhost:" + SkynetServer.SettingsCopy.LocalServerPort + "/Skynet");
                };

                serverStatusLabel.ForeColor = Color.Green;
                serverStatusLabel.Text = "Server running!";
                SkynetServer.Start();
                startStopButton.Text = "Stop";
            }
            else
            {
                Program.Log.Insert(DateTime.Now.ToString(), "Stopping server...");

                SkynetServer.Stop(WebSocketSharp.CloseStatusCode.Normal, "UI Asked to close");
                serverStatusLabel.ForeColor = Color.Red;
                serverStatusLabel.Text = "User stopped server!";
                startStopButton.Text = "Start";
            }
        }
        void AllowScreenshotsCheckedChanged(object sender, EventArgs e)
        {
            serverSettings.AllowScreenshots = allowScreenshots.Checked;
        }
        void PortTextBoxValueChanged(object sender, EventArgs e)
        {
            serverSettings.LocalServerPort = (int)portTextBox.Value;
        }
        void ViewLogButtonClick(object sender, EventArgs e)
        {
            LogViewer v = new LogViewer();
            v.Show();
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            if (SkynetServer.ServerRunning)
            {
                DialogResult dr = MessageBox.Show("Server still running, are you sure you want to quit?", "Skynet Server Still Running", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    SkynetServer.Stop(WebSocketSharp.CloseStatusCode.Normal, "Program shutting down.");
                    Environment.Exit(0);
                }
            }
            else
                Environment.Exit(0);
        }
    }
}
