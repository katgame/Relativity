using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace relativityCalculator.Core.DTOs
{
	[DataContract]
	public class CalculationResult : OUTBaseDTO
	{
		[DataMember]
		public string recommendation { get; set; }
	}
}
