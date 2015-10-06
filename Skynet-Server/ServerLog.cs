using System;
using System.Collections.Generic;

namespace Skynet_Server
{
	public delegate void LogAdded();
	
	public class ServerLog
	{
		public event LogAdded LogUpdated;
		private List<KeyValuePair<string, string>> _log = new List<KeyValuePair<string, string>>();
		public ServerLog()
		{}
		
		public void Insert(string date, string log)
		{
			_log.Add(new KeyValuePair<string, string>(date, log));
			if(LogUpdated != null)
				LogUpdated();
		}
		
		public List<KeyValuePair<string, string>> GetLog()
		{
			return _log;
		}
		
		public List<KeyValuePair<string, string>> Copy()
		{
			List<KeyValuePair<string, string>> logCopy = new List<KeyValuePair<string, string>>(_log);
			return logCopy;
		}
	}
}
