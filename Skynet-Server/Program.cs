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

            if (args.Length > 0)
            {
                if(args[0].ToLower().Trim() == "--silent")
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Log.Insert(DateTime.Now.ToString(), "Running server in silent mode");
                    MainForm m = new MainForm();
                    m.ToggleServer();
                    Application.Run();
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
		}
	}
}