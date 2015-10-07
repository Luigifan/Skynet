using System;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharp.Net;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace Skynet_Server
{
	public class Laputa : WebSocketBehavior
	{
        SystemInformationLibrary.SystemSpecs.SystemSpecifications s = new SystemInformationLibrary.SystemSpecs.SystemSpecifications();
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
            else if(e.Data.StartsWith("{") && e.Data.EndsWith("}"))
            {
                JObject msgAsJson = JObject.Parse(e.Data);
                if(msgAsJson["type"].ToString() == "notice")
                {
                    Program.mainForm.SendNotice(msgAsJson["message"].ToString(), msgAsJson["timeout"].ToObject<int>());
                    Program.Log.Insert(DateTime.Now.ToString(), "Received notice by JSON");
                }
                else if(msgAsJson["type"].ToString() == "getframe")
                {
                    if (SkynetServer.SettingsCopy != null && SkynetServer.SettingsCopy.AllowScreenshots)
                    {
                        string msg = JsonConvert.SerializeObject(
                        new
                        {
                            type = "image",
                            image = Convert.ToBase64String(Conversions.ImageToByte2(Screenshot.GetShotOfDesktop())),
                            timestamp = DateTime.Now
                        });
                        Program.Log.Insert(DateTime.Now.ToString(),
                                           "Responding to JSON with type getframe");
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
                                           "Denying getframe by JSON");
                        Send(msg);
                    }
                }
                else if(msgAsJson["type"].ToString() == "getinfo")
                {
                    string msg = JsonConvert.SerializeObject(new
                    {
                        type = "sysinfo",
                        os = s.OperatingSystem,
                        cpu = s.Processor,
                        gpu = s.VideoCard,
                        ram = s.TotalRAMAvailable,
                        name = Environment.MachineName,
                        localip = SkynetServer.LocalIPAddress().ToString(),
                        timestamp = DateTime.Now
                    });
                    Program.Log.Insert(DateTime.Now.ToString(),
                                           "Responding to getinfo by JSON");
                    Send(msg);
                }
            }
            else if(e.Data == "GETINFO")
            {
                string msg = JsonConvert.SerializeObject(new
                {
                    type = "sysinfo",
                    os = s.OperatingSystem,
                    cpu = s.Processor,
                    gpu = s.VideoCard,
                    ram = s.TotalRAMAvailable,
                    name = Environment.MachineName,
                    localip = SkynetServer.LocalIPAddress().ToString(),
                    timestamp = DateTime.Now
                });
                Program.Log.Insert(DateTime.Now.ToString(),
                                       "Responding to " + e.Data);
                Send(msg);
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
    public delegate void SendNoticeReceived();
	
	public class SkynetServer
	{
		public bool ServerRunning {get;internal set;}
		
		public event ServerStarted StartedServer;
		public event ServerStopped StoppedServer;
        public event SendNoticeReceived NoticeReceived;
		
		private WebSocketServer wssv;
		public static SkynetServerSettings SettingsCopy {get;internal set;}
		
		
		public SkynetServer(){}

        public static IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

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
