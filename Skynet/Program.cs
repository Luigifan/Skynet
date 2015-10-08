﻿/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 10/6/2015
 * Time: 3:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using Skynet_Server;

namespace Skynet
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		/// 
		
		static WebSocket ws = new WebSocket("ws://localhost:4649/Skynet");
		
        static int counter = 0;
        [STAThread]
        private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm("kek"));
		}
		
	}
}
