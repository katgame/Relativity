using System;
using System.Collections.Generic;
using System.Text;

namespace relativityCalculator.Core.Contracts
{
    public interface IAppSettings
    {
        string ConnectionString { get; set; }
        string MandateServiceURL { get; set; }
        string FirstInceptionDate { get; set; }
    }
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
        public string FirstInceptionDate { get; set; }
        public string MandateServiceURL { get; set; }
    }
}
