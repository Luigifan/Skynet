/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 10/6/2015
 * Time: 3:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace Skynet_Server
{
	class Program
	{
		public static ServerLog Log {get; internal set;}
		
		public static void Main(string[] args)
		{
			Log = new ServerLog();
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			//SkynetServer s = new SkynetServer();
			//s.Start();
			//System.Windows.Forms.Application.Run();
		}
	}
}