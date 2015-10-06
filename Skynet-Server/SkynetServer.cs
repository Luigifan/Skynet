using System;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharp.Net;

namespace Skynet_Server
{
	public class Laputa : WebSocketBehavior
	{
		public Laputa()
		{
			
		}
		
		protected override void OnMessage (MessageEventArgs e)
		{
			if(e.Data == "GETSHOT")
			{
				if(SkynetServer.SettingsCopy != null && SkynetServer.SettingsCopy.AllowScreenshots)
				{
					string msg = JsonConvert.SerializeObject(
					new 
					{
						type = "image",
						image = Convert.ToBase64String(Conversions.ImageToByte2(Screenshot.GetShotOfDesktop())),
						timestamp = DateTime.Now
					});
					Program.Log.Insert(DateTime.Now.ToString(),
					                   "Responding to " + e.Data);
					Send(msg);
				}
				else
				{
					string msg = JsonConvert.SerializeObject(new
					                                         {
					                                         	type = "error",
					                                         	reason = "AllowScreenshots not enabled.",
					                                         	timestamp = DateTime.Now
					                                         });
					Program.Log.Insert(DateTime.Now.ToString(),
					                   "Denying " + e.Data);
					Send(msg);
				}
			}
		}
	}
	
	public class ServerStoppedEventArgs
	{
		public CloseStatusCode Code {get;internal set;}
		public string Reason {get;internal set;}
	}
	
	
	public delegate void ServerStarted();
	public delegate void ServerStopped(ServerStoppedEventArgs e);
	public delegate void LaputaMsgReceived();
	
	public class SkynetServer
	{
		public bool ServerRunning {get;internal set;}
		
		public event ServerStarted StartedServer;
		public event ServerStopped StoppedServer;
		
		
		private WebSocketServer wssv;
		public static SkynetServerSettings SettingsCopy {get;internal set;}
		
		
		public SkynetServer(){}
		public SkynetServer(SkynetServerSettings s)
		{
			SettingsCopy = s;
			ServerRunning = false;
		}
		
		public void Start()
		{
			wssv = new WebSocketServer(SettingsCopy.LocalServerPort);
			wssv.AddWebSocketService<Laputa>("/Skynet");
			
			wssv.Start();
			if(StartedServer != null)
				StartedServer();
			ServerRunning = true;
		}
		
		public void Stop(CloseStatusCode code, string reason)
		{
			ServerRunning = false;
			wssv.Stop(code, reason);
			if(StoppedServer != null)
				StoppedServer(new ServerStoppedEventArgs{Code = code, Reason = reason});
		}
		
	}
}
