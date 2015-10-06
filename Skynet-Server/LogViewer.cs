/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 10/6/2015
 * Time: 6:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Skynet_Server
{
	/// <summary>
	/// Description of LogViewer.
	/// </summary>
	public partial class LogViewer : Form
	{
		public LogViewer()
		{
			InitializeComponent();
			Program.Log.LogUpdated += UpdateLogInUI;
			listView1.Clear();
			listView1.Columns.Add("Date", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("Message", 400, HorizontalAlignment.Left);
			
			UpdateLogInUI();
		}
		
		public void UpdateLogInUI()
		{
			List<KeyValuePair<string, string>> log = Program.Log.Copy();
			
			listView1.Clear();
			listView1.Columns.Add("Date", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("Message", 400, HorizontalAlignment.Left);
			foreach(KeyValuePair<string, string> item in log)
			{
				ListViewItem lvi = new ListViewItem(item.Key);
				lvi.SubItems.Add(item.Value);
				listView1.Items.Add(lvi);
			}
		}
	}
}
