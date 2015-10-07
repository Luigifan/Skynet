/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 10/6/2015
 * Time: 5:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Skynet_Server
{
	public class SkynetServerSettings
	{
		public int LocalServerPort { get; set; }
		public bool AllowScreenshots {get;set;}
		public bool AllowKeylogging {get;set;}
        public Keys Keydata { get; set; }
		
		public SkynetServerSettings()
		{
			LocalServerPort = 4649;
			AllowScreenshots = true;
			AllowKeylogging = false;
            //Keydata = "L, Control, Shift";
            Keydata = (Keys.L | Keys.Control | Keys.Shift);
		}
	}
	
}
