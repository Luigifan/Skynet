/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 10/6/2015
 * Time: 5:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
			InitializeComponent();
			
			SkynetServer.StoppedServer += (e) =>
			{
				serverStatusLabel.ForeColor = Color.Red;
				serverStatusLabel.Text = "Server stopped, check log";
				Program.Log.Insert(DateTime.Now.ToString(), "Server stopped. Code: " + e.Code + "; Reason: " + e.Reason);
			};
			
			SkynetServer.StartedServer += () =>
			{
				serverStatusLabel.ForeColor = Color.Green;
				serverStatusLabel.Text = "Server started! Point Skynet Client to: ws://localhost:" + SkynetServer.SettingsCopy.LocalServerPort + "/Skynet";
				Program.Log.Insert(DateTime.Now.ToString(), "Server started; running on: ws://localhost:" + SkynetServer.SettingsCopy.LocalServerPort + "/Skynet");
			};
		}
		void StartStopButtonClick(object sender, EventArgs e)
		{
			if(!SkynetServer.ServerRunning)
			{
				Program.Log.Insert(DateTime.Now.ToString(), "Starting server...");
			
				SkynetServer = new SkynetServer(serverSettings);
				SkynetServer.Start();
				serverStatusLabel.ForeColor = Color.Green;
				serverStatusLabel.Text = "Server running!";
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
	}
}
