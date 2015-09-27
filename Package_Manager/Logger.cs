using System;
using log4net;

namespace PackageManager
{
	internal class Logger
	{
		private ILog log;

		private Logger() { }

		private static Logger instance;
		internal static Logger Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Logger();
				}
				return instance;
			}
		}

		internal void Initialize(ILog logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			log = logger;
		}

		public ILog WriteLog { get { return log; } }
	}
}
