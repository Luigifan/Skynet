/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 10/6/2015
 * Time: 4:47 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;

namespace Skynet_Server
{
	/// <summary>
	/// Description of Conversions.
	/// </summary>
	public class Conversions
	{
		private Conversions()
		{
		}
		public static byte[] ImageToByte2(Image img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }
		
		public static Image FromBase64(string base64)
		{
			byte[] bytes = Convert.FromBase64String(base64);
			Image i;
			using(MemoryStream s = new MemoryStream(bytes))
			{
				i = Image.FromStream(s);
			}
			
			return i;
		}
	}
}
