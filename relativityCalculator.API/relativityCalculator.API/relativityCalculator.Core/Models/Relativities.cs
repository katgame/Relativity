using System;
using System.Collections.Generic;
using System.Text;
using relativityCalculator.Core.Enums;

namespace relativityCalculator.Core.Models
{
	public class Relativities
	{
		public string RelativityName { get; set; }
		public string RequestValue { get; set; }
		public double? RelativityValue { get; set; }
		public int Id { get; set; }
		public RelativityType type { get; set; }
	}
}
