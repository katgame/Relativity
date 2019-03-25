using System;
using System.Collections.Generic;
using System.Text;

namespace relativityCalculator.Core.Models
{
	public class Costs
	{
		public double CostOfSalvage { get; set; }
		public double CompanyPercentage { get; set; }
		public double additionalCost { get; set; }
		public double TotalRepairCost { get; set; }
		public double BasePrice { get; set; }
		public double ExpectedSalvageRecovery { get; set; }
		public double TotalSalvageRecovery { get; set; }
		public double Saving { get; set; }
		public double DifferenceinCost { get; set; }
	}
}
