using System;
using System.Collections.Generic;
using System.Text;

namespace relativityCalculator.Core.Contracts
{
    public interface IAppSettings
    {
        string ConnectionString { get; set; }
		string SQLocal { get; set; }
		string SQLDev { get; set; }
	}
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
		public string SQLocal { get; set; }
		public string SQLDev { get; set; }
	}
}
