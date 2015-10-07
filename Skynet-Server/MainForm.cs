using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Skynet_Server
{
    public partial class MainForm : Form
    {
        private SkynetServer SkynetServer = new SkynetServer();
        public SkynetServerSettings serverSettings = new SkynetServerSettings();

        public MainForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            unlockShortcutTextbox.Text = serverSettings.Keydata.ToString();
        }

        public void RunSilent()
        {
            this.Visible = false;
            notifyIcon1.Icon = Properties.Resources.Favicon;
            notifyIcon1.Text = "Skynet";
            notifyIcon1.Visible = true;

            ToggleServer();
        }

        void StartStopButtonClick(object sender, EventArgs e)
        {
            ToggleServer();
        }

        private void SetControlState(bool state)
        {
            allowKeylogging.Enabled = state;
            allowScreenshots.Enabled = state;
            portTextBox.Enabled = state;
            unlockShortcutTextbox.Enabled = state;
        }

        public void SendNotice(string text, int timeout)
        {
            notifyIcon1.BalloonTipTitle = "Skynet";
            notifyIcon1.BalloonTipText = text;
            notifyIcon1.ShowBalloonTip(timeout);
        }

        public void ToggleServer()
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
                    serverStatusLabel.Text = "Server started! Point Skynet Client to: ws://" + SkynetServer.LocalIPAddress().ToString() + ":" + SkynetServer.SettingsCopy.LocalServerPort + "/Skynet";
                    Program.Log.Insert(DateTime.Now.ToString(), "Server started; running on: ws://" + SkynetServer.LocalIPAddress().ToString() + ":" + SkynetServer.SettingsCopy.LocalServerPort + "/Skynet");
                };

                serverStatusLabel.ForeColor = Color.Green;
                serverStatusLabel.Text = "Server running!";
                SkynetServer.Start();
                startStopButton.Text = "Stop";

                SetControlState(false);
            }
            else
            {
                Program.Log.Insert(DateTime.Now.ToString(), "Stopping server...");

                SetControlState(true);

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
            if (exitButton.Text.ToLower().Trim() == "exit")
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
            else
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if(rk.GetValue("Skynet Server") == null)
                {
                    rk.SetValue("Skynet Server", Application.ExecutablePath.ToString() + " --silent");
                    MessageBox.Show("Added to startup!");
                }
                else
                {
                    rk.DeleteValue("Skynet Server", false);
                    MessageBox.Show("Removed from startup!");
                }

                exitButton.Text = "Exit";
            }
        }

        private void unlockShortcutTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(unlockShortcutTextbox.Focused)
            {
                unlockShortcutTextbox.Text = e.KeyData.ToString();
                serverSettings.Keydata = e.KeyData;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Icon = Skynet_Server.Properties.Resources.Favicon;
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "Skynet";

            e.Cancel = true;
            this.Visible = false;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UnlockForm f = new UnlockForm(this);
            f.ShowDialog();
        }
        
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                exitButton.Text = "Add to Startup";
            }
            else
                exitButton.Text = "Exit";
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                exitButton.Text = "Add to Startup";
            }
            else
                exitButton.Text = "Exit";
        }
    }
}
